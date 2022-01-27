#include "connection_processor.h"
#include "cryptopp/osrng.h"
#include "affix-base/vector_extensions.h"
#include "messaging.h"
#include "transmission_result.h"
#include "pending_outbound_connection.h"

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
using affix_services::pending_outbound_connection;
using affix_services::outbound_connection_configuration;

connection_processor::connection_processor(
	message_processor& a_message_processor,
	const rsa_key_pair& a_local_key_pair
) :
	m_message_processor(a_message_processor),
	m_local_key_pair(a_local_key_pair)
{

}

void connection_processor::process(

)
{
	process_pending_outbound_connections();
	process_connection_results();
	process_authentication_attempts();
	process_authentication_attempt_results();
	process_authenticated_connections();
	process_async_receive_results();
}

void connection_processor::process_pending_outbound_connections(

)
{

}

void connection_processor::process_pending_outbound_connection(
	std::vector<affix_base::data::ptr<pending_outbound_connection>>::iterator a_pending_outbound_connection
)
{

}

void connection_processor::process_connection_results(

)
{
	// Lock the mutex, preventing changes to m_unauthenticated_connections.
	lock_guard<cross_thread_mutex> l_lock_guard(m_connection_results_mutex);

	// Decrement through vector, since processing will erase each element
	for (int i = m_connection_results.size() - 1; i >= 0; i--)
		process_connection_result(m_connection_results.begin() + i);

}

void connection_processor::process_connection_result(
	std::vector<affix_base::data::ptr<connection_result>>::iterator a_connection_result
)
{
	if ((*a_connection_result)->m_successful)
	{
		// Buffer in which the remote seed lives
		std::vector<uint8_t> l_remote_seed(affix_services::security::AS_SEED_SIZE);

		// Generate remote seed
		CryptoPP::AutoSeededRandomPool l_random;
		l_random.GenerateBlock(l_remote_seed.data(), l_remote_seed.size());

		// Pull out data from unauthenticated connection instance
		ptr<tcp::socket> l_socket = (*a_connection_result)->m_socket;
		bool l_inbound_connection = (*a_connection_result)->m_inbound_connection;
	

		// Create authentication attempt
		ptr<authentication_attempt> l_authentication_attempt(
			new authentication_attempt(
				l_socket,
				l_remote_seed,
				m_local_key_pair,
				l_inbound_connection,
				m_authentication_attempt_results_mutex,
				m_authentication_attempt_results
			)
		);

		// Push new authentication attempt to back of vector
		m_authentication_attempts.push_back(l_authentication_attempt);

		// Remove unauthenticated connection from vector
		m_connection_results.erase(a_connection_result);

	}
	else
	{

	}

}

void connection_processor::process_authentication_attempts(

)
{
	// Decrement through vector, since each process call will erase the element
	for (int i = m_authentication_attempts.size() - 1; i >= 0; i--)
		process_authentication_attempt(m_authentication_attempts.begin() + i);
}

void connection_processor::process_authentication_attempt(
	std::vector<affix_base::data::ptr<authentication_attempt>>::iterator a_authentication_attempt
)
{
	// Local variable outside of unnamed scope
	// describing the finished state of the authentication attempt
	bool l_finished = false;

	// Should stay its own scope because of std::lock_guard
	{
		// Lock the authentication attempt's state mutex while we read it
		lock_guard<cross_thread_mutex> l_lock_guard((*a_authentication_attempt)->m_state_mutex);

		// Extract the finished state of the authentication attempt
		l_finished = (*a_authentication_attempt)->m_finished;
	}

	// Utilize the extracted information
	if (l_finished)
	{
		// Just erase the authentication attempt
		m_authentication_attempts.erase(a_authentication_attempt);

	}

}

void connection_processor::process_authentication_attempt_results(

)
{
	// Lock mutex preventing concurrent reads/writes to m_authentication_attempt_results.
	lock_guard l_lock_guard(m_authentication_attempt_results_mutex);

	// Decrement through vector, since each call to process will erase elements.
	for (int i = m_authentication_attempt_results.size() - 1; i >= 0; i--)
		process_authentication_attempt_result(m_authentication_attempt_results.begin() + i);

}

