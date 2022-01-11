#include "affix-base/ptr.h"
#include "server.h"
#include "processor.h"
#include <iostream>
#include <fstream>

using affix_base::data::ptr;
using affix_services_application::server_configuration;
using affix_services_application::server;
using affix_services_application::processor;

int main()
{
	// Redirect std::clog to nullbuffer
	std::ofstream l_nullstream;
	std::clog.rdbuf(l_nullstream.rdbuf());

	affix_base::cryptography::rsa_key_pair l_key_pair = affix_base::cryptography::rsa_generate_key_pair(2048);

	processor l_processor(l_key_pair);

	affix_base::data::ptr<server_configuration> l_server_configuration(
		new server_configuration()
	);

	l_server_configuration->export_connection_information("testing123.txt");
	
	server l_server(
		l_server_configuration,
		l_processor.m_unauthenticated_connections_mutex,
		l_processor.m_unauthenticated_connections
	);

	while (true)
	{
		l_processor.process();
	}

	return 0;
}
