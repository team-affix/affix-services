#include "message_rsp_index.h"

using affix_services::message_rsp_index;

bool message_rsp_index::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	if (!a_output.push_back(m_identities))
	{
		a_result = serialization_status_response_type::error_packing_identities;
		return false;
	}

	return true;
}

bool message_rsp_index::deserialize(
	affix_base::data::byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	if (!a_input.pop_front(m_identities))
	{
		a_result = deserialization_status_response_type::error_unpacking_identities;
		return false;
	}

	return true;
}
