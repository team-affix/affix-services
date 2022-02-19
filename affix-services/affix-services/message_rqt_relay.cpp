#include "message_rqt_relay.h"

using namespace affix_services;
using namespace affix_base::cryptography;

affix_services::messaging::message_types message_rqt_relay::s_message_type(
	affix_services::messaging::message_types::rqt_relay
);

message_rqt_relay::message_rqt_relay(

)
{

}

message_rqt_relay::message_rqt_relay(
	const std::vector<std::string>& a_path,
	const std::vector<uint8_t>& a_payload
) :
	m_path(a_path),
	m_payload(a_payload)
{

}

bool message_rqt_relay::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	// Push the vector of exported identities onto the byte buffer
	if (!a_output.push_back(m_path))
	{
		a_result = serialization_status_response_type::error_packing_path;
		return false;
	}
	
	// Push the payload onto the byte buffer
	if (!a_output.push_back(m_payload))
	{
		a_result = serialization_status_response_type::error_packing_payload;
		return false;
	}

	return true;

}

bool message_rqt_relay::deserialize(
	affix_base::data::byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	// Pop the exported identities from the byte buffer.
	if (!a_input.pop_front(m_path))
	{
		a_result = deserialization_status_response_type::error_unpacking_path;
		return false;
	}

	// Pop payload from byte buffer
	if (!a_input.pop_front(m_payload))
	{
		a_result = deserialization_status_response_type::error_unpacking_payload;
		return false;
	}

	return true;

}
