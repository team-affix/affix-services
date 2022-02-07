#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "authenticated_connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"
#include "json.hpp"
#include "affix-base/cache.h"

namespace affix_services
{
	struct server_configuration : public affix_base::data::cache<nlohmann::json>
	{
	public:
		/// <summary>
		/// JSON file path where the exported configuration lives.
		/// </summary>
		std::string m_json_file_path;

		/// <summary>
		/// Boolean describing whether or not the server should be enabled.
		/// </summary>
		affix_base::data::cache<bool> m_enable;

		/// <summary>
		/// Endpoint to which the acceptor is bound.
		/// </summary>
		affix_base::data::cache<uint16_t> m_bind_port;

		/// <summary>
		/// Endpoint to which the acceptor is bound.
		/// </summary>
		affix_base::data::cache<uint16_t> m_bound_port;

	public:
		/// <summary>
		/// Initializes all member variables, including the acceptor, with port defaulting to zero.
		/// </summary>
		/// <param name="a_callback"></param>
		/// <param name="a_port"></param>
		server_configuration(
			const std::string& a_server_configuration_file_path
		);

	};
}
