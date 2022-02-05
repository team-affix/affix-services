#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "authenticated_connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"
#include "json.hpp"

namespace affix_services
{
	struct server_configuration
	{
	public:
		/// <summary>
		/// JSON file path where the exported configuration lives.
		/// </summary>
		std::string m_json_file_path;

		/// <summary>
		/// Boolean describing whether or not the server should be enabled.
		/// </summary>
		bool m_enable = true;

		/// <summary>
		/// Endpoint to which the acceptor is bound.
		/// </summary>
		asio::ip::tcp::endpoint m_bind_endpoint;

		/// <summary>
		/// Endpoint to which the acceptor is bound.
		/// </summary>
		asio::ip::tcp::endpoint m_bound_endpoint;

	public:
		/// <summary>
		/// Initializes all member variables, including the acceptor, with port defaulting to zero.
		/// </summary>
		/// <param name="a_callback"></param>
		/// <param name="a_port"></param>
		server_configuration(
			const std::string& a_server_configuration_file_path
		);

		/// <summary>
		/// Exports the current configuration to the JSON file.
		/// </summary>
		void export_to_file(

		);

		/// <summary>
		/// Imports the configuration from the JSON file.
		/// </summary>
		void import_from_file(

		);

	protected:
		/// <summary>
		/// Exports the m_enable field to JSON.
		/// </summary>
		/// <param name="a_json"></param>
		/// <returns></returns>
		bool export_enable(
			nlohmann::json& a_json
		);

		/// <summary>
		/// Imports the m_enable field from JSON.
		/// </summary>
		/// <param name="a_json"></param>
		/// <returns></returns>
		bool import_enable(
			nlohmann::json& a_json
		);

		/// <summary>
		/// Initializes the m_enable field in the JSON structure.
		/// </summary>
		/// <param name="a_json"></param>
		void init_enable(
			nlohmann::json& a_json
		);

		bool export_bind_endpoint(

		)

	};
}
