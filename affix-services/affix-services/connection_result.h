#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "asio.hpp"
#include "connection_information.h"

namespace affix_services
{
	struct connection_result
	{
	public:
		/// <summary>
		/// This holds the socket, and relevant information for connection.
		/// </summary>
		affix_base::data::ptr<connection_information> m_connection_information;

		/// <summary>
		/// Boolean describing the success/failure of the connection attempt.
		/// </summary>
		bool m_successful = false;

	public:
		connection_result(
			const affix_base::data::ptr<connection_information>& a_connection_information,
			const bool& a_successful
		);

	};
}
