#include "message_client_path_body.h"

using namespace affix_services;

message_client_path_body::message_client_path_body(

) :
	affix_base::data::serializable(m_client_path, m_register)
{

}

message_client_path_body::message_client_path_body(
	const std::vector<std::string>& a_client_path,
	const bool& a_register
) :
	affix_base::data::serializable(m_client_path, m_register),
	m_client_path(a_client_path),
	m_register(a_register)
{

}

message_client_path_body::message_client_path_body(
	const message_client_path_body& a_message_client_path_body
) :
	message_client_path_body(
		a_message_client_path_body.m_client_path,
		a_message_client_path_body.m_register)
{

}

message_client_path_body& message_client_path_body::operator=(
	const message_client_path_body& a_message_client_path_body
)
{
	m_client_path = a_message_client_path_body.m_client_path;
	m_register = a_message_client_path_body.m_register;
	return *this;
}

message_header<message_types, affix_base::details::semantic_version_number> message_client_path_body::create_message_header(

) const
{
	return message_header(message_types::client_path, i_affix_services_version);
}
