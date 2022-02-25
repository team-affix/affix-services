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

	bool l_ran_tests = false;

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

		if (l_authenticated_connections->size() >= 1)// && !l_ran_tests)
		{
			message_rqt_relay_body l_request_body(
				{
					"MIICIDANBgkqhkiG9w0BAQEFAAOCAg0AMIICCAKCAgEAlSMW3OW7Gj5e/kMRMJ/f+SQB24Twlwy9RxBxYvgp+jJTaCbzZx/UxtOX7w+8WRmnqxiPSXYT8Fn2I/SXt/JuLAdecyVlMBcPFbw5NXk0kHxdTvKTHdlJLWZnVJNyMjVyDK1++GBJl7Z/DhVlH7avrJABgDwPyNywdZqGcOFPuQQqJ2yAQPrbkCpa4qDgTAM7iaSIkdxfwa/tUWYnTscE9WLfL5Srao+5qK+Gdmk6ZIs8eNa7PaRC2iow07ZqM79AX5EFsc3C3rThVUwFlekB7MzAQKafrU020jqU+iDvm/voZ87ichnrhcBqV0YHL1trWSUaN2tRJtqS8T/d5FzQbxGRhUoKLJ45VEi1uKEaJquNRLAO5wVU6xwt5QpWorom+6Iw4Az8kN0JCNYC9/Li2sQWi/m3lziUiZvMCZTkSx5fRIcQF0UmdxmUnrd4mvr2hHRkfTuj+sczXoAIYQSEcSdjBr7uFJFnG01uUeBq1BUnwhvWXqZNN9627yq9HTN7uYRMkY4Od9xIBve4Z9rST2DgRKUydHfsoYAx0t7k1FlwzF8zE8X8p4dPTGxJrqS5WTMw+kOU/5EZjZOZPWinDwUXRdKfhrPHCnyHGPDrY+cAaB8BtxSVJ5XWVlNI7EK4EZ/2Qen1TsEPwYRQoM3cR1CC2WeiS1RjjgJqaJuljMkCARE="
				},
				0,
				std::vector<uint8_t>(1000));

			message l_request(l_request_body.create_message_header(), l_request_body);

			l_application.async_send_message(
				std::string("MIICIDANBgkqhkiG9w0BAQEFAAOCAg0AMIICCAKCAgEAlSMW3OW7Gj5e/kMRMJ/f+SQB24Twlwy9RxBxYvgp+jJTaCbzZx/UxtOX7w+8WRmnqxiPSXYT8Fn2I/SXt/JuLAdecyVlMBcPFbw5NXk0kHxdTvKTHdlJLWZnVJNyMjVyDK1++GBJl7Z/DhVlH7avrJABgDwPyNywdZqGcOFPuQQqJ2yAQPrbkCpa4qDgTAM7iaSIkdxfwa/tUWYnTscE9WLfL5Srao+5qK+Gdmk6ZIs8eNa7PaRC2iow07ZqM79AX5EFsc3C3rThVUwFlekB7MzAQKafrU020jqU+iDvm/voZ87ichnrhcBqV0YHL1trWSUaN2tRJtqS8T/d5FzQbxGRhUoKLJ45VEi1uKEaJquNRLAO5wVU6xwt5QpWorom+6Iw4Az8kN0JCNYC9/Li2sQWi/m3lziUiZvMCZTkSx5fRIcQF0UmdxmUnrd4mvr2hHRkfTuj+sczXoAIYQSEcSdjBr7uFJFnG01uUeBq1BUnwhvWXqZNN9627yq9HTN7uYRMkY4Od9xIBve4Z9rST2DgRKUydHfsoYAx0t7k1FlwzF8zE8X8p4dPTGxJrqS5WTMw+kOU/5EZjZOZPWinDwUXRdKfhrPHCnyHGPDrY+cAaB8BtxSVJ5XWVlNI7EK4EZ/2Qen1TsEPwYRQoM3cR1CC2WeiS1RjjgJqaJuljMkCARE="),
				l_request,
				[&](bool a_result)
				{

				});

			l_ran_tests = true;
		}

		Sleep(10);
	}

	l_context_thread_continue = false;

	if (l_context_thread.joinable())
		l_context_thread.join();

	return 0;
}
