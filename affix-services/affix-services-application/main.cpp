#include "affix-base/ptr.h"
#include "server.h"
#include <iostream>
#include <fstream>

using affix_base::data::ptr;
using affix_services_application::server_configuration;
using affix_services_application::server;

int main()
{
	server_configuration l_server_configuration;

	l_server_configuration.export_connection_information("testing123.txt");

	return 0;
}
