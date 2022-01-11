#include "outbound_connection_configuration.h"

using namespace affix_services_application;
using namespace asio::ip;

asio::io_context outbound_connection_configuration::s_socket_context;

outbound_connection_configuration::outbound_connection_configuration(
	const asio::ip::tcp::endpoint& a_remote_endpoint,
	const uint16_t& a_port
) :
	m_remote_endpoint(a_remote_endpoint),
	m_socket(s_socket_context, tcp::endpoint(tcp::v4(), a_port))
{

}
