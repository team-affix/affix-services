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

using affix_base::data::ptr;
using affix_services::application;
using affix_services::connection_information;
using affix_services::pending_connection;
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
		Sleep(10);
	}

	l_context_thread_continue = false;

	if (l_context_thread.joinable())
		l_context_thread.join();

	return 0;
}
