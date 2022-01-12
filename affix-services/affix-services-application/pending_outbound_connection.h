#pragma once
#include "asio.hpp"
#include "unauthenticated_connection.h"
#include "outbound_connection_configuration.h"
#include "affix-base/threading.h"

namespace affix_services_application
{
	class pending_outbound_connection
	{
	protected:
		/// <summary>
		/// Configuration, which holds the socket, and relevant information for connection.
		/// </summary>
		affix_base::data::ptr<outbound_connection_configuration> m_outbound_connection_configuration;

	public:
		/// <summary>
		/// Mutex preventing concurrent reads/writes to the m_unauthenticated_connections object.
		/// </summary>
		affix_base::threading::cross_thread_mutex& m_unauthenticated_connections_mutex;

		/// <summary>
		/// Vector of unauthenticated connections, which is populated by the constructor method,
		/// and is processed by an instance of the processer type.
		/// </summary>
		std::vector<affix_base::data::ptr<unauthenticated_connection>>& m_unauthenticated_connections;

	public:
		/// <summary>
		/// Initiates the request to connect.
		/// </summary>
		/// <param name="a_outbound_connection_configuration"></param>
		pending_outbound_connection(
			const affix_base::data::ptr<outbound_connection_configuration>& a_outbound_connection_configuration,
			affix_base::threading::cross_thread_mutex& a_unauthenticated_connections_mutex,
			std::vector<affix_base::data::ptr<unauthenticated_connection>>& a_unauthenticated_connections
		);

	};
}
