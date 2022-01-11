#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "affix-services/connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"

namespace affix_services_application
{
	struct server_configuration
	{
	public:
		/// <summary>
		/// IO context by which ASIO performs most of its asynchronous tasks.
		/// </summary>
		asio::io_context m_acceptor_context;

		/// <summary>
		/// If the server has a dedicated port, the endpoint for this acceptor should include that port.
		/// If the server doesn't have a dedicated port, the endpoint should have port set to 0.
		/// </summary>
		asio::ip::tcp::acceptor m_acceptor;
		
	public:
		/// <summary>
		/// Initializes all member variables, including the acceptor, with port defaulting to zero.
		/// </summary>
		/// <param name="a_callback"></param>
		/// <param name="a_port"></param>
		server_configuration(
			const uint16_t& a_port = 0
		);

	public:
		/// <summary>
		/// Exports configuration, (including the current assigned port number) to a file,
		/// so other applications wishing to connect on this local machine are able to.
		/// </summary>
		void export_connection_information(
			const std::string& a_file_path
		) const;

		/// <summary>
		/// Tries to import all information regarding how a server will be configured from a file.
		/// </summary>
		/// <param name="a_istream"></param>
		/// <param name="a_result"></param>
		/// <returns></returns>
		static bool try_import(
			const std::string& a_file_path,
			affix_base::data::ptr<server_configuration> a_result
		);

	};
}
