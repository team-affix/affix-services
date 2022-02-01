#include "pending_outbound_connection.h"
#include "connection_result.h"

using namespace affix_services;
using namespace affix_base::threading;
using affix_base::data::ptr;
using std::lock_guard;
using namespace asio::ip;

pending_outbound_connection::pending_outbound_connection(
	const affix_base::data::ptr<outbound_connection_configuration>& a_outbound_connection_configuration,
	affix_base::threading::cross_thread_mutex& a_connection_results_mutex,
	std::vector<affix_base::data::ptr<connection_result>>& a_connection_results
) :
	m_outbound_connection_configuration(a_outbound_connection_configuration)
{
	m_outbound_connection_configuration->m_socket->async_connect(m_outbound_connection_configuration->m_remote_endpoint,
		[&](asio::error_code a_ec)
		{
			// Lock the mutex preventing concurrent reads/writes to this object's state
			lock_guard<cross_thread_mutex> l_state_lock_guard(m_state_mutex);

			// Lock the mutex preventing concurrent reads/writes to the unauthenticated connections vector.
			lock_guard<cross_thread_mutex> l_lock_guard(a_connection_results_mutex);

			// Push new unauthenticated connection onto vector
			a_connection_results.push_back(
				new connection_result(
					m_outbound_connection_configuration->m_socket,
					false,
					!a_ec
				)
			);

			m_finished = true;

		});
}
