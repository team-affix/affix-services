#include "message_rsp_identity_delete.h"

using namespace affix_services;
using namespace affix_base::data;
using namespace affix_services::networking;

message_rsp_identity_delete::message_rsp_identity_delete(

)
{

}

message_rsp_identity_delete::message_rsp_identity_delete(
	const message_rqt_identity_delete::deserialization_status_response_type& a_deserialization_status_response,
	const message_rqt_identity_delete::processing_status_response_type& a_processing_status_response
) :
	m_deserialization_status_response(a_deserialization_status_response),
	m_processing_status_response(a_processing_status_response)
{

}

bool message_rsp_identity_delete::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	// Pack deserialization status response data
	if (!a_output.push_back(m_deserialization_status_response))
	{
		a_result = serialization_status_response_type::error_packing_deserialization_status_response;
		return false;
	}

	// Pack processing status response data
	if (!a_output.push_back(m_processing_status_response))
	{
		a_result = serialization_status_response_type::error_packing_processing_status_response;
		return false;
	}

	return true;

}

bool message_rsp_identity_delete::deserialize(
	affix_base::data::byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	// Unpack deserialization status response data
	if (!a_input.pop_front(m_deserialization_status_response))
	{
		a_result = deserialization_status_response_type::error_unpacking_deserialization_status_response;
		return false;
	}

	// Unpack processing status response data
	if (!a_input.pop_front(m_processing_status_response))
	{
		a_result = deserialization_status_response_type::error_unpacking_processing_status_response;
		return false;
	}
	
	return true;

}
