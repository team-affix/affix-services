#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "asio.hpp"

namespace affix_services
{
	struct unauthenticated_connection
	{
	public:
		/// <summary>
		/// Socket which holds the actual connection interface.
		/// </summary>
		affix_base::data::ptr<asio::ip::tcp::socket> m_socket;

		/// <summary>
		/// Boolean describing whether or not the connection was established in an inbound fashion (by accepting).
		/// </summary>
		bool m_inbound_connection;

	public:
		unauthenticated_connection(
			const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
			const bool& a_inbound_connection
		);

	};
}
