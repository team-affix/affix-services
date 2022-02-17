#include "connection_processor.h"
#include "cryptopp/osrng.h"
#include "affix-base/vector_extensions.h"
#include "messaging.h"
#include "transmission_result.h"
#include "pending_connection.h"

#if 1
#define LOG(x) std::clog << x << std::endl
#define LOG_ERROR(x) std::cerr << x << std::endl
#else
#define LOG(x)
#define LOG_ERROR(x)
#endif

using namespace affix_services;
using namespace affix_services::messaging;
using namespace asio::ip;
using std::vector;
using affix_base::data::ptr;
using affix_services::networking::authenticated_connection;
using std::lock_guard;
using std::mutex;
using affix_base::threading::cross_thread_mutex;
using affix_base::cryptography::rsa_key_pair;
using affix_base::cryptography::rsa_to_base64_string;
using affix_base::data::to_string;
using affix_services::networking::transmission_result;
using affix_services::networking::transmission_result_strings;
using affix_services::pending_connection;
using affix_services::connection_information;
using namespace affix_base::threading;

connection_processor::connection_processor(
	asio::io_context& a_io_context,
	affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection_receive_result>>, affix_base::threading::cross_thread_mutex>& a_authenticated_connection_receive_results,
	affix_base::data::ptr<connection_processor_configuration> a_connection_processor_configuration
) :
	m_io_context(a_io_context),
	m_authenticated_connection_receive_results(a_authenticated_connection_receive_results),
	m_connection_processor_configuration(a_connection_processor_configuration)
{

}

void connection_processor::start_pending_outbound_connection(
	const uint16_t& a_bind_port,
	asio::ip::tcp::endpoint a_remote_endpoint,
	const bool& a_remote_localhost
)
{
	// Lock mutex preventing concurrent pushes/pops from pending outbound connections vector.
	affix_base::threading::locked_resource l_locked_resource = m_pending_outbound_connections.lock();

	
	// Instantiate local endpoint object.
	tcp::endpoint l_local_endpoint(tcp::v4(), a_bind_port);


	// If the remote is localhost, assign the remote endpoint's address to the local IP address
	if (a_remote_localhost)
	{
		asio::ip::address l_local_ip_address;

		// Get local IP address
		if (!affix_base::networking::socket_internal_ip_address(l_local_ip_address))
		{
			std::cerr << "Unable to get the local ip address." << std::endl;

			// If the local IP address is unable to be retrieved, set the outbound connection to retry
			restart_pending_outbound_connection(a_bind_port, a_remote_endpoint, a_remote_localhost);

			return;

		}

		a_remote_endpoint = tcp::endpoint(l_local_ip_address, a_remote_endpoint.port());

	}


	// Create new pending connection and push it to the back of the vector.
	l_locked_resource->push_back(
		new pending_connection(
			new connection_information(
				new tcp::socket(m_io_context, l_local_endpoint),
				a_remote_endpoint,
				a_remote_localhost,
				l_local_endpoint,
				false,
				false
			),
			m_connection_results
		));

}

void connection_processor::restart_pending_outbound_connection(
	const uint16_t& a_bind_port,
	asio::ip::tcp::endpoint a_remote_endpoint,
	const bool& a_remote_localhost
)
{
	locked_resource l_pending_function_calls = m_pending_function_calls.lock();

	// The inclusive minimum UTC time at which this pending function should trigger.
	uint64_t l_time_to_reconnect = affix_base::timing::utc_time() + m_connection_processor_configuration->m_reconnect_delay_in_seconds.resource();

	// Create delayed function call
	l_pending_function_calls->push_back(
		std::tuple(
			l_time_to_reconnect,
			[&, a_remote_endpoint, a_bind_port, a_remote_localhost]
			{
				// Start a normal pending connection.
				start_pending_outbound_connection(a_bind_port, a_remote_endpoint, a_remote_localhost);
			}
		));
}

bool connection_processor::identity_approved(
	const CryptoPP::RSA::PublicKey& a_identity
)
{
	try
	{
		// Extract the identity of the remote peer
		std::string l_identity = rsa_to_base64_string(a_identity);

		// Get current approved identities
		std::vector<std::string>& l_approved_identities = m_connection_processor_configuration->m_approved_identities.resource();

		return std::find(l_approved_identities.begin(), l_approved_identities.end(), l_identity) !=
			l_approved_identities.end();

	}
	catch (std::exception a_exception)
	{
		LOG_ERROR("[ CONNECTION PROCESSOR ] Error checking identity approval: " << a_exception.what());

		return false;

	}
}

