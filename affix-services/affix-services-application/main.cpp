#include "affix-base/ptr.h"
#include "server.h"
#include "processor.h"
#include <iostream>
#include <fstream>
#include "outbound_connection_configuration.h"
#include "pending_outbound_connection.h"

using affix_base::data::ptr;
using affix_services_application::server_configuration;
using affix_services_application::server;
using affix_services_application::processor;
using affix_services_application::outbound_connection_configuration;
using affix_services_application::pending_outbound_connection;
using namespace asio::ip;

int main()
{
	// Redirect std::clog to nullbuffer
	std::ofstream l_nullstream;
	//std::clog.rdbuf(l_nullstream.rdbuf());

	// Create IO context object, which will be used for entire program's networking
	asio::io_context l_io_context;
	//asio::io_context l_outbound_io_context;

	affix_base::cryptography::rsa_key_pair l_key_pair = affix_base::cryptography::rsa_generate_key_pair(2048);

	processor l_processor(l_key_pair);

	affix_base::data::ptr<server_configuration> l_server_configuration(
		new server_configuration(l_io_context)
	);

	l_server_configuration->export_connection_information("testing123.txt");
	
	server l_server(
		l_server_configuration,
		l_processor.m_unauthenticated_connections_mutex,
		l_processor.m_unauthenticated_connections
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

	ptr<outbound_connection_configuration> l_outbound_connection_config(
		new outbound_connection_configuration(l_io_context, l_server_local_endpoint)
	);

	pending_outbound_connection l_pending_outbound_connection(
		l_outbound_connection_config,
		l_processor.m_unauthenticated_connections_mutex,
		l_processor.m_unauthenticated_connections
	);

	std::thread l_context_thread(
		[&]
		{
			while (true)
			{
				asio::io_context::work l_idle_work(l_io_context);
				l_io_context.run();
				l_io_context.reset();
			}
		});
	/*std::thread l_outbound_context_thread(
		[&]
		{
			while (true)
			{
				asio::io_context::work l_idle_work(l_outbound_io_context);
				l_outbound_io_context.run();
				l_outbound_io_context.reset();
			}
		});*/

	while (true)
	{
		l_processor.process();
	}

	if (l_context_thread.joinable())
		l_context_thread.join();

	/*if (l_outbound_context_thread.joinable())
		l_outbound_context_thread.join();*/

	return 0;
}
