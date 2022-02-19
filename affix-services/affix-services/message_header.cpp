#include "message_header.h"

using affix_services::messaging::message_header;
using namespace affix_services::messaging;
using affix_base::data::byte_buffer;
using affix_services::networking::transmission_result;

message_header::message_header(

)
{

}

message_header::message_header(
	const message_types& a_message_type
) :
	m_affix_services_version(details::i_affix_services_version),
	m_message_type(a_message_type)
{

}

bool message_header::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	if (!a_output.push_back(m_affix_services_version))
	{
		a_result = serialization_status_response_type::error_packing_affix_services_version;
		return false;
	}

	if (!a_output.push_back(m_message_type))
	{
		a_result = serialization_status_response_type::error_packing_message_type;
		return false;
	}

	return true;

}

bool message_header::deserialize(
	byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	if (!a_input.pop_front(m_affix_services_version))
	{
		a_result = deserialization_status_response_type::error_unpacking_affix_services_version;
		return false;
	}

	if (m_affix_services_version.m_major != details::i_affix_services_version.m_major ||
		m_affix_services_version.m_minor != details::i_affix_services_version.m_minor) {
		a_result = deserialization_status_response_type::error_affix_services_version_mismatch;
		return false;
	}

	if (!a_input.pop_front(m_message_type))
	{
		a_result = deserialization_status_response_type::error_unpacking_message_type;
		return false;
	}

	return true;

}
