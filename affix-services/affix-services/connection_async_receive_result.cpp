#include "connection_async_receive_result.h"

using namespace affix_services::networking;

authenticated_connection_receive_result::authenticated_connection_receive_result(
	const affix_base::data::ptr<authenticated_connection>& a_authenticated_connection,
	affix_base::data::byte_buffer a_byte_buffer
) :
	m_authenticated_connection(a_authenticated_connection),
	m_byte_buffer(a_byte_buffer)
{

}
