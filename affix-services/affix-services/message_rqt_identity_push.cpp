#include "message_rqt_identity_push.h"
#include "affix-base/rsa.h"
#include "transmission_result.h"

using namespace affix_services::messaging;
using namespace affix_base::cryptography;
using affix_services::networking::transmission_result;

rqt_identity_push::rqt_identity_push(

)
{

}

rqt_identity_push::rqt_identity_push(
	const CryptoPP::RSA::PublicKey& a_public_key
) :
	m_public_key(a_public_key)
{

}

bool rqt_identity_push::serialize(
	affix_base::data::byte_buffer& a_output,
	affix_services::networking::transmission_result& a_result
)
{
	// Serialize public key
	std::vector<uint8_t> l_serialized_public_key;
	rsa_export(m_public_key, l_serialized_public_key);

	// Push serialized public key onto byte buffer
	if (!a_output.push_back(l_serialized_public_key))
	{
		a_result = transmission_result::error_serializing_data;
		return false;
	}

	a_result = transmission_result::success;

	return true;

}

bool rqt_identity_push::deserialize(
	affix_base::data::byte_buffer& a_input,
	affix_services::networking::transmission_result& a_result
)
{
	std::vector<uint8_t> l_serialized_public_key;
	if (!a_input.pop_front(l_serialized_public_key))
	{
		a_result = transmission_result::error_deserializing_data;
		return false;
	}
	if (!rsa_try_import(m_public_key, l_serialized_public_key))
	{
		a_result = transmission_result::error_deserializing_data;
		return false;
	}

	a_result = transmission_result::success;

	return true;

}
