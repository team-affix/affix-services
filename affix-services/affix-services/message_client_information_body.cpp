#include "message_client_information_body.h"

using namespace affix_services;

message_client_information_body::message_client_information_body(

) :
	affix_base::data::serializable(m_client_information)
{

}

message_client_information_body::message_client_information_body(
	const client_information& a_client_information
) :
	affix_base::data::serializable(m_client_information),
	m_client_information(a_client_information)
{

}

message_client_information_body::message_client_information_body(
	const message_client_information_body& a_message_rqt_index_body
) :
	message_client_information_body(
		a_message_rqt_index_body.m_client_information)
{

}

message_header<message_types, affix_base::details::semantic_version_number> message_client_information_body::create_message_header(

) const
{
	return message_header(message_types::client_information, i_affix_services_version);
}
