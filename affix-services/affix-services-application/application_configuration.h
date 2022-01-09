#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "affix-services/connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"
#include "server_configuration.h"

namespace affix_services_application
{
	struct application_configuration
	{
		server_configuration m_server_configuration;
	};
}
