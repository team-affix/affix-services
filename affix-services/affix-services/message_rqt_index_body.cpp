#include "message_rqt_index_body.h"

using namespace affix_services;

message_rqt_index_body::message_rqt_index_body(

)
{

}

message_rqt_index_body::message_rqt_index_body(
	const affix_base::data::tree<std::string>& a_identities
) :
	m_identities(a_identities)
{

}

message_header message_rqt_index_body::create_message_header(

) const
{
	return message_header(message_types::rqt_index, message_header::random_discourse_identifier());
}

bool message_rqt_index_body::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
) const
{
	if (!a_output.push_back(m_identities))
	{
		a_result = serialization_status_response_type::error_packing_identities;
		return false;
	}

	return true;
}

bool message_rqt_index_body::deserialize(
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
