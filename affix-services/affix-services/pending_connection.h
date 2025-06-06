#pragma once
#include "asio.hpp"
#include "connection_result.h"
#include "connection_information.h"
#include "affix-base/threading.h"

namespace affix_services
{
	class client;

	class pending_connection
	{
	protected:
		/// <summary>
		/// This holds the socket, and relevant information for connection.
		/// </summary>
		affix_base::data::ptr<connection_information> m_connection_information;

	public:
		/// <summary>
		/// Boolean describing whether or not the outbound connection attempt has finished.
		/// </summary>
		affix_base::threading::guarded_resource<bool> m_finished = false;

	public:
		/// <summary>
		/// Initiates the request to connect.
		/// </summary>
		/// <param name="a_outbound_connection_configuration"></param>
		pending_connection(
			affix_base::data::ptr<connection_information> a_connection_information,
			client& a_client
		);

	};
}
