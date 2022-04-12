#include "message_agent_information_body.h"

using namespace affix_services;

message_agent_information_body::message_agent_information_body(

) :
	affix_base::data::serializable(m_client_path, m_agent_information)
{

}

message_agent_information_body::message_agent_information_body(
	const std::vector<std::string>& a_client_path,
	const agent_information& a_agent_information
) :
	affix_base::data::serializable(m_client_path, m_agent_information),
	m_client_path(a_client_path),
	m_agent_information(a_agent_information)
{

}

message_agent_information_body::message_agent_information_body(
	const message_agent_information_body& a_message_rqt_index_body
) :
	message_agent_information_body(
		a_message_rqt_index_body.m_client_path,
		a_message_rqt_index_body.m_agent_information)
{

}

message_header<message_types, affix_base::details::semantic_version_number> message_agent_information_body::create_message_header(

) const
{
	return message_header(message_types::agent_information, i_affix_services_version);
}
