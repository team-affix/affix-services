#include "authentication_attempt.h"
#include "affix-base/timing.h"

using namespace affix_services_application;

authentication_attempt::authentication_attempt(
	const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
	const std::vector<uint8_t>& a_remote_seed,
	const affix_base::cryptography::rsa_key_pair& a_local_key_pair,
	const bool& a_authenticate_remote_first
) :
	m_start_time(affix_base::timing::utc_time()),
	m_socket(a_socket),
	m_socket_io_guard(*m_socket),
	m_async_authenticate(m_socket_io_guard, a_remote_seed, a_local_key_pair, a_authenticate_remote_first, [&](bool a_result) { m_authenticated = a_result; })
{

}

uint64_t authentication_attempt::lifetime(

) const
{
	return affix_base::timing::utc_time() - m_start_time;
}
