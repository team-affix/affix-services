#include "message_trace_path_body.h"

using namespace affix_services;

message_trace_path_body::message_trace_path_body(
	const std::vector<std::string>& a_path
) :
	affix_base::data::serializable(m_path),
	m_path(a_path)
{

}

message_trace_path_body::message_trace_path_body(
	const message_trace_path_body& a_message_rqt_index_body
) :
	message_trace_path_body(a_message_rqt_index_body.m_path)
{

}

message_header message_trace_path_body::create_message_header(

) const
{
	return message_header(message_types::trace_path, message_header::random_discourse_identifier());
}
