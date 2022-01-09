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
		bool m_enable_server;
		uint16_t m_acceptor_binding_port;
		std::string m_server_live_export_file_path;
	};
}
