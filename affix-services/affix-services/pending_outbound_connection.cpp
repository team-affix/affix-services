#include "pending_outbound_connection.h"
#include "connection_result.h"

using namespace affix_services;
using namespace affix_base::threading;
using affix_base::data::ptr;
using std::lock_guard;
using namespace asio::ip;

pending_outbound_connection::pending_outbound_connection(
	affix_base::data::ptr<outbound_connection_configuration> a_outbound_connection_configuration,
	affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_result>>, affix_base::threading::cross_thread_mutex>& a_connection_results
) :
	m_outbound_connection_configuration(a_outbound_connection_configuration)
{
	a_outbound_connection_configuration->m_socket->async_connect(a_outbound_connection_configuration->m_remote_endpoint,
		[&,a_outbound_connection_configuration](asio::error_code a_ec)
		{
			// Lock the mutex preventing concurrent reads/writes to this object's state
			locked_resource l_finished = m_finished.lock();

			// Lock the mutex preventing concurrent reads/writes to the unauthenticated connections vector.
			locked_resource l_connection_results = a_connection_results.lock();

			// Cancel async operations on socket
			(*a_outbound_connection_configuration->m_socket).cancel();

			// Push new unauthenticated connection onto vector
			l_connection_results->push_back(
				new connection_result(
					m_outbound_connection_configuration->m_socket,
					m_outbound_connection_configuration->m_remote_endpoint,
					false,
					!a_ec
				)
			);

			(*l_finished) = true;

		});
}
