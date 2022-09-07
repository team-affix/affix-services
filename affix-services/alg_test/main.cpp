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
	std::clog << "[ APPLICATION ] Creating client 0..." << std::endl;
	
	client l_client_0(
		l_io_context,
		"config/client_configuration_0.json"
	);
	agent<std::string, std::string> l_agent_0(l_client_0, "test_agent", "agent-specific-information-0");
	int message_iteration = 0;

	l_agent_0.add_function(
		"test-request",
		std::function([&](std::string a_client_identity, int a_request_index)
			{
				if (message_iteration % 100 == 0)
					std::cout << "RECEIVED MESSAGE:" << message_iteration << " REQUEST INDEX: " << a_request_index << std::endl;

				l_agent_0.invoke(a_client_identity, "test-response", int(10));

				message_iteration++;

			}));

	std::clog << "[ APPLICATION ] Creating client 1..." << std::endl;
	
	client l_client_1(
		l_io_context,
		"config/client_configuration_1.json"
	);
	agent<std::string, std::string> l_agent_1(l_client_1, "test_agent", "agent-specific-information-1");

	std::clog << "[ APPLICATION ] Creating client 2..." << std::endl;
	
	client l_client_2(
		l_io_context,
		"config/client_configuration_2.json"
	);
	agent<std::string, std::string> l_agent_2(l_client_2, "test_agent", "agent-specific-information-2");

	// Disclose all agent information
	l_agent_0.disclose_agent_information();
	l_agent_1.disclose_agent_information();
	l_agent_2.disclose_agent_information();

	// Boolean describing whether the context thread should continue
	bool l_background_thread_continue = true;

	// Run context
	std::thread l_context_thread(
		[&]
		{
			while (l_background_thread_continue)
			{
				asio::io_context::work l_idle_work(l_io_context);
				l_io_context.run();
			}
		});

	std::thread l_affix_services_thread(
		[&]
		{
			while (l_background_thread_continue)
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
		{
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
		}

		if (l_relayed_messages < l_max_relay_messages)
		{
			int l_response = 0;

			if (l_agent_1.synchronize(
				l_client_0.m_local_identity,
				"test-request", "test-response", std::tuple{int(l_relayed_messages)}, std::forward_as_tuple(l_response)))
			{
				l_relayed_messages++;
			}
		}

	}

	l_background_thread_continue = false;

	if (l_context_thread.joinable())
		l_context_thread.join();
	if (l_affix_services_thread.joinable())
		l_affix_services_thread.join();

	return 0;
}
