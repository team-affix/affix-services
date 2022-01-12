#include "authentication_attempt.h"
#include "affix-base/timing.h"

using namespace affix_services_application;
using affix_base::networking::async_authenticate;
using namespace asio::ip;
using std::lock_guard;
using affix_base::threading::cross_thread_mutex;

uint64_t authentication_attempt::s_expire_time(3);

authentication_attempt::authentication_attempt(
	const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
	const std::vector<uint8_t>& a_remote_seed,
	const affix_base::cryptography::rsa_key_pair& a_local_key_pair,
	const bool& a_authenticate_remote_first
) :
	m_start_time(affix_base::timing::utc_time()),
	m_socket(a_socket),
	m_socket_io_guard(*a_socket)
{
	// Local variable pointing to the member instance of the boolean 
	// so that the lambda can capture it by copy constructor.
	affix_base::data::ptr<bool> l_authenticated = m_authenticated;
	
	// Instantiate async_authenticate instance, which will begin the authentication procedure.
	m_async_authenticate = new async_authenticate(
		m_socket_io_guard,
		a_remote_seed,
		a_local_key_pair,
		a_authenticate_remote_first,
		[&, l_authenticated](bool a_result)
		{
			//Sleep(4000);
			(*l_authenticated) = a_result;
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
