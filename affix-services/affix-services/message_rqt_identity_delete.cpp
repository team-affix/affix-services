#include "message_rqt_identity_delete.h"

using namespace affix_services;
using namespace affix_base::data;
using namespace affix_services::networking;
using namespace affix_base::cryptography;

message_rqt_identity_delete::message_rqt_identity_delete(

)
{

}

message_rqt_identity_delete::message_rqt_identity_delete(
	const CryptoPP::RSA::PublicKey& a_public_key
) :
	m_public_key(a_public_key)
{

}

bool message_rqt_identity_delete::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	// Validate the public key first.
	CryptoPP::AutoSeededRandomPool l_random;
	if (!m_public_key.Validate(l_random, 3))
	{
		a_result = serialization_status_response_type::error_validating_public_key;
		return false;
	}

	// Export the public key data
	std::vector<uint8_t> l_public_key;
	rsa_export(m_public_key, l_public_key);

	// Pack the public key data
	if (!a_output.push_back(l_public_key))
	{
		a_result = serialization_status_response_type::error_packing_public_key_data;
		return false;
	}
	
	return true;

}

bool message_rqt_identity_delete::deserialize(
	affix_base::data::byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	// Pop public key data off of the byte buffer
	std::vector<uint8_t> l_public_key;
	if (!a_input.pop_front(l_public_key))
	{
		a_result = deserialization_status_response_type::error_unpacking_public_key_data;
		return false;
	}

	// Import the public key
	if (!rsa_try_import(m_public_key, l_public_key))
	{
		a_result = deserialization_status_response_type::error_importing_public_key;
		return false;
	}

	// Validate the public key
	CryptoPP::AutoSeededRandomPool l_random;
	if (!m_public_key.Validate(l_random, 3))
	{
		a_result = deserialization_status_response_type::error_validating_public_key;
		return false;
	}

	return true;

}
