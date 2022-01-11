#include "server.h"

using namespace affix_services_application;
using namespace asio::ip;
using std::lock_guard;
using affix_base::threading::cross_thread_mutex;

server::~server(

)
{
	// m_context_thread will automatically be disposed of, and
	// the deconstructor for perisistent_thread will prevent a thread leak.
}

server::server(
	const affix_base::data::ptr<server_configuration>& a_configuration
) :
	m_server_configuration(a_configuration),
	m_context_thread([&] { m_server_configuration->m_acceptor_context.reset(); m_server_configuration->m_acceptor_context.run(); })
{
	async_accept_next();
	m_context_thread.call();
}

void server::async_accept_next(

)
{
	m_server_configuration->m_acceptor.async_accept(
		[&](asio::error_code a_ec, tcp::socket a_socket)
		{
			// Store the new socket in the list of connections
			lock_guard<cross_thread_mutex> l_lock_guard(m_accepted_sockets_mutex);
			m_accepted_sockets.push_back(new tcp::socket(std::move(a_socket)));

			// If there was an error, return and do not make another async 
			// accept request. Otherwise, try to accept another connection.
			if (!a_ec)
				async_accept_next();

		});
}
