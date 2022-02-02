#include "authentication_attempt.h"
#include "affix-base/timing.h"

using namespace affix_services;
using affix_base::networking::async_authenticate;
using namespace asio::ip;
using std::lock_guard;
using affix_base::threading::cross_thread_mutex;
using affix_base::data::ptr;
using namespace affix_base::threading;

uint64_t authentication_attempt::s_expire_time(3);

authentication_attempt::authentication_attempt(
	affix_base::data::ptr<asio::ip::tcp::socket> a_socket,
	const std::vector<uint8_t>& a_remote_seed,
	const affix_base::cryptography::rsa_key_pair& a_local_key_pair,
	const bool& a_inbound_connection,
	affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<authentication_attempt_result>>, affix_base::threading::cross_thread_mutex>& a_authentication_attempt_results
) :
	m_inbound_connection(a_inbound_connection),
	m_start_time(affix_base::timing::utc_time())
{
	ptr<affix_base::networking::socket_io_guard> l_socket_io_guard(new affix_base::networking::socket_io_guard(*a_socket));

	// Instantiate async_authenticate instance, which will begin the authentication procedure.
	m_async_authenticate = new async_authenticate(
		*l_socket_io_guard,
		a_remote_seed,
		a_local_key_pair,
		a_inbound_connection,
		[&,l_socket_io_guard,a_socket,a_inbound_connection](bool a_result)
		{
			// Lock mutex preventing concurrent reads/writes to a vector of authentication attempt results.
			locked_resource l_authentication_attempt_results = a_authentication_attempt_results.lock();

			// Lock mutex preventing concurrent reads/writes to the state of this authentication attempt.
			locked_resource l_finished = m_finished.lock();

			// Cancel async operations on socket
			(*a_socket).cancel();

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
						a_socket,
						true,
						a_inbound_connection,
						l_remote_public_key,
						l_remote_seed,
						l_local_seed
					)
				);

				// Push success result into vector.
				l_authentication_attempt_results->push_back(l_authentication_attempt_result);

			}
			else
			{
				// This scope represents if authentication failed.

				// Create failure result.
				ptr<authentication_attempt_result> l_authentication_attempt_result(
					new authentication_attempt_result(
						a_socket,
						false,
						a_inbound_connection
					)
				);

				// Push failure result into vector.
				l_authentication_attempt_results->push_back(l_authentication_attempt_result);

			}

			// Store the finished state of the authentication process.
			(*l_finished) = true;

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
