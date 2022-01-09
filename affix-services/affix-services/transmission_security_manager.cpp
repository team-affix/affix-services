#include "transmission_security_manager.h"
#include "affix-base/rsa.h"
#include "rolling_token.h"

using namespace affix_base::cryptography;
using affix_services::security::transmission_security_manager;
using affix_base::data::byte_buffer;
using affix_services::networking::transmission_result;
using std::vector;
using affix_services::security::rolling_token;

transmission_security_manager::transmission_security_manager(
	const CryptoPP::RSA::PrivateKey& a_local_private_key,
	const affix_services::security::rolling_token& a_local_token,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const affix_services::security::rolling_token& a_remote_token
) :
	m_local_private_key(a_local_private_key),
	m_local_token(a_local_token),
	m_remote_public_key(a_remote_public_key),
	m_remote_token(a_remote_token)
{

}

bool transmission_security_manager::export_transmission(
	const std::vector<uint8_t>& a_message_data,
	vector<uint8_t>& a_output,
	affix_services::networking::transmission_result& a_result
)
{
	
	byte_buffer l_byte_buffer;

	// PUSH RAW MESSAGE DATA INTO BUFFER
	if (!l_byte_buffer.push_front(a_message_data)) {
		a_result = transmission_result::error_serializing_data;
		return false;
	}

	// PUSH TOKEN AND SIGNATURE INTO BUFFER
	if (!pack_token(l_byte_buffer, a_result)) return false;
	if (!pack_signature(l_byte_buffer, a_result)) return false;
	m_local_token++;

	// ENCRYPT BUFFER
	if (!rsa_try_encrypt_in_chunks(l_byte_buffer.data(), m_remote_public_key, a_output)) {
		a_result = transmission_result::error_encrypting_data;
		return false;
	}

	return true;

}

bool transmission_security_manager::import_transmission(
	const vector<uint8_t>& a_data,
	std::vector<uint8_t>& a_output,
	transmission_result& a_result
)
{

	// DECRYPT DATA
	vector<uint8_t> l_decrypted_data;
	if (!rsa_try_decrypt_in_chunks(a_data, m_local_private_key, l_decrypted_data)) {
		a_result = transmission_result::error_decrypting_data;
		return false;
	}

	// INITIALIZE BYTE BUFFER WITH DECRYPTED DATA
	byte_buffer l_byte_buffer = byte_buffer(l_decrypted_data);

	// VERIFY AUTHENTICITY OF DATA
	if (!unpack_signature(l_byte_buffer, a_output, a_result))
		return false;
	if (!unpack_token(l_byte_buffer, a_output, a_result))
		return false;

	// INCREMENT REMOTE TOKEN ONLY IF AUTHENTIC MESSAGE
	m_remote_token++;

	// STORE DATA IN OUTPUT VECTOR
	a_output = l_byte_buffer.data();

	return true;

}

bool transmission_security_manager::pack_signature(
	byte_buffer& a_data,
	transmission_result& a_result
)
{
	vector<uint8_t> l_signature;

	// TRY TO SIGN DATA
	if (!rsa_try_sign(a_data.data(), m_local_private_key, l_signature)) {
		a_result = transmission_result::error_signing_data;
		return false;
	}

	// TRY TO PUSH DATA TO BUFFER
	if (!a_data.push_back(l_signature)) {
		a_result = transmission_result::error_packing_signature;
		return false;
	}

	return true;

}

bool transmission_security_manager::unpack_signature(
	byte_buffer& a_data,
	std::vector<uint8_t>& a_output,
	transmission_result& a_result
)
{
	vector<uint8_t> l_signature;

	// TRY TO POP SIGNATURE FROM BUFFER
	if (!a_data.pop_back(l_signature)) {
		a_result = transmission_result::error_unpacking_signature;
		return false;
	}

	// POPULATE FIELDS IN MESSAGE
	std::vector<uint8_t> l_signed_data = a_data.data();

	// TRY TO VERIFY SIGNATURE ASSOCIATED WITH MESSAGE
	bool l_signature_valid = false;
	if (!rsa_try_verify(l_signed_data, l_signed_data, m_remote_public_key, l_signature_valid))
	{
		a_result = transmission_result::error_unpacking_signature;
		return false;
	}

	// CHECK IF SIGNATURE IS VALID
	if (!l_signature_valid)
	{
		a_result = transmission_result::error_authenticating_data;
		return false;
	}

	return true;
}

bool transmission_security_manager::pack_token(
	byte_buffer& a_data,
	transmission_result& a_result
)
{

	// TRY TO PUSH TOKEN TO BUFFER
	if (!a_data.push_back(m_local_token.serialize())) { 
		a_result = transmission_result::error_packing_token;
		return false; 
	}

	return true;
}

bool transmission_security_manager::unpack_token(
	byte_buffer& a_data,
	std::vector<uint8_t>& a_output,
	transmission_result& a_result
)
{
	// GET EXPECTED REMOTE TOKEN
	vector<uint8_t> l_expected_remote_token = m_remote_token.serialize();

	vector<uint8_t> l_remote_token;

	// TRY TO POP TOKEN OFF BUFFER
	if (!a_data.pop_back(l_remote_token)) {
		a_result = transmission_result::error_unpacking_token;
		return false;
	}

	if (!std::equal(l_remote_token.begin(), l_remote_token.end(), l_expected_remote_token.begin(), l_expected_remote_token.end()))
	{
		a_result = transmission_result::error_authenticating_data;
		return false;
	}

	return true;

}
