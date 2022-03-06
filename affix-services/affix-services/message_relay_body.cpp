#include "message_relay_body.h"

using namespace affix_services;
using namespace affix_base::cryptography;

message_relay_body::message_relay_body(

) :
	affix_base::data::serializable(m_path, m_path_index, m_payload)
{

}

message_relay_body::message_relay_body(
	const std::vector<std::string>& a_path,
	const size_t& a_path_index,
	const std::vector<uint8_t>& a_payload
) :
	affix_base::data::serializable(m_path, m_path_index, m_payload),
	m_path(a_path),
	m_path_index(a_path_index),
	m_payload(a_payload)
{

}

message_relay_body::message_relay_body(
	const message_relay_body& a_message_rqt_relay_body
) :
	message_relay_body(a_message_rqt_relay_body.m_path, a_message_rqt_relay_body.m_path_index, a_message_rqt_relay_body.m_payload)
{

}

message_header message_relay_body::create_message_header(

) const
{
	return message_header(message_types::relay, message_header::random_discourse_identifier());
}
