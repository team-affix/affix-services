#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "asio.hpp"
#include "affix-base/threading.h"
#include "server_configuration.h"

namespace affix_services_application
{
	class server
	{
	protected:
		/// <summary>
		/// The configuration for this server.
		/// </summary>
		affix_base::data::ptr<server_configuration> m_server_configuration;

		/// <summary>
		/// The thread in which the IO context for this server is performing asynchronous tasks.
		/// </summary>
		affix_base::threading::persistent_thread m_context_thread;

	public:
		/// <summary>
		/// Mutex preventing concurrent reads/writes to the m_accepted_sockets object.
		/// </summary>
		affix_base::threading::cross_thread_mutex m_accepted_sockets_mutex;

		/// <summary>
		/// Vector of accepted connections, which is populated by the async_accept_next method,
		/// and is cleared by an instance of the processer type.
		/// </summary>
		std::vector<affix_base::data::ptr<asio::ip::tcp::socket>> m_accepted_sockets;

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
			const affix_base::data::ptr<server_configuration>& a_configuration
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
