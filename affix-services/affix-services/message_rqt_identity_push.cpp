#include "message_rqt_identity_push.h"
#include "affix-base/rsa.h"
#include "transmission_result.h"

using namespace affix_services;
using namespace affix_base::cryptography;
using affix_services::networking::transmission_result;

message_rqt_identity_push::message_rqt_identity_push(

)
{

}

message_rqt_identity_push::message_rqt_identity_push(
	const CryptoPP::RSA::PublicKey& a_public_key
) :
	m_public_key(a_public_key)
{

}

bool message_rqt_identity_push::serialize(
	affix_base::data::byte_buffer& a_output,
	serialization_status_response_type& a_result
)
{
	// Validate public key
	CryptoPP::AutoSeededRandomPool l_random;
	if (!m_public_key.Validate(l_random, 3))
	{
		a_result = serialization_status_response_type::error_validating_public_key;
		return false;
	}

	// Serialize public key
	std::vector<uint8_t> l_serialized_public_key;
	rsa_export(m_public_key, l_serialized_public_key);

	// Push serialized public key onto byte buffer
	if (!a_output.push_back(l_serialized_public_key))
	{
		a_result = serialization_status_response_type::error_packing_public_key_data;
		return false;
	}

	return true;

}

bool message_rqt_identity_push::deserialize(
	affix_base::data::byte_buffer& a_input,
	deserialization_status_response_type& a_result
)
{
	// Unpack public key data
	std::vector<uint8_t> l_serialized_public_key;
	if (!a_input.pop_front(l_serialized_public_key))
	{
		a_result = deserialization_status_response_type::error_unpacking_public_key_data;
		return false;
	}

	// Import public key
	if (!rsa_try_import(m_public_key, l_serialized_public_key))
	{
		a_result = deserialization_status_response_type::error_importing_public_key;
		return false;
	}

	// Validate public key
	CryptoPP::AutoSeededRandomPool l_random;
	if (!m_public_key.Validate(l_random, 3))
	{
		a_result = deserialization_status_response_type::error_validating_public_key;
		return false;
	}

	return true;

}
