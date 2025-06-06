#include "message_relay_body.h"

using namespace affix_services;
using namespace affix_base::cryptography;

message_relay_body::message_relay_body(

) :
	affix_base::data::serializable(m_source_client_identity, m_targeted_agent_type_identifier, m_payload, m_path)
{

}

message_relay_body::message_relay_body(
	const std::string& a_source_client_identity,
	const std::string& a_targeted_agent_type_identifier,
	const std::vector<uint8_t>& a_payload,
	const std::vector<std::string>& a_path
) :
	affix_base::data::serializable(m_source_client_identity, m_targeted_agent_type_identifier, m_payload, m_path),
	m_source_client_identity(a_source_client_identity),
	m_targeted_agent_type_identifier(a_targeted_agent_type_identifier),
	m_payload(a_payload),
	m_path(a_path)
{

}

message_relay_body::message_relay_body(
	const message_relay_body& a_message_rqt_relay_body
) :
	message_relay_body(
		a_message_rqt_relay_body.m_source_client_identity,
		a_message_rqt_relay_body.m_targeted_agent_type_identifier,
		a_message_rqt_relay_body.m_payload,
		a_message_rqt_relay_body.m_path)
{

}

message_relay_body& message_relay_body::operator=(
	const message_relay_body& a_message_relay_body
)
{
	m_source_client_identity = a_message_relay_body.m_source_client_identity;
	m_targeted_agent_type_identifier = a_message_relay_body.m_targeted_agent_type_identifier;
	m_payload = a_message_relay_body.m_payload;
	m_path = a_message_relay_body.m_path;
	return *this;
}

message_header<message_types, affix_base::details::semantic_version_number> message_relay_body::create_message_header(

) const
{
	return message_header(message_types::relay, i_affix_services_version);
}
