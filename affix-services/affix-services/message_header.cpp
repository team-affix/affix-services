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
	const uint32_t& a_discourse_id,
	const message_types& a_message_type,
	const transmission_result& a_transmission_result
)
{
	m_affix_services_version = details::i_affix_services_version;
	m_message_type = a_message_type;
	m_transmission_result = a_transmission_result;

}

bool message_header::serialize(
	affix_base::data::byte_buffer& a_output,
	affix_services::networking::transmission_result& a_result
)
{
	try
	{
		if (!a_output.push_back(m_affix_services_version))
		{
			a_result = transmission_result::error_serializing_data;
			return false;
		}

		if (!a_output.push_back(m_message_type))
		{
			a_result = transmission_result::error_serializing_data;
			return false;
		}

		if (!a_output.push_back(m_transmission_result))
		{
			a_result = transmission_result::error_serializing_data;
			return false;
		}

	}
	catch (std::exception& ex)
	{
		LOG("[ SERIALIZE ] Error: failed to serialize message_header: " << ex.what());
		a_result = transmission_result::error_serializing_data;
		return false;
	}

	a_result = transmission_result::success;

	return true;

}

bool message_header::deserialize(
	byte_buffer& a_input,
	message_header& a_output,
	affix_services::networking::transmission_result& a_result
)
{

	try
	{
		if (!a_input.pop_front(a_output.m_affix_services_version))
		{
			a_result = transmission_result::error_deserializing_data;
			return false;
		}

		if (a_output.m_affix_services_version.m_major != details::i_affix_services_version.m_major ||
			a_output.m_affix_services_version.m_minor != details::i_affix_services_version.m_minor) {
			a_result = transmission_result::error_version_mismatch;
			return false;
		}

		if (!a_input.pop_front(a_output.m_message_type))
		{
			a_result = transmission_result::error_deserializing_data;
			return false;
		}

		if (!a_input.pop_front(a_output.m_transmission_result))
		{
			a_result = transmission_result::error_deserializing_data;
			return false;
		}

	}
	catch (std::exception& ex)
	{
		LOG("[ DESERIALIZE ] Error: failed to deserialize message_header: " << ex.what());
		a_result = transmission_result::error_deserializing_data;
		return false;
	}

	a_result = transmission_result::success;

	return false;

}
