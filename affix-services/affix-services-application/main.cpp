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
	std::clog << "[ APPLICATION ] Importing application configuration..." << std::endl;
	ptr<application_configuration> l_application_configuration_0(new application_configuration("config/application_configuration_0.json"));
	l_application_configuration_0->import_resource();
	l_application_configuration_0->export_resource();

	application l_application_0(
		l_io_context,
		l_application_configuration_0
	);

	ptr<application_configuration> l_application_configuration_1(new application_configuration("config/application_configuration_1.json"));
	l_application_configuration_1->import_resource();
	l_application_configuration_1->export_resource();

	application l_application_1(
		l_io_context,
		l_application_configuration_1
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
			l_application_0.process();
			l_application_1.process();
		}
		catch (std::exception a_ex)
		{
			std::cerr << a_ex.what() << std::endl;
		}

		affix_base::threading::locked_resource l_authenticated_connections = l_application_0.m_authenticated_connections.lock();
		affix_base::threading::locked_resource l_module_received_relay_requests = l_application_0.m_module_received_relay_requests.lock();
		affix_base::threading::locked_resource l_module_received_relay_responses = l_application_0.m_module_received_relay_responses.lock();

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
				l_application_1.m_local_identity,
				l_application_0.m_local_identity
			};

			l_application_0.relay(
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
