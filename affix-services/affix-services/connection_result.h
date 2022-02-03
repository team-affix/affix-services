#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "asio.hpp"

namespace affix_services
{
	struct connection_result
	{
	public:
		/// <summary>
		/// Socket which holds the actual connection interface.
		/// </summary>
		affix_base::data::ptr<asio::ip::tcp::socket> m_socket;

		/// <summary>
		/// The endpoint which the socket is connected to.
		/// </summary>
		asio::ip::tcp::endpoint m_remote_endpoint;

		/// <summary>
		/// Boolean describing whether or not the connection was established in an inbound fashion (by accepting).
		/// </summary>
		bool m_inbound_connection = false;

		/// <summary>
		/// Boolean describing the success/failure of the connection attempt.
		/// </summary>
		bool m_successful = false;

	public:
		connection_result(
			const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
			const asio::ip::tcp::endpoint& a_remote_endpoint,
			const bool& a_inbound_connection,
			const bool& a_successful
		);

	};
}
