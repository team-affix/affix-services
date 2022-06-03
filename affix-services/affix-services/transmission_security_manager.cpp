#include "transmission_security_manager.h"
#include "affix-base/rsa.h"
#include "rolling_token.h"
#include "affix-base/aes.h"
#include "affix-base/sha.h"

using namespace affix_base::cryptography;
using affix_services::security::transmission_security_manager;
using affix_base::data::byte_buffer;
using affix_services::networking::transmission_result;
using std::vector;
using affix_services::security::rolling_token;

transmission_security_manager::transmission_security_manager(
	affix_base::data::ptr<security_information> a_security_information
) :
	m_security_information(a_security_information)
{

}

bool transmission_security_manager::export_transmission(
	const std::vector<uint8_t>& a_message_data,
	vector<uint8_t>& a_output,
	affix_services::networking::transmission_result& a_result
)
{
	byte_buffer l_byte_buffer;

	// PUSH TOKEN AND SIGNATURE INTO BUFFER
	if (!pack_token(l_byte_buffer, a_result))
		return false;

	// PUSH RAW MESSAGE DATA INTO BUFFER
	if (!l_byte_buffer.push_back(a_message_data)) {
		a_result = transmission_result::error_serializing_data;
		return false;
	}

	// ENCRYPT BUFFER
	a_output = affix_base::cryptography::aes_encrypt(l_byte_buffer.data(), local_aes_key());

	// Increment the local token (necessary for next send request to be valid)
	m_security_information->m_local_token.increment();

	return true;

}

bool transmission_security_manager::import_transmission(
	const vector<uint8_t>& a_data,
	std::vector<uint8_t>& a_output,
	transmission_result& a_result
)
{

	// DECRYPT DATA
	vector<uint8_t> l_decrypted_data = affix_base::cryptography::aes_decrypt(a_data, remote_aes_key());

	// INITIALIZE BYTE BUFFER WITH DECRYPTED DATA
	byte_buffer l_byte_buffer = byte_buffer(l_decrypted_data);

	// VERIFY AUTHENTICITY OF DATA
	if (!unpack_token(l_byte_buffer, a_output, a_result))
		return false;

	// INCREMENT REMOTE TOKEN ONLY IF AUTHENTIC MESSAGE
	m_security_information->m_remote_token.increment();

	// STORE DATA IN OUTPUT VECTOR
	if (!l_byte_buffer.pop_front(a_output))
	{
		a_result = transmission_result::error_deserializing_data;
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
	if (!a_data.push_back(m_security_information->m_local_token.serialize())) {
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
	vector<uint8_t> l_expected_remote_token = m_security_information->m_remote_token.serialize();

	vector<uint8_t> l_remote_token;

	// TRY TO POP TOKEN OFF BUFFER
	if (!a_data.pop_front(l_remote_token)) {
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

std::vector<uint8_t> transmission_security_manager::remote_aes_key(

) const
{
	// Get a truncated sha256 hash of the remote token, to use as an encryption key
	std::vector<uint8_t> l_remote_token_hash;

	// Generate the sha256 digest
	affix_base::data::sha256_digest(
		m_security_information->m_remote_token.serialize(),
		l_remote_token_hash,
		CryptoPP::AES::BLOCKSIZE
	);

	return l_remote_token_hash;

}

std::vector<uint8_t> transmission_security_manager::local_aes_key(

) const
{
	// Get a truncated sha256 hash of the local token, to use as a decryption key
	std::vector<uint8_t> l_local_token_hash;

	// Generate the sha256 digest
	affix_base::data::sha256_digest(
		m_security_information->m_local_token.serialize(),
		l_local_token_hash,
		CryptoPP::AES::BLOCKSIZE
	);

	return l_local_token_hash;

}
