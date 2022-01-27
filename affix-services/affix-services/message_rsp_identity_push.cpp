#include "message_rsp_identity_push.h"

using namespace affix_services;
using affix_services::networking::transmission_result;

message_rsp_identity_push::message_rsp_identity_push(

)
{

}

message_rsp_identity_push::message_rsp_identity_push(
	const message_rqt_identity_push::deserialization_status_response_type& a_deserialization_status_response,
	const message_rqt_identity_push::processing_status_response_type& a_processing_status_response
) :
	m_deserialization_status_response(a_deserialization_status_response),
	m_processing_status_response(a_processing_status_response)
{

}

bool message_rsp_identity_push::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	// Push the deserialization status response onto the byte buffer.
	if (!a_output.push_back(m_deserialization_status_response))
	{
		a_result = serialization_status_response_type::error_packing_deserialization_status_response;
		return false;
	}

	// Push the processing status response onto the byte buffer.
	if (!a_output.push_back(m_processing_status_response))
	{
		a_result = serialization_status_response_type::error_packing_processing_status_response;
		return false;
	}

	return true;

}

bool message_rsp_identity_push::deserialize(
	affix_base::data::byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	// Pop the deserialization status response from the byte buffer
	if (!a_input.pop_front(m_deserialization_status_response))
	{
		a_result = deserialization_status_response_type::error_unpacking_deserialization_status_response;
		return false;
	}

	// Pop the processing status response from the byte buffer
	if (!a_input.pop_front(m_processing_status_response))
	{
		a_result = deserialization_status_response_type::error_unpacking_processing_status_response;
		return false;
	}

	return true;

}
