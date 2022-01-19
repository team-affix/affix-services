#include "message_rsp_identity_push.h"

using namespace affix_services::messaging;
using affix_services::networking::transmission_result;

rsp_identity_push::rsp_identity_push(

)
{

}

rsp_identity_push::rsp_identity_push(
	const identity_push_status_response_type& a_status_response
) :
	m_status_response(a_status_response)
{

}

bool rsp_identity_push::serialize(
	affix_base::data::byte_buffer& a_output,
	affix_services::networking::transmission_result& a_result
)
{
	// Push the status response onto the byte buffer.
	if (!a_output.push_back(m_status_response))
	{
		a_result = transmission_result::error_serializing_data;
		return false;
	}

	a_result = transmission_result::success;

	return true;

}

bool rsp_identity_push::deserialize(
	affix_base::data::byte_buffer& a_input,
	affix_services::networking::transmission_result& a_result
)
{
	// Pop the status response from the byte buffer
	if (!a_input.pop_front(m_status_response))
	{
		a_result = transmission_result::error_deserializing_data;
		return false;
	}

	a_result = transmission_result::success;

	return true;

}
