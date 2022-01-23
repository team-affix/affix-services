#include "message_rsp_turn_create.h"

using namespace affix_base::data;
using namespace affix_services;

message_rsp_turn_create::message_rsp_turn_create(

)
{

}

message_rsp_turn_create::message_rsp_turn_create(
	const message_rqt_turn_create::deserialization_status_response_type& a_deserialization_status_response,
	const message_rqt_turn_create::processing_status_response_type& a_processing_status_response
) :
	m_deserialization_status_response(a_deserialization_status_response),
	m_processing_status_response(a_processing_status_response)
{

}

bool message_rsp_turn_create::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	// Push deserialization status response
	if (!a_output.push_back(m_deserialization_status_response))
	{
		a_result = serialization_status_response_type::error_unable_to_pack_deserialiation_status_response;
		return false;
	}

	// Push processing status response
	if (!a_output.push_back(m_processing_status_response))
	{
		a_result = serialization_status_response_type::error_unable_to_pack_processing_status_response;
		return false;
	}

	return false;

}

bool message_rsp_turn_create::deserialize(
	affix_base::data::byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	return false;
}