void connection_processor::process(

)
{
	process_pending_outbound_connections();
	process_connection_results();
	process_authentication_attempts();
	process_authentication_attempt_results();
	process_authenticated_connections();
	process_pending_function_calls();
}

void connection_processor::process_pending_outbound_connections(

)
{
	// Lock the mutex, preventing changes to m_unauthenticated_connections.
	locked_resource l_pending_outbound_connections = m_pending_outbound_connections.lock();

	// Decrement through vector, since processing will erase each element
	for (int i = l_pending_outbound_connections->size() - 1; i >= 0; i--)
		process_pending_outbound_connection(l_pending_outbound_connections.resource(), l_pending_outbound_connections->begin() + i);

}

void connection_processor::process_pending_outbound_connection(
	std::vector<affix_base::data::ptr<pending_connection>>& a_pending_outbound_connections,
	std::vector<affix_base::data::ptr<pending_connection>>::iterator a_pending_outbound_connection
)
{
	// Store local variable describing the finished/unfinished state of the pending outbound connection.
	bool l_finished = false;

	{
		// Lock the state mutex for the pending outbound connection object
		locked_resource l_locked_resource = (*a_pending_outbound_connection)->m_finished.lock();

		// Extract state from object
		l_finished = *l_locked_resource;
	}

	if (l_finished)
	{
		// Erase pending outbound connection
		a_pending_outbound_connections.erase(a_pending_outbound_connection);
	}

}

void connection_processor::process_connection_results(

)
{
	// Lock the mutex, preventing changes to m_unauthenticated_connections.
	locked_resource l_connection_results = m_connection_results.lock();

	// Decrement through vector, since processing will erase each element
	for (int i = l_connection_results->size() - 1; i >= 0; i--)
		process_connection_result(l_connection_results.resource(), l_connection_results->begin() + i);

}

void connection_processor::process_connection_result(
	std::vector<affix_base::data::ptr<connection_result>>& a_connection_results,
	std::vector<affix_base::data::ptr<connection_result>>::iterator a_connection_result
)
{
	if ((*a_connection_result)->m_successful)
	{
		// Lock mutex for authentication attempts
		locked_resource l_authentication_attempts = m_authentication_attempts.lock();

		// Buffer in which the remote seed lives
		std::vector<uint8_t> l_remote_seed(affix_services::security::AS_SEED_SIZE);

		// Generate remote seed
		CryptoPP::AutoSeededRandomPool l_random;
		l_random.GenerateBlock(l_remote_seed.data(), l_remote_seed.size());

		// Create authentication attempt
		ptr<pending_authentication> l_authentication_attempt(
			new pending_authentication(
				(*a_connection_result)->m_connection_information,
				l_remote_seed,
				m_connection_processor_configuration->m_local_key_pair.resource(),
				m_authentication_attempt_results,
				m_connection_processor_configuration->m_enable_pending_authentication_timeout.resource(),
				m_connection_processor_configuration->m_pending_authentication_timeout_in_seconds.resource()
			)
		);

		// Push new authentication attempt to back of vector
		l_authentication_attempts->push_back(l_authentication_attempt);
		
	}
	else if (!(*a_connection_result)->m_connection_information->m_inbound)
	{
		// Reconnect to the remote peer
		restart_pending_outbound_connection(
			(*a_connection_result)->m_connection_information->m_local_endpoint.port(),
			(*a_connection_result)->m_connection_information->m_remote_endpoint,
			(*a_connection_result)->m_connection_information->m_remote_localhost
		);

	}

	// Remove unauthenticated connection from vector
	a_connection_results.erase(a_connection_result);

}

void connection_processor::process_authentication_attempts(

)
{
	// Lock mutex for authentication attempts
	locked_resource l_authentication_attempts = m_authentication_attempts.lock();

	// Decrement through vector, since each process call will erase the element
	for (int i = l_authentication_attempts->size() - 1; i >= 0; i--)
		process_authentication_attempt(l_authentication_attempts.resource(), l_authentication_attempts->begin() + i);

}

