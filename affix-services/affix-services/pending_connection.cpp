#include "pending_connection.h"
#include "connection_result.h"
#include "client.h"

using namespace affix_services;
using namespace affix_base::threading;
using affix_base::data::ptr;
using std::lock_guard;
using namespace asio::ip;

pending_connection::pending_connection(
	affix_base::data::ptr<connection_information> a_connection_information,
	client& a_client
) :
	m_connection_information(a_connection_information)
{
	a_connection_information->m_socket->async_connect(a_connection_information->m_remote_endpoint,
		[&,a_connection_information](asio::error_code a_ec)
		{
			// Lock the mutex preventing concurrent reads/writes to the unauthenticated connections vector.
			std::scoped_lock l_client_guarded_data_lock(a_client.m_guarded_data, m_finished);

			// Cancel async operations on socket
			(*a_connection_information->m_socket).cancel();

			// Push new unauthenticated connection onto vector
			a_client.m_guarded_data->m_connection_results.push_back(
				new connection_result(
					m_connection_information,
					!a_ec
				)
			);

			(*m_finished) = true;

		});
}
