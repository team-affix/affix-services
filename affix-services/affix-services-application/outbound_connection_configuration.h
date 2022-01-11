#pragma once
#include "asio.hpp"

namespace affix_services_application
{
	struct outbound_connection_configuration
	{
	public:
		/// <summary>
		/// Utility by which ASIO performs socket work.
		/// This field is STATIC so there will only be one io_context for all
		/// pending outbound connections.
		/// </summary>
		static asio::io_context s_socket_context;

		/// <summary>
		/// Actual network interface provided by ASIO.
		/// </summary>
		asio::ip::tcp::socket m_socket;

		/// <summary>
		/// Endpoint the socket will attempt to connect to.
		/// </summary>
		asio::ip::tcp::endpoint m_remote_endpoint;

	public:
		/// <summary>
		/// Constructs the outbound connection configuration, with the socket being assigned the
		/// specified port. If no port is specified, the socket will be assigned a random available port.
		/// </summary>
		/// <param name="a_remote_endpoint"></param>
		/// <param name="a_local_port"></param>
		outbound_connection_configuration(
			const asio::ip::tcp::endpoint& a_remote_endpoint,
			const uint16_t& a_local_port = 0
		);

	};
}
