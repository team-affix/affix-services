#include "transmission_security_manager.h"
#include "affix-base/rsa.h"

using namespace affix_base::cryptography;
using affix_services::security::transmission_security_manager;
using affix_base::data::byte_buffer;

bool transmission_security_manager::export_transmission(const vector<uint8_t>& a_message_data, vector<uint8_t>& a_output, transmission_result& a_result) {
	
	byte_buffer l_byte_buffer;

	// PUSH RAW MESSAGE DATA INTO BUFFER
	if (!l_byte_buffer.push_front(a_message_data)) {
		a_result = transmission_result::error_packing_message_data;
		return false;
	}

	// PUSH TOKEN AND SIGNATURE INTO BUFFER
	if (m_outbound_authenticated) {
		if (pack_token(l_byte_buffer, a_result)) return false;
		if (pack_signature(l_byte_buffer, a_result)) return false;
		m_outbound_token++;
	}

	// ENCRYPT BUFFER IF OUTBOUND IS CONFIDENTIAL
	if (m_outbound_confidential) {

		if (!rsa_try_encrypt_in_chunks(l_byte_buffer.data(), m_outbound_public_key, a_output)) {
			a_result = transmission_result::error_encrypting_data;
			return false;
		}

	}
	else {
		a_output = l_byte_buffer.data();
	}

	return true;

}

bool transmission_security_manager::import_transmission(const vector<uint8_t>& a_data, transmission& a_output, transmission_result& a_result) {

	byte_buffer l_byte_buffer;

	// DECRYPT DATA IF INBOUND IS CONFIDENTIAL
	if (m_inbound_confidential) {

		// TRY TO DECRYPT DATA
		vector<uint8_t> l_decrypted_data;
		if (!rsa_try_decrypt_in_chunks(a_data, m_inbound_private_key, l_decrypted_data)) {
			a_result = transmission_result::error_decrypting_data;
			return false;
		}

		// INITIALIZE BYTE BUFFER WITH DECRYPTED DATA
		l_byte_buffer = byte_buffer(l_decrypted_data);

	}
	else {
		l_byte_buffer = byte_buffer(a_data);
	}

	a_output.m_full_data = l_byte_buffer.data();

	// VERIFY AUTHENTICITY OF DATA IF INBOUND MUST BE AUTHENTICATED
	if (m_inbound_authenticated) {

		if (!unpack_signature(l_byte_buffer, a_output, a_result)) return false;
		if (!unpack_token(l_byte_buffer, a_output, a_result)) return false;

		// IF DATA IS INAUTHENTIC, RETURN FALSE
		if (!a_output.authentic(m_outbound_public_key, m_inbound_token.serialize())) {
			a_result = transmission_result::error_authenticating_data;
			return false;
		}

		m_inbound_token++;
	}

	a_output.m_message_data = l_byte_buffer.data();

	return true;

}

bool transmission_security_manager::pack_signature(byte_buffer& a_data, transmission_result& a_result) {
	
	vector<uint8_t> l_signature;

	// TRY TO SIGN DATA
	if (!rsa_try_sign(a_data.data(), m_inbound_private_key, l_signature)) {
		a_result = transmission_result::error_signing_data;
		return false;
	}

	// TRY TO PUSH DATA TO BUFFER
	if (!a_data.push_front(l_signature)) {
		a_result = transmission_result::error_packing_signature;
		return false;
	}

	return true;

}

bool transmission_security_manager::unpack_signature(byte_buffer& a_data, transmission& a_output, transmission_result& a_result) {

	vector<uint8_t> l_signature;

	// TRY TO POP SIGNATURE FROM BUFFER
	if (!a_data.pop_front(l_signature)) {
		a_result = transmission_result::error_unpacking_signature;
		return false;
	}

	// POPULATE FIELDS IN MESSAGE
	a_output.m_signature = l_signature;
	a_output.m_signed_data = a_data.data();

	return true;
}

bool transmission_security_manager::pack_token(byte_buffer& a_data, transmission_result& a_result) {

	// TRY TO PUSH TOKEN TO BUFFER
	if (!a_data.push_front(m_outbound_token.serialize())) { 
		a_result = transmission_result::error_packing_token;
		return false; 
	}

	return true;
}

bool transmission_security_manager::unpack_token(byte_buffer& a_data, transmission& a_output, transmission_result& a_result) {

	vector<uint8_t> l_inbound_token;

	// TRY TO POP TOKEN OFF BUFFER
	if (!a_data.pop_front(l_inbound_token)) {
		a_result = transmission_result::error_unpacking_token;
		return false;
	}

	// POPULATE FIELDS IN MESSAGE
	a_output.m_token = l_inbound_token;

	return true;

}

bool transmission_security_manager::secured() const {
	return
		m_outbound_confidential &&
		m_outbound_authenticated &&
		m_inbound_confidential &&
		m_inbound_authenticated;
}
