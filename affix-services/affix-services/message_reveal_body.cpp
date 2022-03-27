#include "message_reveal_body.h"

using namespace affix_services;

message_reveal_body::message_reveal_body(

) :
	affix_base::data::serializable(m_client_identity, m_agent_information, m_path)
{

}

message_reveal_body::message_reveal_body(
	const std::string& a_client_identity,
	const agent_information& a_agent_information,
	const std::vector<std::string>& a_path
) :
	affix_base::data::serializable(m_client_identity, m_agent_information, m_path),
	m_client_identity(a_client_identity),
	m_agent_information(a_agent_information),
	m_path(a_path)
{

}

message_reveal_body::message_reveal_body(
	const message_reveal_body& a_message_rqt_index_body
) :
	message_reveal_body(
		a_message_rqt_index_body.m_client_identity,
		a_message_rqt_index_body.m_agent_information,
		a_message_rqt_index_body.m_path)
{

}

message_header<message_types, affix_base::details::semantic_version_number> message_reveal_body::create_message_header(

) const
{
	return message_header(message_types::reveal, i_affix_services_version);
}
