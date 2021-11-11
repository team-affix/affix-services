#include "message_security_manager.h"
#include "affix-base/rsa.h"

using namespace affix_base::cryptography;
using affix_services::security::message_security_manager;
using affix_base::data::byte_buffer;

bool message_security_manager::try_export(const vector<uint8_t>& a_data, vector<uint8_t>& a_output) {
	
	byte_buffer result;

	if (!result.push_front(a_data)) return false;
	if (!try_pack_security_header(result)) return false;

	if (secured()) {
		a_output = rsa_encrypt_in_chunks(result.data(), m_outbound_public_key);
	}
	else {
		a_output = result.data();
	}

	return true;

}

bool message_security_manager::try_import(const vector<uint8_t>& a_data, vector<uint8_t>& a_output) {

	byte_buffer result;

	if (secured()) {
		result = byte_buffer(rsa_decrypt_in_chunks(a_data, m_inbound_private_key));
	}
	else {
		result = byte_buffer(a_data);
	}




}

bool message_security_manager::try_pack_security_header(byte_buffer& a_data) {
	if (!try_pack_token(a_data)) return false;
	if (!try_pack_signature(a_data)) return false;
	return true;
}

bool message_security_manager::try_unpack_security_header(byte_buffer& a_data, authenticated_message& a_authenticated_message) {
	if (!try_unpack_signature(a_data, a_authenticated_message)) return false;
	if (!try_unpack_token(a_data, a_authenticated_message)) return false;
	return true;
}

bool message_security_manager::try_pack_signature(byte_buffer& a_data) {
	vector<uint8_t> l_signature;
	if (!rsa_try_sign(a_data.data(), m_inbound_private_key, l_signature)) return false;
	if (!a_data.push_front(l_signature)) return false;
	return true;
}

bool message_security_manager::try_unpack_signature(byte_buffer& a_data, authenticated_message& a_authenticated_message) {
	vector<uint8_t> l_signature;
	if (!a_data.pop_front(l_signature)) return false;
	a_authenticated_message.m_signature = l_signature;
	return true;
}

bool message_security_manager::try_pack_token(byte_buffer& a_data) {
	if (!a_data.push_front(m_outbound_token.serialize())) return false;
	return true;
}

bool message_security_manager::try_unpack_token(byte_buffer& a_data, authenticated_message& a_authenticated_message) {
	vector<uint8_t> l_inbound_token;
	if (!a_data.pop_front(l_inbound_token)) return false;
	a_authenticated_message.m_token = l_inbound_token;
	return true;
}

bool message_security_manager::secured() const {

	AutoSeededRandomPool l_random;
	if (!m_outbound_public_key.Validate(l_random, 3)) return false;
	if (!m_inbound_private_key.Validate(l_random, 3)) return false;
	if (!m_outbound_token.initialized()) return false;
	if (!m_inbound_token.initialized()) return false;
	return true;

}
