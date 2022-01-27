#include "authentication_attempt.h"
#include "affix-base/timing.h"

using namespace affix_services;
using affix_base::networking::async_authenticate;
using namespace asio::ip;
using std::lock_guard;
using affix_base::threading::cross_thread_mutex;
using affix_base::data::ptr;

uint64_t authentication_attempt::s_expire_time(3);

authentication_attempt::authentication_attempt(
	const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
	const std::vector<uint8_t>& a_remote_seed,
	const affix_base::cryptography::rsa_key_pair& a_local_key_pair,
	const bool& a_inbound_connection,
	affix_base::threading::cross_thread_mutex& a_authentication_attempt_results_mutex,
	std::vector<affix_base::data::ptr<authentication_attempt_result>>& a_authentication_attempt_results
) :
	m_start_time(affix_base::timing::utc_time()),
	m_socket(a_socket),
	m_socket_io_guard(*a_socket),
	m_inbound_connection(a_inbound_connection)
{
	// Instantiate async_authenticate instance, which will begin the authentication procedure.
	m_async_authenticate = new async_authenticate(
		m_socket_io_guard,
		a_remote_seed,
		a_local_key_pair,
		a_inbound_connection,
		[&](bool a_result)
		{
			// Lock mutex preventing concurrent reads/writes to the state of this authentication attempt.
			lock_guard<cross_thread_mutex> l_state_lock_guard(m_state_mutex);

			// Lock mutex preventing concurrent reads/writes to a vector of authentication attempt results.
			lock_guard<cross_thread_mutex> l_lock_guard(a_authentication_attempt_results_mutex);

			if (a_result && !expired())
			{
				// This scope represents if the authentication of the local and remote were successful.

				// Get remote public key.
				CryptoPP::RSA::PublicKey l_remote_public_key = m_async_authenticate->m_authenticate_remote->m_remote_public_key;

				// Get remote seed.
				std::vector<uint8_t> l_remote_seed = m_async_authenticate->m_authenticate_remote->m_remote_seed;

				// Get local seed.
				std::vector<uint8_t> l_local_seed = m_async_authenticate->m_authenticate_local->m_local_seed;

				// Create success result.
				ptr<authentication_attempt_result> l_authentication_attempt_result(
					new authentication_attempt_result(
						m_socket,
						true,
						m_inbound_connection,
						l_remote_public_key,
						l_remote_seed,
						l_local_seed
					)
				);

				// Push success result into vector.
				a_authentication_attempt_results.push_back(l_authentication_attempt_result);

			}
			else
			{
				// This scope represents if authentication failed.

				// Create failure result.
				ptr<authentication_attempt_result> l_authentication_attempt_result(
					new authentication_attempt_result(
						m_socket,
						false,
						m_inbound_connection
					)
				);

				// Push failure result into vector.
				a_authentication_attempt_results.push_back(l_authentication_attempt_result);

			}

			// Store the finished state of the authentication process.
			m_finished = true;

		});

}

bool authentication_attempt::expired(

) const
{
	return lifetime() >= s_expire_time;
}

uint64_t authentication_attempt::lifetime(

) const
{
	return affix_base::timing::utc_time() - m_start_time;
}
