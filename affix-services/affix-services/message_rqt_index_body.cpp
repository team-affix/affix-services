#include "message_rqt_index_body.h"

using namespace affix_services;

message_rqt_index_body::message_rqt_index_body(

) :
	affix_base::data::serializable(m_agents)
{

}

message_rqt_index_body::message_rqt_index_body(
	const affix_base::data::tree<std::string>& a_agents
) :
	affix_base::data::serializable(m_agents),
	m_agents(a_agents)
{

}

message_rqt_index_body::message_rqt_index_body(
	const message_rqt_index_body& a_message_rqt_index_body
) :
	message_rqt_index_body(a_message_rqt_index_body.m_agents)
{

}

message_header message_rqt_index_body::create_message_header(

) const
{
	return message_header(message_types::rqt_index, message_header::random_discourse_identifier());
}
