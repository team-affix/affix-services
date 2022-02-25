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

	uint64_t l_start_time = affix_base::timing::utc_time();

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

		if (l_authenticated_connections->size() >= 1 && affix_base::timing::utc_time() - l_start_time <= 10)// && !l_ran_tests)
		{
			std::vector<std::string> l_path =
			{
				"MIICIDANBgkqhkiG9w0BAQEFAAOCAg0AMIICCAKCAgEAswopTFSw/Q3DjMY3dckiCoTuO5ddzNAF2cowZKeS24X0/POiK/13Z81gP3iuNsueYiEZv6LCn234DGgUnWkcRtVvhJWP7hX1sPbyFe/Qp12R/L1CRVmJKAyAoHAOkzdapQxpPl1IvVimtvAEfdjDPLg6GBBmQyeJIOj/gDOYAF3VXYMAxUKjdvCyv1+vFtVzmoU7X/lvMskM7TkKrj9V7W0aqUjyWsVlF5ws8U5o4MPd6xTwoVOSU+sBI+puhuW0TgTyHbQLVIybItsbUex+ExdeQoUo3/D+l3YWGZQs54+VtsTAtM6Ri8GuPCZnBF7L7DmdcF9f5kRJmkOvDXwYACpolW4o+vfmqHwLKw4jM5g16RW1lVZXlZeZCusJ145BOYX0kPz1UH757fS07oCaELb4hXqgYi0HbsTBYTwSIhuRISszk9g86o8Fn96DjQze2sPP5rxFkGnbcpLdnDOecWUPQ5mojk8S5fNF0HSH8UvdFNMOEaAnd8TE14bRLx1XIcoSX0vHJC4csIkCU3RgkwOqaxb3KwiuU5jWHXy4b0WfMQGKsninj6Cu+nFZ4917gOLQtUrOhnG8kYzlaHUoRqdMH7FAPAfoAX7Q0/YyXCAZGZlZfIfQXy3LtckNLQdOs7Gl4bA65DNm5bJKBu+OC2yJ7iZvdBWBnOW8ZTUH6+sCARE=",
				"MIICIDANBgkqhkiG9w0BAQEFAAOCAg0AMIICCAKCAgEAlSMW3OW7Gj5e/kMRMJ/f+SQB24Twlwy9RxBxYvgp+jJTaCbzZx/UxtOX7w+8WRmnqxiPSXYT8Fn2I/SXt/JuLAdecyVlMBcPFbw5NXk0kHxdTvKTHdlJLWZnVJNyMjVyDK1++GBJl7Z/DhVlH7avrJABgDwPyNywdZqGcOFPuQQqJ2yAQPrbkCpa4qDgTAM7iaSIkdxfwa/tUWYnTscE9WLfL5Srao+5qK+Gdmk6ZIs8eNa7PaRC2iow07ZqM79AX5EFsc3C3rThVUwFlekB7MzAQKafrU020jqU+iDvm/voZ87ichnrhcBqV0YHL1trWSUaN2tRJtqS8T/d5FzQbxGRhUoKLJ45VEi1uKEaJquNRLAO5wVU6xwt5QpWorom+6Iw4Az8kN0JCNYC9/Li2sQWi/m3lziUiZvMCZTkSx5fRIcQF0UmdxmUnrd4mvr2hHRkfTuj+sczXoAIYQSEcSdjBr7uFJFnG01uUeBq1BUnwhvWXqZNN9627yq9HTN7uYRMkY4Od9xIBve4Z9rST2DgRKUydHfsoYAx0t7k1FlwzF8zE8X8p4dPTGxJrqS5WTMw+kOU/5EZjZOZPWinDwUXRdKfhrPHCnyHGPDrY+cAaB8BtxSVJ5XWVlNI7EK4EZ/2Qen1TsEPwYRQoM3cR1CC2WeiS1RjjgJqaJuljMkCARE=",
			};

			message_rqt_relay_body l_request_body(
				l_path,
				0,
				std::vector<uint8_t>(1000));

			message l_request(l_request_body.create_message_header(), l_request_body);

			l_application.async_send_message(
				l_path.front(),
				l_request,
				[&](bool a_result)
				{

				});


		}

		Sleep(10);
	}

	l_context_thread_continue = false;

	if (l_context_thread.joinable())
		l_context_thread.join();

	return 0;
}
