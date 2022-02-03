#pragma once
#include "asio.hpp"
#include "affix-base/ptr.h"

namespace affix_services
{
	struct connection_information
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

		/// <summary>
		/// Boolean describing whether the connection was established in an inbound fashion.
		/// </summary>
		bool m_inbound = false;

		/// <summary>
		/// Boolean describing whether the connection is still valid.
		/// </summary>
		affix_base::threading::guarded_resource<bool, affix_base::threading::cross_thread_mutex> m_connected = false;

	public:
		/// <summary>
		/// Constructs the structure with all necessary connection info.
		/// </summary>
		/// <param name="a_socket"></param>
		/// <param name="a_remote_endpoint"></param>
		/// <param name="a_local_endpoint"></param>
		/// <param name="a_inbound"></param>
		/// <param name="a_connected"></param>
		connection_information(
			const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
			const asio::ip::tcp::endpoint& a_remote_endpoint,
			const asio::ip::tcp::endpoint& a_local_endpoint,
			const bool& a_inbound,
			const bool& a_connected
		);

	};
}
