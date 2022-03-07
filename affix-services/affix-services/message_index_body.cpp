#include "message_index_body.h"

using namespace affix_services;

message_index_body::message_index_body(

) :
	affix_base::data::serializable(m_client_identities, m_current_client_identity_path, m_agents)
{

}

message_index_body::message_index_body(
	const affix_base::data::tree<std::string>& a_client_identities,
	const std::vector<std::string>& a_current_client_identity_resource_path,
	const std::map<std::string, affix_services::agent_information> a_agents
) :
	affix_base::data::serializable(m_client_identities, m_current_client_identity_path, m_agents),
	m_client_identities(a_client_identities),
	m_current_client_identity_path(a_current_client_identity_resource_path),
	m_agents(a_agents)
{

}

message_index_body::message_index_body(
	const message_index_body& a_message_rqt_index_body
) :
	message_index_body(a_message_rqt_index_body.m_client_identities, a_message_rqt_index_body.m_current_client_identity_path, a_message_rqt_index_body.m_agents)
{

}

message_header message_index_body::create_message_header(

) const
{
	return message_header(message_types::index, message_header::random_discourse_identifier());
}
