#include "message_header.h"
#include "cryptopp/osrng.h"
#include "affix-base/string_extensions.h"

using affix_services::message_header;
using affix_base::data::byte_buffer;
using affix_services::networking::transmission_result;

size_t message_header::s_discourse_identifier_size(25);

message_header::message_header(

) :
	affix_base::data::serializable(m_message_type, m_discourse_identifier, m_affix_services_version)
{

}

message_header::message_header(
	const message_types& a_message_type,
	const std::string& a_discourse_identifier,
	const affix_base::details::semantic_version_number& a_affix_services_version
) :
	affix_base::data::serializable(m_message_type, m_discourse_identifier, m_affix_services_version),
	m_message_type(a_message_type),
	m_discourse_identifier(a_discourse_identifier),
	m_affix_services_version(a_affix_services_version)
{

}

message_header::message_header(
	const message_header& a_message_header
) :
	message_header(a_message_header.m_message_type, a_message_header.m_discourse_identifier, a_message_header.m_affix_services_version)
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
