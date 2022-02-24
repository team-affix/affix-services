#include "message_rsp_relay_body.h"

using namespace affix_services;

message_rsp_relay_body::message_rsp_relay_body(

)
{

}

message_rsp_relay_body::message_rsp_relay_body(
	message_rqt_relay_body::processing_status_response_type a_processing_status_response
) :
	m_processing_status_response(a_processing_status_response)
{

}

message_header message_rsp_relay_body::create_message_header(
	const message_header& a_request_message_header
) const
{
	return message_header(message_types::rsp_relay, a_request_message_header.m_discourse_identifier);
}

bool message_rsp_relay_body::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
) const
{
	// Try to push the processing status response onto the byte buffer
	if (!a_output.push_back(m_processing_status_response))
	{
		a_result = serialization_status_response_type::error_packing_processing_status_response;
		return false;
	}

	return true;

}

bool message_rsp_relay_body::deserialize(
	affix_base::data::byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	// Try to pop the processing status response from the byte buffer
	if (!a_input.pop_front(m_processing_status_response))
	{
		a_result = deserialization_status_response_type::error_unpacking_processing_status_response;
		return false;
	}

	return true;

}
