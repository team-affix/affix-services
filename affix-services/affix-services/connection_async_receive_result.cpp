#include "connection_async_receive_result.h"

using namespace affix_services::networking;

connection_async_receive_result::connection_async_receive_result(
	const affix_base::data::ptr<authenticated_connection>& a_owner,
	affix_base::data::byte_buffer a_byte_buffer
) :
	m_owner(a_owner),
	m_byte_buffer(a_byte_buffer)
{

}

bool connection_async_receive_result::successful(

) const
{
	return m_byte_buffer.size() > 0;
}
