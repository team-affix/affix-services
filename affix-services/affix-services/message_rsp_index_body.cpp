#include "message_rsp_index_body.h"

using namespace affix_services;

message_rsp_index_body::message_rsp_index_body(

) :
	affix_base::data::serializable(m_identities)
{

}

message_rsp_index_body::message_rsp_index_body(
	const affix_base::data::tree<std::string> a_identities
) :
	affix_base::data::serializable(m_identities),
	m_identities(a_identities)
{

}

message_rsp_index_body::message_rsp_index_body(
	const message_rsp_index_body& a_message_rsp_index_body
) :
	message_rsp_index_body(a_message_rsp_index_body.m_identities)
{

}

message_header message_rsp_index_body::create_message_header(
	const message_header& a_request_message_header
) const
{
	return message_header(message_types::rsp_index, a_request_message_header.m_discourse_identifier);
}
