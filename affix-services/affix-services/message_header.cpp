#include "message_header.h"
#include "cryptopp/osrng.h"
#include "affix-base/string_extensions.h"

using affix_services::messaging::message_header;
using namespace affix_services::messaging;
using affix_base::data::byte_buffer;
using affix_services::networking::transmission_result;

size_t message_header::s_discourse_identifier_size(25);

message_header::message_header(

)
{

}

message_header::message_header(
	const message_types& a_message_type,
	const std::string& a_discourse_identifier
) :
	m_affix_services_version(details::i_affix_services_version),
	m_message_type(a_message_type),
	m_discourse_identifier(a_discourse_identifier)
{

}

std::string message_header::random_discourse_identifier(

)
{
	std::string l_result;
	CryptoPP::AutoSeededRandomPool l_random;
	l_result.resize(s_discourse_identifier_size);
	l_random.GenerateBlock((CryptoPP::byte*)l_result.data(), l_result.size());
	return l_result;
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

	if (!a_output.push_back(m_discourse_identifier))
	{
		a_result = serialization_status_response_type::error_packing_discourse_identifier;
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

	if (!a_input.pop_front(m_discourse_identifier))
	{
		a_result = deserialization_status_response_type::error_unpacking_discourse_identifier;
		return false;
	}

	return true;

}
