#include "affix-base/ptr.h"
#include "affix-services/client.h"
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
using affix_services::client;
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

	// Create agent information
	ptr<agent_information> l_agent_information(new agent_information("test-agent"));

	// Get configuration for the connection processor
	std::clog << "[ APPLICATION ] Importing client_0 configuration..." << std::endl;
	ptr<client_configuration> l_client_configuration_0(new client_configuration("config/client_configuration_0.json"));
	l_client_configuration_0->import_resource();
	l_client_configuration_0->export_resource();

	client l_client_0(
		l_io_context,
		l_client_configuration_0,
		l_agent_information
	);

	std::clog << "[ APPLICATION ] Importing client_1 configuration..." << std::endl;
	ptr<client_configuration> l_client_configuration_1(new client_configuration("config/client_configuration_1.json"));
	l_client_configuration_1->import_resource();
	l_client_configuration_1->export_resource();

	client l_client_1(
		l_io_context,
		l_client_configuration_1,
		l_agent_information
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
	size_t l_max_relay_messages = 1000;

	bool l_displayed_requests = false;
	bool l_displayed_responses = false;

	// Processing loop
	for (int i = 0; true; i++)
	{
		try
		{
			l_client_0.process();
			l_client_1.process();
		}
		catch (std::exception a_ex)
		{
			std::cerr << a_ex.what() << std::endl;
		}

		affix_base::threading::locked_resource l_authenticated_connections = l_client_0.m_authenticated_connections.lock();
		affix_base::threading::locked_resource l_agent_received_messages = l_client_0.m_agent_received_messages.lock();

		if (!l_displayed_requests && l_agent_received_messages->size() == l_max_relay_messages)
		{
			l_displayed_requests = true;
			std::cout << "ALL RELAY REQUESTS RECEIVED" << std::endl;
		}

		if (l_authenticated_connections->size() >= 1 && l_relayed_messages < l_max_relay_messages)
		{
			l_relayed_messages++;

			std::vector<std::string> l_path_0 =
			{
				l_client_0.m_local_identity,
				l_client_1.m_local_identity,
			};
			std::vector<std::string> l_path_1 =
			{
				l_client_1.m_local_identity,
				l_client_0.m_local_identity,
			};

			l_client_0.relay(
				l_path_0
			);

			l_client_1.relay(
				l_path_1
			);

			l_client_0.trace_paths();
			l_client_1.trace_paths();

		}

	}

	l_context_thread_continue = false;

	if (l_context_thread.joinable())
		l_context_thread.join();

	return 0;
}
