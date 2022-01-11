#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "affix-services/connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"
#include "server.h"
#include "authentication_attempt.h"

namespace affix_services_application
{
	class connection_processor
	{
	protected:
		/// <summary>
		/// A mutex guarding m_new_connections.
		/// </summary>
		affix_base::threading::cross_thread_mutex m_new_connections_mutex;

		/// <summary>
		/// A vector of all newly established connections.
		/// </summary>
		std::vector<affix_base::data::ptr<asio::ip::tcp::socket>> m_new_connections;

		/// <summary>
		/// A vector of all current authentication attempts, which holds those for both inbound and outbound connections.
		/// </summary>
		std::vector<affix_base::data::ptr<authentication_attempt>> m_authentication_attempts;

		/// <summary>
		/// A vector of fully authenticated connections.
		/// </summary>
		std::vector<affix_base::data::ptr<affix_services::networking::connection>> m_connections;
		
	public:
		/// <summary>
		/// Processes all active connections with this client.
		/// </summary>
		void process_connections(

		);

	protected:
		/// <summary>
		/// Processes all new (unauthenticated) connections.
		/// </summary>
		void process_new_connections(

		);

		/// <summary>
		/// Processes all authentication attempts, looping through, checking if any have been successful,
		/// and stopping any if they've expired.
		/// </summary>
		void process_authentication_attempts(

		);

		/// <summary>
		/// Processes all authenticated connections, checking if any have disconnected, and disposing
		/// of the allocated resources if so.
		/// </summary>
		void process_authenticated_connections(

		);

	};
}

