#include "unauthenticated_connection.h"

using namespace affix_services_application;

unauthenticated_connection::unauthenticated_connection(
	const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
	const bool& a_inbound_connection
) :
	m_socket(a_socket),
	m_inbound_connection(a_inbound_connection)
{

}
