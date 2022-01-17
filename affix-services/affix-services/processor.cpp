#include "processor.h"
#include "cryptopp/osrng.h"
#include "affix-base/vector_extensions.h"
#include "messaging.h"
#include "transmission_result.h"

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
using affix_services::networking::connection;
using std::lock_guard;
using std::mutex;
using affix_base::threading::cross_thread_mutex;
using affix_base::cryptography::rsa_key_pair;
using affix_base::cryptography::rsa_to_base64_string;
using affix_base::data::to_string;
using affix_services::networking::transmission_result;
using affix_services::networking::transmission_result_strings;

processor::processor(
	const rsa_key_pair& a_local_key_pair
) :
	m_local_key_pair(a_local_key_pair)
{

}

void processor::process(

)
{
	process_unauthenticated_connections();
	process_authentication_attempts();
	process_authentication_attempt_results();
	process_authenticated_connections();
	process_async_receive_results();
}

void processor::process_unauthenticated_connections(

)
{
	// Lock the mutex, preventing changes to m_unauthenticated_connections.
	lock_guard<cross_thread_mutex> l_lock_guard(m_unauthenticated_connections_mutex);

	// Decrement through vector, since processing will erase each element
	for (int i = m_unauthenticated_connections.size() - 1; i >= 0; i--)
		process_unauthenticated_connection(m_unauthenticated_connections.begin() + i);

}

void processor::process_unauthenticated_connection(
	std::vector<affix_base::data::ptr<unauthenticated_connection>>::iterator a_unauthenticated_connection
)
{
	// Buffer in which the remote seed lives
	std::vector<uint8_t> l_remote_seed(affix_services::security::AS_SEED_SIZE);

	// Generate remote seed
	CryptoPP::AutoSeededRandomPool l_random;
	l_random.GenerateBlock(l_remote_seed.data(), l_remote_seed.size());

	// Pull out data from unauthenticated connection instance
	ptr<tcp::socket> l_socket = (*a_unauthenticated_connection)->m_socket;
	bool l_inbound_connection = (*a_unauthenticated_connection)->m_inbound_connection;
	

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
	m_unauthenticated_connections.erase(a_unauthenticated_connection);

}

void processor::process_authentication_attempts(

)
{
	// Decrement through vector, since each process call will erase the element
	for (int i = m_authentication_attempts.size() - 1; i >= 0; i--)
		process_authentication_attempt(m_authentication_attempts.begin() + i);
}

void processor::process_authentication_attempt(
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

void processor::process_authentication_attempt_results(

)
{
	// Lock mutex preventing concurrent reads/writes to m_authentication_attempt_results.
	lock_guard l_lock_guard(m_authentication_attempt_results_mutex);

	// Decrement through vector, since each call to process will erase elements.
	for (int i = m_authentication_attempt_results.size() - 1; i >= 0; i--)
		process_authentication_attempt_result(m_authentication_attempt_results.begin() + i);

}

void processor::process_authentication_attempt_result(
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
		ptr<connection> l_authenticated_connection(
			new connection(
				(*a_authentication_attempt_result)->m_socket,
				m_local_key_pair.private_key,
				l_local_token,
				(*a_authentication_attempt_result)->m_remote_public_key,
				l_remote_token,
				m_connection_async_receive_results_mutex,
				m_connection_async_receive_results
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

void processor::process_authenticated_connections(

)
{
	// Decrement through vector since processing might erase elements from the vector.
	for (int i = m_authenticated_connections.size() - 1; i >= 0; i--)
		process_authenticated_connection(m_authenticated_connections.begin() + i);
}

void processor::process_authenticated_connection(
	std::vector<affix_base::data::ptr<connection>>::iterator a_authenticated_connection
)
{
	if (!(*a_authenticated_connection)->m_socket->is_open())
	{
		// If socket is closed, dispose of the connection object
		m_authenticated_connections.erase(a_authenticated_connection);

	}
}

void processor::process_async_receive_results(

)
{
	// Lock the mutex preventing concurrent reads/writes to the async_receive_results vector
	lock_guard<cross_thread_mutex> l_lock_guard(m_connection_async_receive_results_mutex);

	// Decrement through vector since processing might erase elements from the vector.
	for (int i = m_connection_async_receive_results.size() - 1; i >= 0; i--)
		process_async_receive_result(m_connection_async_receive_results.begin() + i);

}

void processor::process_async_receive_result(
	std::vector<affix_base::data::ptr<affix_services::networking::connection_async_receive_result>>::iterator a_async_receive_result
)
{
	// Get the owner connection from the vector of authenticated connections
	std::vector<ptr<connection>>::iterator l_connection(std::find(m_authenticated_connections.begin(), m_authenticated_connections.end(), (*a_async_receive_result)->m_owner));

	if (l_connection != m_authenticated_connections.end())
	{
		if (!(*a_async_receive_result)->m_successful)
		{
			// Log that there was an error receiving data.
			LOG_ERROR("[ PROCESSOR ] Error: Failed to receive data from connection: ");
			LOG_ERROR("Remote Identity (base64): " << std::endl << rsa_to_base64_string((*a_async_receive_result)->m_owner->m_transmission_security_manager.m_remote_public_key) << std::endl);

			// Close connection
			m_authenticated_connections.erase(l_connection);

		}
		else
		{
			// If the connection is still active, process the inbound message
			process_message_data(
				(*a_async_receive_result)->m_message_header_data,
				(*a_async_receive_result)->m_message_body_data,
				(*a_async_receive_result)->m_owner
			);

			// Prime the IO context with another async receive request
			(*l_connection)->async_receive();

		}
	}

	m_connection_async_receive_results.erase(a_async_receive_result);

}

void processor::process_message_data(
	const std::vector<uint8_t>& a_message_header_data,
	const std::vector<uint8_t>& a_message_body_data,
	const affix_base::data::ptr<affix_services::networking::connection>& a_connection
)
{
	// Transmission result, which could represent different possible failure modes.
	transmission_result l_transmission_result;

	// Create the message header used to identify the type of message
	message_header l_message_header;

	// Create the byte buffer for the header
	affix_base::data::byte_buffer l_header_byte_buffer(a_message_header_data);

	// Try to deserialize the header.
	if (!l_message_header.deserialize(l_header_byte_buffer, l_transmission_result))
	{
		LOG_ERROR("[ PROCESSOR ] Error: unable to deserialize message header:");
		LOG_ERROR(transmission_result_strings[l_transmission_result]);
		return;
	}

	switch (l_message_header.m_message_type)
	{
	case message_types::rqt_identity_push:

		break;
	case message_types::rsp_identity_push:

		break;
	}


}