void connection_processor::process_authentication_attempt(
	std::vector<affix_base::data::ptr<pending_authentication>>& a_authentication_attempts,
	std::vector<affix_base::data::ptr<pending_authentication>>::iterator a_authentication_attempt
)
{
	// Local variable outside of unnamed scope
	// describing the finished state of the authentication attempt
	bool l_finished = false;

	// Should stay its own scope because of std::lock_guard
	{
		// Lock the authentication attempt's state mutex while we read it
		locked_resource l_locked_resource = (*a_authentication_attempt)->m_finished.lock();

		// Extract the finished state of the authentication attempt
		l_finished = l_locked_resource.resource();
		
	}
	
	// Utilize the extracted information
	if (l_finished)
	{
		// Just erase the authentication attempt
		a_authentication_attempts.erase(a_authentication_attempt);

	}

}

void connection_processor::process_authentication_attempt_results(

)
{
	// Lock mutex preventing concurrent reads/writes to m_authentication_attempt_results.
	locked_resource l_authentication_attempt_results = m_authentication_attempt_results.lock();

	// Decrement through vector, since each call to process will erase elements.
	for (int i = l_authentication_attempt_results->size() - 1; i >= 0; i--)
		process_authentication_attempt_result(l_authentication_attempt_results.resource(), l_authentication_attempt_results->begin() + i);

}

void connection_processor::process_authentication_attempt_result(
	std::vector<affix_base::data::ptr<authentication_result>>& a_authentication_attempt_results,
	std::vector<affix_base::data::ptr<authentication_result>>::iterator a_authentication_attempt_result
)
{
	// Boolean suggesting the approved state of the identity.
	bool l_identity_approved = identity_approved(
		(*a_authentication_attempt_result)->m_security_information->m_remote_public_key);

	if ((*a_authentication_attempt_result)->m_successful && l_identity_approved)
	{
		// Lock mutex for authenticated connections
		locked_resource l_authenticated_connections = m_authenticated_connections.lock();

		// Log the success of the authentication attempt
		LOG("============================================================");
		LOG("[ PROCESSOR ] Success: authentication attempt successful: " << std::endl);
		LOG("Remote IPv4: " << (*a_authentication_attempt_result)->m_connection_information->m_socket->remote_endpoint().address().to_string() << ":" << (*a_authentication_attempt_result)->m_connection_information->m_socket->remote_endpoint().port());
		LOG("Remote Identity (base64): " << std::endl << rsa_to_base64_string((*a_authentication_attempt_result)->m_security_information->m_remote_public_key) << std::endl);
		LOG("Remote Seed: " << to_string((*a_authentication_attempt_result)->m_security_information->m_remote_token.m_seed, "-"));
		LOG("Local Seed:  " << to_string((*a_authentication_attempt_result)->m_security_information->m_local_token.m_seed, "-"));
		LOG("============================================================");

		// Create authenticated connection object
		ptr<authenticated_connection> l_authenticated_connection(
			new authenticated_connection(
				(*a_authentication_attempt_result)->m_connection_information,
				(*a_authentication_attempt_result)->m_security_information,
				m_authenticated_connection_receive_results
			)
		);

		// Push authenticated connection object onto vector
		l_authenticated_connections->push_back(l_authenticated_connection);

		// Begin receiving data from socket
		l_authenticated_connection->async_receive();
		
	}
	else
	{

		// Print error message
		if (!(*a_authentication_attempt_result)->m_successful)
		{
			// Log the success of the authentication attempt
			LOG("[ PROCESSOR ] Error: authentication attempt failed: ");
			LOG("Remote IPv4: " << (*a_authentication_attempt_result)->m_connection_information->m_socket->remote_endpoint().address().to_string() << ":" << (*a_authentication_attempt_result)->m_connection_information->m_socket->remote_endpoint().port());
		}
		else
		{
			// Log the success of the authentication attempt
			LOG("[ PROCESSOR ] Error: authentication attempt succeeded but identity not approved: ");
			LOG("Remote Identity (base64): " << std::endl << rsa_to_base64_string((*a_authentication_attempt_result)->m_security_information->m_remote_public_key) << std::endl);

		}

		// Close the socket. Authentication was bad or identity not approved
		(*a_authentication_attempt_result)->m_connection_information->m_socket->close();

		if (!(*a_authentication_attempt_result)->m_connection_information->m_inbound)
		{
			// If the connection was outbound, reconnect to the remote peer
			restart_pending_outbound_connection(
				(*a_authentication_attempt_result)->m_connection_information->m_local_endpoint.port(),
				(*a_authentication_attempt_result)->m_connection_information->m_remote_endpoint,
				(*a_authentication_attempt_result)->m_connection_information->m_remote_localhost
			);
		}

	}

	// Erase authentication attempt result object
	a_authentication_attempt_results.erase(a_authentication_attempt_result);
	
}

