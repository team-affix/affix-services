#pragma once
#include "affix-base/pch.h"
#include "affix-base/rsa.h"

namespace affix_services
{
	struct connection_processor_configuration
	{
	public:
		/// <summary>
		/// Path to the JSON file.
		/// </summary>
		std::string m_json_file_path;

		/// <summary>
		/// Boolean describing whether or not timing out is enabled for pending authentication attempts.
		/// </summary>
		bool m_enable_pending_authentication_timeout = true;

		/// <summary>
		/// Maximum time after which pending authentication attempts will be discarded.
		/// </summary>
		uint64_t m_pending_authentication_maximum_idle_time_in_seconds = 10;

		/// <summary>
		/// Boolean describing whether or not to close sockets after the connections have gone stale.
		/// </summary>
		bool m_enable_authenticated_connection_disconnect_after_maximum_idle_time = true;

		/// <summary>
		/// Maximum time after which connections should be closed if they been idling.
		/// (if m_authenticated_connection_enable_disconnect_after_maximum_idle_time is false, this will not take effect)
		/// </summary>
		uint64_t m_authenticated_connection_maximum_idle_time_in_seconds = 21600;

		/// <summary>
		/// The local RSA key pair, used for all message security
		/// </summary>
		affix_base::cryptography::rsa_key_pair m_local_key_pair;

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
			const std::string& a_json_file_path
		);

		/// <summary>
		/// Exports the configuration to a JSON file.
		/// </summary>
		/// <param name="a_file_path"></param>
		void export_to_file(

		);

		/// <summary>
		/// Imports the configuration from a JSON file.
		/// </summary>
		/// <param name="a_file_path"></param>
		/// <returns></returns>
		void import_from_file(

		);

	};
}

