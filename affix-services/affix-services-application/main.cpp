#include "affix-base/ptr.h"
#include "affix-services/server.h"
#include "affix-services/connection_processor.h"
#include <iostream>
#include <fstream>
#include "affix-services/connection_information.h"
#include "affix-services/pending_connection.h"
#include "affix-base/timing.h"
#include "json.hpp"
#include <filesystem>

using affix_base::data::ptr;
using affix_services::server_configuration;
using affix_services::server;
using affix_services::connection_processor;
using affix_services::connection_information;
using affix_services::pending_connection;
using affix_services::message_processor;
using namespace affix_services;
using namespace asio::ip;
namespace fs = std::filesystem;

int main()
{
	// Redirect std::clog to nullbuffer
	/*std::ofstream l_nullstream;
	std::clog.rdbuf(l_nullstream.rdbuf());*/
	// Create IO context object, which will be used for entire program's networking

	asio::io_context l_io_context;
	message_processor l_message_processor;

	if (!fs::exists("config/"))
	{
		// If config directory doesn't exist, create it.
		fs::create_directory("config/");
	}

	// Get configuration for the connection processor
	std::clog << "[ APPLICATION ] Importing connection processor configuration..." << std::endl;
	ptr<connection_processor_configuration> l_connection_processor_configuration(new connection_processor_configuration("config/connection_processor_configuration.json"));
	l_connection_processor_configuration->import_resource();
	l_connection_processor_configuration->export_resource();
	
	connection_processor l_processor(
		l_io_context,
		l_message_processor,
		l_connection_processor_configuration
	);

	// Get configuration for the server
	std::clog << "[ APPLICATION ] Importing server configuration..." << std::endl;
	affix_base::data::ptr<server_configuration> l_server_configuration(new server_configuration("config/server_configuration.json"));
	l_server_configuration->import_resource();

	server l_server(
		l_io_context,
		l_processor.m_connection_results,
		l_server_configuration
	);

	// Export the server configuration, which now contains the bound port
	l_server_configuration->export_resource();

	asio::ip::address l_local_ip_address;

	if (!affix_base::networking::socket_internal_ip_address(l_local_ip_address))
	{
		std::cerr << "Unable to get the local ip address." << std::endl;
		return 1;
	}

	tcp::endpoint l_server_local_endpoint(
		l_local_ip_address,
		l_server_configuration->m_bound_port.resource()
	);

	bool l_context_thread_continue = true;

	std::thread l_context_thread(
		[&]
		{
			while (l_context_thread_continue)
			{
				asio::io_context::work l_idle_work(l_io_context);
				l_io_context.run();
			}
		});
		
	if (l_server_configuration->m_enable.resource())
	{
		// If the server is enabled, connect to it
		l_processor.start_pending_outbound_connection(l_server_local_endpoint);
		std::clog << "[ APPLICATION ] Connecting to server..." << std::endl;
	}

	for(int i = 0; true; i++)
	{
		try
		{
			l_processor.process();
		}
		catch (std::exception a_ex)
		{
			std::cerr << a_ex.what() << std::endl;
		}
		Sleep(10);
	}

	l_context_thread_continue = false;

	if (l_context_thread.joinable())
		l_context_thread.join();

	return 0;
}
