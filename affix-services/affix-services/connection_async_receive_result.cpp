#include "connection_async_receive_result.h"

using namespace affix_services::networking;

connection_async_receive_result::connection_async_receive_result(
	const affix_base::data::ptr<connection>& a_owner,
	const std::vector<uint8_t>& a_message_header_data,
	const std::vector<uint8_t>& a_message_body_data
) :
	m_owner(a_owner),
	m_message_header_data(a_message_header_data),
	m_message_body_data(a_message_body_data)
{
	m_successful = 
		m_message_header_data.size() > 0 &&
		m_message_body_data.size() > 0;
}
