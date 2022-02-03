#pragma once
#include "asio.hpp"
#include "affix-base/ptr.h"

namespace affix_services
{
	struct outbound_connection_configuration
	{
	public:
		/// <summary>
		/// Actual network interface provided by ASIO.
		/// </summary>
		affix_base::data::ptr<asio::ip::tcp::socket> m_socket;

		/// <summary>
		/// Endpoint the socket will attempt to connect to.
		/// </summary>
		asio::ip::tcp::endpoint m_remote_endpoint;

		/// <summary>
		/// Endpoint the socket is bound to.
		/// </summary>
		asio::ip::tcp::endpoint m_local_endpoint;

	public:
		/// <summary>
		/// Constructs the outbound connection configuration, with the socket being assigned the
		/// specified port. If no port is specified, the socket will be assigned a random available port.
		/// </summary>
		/// <param name="a_remote_endpoint"></param>
		/// <param name="a_local_port"></param>
		outbound_connection_configuration(
			asio::io_context& a_io_context,
			const asio::ip::tcp::endpoint& a_remote_endpoint,
			const uint16_t& a_local_port = 0
		);

	};
}