void connection_processor::process_authentication_attempt_result(
	std::vector<affix_base::data::ptr<authentication_attempt_result>>::iterator a_authentication_attempt_result
)
{
	if ((*a_authentication_attempt_result)->m_successful)
	{
		// Log the success of the authentication attempt.
		LOG("============================================================");
		LOG("[ PROCESSOR ] Success: authentication attempt successful: " << std::endl);
		LOG("Remote IPv4: " << (*a_authentication_attempt_result)->m_socket->remote_endpoint().address().to_string() << ":" << (*a_authentication_attempt_result)->m_socket->remote_endpoint().port());
		LOG("Remote Identity (base64): " << std::endl << rsa_to_base64_string((*a_authentication_attempt_result)->m_remote_public_key) << std::endl);
		LOG("Remote Seed: " << to_string((*a_authentication_attempt_result)->m_remote_seed, "-"));
		LOG("Local Seed:  " << to_string((*a_authentication_attempt_result)->m_local_seed, "-"));
		LOG("============================================================");

		// Get local and remote tokens
		affix_services::security::rolling_token l_local_token((*a_authentication_attempt_result)->m_local_seed);
		affix_services::security::rolling_token l_remote_token((*a_authentication_attempt_result)->m_remote_seed);

		// Create authenticated connection object
		ptr<authenticated_connection> l_authenticated_connection(
			new authenticated_connection(
				(*a_authentication_attempt_result)->m_socket,
				m_local_key_pair.private_key,
				l_local_token,
				(*a_authentication_attempt_result)->m_remote_public_key,
				l_remote_token,
				m_connection_async_receive_results_mutex,
				m_connection_async_receive_results,
				(*a_authentication_attempt_result)->m_inbound_connection
			)
		);

		// Push authenticated connection object onto vector
		m_authenticated_connections.push_back(l_authenticated_connection);

		// Erase authentication attempt result object
		m_authentication_attempt_results.erase(a_authentication_attempt_result);
		
		// Begin receiving data from socket
		l_authenticated_connection->async_receive();

	}
	else
	{
		// Log the success of the authentication attempt.
		LOG("[ PROCESSOR ] Error: authentication attempt failed: ");
		LOG("Remote IPv4: " << (*a_authentication_attempt_result)->m_socket->remote_endpoint().address().to_string() << ":" << (*a_authentication_attempt_result)->m_socket->remote_endpoint().port());
		
		// Erase authentication attempt result object
		m_authentication_attempt_results.erase(a_authentication_attempt_result);

	}
	
}

void connection_processor::process_authenticated_connections(

)
{
	// Decrement through vector since processing might erase elements from the vector.
	for (int i = m_authenticated_connections.size() - 1; i >= 0; i--)
		process_authenticated_connection(m_authenticated_connections.begin() + i);
}

void connection_processor::process_authenticated_connection(
	std::vector<affix_base::data::ptr<authenticated_connection>>::iterator a_authenticated_connection
)
{
	if (!(*a_authenticated_connection)->m_socket->is_open())
	{
		// If socket is closed, dispose of the connection object
		m_authenticated_connections.erase(a_authenticated_connection);

	}
}

void connection_processor::process_async_receive_results(

)
{
	// Lock the mutex preventing concurrent reads/writes to the async_receive_results vector
	lock_guard<cross_thread_mutex> l_lock_guard(m_connection_async_receive_results_mutex);

	// Decrement through vector since processing might erase elements from the vector.
	for (int i = m_connection_async_receive_results.size() - 1; i >= 0; i--)
		process_async_receive_result(m_connection_async_receive_results.begin() + i);

}

void connection_processor::process_async_receive_result(
	std::vector<affix_base::data::ptr<affix_services::networking::connection_async_receive_result>>::iterator a_async_receive_result
)
{
	// Get the owner connection from the vector of authenticated connections
	std::vector<ptr<authenticated_connection>>::iterator l_connection(std::find(m_authenticated_connections.begin(), m_authenticated_connections.end(), (*a_async_receive_result)->m_owner));

	if (l_connection != m_authenticated_connections.end())
	{
		if (!(*a_async_receive_result)->m_successful)
		{
			// Log that there was an error receiving data.
			LOG_ERROR("[ PROCESSOR ] Error: Failed to receive data from connection: ");
			LOG_ERROR("Remote Identity (base64): " << std::endl << rsa_to_base64_string((*a_async_receive_result)->m_owner->m_transmission_security_manager.m_remote_public_key) << std::endl);

			// Boolean describing the direction of the established connection.
			bool l_inbound_connection = (*l_connection)->m_inbound_connection;

			if (!l_inbound_connection)
			{
				// Reconnect to the remote peer.
				ptr<outbound_connection_configuration> l_outbound_connection_configuration = new outbound_connection_configuration(
					*(*l_connection)->m_socket->get_executor().target<asio::io_context>(),
					(*l_connection)->m_socket->remote_endpoint()
				);

				ptr<pending_outbound_connection> l_pending_outbound_connection = new pending_outbound_connection(
					l_outbound_connection_configuration,
					m_connection_results_mutex,
					m_connection_results
				);

			}

			// Close connection
			m_authenticated_connections.erase(l_connection);


		}
		else
		{
			// If the connection is still active, process the inbound message
			m_message_processor.process_async_receive_result((*a_async_receive_result));

			// Prime the IO context with another async receive request
			(*l_connection)->async_receive();

		}
	}

	m_connection_async_receive_results.erase(a_async_receive_result);

}
