#include "connection_result.h"

using namespace affix_services;

connection_result::connection_result(
	const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
	const asio::ip::tcp::endpoint& a_remote_endpoint,
	const bool& a_inbound_connection,
	const bool& a_successful
) :
	m_socket(a_socket),
	m_remote_endpoint(a_remote_endpoint),
	m_inbound_connection(a_inbound_connection),
	m_successful(a_successful)
{

}
