#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "asio.hpp"
#include "affix-base/threading.h"
#include "server_configuration.h"
#include "connection_result.h"
#include "affix-base/threading.h"

namespace affix_services
{
	class server
	{
	public:
		/// <summary>
		/// The configuration for this server.
		/// </summary>
		affix_base::data::ptr<server_configuration> m_server_configuration;

		/// <summary>
		/// Vector of accepted connections, which is populated by the async_accept_next method,
		/// and is cleared by an instance of the processer type.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_result>>, affix_base::threading::cross_thread_mutex>& m_connection_results;

		/// <summary>
		/// If the server has a dedicated port, the endpoint for this acceptor should include that port.
		/// If the server doesn't have a dedicated port, the endpoint should have port set to 0.
		/// </summary>
		affix_base::data::ptr<asio::ip::tcp::acceptor> m_acceptor;

	public:
		/// <summary>
		/// m_context_thread will automatically be disposed of, and
		/// the deconstructor for m_context_thread will prevent a thread leak.
		/// </summary>
		virtual ~server(

		);
		/// <summary>
		/// Constructs a server instance with the given configuration.
		/// This method moves the server_configuration instance.
		/// </summary>
		/// <param name="a_configuration"></param>
		server(
			asio::io_context& a_io_context,
			affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_result>>, affix_base::threading::cross_thread_mutex>& a_connection_results,
			affix_base::data::ptr<server_configuration> a_configuration
		);

	protected:
		/// <summary>
		/// Accepts a single connection asynchronously, and if the callback is successful, accept_next runs again.
		/// If callback unsuccessful, no more connections are accepted. The accepted connection is pushed into 
		/// m_accepted_socket_connections.
		/// </summary>
		void async_accept_next(

		);

	};
}
