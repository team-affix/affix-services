#include "server.h"

#if 1
#define LOG_ERROR(x) std::cerr << x << std::endl;
#else
#define LOG_ERROR(x)
#endif

using namespace affix_services;
using namespace asio::ip;
using std::lock_guard;
using affix_base::threading::cross_thread_mutex;
using affix_base::data::ptr;
using namespace affix_base::threading;

server::~server(

)
{
	// m_context_thread will automatically be disposed of, and
	// the deconstructor for perisistent_thread will prevent a thread leak.
}

server::server(
	asio::io_context& a_io_context,
	affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_result>>, affix_base::threading::cross_thread_mutex>& a_connection_results,
	affix_base::data::ptr<server_configuration> a_configuration
) :
	m_connection_results(a_connection_results),
	m_server_configuration(a_configuration)
{
	if (a_configuration->m_enable.resource())
	{
		// Create acceptor object using the specified endpoint
		m_acceptor = new tcp::acceptor(a_io_context, tcp::endpoint(tcp::v4(), a_configuration->m_bind_port.resource()));

		// Save bound endpoint and write it to the config file
		a_configuration->m_bound_port.resource() = m_acceptor->local_endpoint().port();

		// Begin accepting connections
		async_accept_next();

	}

}

void server::async_accept_next(

)
{
	m_acceptor->async_accept(
		[&](asio::error_code a_ec, tcp::socket a_socket)
		{
			// Store the new socket in the list of connections
			locked_resource l_connection_results = m_connection_results.lock();

			try
			{
				// Extract remote endpoint from socket object
				asio::ip::tcp::endpoint l_remote_endpoint = a_socket.remote_endpoint();

				// Extract local endpoint from socket object
				asio::ip::tcp::endpoint l_local_endpoint = a_socket.local_endpoint();

				// Initialize connection information struct
				affix_base::data::ptr<connection_information> l_connection_information = new connection_information(
					new tcp::socket(std::move(a_socket)),
					l_remote_endpoint,
					false,
					l_local_endpoint,
					true,
					true
				);

				l_connection_results->push_back(
					new connection_result(
						l_connection_information,
						!a_ec
					)
				);

			}
			catch (std::exception a_exception)
			{
				LOG_ERROR("[ SERVER ] Error: " << a_exception.what());
				return;
			}

			// If there was an error, return and do not make another async 
			// accept request. Otherwise, try to accept another connection.
			if (!a_ec)
				async_accept_next();

		});
}
