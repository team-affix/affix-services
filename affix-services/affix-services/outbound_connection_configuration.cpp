#include "outbound_connection_configuration.h"

using namespace affix_services;
using namespace asio::ip;

outbound_connection_configuration::outbound_connection_configuration(
	asio::io_context& a_io_context,
	const asio::ip::tcp::endpoint& a_remote_endpoint,
	const uint16_t& a_port
) :
	m_remote_endpoint(a_remote_endpoint),
	m_socket(new tcp::socket(a_io_context, tcp::endpoint(tcp::v4(), a_port)))
{

}
