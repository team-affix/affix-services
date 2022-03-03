#include "message_rsp_relay_body.h"

using namespace affix_services;

message_rsp_relay_body::message_rsp_relay_body(

) :
	affix_base::data::serializable(m_path, m_path_index, m_processing_status_response)
{

}

message_rsp_relay_body::message_rsp_relay_body(
	const std::vector<std::string>& a_path,
	const size_t& a_path_index,
	message_rqt_relay_body::processing_status_response_type a_processing_status_response
) :
	affix_base::data::serializable(m_path, m_path_index, m_processing_status_response),
	m_path(a_path),
	m_processing_status_response(a_processing_status_response),
	m_path_index(a_path_index)
{

}

message_rsp_relay_body::message_rsp_relay_body(
	const message_rsp_relay_body& a_message_rsp_relay_body
) :
	message_rsp_relay_body(a_message_rsp_relay_body.m_path, a_message_rsp_relay_body.m_path_index, a_message_rsp_relay_body.m_processing_status_response)
{

}

message_header message_rsp_relay_body::create_message_header(
	const message_header& a_request_message_header
) const
{
	return message_header(message_types::rsp_relay, a_request_message_header.m_discourse_identifier);
}
