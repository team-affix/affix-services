#include "message_rqt_relay_body.h"

using namespace affix_services;
using namespace affix_base::cryptography;

message_rqt_relay_body::message_rqt_relay_body(

) :
	affix_base::data::serializable(m_path, m_payload, m_path_index)
{

}

message_rqt_relay_body::message_rqt_relay_body(
	const std::vector<std::string>& a_path,
	const std::vector<uint8_t>& a_payload,
	const size_t& a_path_index
) :
	affix_base::data::serializable(m_path, m_payload, m_path_index),
	m_path(a_path),
	m_payload(a_payload),
	m_path_index(a_path_index)
{

}


message_rqt_relay_body::message_rqt_relay_body(
	const message_rqt_relay_body& a_message_rqt_relay_body
) :
	message_rqt_relay_body(a_message_rqt_relay_body.m_path, a_message_rqt_relay_body.m_payload, a_message_rqt_relay_body.m_path_index)
{

}

message_header message_rqt_relay_body::create_message_header(

) const
{
	return message_header(message_types::rqt_relay, message_header::random_discourse_identifier());
}
