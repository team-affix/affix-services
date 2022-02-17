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
#include "affix-base/string_extensions.h"

using affix_base::data::ptr;
using affix_services::server_configuration;
using affix_services::server;
using affix_services::connection_processor;
using affix_services::connection_information;
using affix_services::pending_connection;
using affix_services::message_processor;
using affix_services::message_rqt_relay;
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
		l_message_processor.authenticated_connection_receive_results(),
		l_connection_processor_configuration
	);

	// Get configuration for the server
	std::clog << "[ APPLICATION ] Importing server configuration..." << std::endl;
	affix_base::data::ptr<server_configuration> l_server_configuration(new server_configuration("config/server_configuration.json"));
	l_server_configuration->import_resource();

	server l_server(
		l_io_context,
		l_processor.connection_results(),
		l_server_configuration
	);

	// Export the server configuration, which now contains the bound port
	l_server_configuration->export_resource();

	// Get remote endpoint strings
	const std::vector<std::string>& l_remote_endpoint_strings = l_connection_processor_configuration->m_remote_endpoint_strings.resource();

	// Connect to remote parties
	for (int i = 0; i < l_remote_endpoint_strings.size(); i++)
	{
		std::vector<std::string> l_remote_endpoint_string_split = affix_base::data::string_split(l_remote_endpoint_strings[i], ':');

		// Check if the remote endpoint is localhost
		bool l_remote_localhost = l_remote_endpoint_string_split[0] == "localhost";

		asio::ip::tcp::endpoint l_remote_endpoint;

		// Configure address of remote endpoint
		if (!l_remote_localhost)
			l_remote_endpoint.address(asio::ip::make_address(l_remote_endpoint_string_split[0]));

		// Configure port of remote endpoint
		l_remote_endpoint.port(std::stoi(l_remote_endpoint_string_split[1]));

		// Start pending outbound connection
		std::clog << "[ APPLICATION ] Connecting to: " << l_remote_endpoint_strings[i] << std::endl;
		l_processor.start_pending_outbound_connection(0, l_remote_endpoint, l_remote_localhost);

	}

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

	// Processing loop
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
