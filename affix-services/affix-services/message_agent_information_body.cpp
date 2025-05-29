#include "message_agent_information_body.h"

using namespace affix_services;

message_agent_information_body::message_agent_information_body(

) :
	affix_base::data::serializable(m_client_identity, m_agent_information)
{

}

message_agent_information_body::message_agent_information_body(
	const std::string& a_client_identity,
	const agent_information& a_agent_information
) :
	affix_base::data::serializable(m_client_identity, m_agent_information),
	m_client_identity(a_client_identity),
	m_agent_information(a_agent_information)
{

}

message_agent_information_body::message_agent_information_body(
	const message_agent_information_body& a_message_rqt_index_body
) :
	message_agent_information_body(
		a_message_rqt_index_body.m_client_identity,
		a_message_rqt_index_body.m_agent_information)
{

}

message_agent_information_body& message_agent_information_body::operator=(
	const message_agent_information_body& a_message_agent_information_body
)
{
	m_client_identity = a_message_agent_information_body.m_client_identity;
	m_agent_information = a_message_agent_information_body.m_agent_information;
	return *this;
}

message_header<message_types, affix_base::details::semantic_version_number> message_agent_information_body::create_message_header(

) const
{
	return message_header(message_types::agent_information, i_affix_services_version);
}
