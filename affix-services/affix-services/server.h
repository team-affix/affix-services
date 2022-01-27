#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "asio.hpp"
#include "affix-base/threading.h"
#include "server_configuration.h"
#include "connection_result.h"

namespace affix_services
{
	class server
	{
	protected:
		/// <summary>
		/// The configuration for this server.
		/// </summary>
		affix_base::data::ptr<server_configuration> m_server_configuration;

		/// <summary>
		/// Mutex preventing concurrent reads/writes to the m_unauthenticated_connections object.
		/// </summary>
		affix_base::threading::cross_thread_mutex& m_unauthenticated_connections_mutex;

		/// <summary>
		/// Vector of accepted connections, which is populated by the async_accept_next method,
		/// and is cleared by an instance of the processer type.
		/// </summary>
		std::vector<affix_base::data::ptr<connection_result>>& m_unauthenticated_connections;

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
			const affix_base::data::ptr<server_configuration>& a_configuration,
			affix_base::threading::cross_thread_mutex& a_unauthenticated_connections_mutex,
			std::vector<affix_base::data::ptr<connection_result>>& a_unauthenticated_connections
		);

	protected:
		/// <summary>
		/// Accepts a single connection asynchronously, and if the callback is successful, accept_next runs again.
		/// If callback unsuccessful, no more connections are accepted. The accepted connection is pushed into 
		/// m_accepted_socket_connections.
		/// </summary>
		void async_accept_next(

		);

	public:
		/// <summary>
		/// Get a const reference to the configuration of the server.
		/// </summary>
		/// <returns></returns>
		const affix_base::data::ptr<server_configuration>& configuration(
			
		) const;

	};
}
