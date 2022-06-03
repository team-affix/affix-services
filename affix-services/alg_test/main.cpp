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
#include "affix-services/agent.h"

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
	};

	// Get configuration for the connection processor
	std::clog << "[ APPLICATION ] Importing client_0 configuration..." << std::endl;
	ptr<client_configuration> l_client_configuration_0(new client_configuration("config/client_configuration_0.json"));
	l_client_configuration_0->import_resource();
	l_client_configuration_0->export_resource();

	client l_client_0(
		l_io_context,
		l_client_configuration_0
	);
	agent<std::string, std::string> l_agent_0(l_client_0, "test_agent", "agent-specific-information-0");

	int message_iteration = 0;

	l_agent_0.m_guarded_data.lock();
	
	l_agent_0.m_guarded_data->m_remote_invocation_processor.add_function(
		"test-function",
		std::function([&](std::string a_client_identity)
			{
				if (message_iteration % 100 == 0)
					std::cout << "RECEIVED MESSAGE:" << message_iteration << std::endl;
				message_iteration++;
			}));

	l_agent_0.m_guarded_data.unlock();

	std::clog << "[ APPLICATION ] Importing client_1 configuration..." << std::endl;
	ptr<client_configuration> l_client_configuration_1(new client_configuration("config/client_configuration_1.json"));
	l_client_configuration_1->import_resource();
	l_client_configuration_1->export_resource();

	client l_client_1(
		l_io_context,
		l_client_configuration_1
	);
	agent<std::string, std::string> l_agent_1(l_client_1, "test_agent", "agent-specific-information-1");

	std::clog << "[ APPLICATION ] Importing client_2 configuration..." << std::endl;
	ptr<client_configuration> l_client_configuration_2(new client_configuration("config/client_configuration_2.json"));
	l_client_configuration_2->import_resource();
	l_client_configuration_2->export_resource();

	client l_client_2(
		l_io_context,
		l_client_configuration_2
	);
	agent<std::string, std::string> l_agent_2(l_client_2, "test_agent", "agent-specific-information-2");

	// Disclose all agent information
	l_agent_0.disclose_agent_information();
	l_agent_1.disclose_agent_information();
	l_agent_2.disclose_agent_information();

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

	uint64_t l_start_time = affix_base::timing::utc_time();

	// Processing loop
	for (int i = 0; true; i++)
	{
		try
		{
			l_client_0.process();
			l_client_1.process();
			l_client_2.process();

			l_agent_0.process();
			l_agent_1.process();
			l_agent_2.process();
		}
		catch (std::exception a_ex)
		{
			std::cerr << a_ex.what() << std::endl;
		}

		std::scoped_lock l_client_0_lock(l_client_0.m_guarded_data);
		std::scoped_lock l_client_1_lock(l_client_1.m_guarded_data);

		const auto& l_authenticated_connections = l_client_0.m_guarded_data->m_authenticated_connections;
		
		auto& l_client_1_auth_connections = l_client_1.m_guarded_data->m_authenticated_connections;

		if (affix_base::timing::utc_time() - l_start_time > 5 &&
			l_client_1_auth_connections.size() > 0)
		{
			auto l_connection_with_client_0 =
				std::find_if(l_client_1_auth_connections.begin(), l_client_1_auth_connections.end(),
					[&](ptr<affix_services::networking::authenticated_connection> a_auth_conn)
					{
						return a_auth_conn->remote_identity() == l_client_0.m_local_identity;
					});

			if (l_connection_with_client_0 != l_client_1_auth_connections.end())
			{
				(*l_connection_with_client_0)->close();
				std::cout << "CLOSING CONNECTION TO CLIENT 0" << std::endl;
			}

		}

		if (l_authenticated_connections.size() >= 1 && l_relayed_messages < l_max_relay_messages)
		{
			if (l_client_1.fastest_path_to_identity(l_client_0.m_local_identity).size() > 0)
			{
				l_agent_1.invoke(
					l_client_0.m_local_identity,
					"test-function"
				);
				l_relayed_messages++;
			}
		}

	}

	l_context_thread_continue = false;

	if (l_context_thread.joinable())
		l_context_thread.join();

	return 0;
}
