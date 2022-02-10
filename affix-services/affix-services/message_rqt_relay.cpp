#include "message_rqt_relay.h"

using namespace affix_services;
using namespace affix_base::cryptography;

message_rqt_relay::message_rqt_relay(

)
{

}

message_rqt_relay::message_rqt_relay(
	const std::vector<CryptoPP::RSA::PublicKey>& a_path,
	const affix_base::data::ptr<std::vector<uint8_t>>& a_payload
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
	// Create a vector of exported representations of the RSA keys
	std::vector<std::vector<uint8_t>> l_path(m_path.size());

	for (int i = 0; i < m_path.size(); i++)
	{
		// Export identity and push it into the vector of exported identities
		std::vector<uint8_t> l_exported_identity;
		rsa_export(m_path[i], l_exported_identity);
		l_path[i] = l_exported_identity;
	}

	// Push the vector of exported identities onto the byte buffer
	if (!a_output.push_back(l_path))
	{
		a_result = serialization_status_response_type::error_packing_path;
		return false;
	}
	
	// Push the payload onto the byte buffer
	if (!a_output.push_back(*m_payload))
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
	std::vector<std::vector<uint8_t>> l_path;

	// Pop the exported identities from the byte buffer.
	if (!a_input.pop_front(l_path))
	{
		a_result = deserialization_status_response_type::error_unpacking_path;
		return false;
	}

	// Resize the identity path vector to the correct size
	m_path.resize(l_path.size());

	// Try to import all public keys associated with the path
	for (int i = 0; i < l_path.size(); i++)
	{
		try
		{
			rsa_import(m_path[i], l_path[i]);
		}
		catch (...)
		{
			a_result = deserialization_status_response_type::error_importing_identity;
			return false;
		}
	}

	// Instantiate new empty payload so it can be populated
	m_payload = new std::vector<uint8_t>();

	// Pop payload from byte buffer
	if (!a_input.pop_front(*m_payload))
	{
		a_result = deserialization_status_response_type::error_unpacking_payload;
		return false;
	}

	return true;

}
