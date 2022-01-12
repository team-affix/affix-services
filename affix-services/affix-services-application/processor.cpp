#include "processor.h"
#include "cryptopp/osrng.h"

using namespace affix_services_application;
using namespace asio::ip;
using std::vector;
using affix_base::data::ptr;
using affix_services::networking::connection;
using std::lock_guard;
using std::mutex;
using affix_base::threading::cross_thread_mutex;
using affix_base::cryptography::rsa_key_pair;

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
			l_inbound_connection
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
	if ((*a_authentication_attempt)->expired())
	{
		// Just erase the authentication attempt.
		m_authentication_attempts.erase(a_authentication_attempt);
	}
	else if (*(*a_authentication_attempt)->m_authenticated)
	{
		// Create local and remote tokens
		affix_services::security::rolling_token l_local_token((*a_authentication_attempt)->m_async_authenticate->m_authenticate_local->m_local_seed);
		affix_services::security::rolling_token l_remote_token((*a_authentication_attempt)->m_async_authenticate->m_authenticate_remote->m_remote_seed);

		// Create authenticated connection object
		ptr<connection> l_authenticated_connection(
			new connection(
				*(*a_authentication_attempt)->m_socket,
				m_local_key_pair.private_key,
				l_local_token,
				(*a_authentication_attempt)->m_async_authenticate->m_authenticate_remote->m_remote_public_key,
				l_remote_token,
				m_connection_async_receive_results_mutex,
				m_connection_async_receive_results
			)
		);

		// Push authenticated connection object onto vector
		m_authenticated_connections.push_back(l_authenticated_connection);

		// Erase authentication attempt object
		m_authentication_attempts.erase(a_authentication_attempt);

		// Begin receiving data from socket
		l_authenticated_connection->async_receive();

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
	if (!(*a_authenticated_connection)->m_socket.is_open())
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
		// If the connection is still active, process the inbound message
		process_message_data(
			(*a_async_receive_result)->m_message_header_data,
			(*a_async_receive_result)->m_message_body_data,
			(*a_async_receive_result)->m_owner
		);

	}

	m_connection_async_receive_results.erase(a_async_receive_result);

}

void processor::process_message_data(
	const std::vector<uint8_t>& a_message_header_data,
	const std::vector<uint8_t>& a_message_body_data,
	const affix_base::data::ptr<affix_services::networking::connection>& a_connection
)
{
	
}
