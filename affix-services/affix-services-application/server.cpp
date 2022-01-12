#include "server.h"

using namespace affix_services_application;
using namespace asio::ip;
using std::lock_guard;
using affix_base::threading::cross_thread_mutex;
using affix_base::data::ptr;

server::~server(

)
{
	// m_context_thread will automatically be disposed of, and
	// the deconstructor for perisistent_thread will prevent a thread leak.
}

server::server(
	const affix_base::data::ptr<server_configuration>& a_configuration,
	affix_base::threading::cross_thread_mutex& a_unauthenticated_connections_mutex,
	std::vector<affix_base::data::ptr<unauthenticated_connection>>& a_unauthenticated_connections
) :
	m_server_configuration(a_configuration),
	m_unauthenticated_connections_mutex(a_unauthenticated_connections_mutex),
	m_unauthenticated_connections(a_unauthenticated_connections)
{
	async_accept_next();
}

void server::async_accept_next(

)
{
	m_server_configuration->m_acceptor.async_accept(
		[&](asio::error_code a_ec, tcp::socket a_socket)
		{
			// Store the new socket in the list of connections
			lock_guard<cross_thread_mutex> l_lock_guard(m_unauthenticated_connections_mutex);
			m_unauthenticated_connections.push_back(new unauthenticated_connection(new tcp::socket(std::move(a_socket)), true));

			// If there was an error, return and do not make another async 
			// accept request. Otherwise, try to accept another connection.
			if (!a_ec)
				async_accept_next();

		});
}

const ptr<server_configuration>& server::configuration(

) const
{
	return m_server_configuration;
}
