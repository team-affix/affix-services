#include "message_relay_body.h"

using namespace affix_services;
using namespace affix_base::cryptography;

message_relay_body::message_relay_body(

) :
	affix_base::data::serializable(m_path, m_payload, m_client_identity, m_agent_information)
{

}

message_relay_body::message_relay_body(
	const std::vector<std::string>& a_path,
	const std::vector<uint8_t>& a_payload,
	const std::string a_client_identity,
	const agent_information& a_agent_information
) :
	affix_base::data::serializable(m_path, m_payload, m_client_identity, m_agent_information),
	m_path(a_path),
	m_payload(a_payload),
	m_client_identity(a_client_identity),
	m_agent_information(a_agent_information)
{

}

message_relay_body::message_relay_body(
	const message_relay_body& a_message_rqt_relay_body
) :
	message_relay_body(a_message_rqt_relay_body.m_path, a_message_rqt_relay_body.m_payload, a_message_rqt_relay_body.m_client_identity, a_message_rqt_relay_body.m_agent_information)
{

}

message_header message_relay_body::create_message_header(

) const
{
	return message_header(message_types::relay, message_header::random_discourse_identifier());
}
