#include "message_rqt_turn_create.h"
#include "affix-base/byte_buffer.h"

using namespace affix_services;
using namespace affix_base::data;

message_rqt_turn_create::message_rqt_turn_create(

)
{

}

message_rqt_turn_create::message_rqt_turn_create(
	const std::string& a_turn_name
)
{

}

bool message_rqt_turn_create::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	if (!a_output.push_back(m_turn_name))
	{
		a_result = serialization_status_response_type::error_packing_turn_name;
		return false;
	}

	return true;

}

bool message_rqt_turn_create::deserialize(
	affix_base::data::byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	if (!a_input.pop_front(m_turn_name))
	{
		a_result = deserialization_status_response_type::error_unpacking_turn_name;
		return false;
	}

	return true;

}
