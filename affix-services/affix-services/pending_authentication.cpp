#include "pending_authentication.h"
#include "affix-base/timing.h"
#include "client.h"

using namespace affix_services;
using affix_base::networking::async_authenticate;
using namespace asio::ip;
using std::lock_guard;
using affix_base::threading::cross_thread_mutex;
using affix_base::data::ptr;
using namespace affix_base::threading;

pending_authentication::~pending_authentication(

)
{

}

pending_authentication::pending_authentication(
	affix_base::data::ptr<connection_information> a_connection_information,
	const std::vector<uint8_t>& a_remote_seed,
	const affix_base::cryptography::rsa_key_pair& a_local_key_pair,
	client& a_client,
	const bool& a_enable_timeout,
	const uint64_t& a_timeout_in_seconds
) :
	m_connection_information(a_connection_information),
	m_start_time(affix_base::timing::utc_time()),
	m_socket_io_guard(*a_connection_information->m_socket),
	m_enable_timeout(a_enable_timeout),
	m_timeout_in_seconds(a_timeout_in_seconds)
{
	// Instantiate async_authenticate instance, which will begin the authentication procedure.
	m_async_authenticate = new async_authenticate(
		m_socket_io_guard,
		a_remote_seed,
		a_local_key_pair,
		m_connection_information->m_inbound,
		[&,a_connection_information](bool a_result)
		{
			// Lock mutex preventing concurrent reads/writes to a vector of authentication attempt results.
			std::scoped_lock l_client_guarded_data_lock(a_client.m_guarded_data, m_finished);

			// Cancel async operations on socket
			(*a_connection_information->m_socket).cancel();

			if (a_result && !expired())
			{
				// This scope represents if the authentication of the local and remote were successful.

				// Get remote public key.
				CryptoPP::RSA::PublicKey l_remote_public_key = m_async_authenticate->m_remote_public_key;

				// Get remote seed.
				std::vector<uint8_t> l_remote_seed = m_async_authenticate->m_remote_seed;

				// Get local seed.
				std::vector<uint8_t> l_local_seed = m_async_authenticate->m_local_seed;
				
				// Generate the security information structure
				ptr<security_information> l_security_information = new security_information(
					a_local_key_pair,
					affix_services::security::rolling_token(l_local_seed),
					l_remote_public_key,
					affix_services::security::rolling_token(l_remote_seed)
				);

				// Create success result.
				ptr<authentication_result> l_authentication_attempt_result(
					new authentication_result(
						a_connection_information,
						l_security_information,
						true
					)
				);

				// Push success result into vector.
				a_client.m_guarded_data->m_authentication_attempt_results.push_back(l_authentication_attempt_result);

			}
			else
			{
				// This scope represents if authentication failed.

				// Create failure result.
				ptr<authentication_result> l_authentication_attempt_result(
					new authentication_result(
						a_connection_information,
						nullptr,
						false
					)
				);

				// Push failure result into vector.
				a_client.m_guarded_data->m_authentication_attempt_results.push_back(l_authentication_attempt_result);

			}

			// Store the finished state of the authentication process.
			(*m_finished) = true;

		});

}

bool pending_authentication::expired(

) const
{
	return m_enable_timeout && lifetime() >= m_timeout_in_seconds;
}

uint64_t pending_authentication::lifetime(

) const
{
	return affix_base::timing::utc_time() - m_start_time;
}
