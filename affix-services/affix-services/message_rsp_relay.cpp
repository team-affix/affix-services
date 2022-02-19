#include "message_rsp_relay.h"

using namespace affix_services;

affix_services::messaging::message_types message_rsp_relay::s_message_type(
	affix_services::messaging::message_types::rsp_relay
);

message_rsp_relay::message_rsp_relay(

)
{

}

message_rsp_relay::message_rsp_relay(
	message_rqt_relay::processing_status_response_type a_processing_status_response
) :
	m_processing_status_response(a_processing_status_response)
{

}

bool message_rsp_relay::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	// Try to push the processing status response onto the byte buffer
	if (!a_output.push_back(m_processing_status_response))
	{
		a_result = serialization_status_response_type::error_packing_processing_status_response;
		return false;
	}

	return true;

}

bool message_rsp_relay::deserialize(
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
