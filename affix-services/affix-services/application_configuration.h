#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "authenticated_connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"
#include "server_configuration.h"

namespace affix_services
{
	struct application_configuration
	{
	public:
		/// <summary>
		/// Server configuration for the application. If there is not to be a server, this remains nullptr.
		/// </summary>
		affix_base::data::ptr<affix_services::server_configuration> m_server_configuration;

	public:
		application_configuration(
			affix_base::data::ptr<affix_services::server_configuration> a_server_configuration
		);
		application_configuration(
			std::istream& a_istream
		);

	};
}
