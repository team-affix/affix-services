#include "message_index_body.h"

using namespace affix_services;

message_index_body::message_index_body(

) :
	affix_base::data::serializable(m_client_identity_path, m_agent_information)
{

}

message_index_body::message_index_body(
	const std::vector<std::string>& a_client_identity_path,
	const affix_services::agent_information& a_agent_information
) :
	affix_base::data::serializable(m_client_identity_path, m_agent_information),
	m_client_identity_path(a_client_identity_path),
	m_agent_information(a_agent_information)
{

}

message_index_body::message_index_body(
	const message_index_body& a_message_rqt_index_body
) :
	message_index_body(a_message_rqt_index_body.m_client_identity_path, a_message_rqt_index_body.m_agent_information)
{

}

message_header message_index_body::create_message_header(

) const
{
	return message_header(message_types::index, message_header::random_discourse_identifier());
}