void connection_processor::process_authenticated_connections(

)
{
	// Lock mutex for authenticated connections
	locked_resource l_authenticated_connections = m_authenticated_connections.lock();

	// Decrement through vector since processing might erase elements from the vector.
	for (int i = l_authenticated_connections->size() - 1; i >= 0; i--)
		process_authenticated_connection(l_authenticated_connections.resource(), l_authenticated_connections->begin() + i);
}

void connection_processor::process_authenticated_connection(
	std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>>& a_authenticated_connections,
	std::vector<affix_base::data::ptr<authenticated_connection>>::iterator a_authenticated_connection
)
{
	bool l_connection_timed_out = m_connection_processor_configuration->m_enable_authenticated_connection_timeout.resource() &&
		(*a_authenticated_connection)->idletime() >
		m_connection_processor_configuration->m_authenticated_connection_timeout_in_seconds.resource();

	// Boolean describing whether the authenticated connection is still active (connected)
	bool l_connected = false;

	{
		// This must stay it's own scope
		locked_resource l_connection_connected = (*a_authenticated_connection)->m_connected.lock();
		l_connected = *l_connection_connected;
	}

	// Boolean describing whether or not callbacks are currently dispatched that have not been triggered yet.
	bool l_callbacks_currently_dispatched = (*a_authenticated_connection)->m_callback_dispatcher.dispatched();

	if (l_connected && l_connection_timed_out)
	{
		// Handle disposing of connection.

		// Close connection, since it has timed out (this should cause the connection's receive callback to trigger with a failure response).
		(*a_authenticated_connection)->close();
		
	}

	if (!l_connected && !l_callbacks_currently_dispatched)
	{
		if (!(*a_authenticated_connection)->m_connection_information->m_inbound)
		{
			// Reconnect to the remote peer
			restart_pending_outbound_connection(
				(*a_authenticated_connection)->m_connection_information->m_local_endpoint.port(),
				(*a_authenticated_connection)->m_connection_information->m_remote_endpoint,
				(*a_authenticated_connection)->m_connection_information->m_remote_localhost
			);
		}

		// Since the connection is no longer be active, just erase the object.
		a_authenticated_connections.erase(a_authenticated_connection);

	}

}

void connection_processor::process_pending_function_calls(

)
{
	// Lock the vector preventing concurrent reads/writes to it
	locked_resource l_pending_function_calls = m_pending_function_calls.lock();

	// Process each individual pending function call request
	for (int i = l_pending_function_calls->size() - 1; i >= 0; i--)
		process_pending_function_call(l_pending_function_calls.resource(), l_pending_function_calls->begin() + i);

}

void connection_processor::process_pending_function_call(
	std::vector<std::tuple<uint64_t, std::function<void()>>>& a_pending_function_calls,
	std::vector<std::tuple<uint64_t, std::function<void()>>>::iterator a_pending_function_call
)
{
	// Get the time when the pending function call was created
	const uint64_t& l_call_time = std::get<0>(*a_pending_function_call);

	if (affix_base::timing::utc_time() >= l_call_time)
	{
		// Extract the actual function from the function call request.
		std::function<void()> l_function = std::get<1>(*a_pending_function_call);

		// Erase the pending function call from the vector BEFORE CALLING the function.
		// We want to be the ones to invalidate this iterator by erasing it, instead of have the possibility
		// where the function we call writes to the vector while we're using the iterator.
		a_pending_function_calls.erase(a_pending_function_call);
		
		// Call the function associated with this pending function call request
		l_function();

	}

}

affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_result>>, affix_base::threading::cross_thread_mutex>& connection_processor::connection_results(

)
{
	return m_connection_results;
}
