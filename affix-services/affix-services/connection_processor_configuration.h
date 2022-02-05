#pragma once
#include "affix-base/pch.h"

namespace affix_services
{
	struct connection_processor_configuration
	{
	public:
		/// <summary>
		/// Boolean describing whether or not timing out is enabled for pending authentication attempts.
		/// </summary>
		bool m_pending_authentication_enable_timeout;

		/// <summary>
		/// Maximum time after which pending authentication attempts will be discarded.
		/// </summary>
		uint64_t m_pending_authentication_maximum_idle_time_in_seconds;

		/// <summary>
		/// Boolean describing whether or not to close sockets after the connections have gone stale.
		/// </summary>
		bool m_authenticated_connection_enable_disconnect_after_maximum_idle_time;

		/// <summary>
		/// Maximum time after which connections should be closed if they been idling.
		/// (if m_authenticated_connection_enable_disconnect_after_maximum_idle_time is false, this will not take effect)
		/// </summary>
		uint64_t m_authenticated_connection_maximum_idle_time_in_seconds;

	public:
		/// <summary>
		/// Default constructor for the connection processor configuration.
		/// </summary>
		connection_processor_configuration(

		);

		/// <summary>
		/// Constructor which takes an argument for each field it is to populate.
		/// </summary>
		/// <param name="a_connection_enable_disconnect_after_maximum_idle_time"></param>
		/// <param name="a_connection_maximum_idle_time_in_seconds"></param>
		connection_processor_configuration(
			const bool& a_pending_authentication_enable_timeout,
			const uint64_t& a_pending_authentication_maximum_idle_time_in_seconds,
			const bool& a_authenticated_connection_enable_disconnect_after_maximum_idle_time,
			const uint64_t& a_authenticated_connection_maximum_idle_time_in_seconds
		);

		/// <summary>
		/// Imports the configuration from a file.
		/// </summary>
		/// <param name="a_file_path"></param>
		/// <returns></returns>
		static bool import_from_file(
			const std::string& a_file_path
		);

	};
}

