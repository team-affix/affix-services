#include "affix-base/ptr.h"
#include "affix-services/server.h"
#include "affix-services/connection_processor.h"
#include <iostream>
#include <fstream>
#include "affix-services/outbound_connection_configuration.h"
#include "affix-services/pending_outbound_connection.h"
#include "affix-base/timing.h"

using affix_base::data::ptr;
using affix_services::server_configuration;
using affix_services::server;
using affix_services::connection_processor;
using affix_services::outbound_connection_configuration;
using affix_services::pending_outbound_connection;
using affix_services::message_processor;
using namespace asio::ip;

int main()
{
	// Redirect std::clog to nullbuffer
	/*std::ofstream l_nullstream;
	std::clog.rdbuf(l_nullstream.rdbuf());*/
	// Create IO context object, which will be used for entire program's networking

	affix_base::cryptography::rsa_key_pair l_key_pair = affix_base::cryptography::rsa_generate_key_pair(2048);

	asio::io_context l_io_context;

	message_processor l_message_processor;


	connection_processor l_processor(
		l_message_processor,
		l_key_pair
	);

	affix_base::data::ptr<server_configuration> l_server_configuration(
		new server_configuration(l_io_context)
	);

	l_server_configuration->export_connection_information("testing123.log");
	
	server l_server(
		l_server_configuration,
		l_processor.m_connection_results
	);

	asio::ip::address l_local_ip_address;

	if (!affix_base::networking::socket_internal_ip_address(l_local_ip_address))
	{
		std::cerr << "Unable to get the local ip address." << std::endl;
		return 1;
	}

	tcp::endpoint l_server_local_endpoint(
		l_local_ip_address,
		l_server.configuration()->acceptor().local_endpoint().port()
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
	
	l_processor.start_pending_outbound_connection(
		new outbound_connection_configuration(l_io_context, l_server_local_endpoint)
	);
		
	uint64_t l_start_time = affix_base::timing::utc_time();

	for(int i = 0; true; i++)
	{
		if (i % 1 == 0)
		{
			l_processor.start_pending_outbound_connection(
				new outbound_connection_configuration(l_io_context, l_server_local_endpoint)
			);
		}
		try
		{
			l_processor.process();
		}
		catch (std::exception a_ex)
		{
			std::cerr << a_ex.what() << std::endl;
		}
		Sleep(100);
	}

	l_context_thread_continue = false;

	if (l_context_thread.joinable())
		l_context_thread.join();



	return 0;
}
