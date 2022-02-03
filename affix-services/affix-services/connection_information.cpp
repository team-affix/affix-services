#include "connection_information.h"

using namespace affix_services;
using namespace asio::ip;

connection_information::connection_information(
	const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
	const asio::ip::tcp::endpoint& a_remote_endpoint,
	const asio::ip::tcp::endpoint& a_local_endpoint,
	const bool& a_inbound,
	const bool& a_connected
) :
	m_socket(a_socket),
	m_remote_endpoint(a_remote_endpoint),
	m_local_endpoint(a_local_endpoint),
	m_inbound(a_inbound),
	m_connected(a_connected)
{

}
