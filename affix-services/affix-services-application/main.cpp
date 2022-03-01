#include "affix-base/ptr.h"
#include "affix-services/application.h"
#include <iostream>
#include <fstream>
#include "affix-services/connection_information.h"
#include "affix-services/pending_connection.h"
#include "affix-base/timing.h"
#include "json.hpp"
#include <filesystem>
#include "affix-base/string_extensions.h"
#include "affix-base/aes.h"

using affix_base::data::ptr;
using affix_services::application;
using affix_services::connection_information;
using affix_services::pending_connection;
using namespace affix_services;
using namespace asio::ip;
namespace fs = std::filesystem;

int main()
{
	asio::io_context l_io_context;

	if (!fs::exists("config/"))
	{
		// If config directory doesn't exist, create it.
		fs::create_directory("config/");
	}

	// Get configuration for the connection processor
	std::clog << "[ APPLICATION ] Importing connection processor configuration..." << std::endl;
	ptr<application_configuration> l_application_configuration(new application_configuration("config/application_configuration.json"));
	l_application_configuration->import_resource();
	l_application_configuration->export_resource();

	application l_application(
		l_io_context,
		l_application_configuration
	);

	// Boolean describing whether the context thread should continue
	bool l_context_thread_continue = true;

	// Run context
	std::thread l_context_thread(
		[&]
		{
			while (l_context_thread_continue)
			{
				asio::io_context::work l_idle_work(l_io_context);
				l_io_context.run();
			}
		});

	size_t l_relayed_messages = 0;
	size_t l_max_relay_messages = 10000;

	bool l_displayed_requests = false;
	bool l_displayed_responses = false;

	// Processing loop
	for(int i = 0; true; i++)
	{
		try
		{
			l_application.process();
		}
		catch (std::exception a_ex)
		{
			std::cerr << a_ex.what() << std::endl;
		}

		affix_base::threading::locked_resource l_authenticated_connections = l_application.m_authenticated_connections.lock();

		affix_base::threading::locked_resource l_module_received_relay_requests = l_application.m_module_received_relay_requests.lock();

		affix_base::threading::locked_resource l_module_received_relay_responses = l_application.m_module_received_relay_responses.lock();

		if (!l_displayed_requests && l_module_received_relay_requests->size() == l_max_relay_messages)
		{
			l_displayed_requests = true;
			std::cout << "ALL REQUESTS RECEIVED" << std::endl;
		}

		if (!l_displayed_responses && l_module_received_relay_responses->size() == l_max_relay_messages)
		{
			l_displayed_responses = true;
			std::cout << "ALL RESPONSES RECEIVED" << std::endl;
		}

		if (l_authenticated_connections->size() >= 1 && l_relayed_messages < l_max_relay_messages)
		{
			l_relayed_messages++;

			std::vector<std::string> l_path =
			{
				"MIICIDANBgkqhkiG9w0BAQEFAAOCAg0AMIICCAKCAgEAswopTFSw/Q3DjMY3dckiCoTuO5ddzNAF2cowZKeS24X0/POiK/13Z81gP3iuNsueYiEZv6LCn234DGgUnWkcRtVvhJWP7hX1sPbyFe/Qp12R/L1CRVmJKAyAoHAOkzdapQxpPl1IvVimtvAEfdjDPLg6GBBmQyeJIOj/gDOYAF3VXYMAxUKjdvCyv1+vFtVzmoU7X/lvMskM7TkKrj9V7W0aqUjyWsVlF5ws8U5o4MPd6xTwoVOSU+sBI+puhuW0TgTyHbQLVIybItsbUex+ExdeQoUo3/D+l3YWGZQs54+VtsTAtM6Ri8GuPCZnBF7L7DmdcF9f5kRJmkOvDXwYACpolW4o+vfmqHwLKw4jM5g16RW1lVZXlZeZCusJ145BOYX0kPz1UH757fS07oCaELb4hXqgYi0HbsTBYTwSIhuRISszk9g86o8Fn96DjQze2sPP5rxFkGnbcpLdnDOecWUPQ5mojk8S5fNF0HSH8UvdFNMOEaAnd8TE14bRLx1XIcoSX0vHJC4csIkCU3RgkwOqaxb3KwiuU5jWHXy4b0WfMQGKsninj6Cu+nFZ4917gOLQtUrOhnG8kYzlaHUoRqdMH7FAPAfoAX7Q0/YyXCAZGZlZfIfQXy3LtckNLQdOs7Gl4bA65DNm5bJKBu+OC2yJ7iZvdBWBnOW8ZTUH6+sCARE="
			};

			l_application.relay(
				l_path,
				{ 1,2,3,4,5 }
			);

		}

	}

	l_context_thread_continue = false;

	if (l_context_thread.joinable())
		l_context_thread.join();

	return 0;
}
