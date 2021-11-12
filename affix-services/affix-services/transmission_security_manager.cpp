#include "transmission_security_manager.h"
#include "affix-base/rsa.h"

using namespace affix_base::cryptography;
using affix_services::security::transmission_security_manager;
using affix_base::data::byte_buffer;

bool transmission_security_manager::export_transmission(const vector<uint8_t>& a_message_data, vector<uint8_t>& a_output) {
	
	byte_buffer l_byte_buffer;

	if (!l_byte_buffer.push_front(a_message_data)) return false;

	if (m_outbound_authenticated) {
		if (!pack_token(l_byte_buffer)) return false;
		if (!pack_signature(l_byte_buffer)) return false;
		m_outbound_token++;
	}

	if (m_outbound_confidential) {
		a_output = rsa_encrypt_in_chunks(l_byte_buffer.data(), m_outbound_public_key);
	}
	else {
		a_output = l_byte_buffer.data();
	}

	return true;

}

bool transmission_security_manager::import_transmission(const vector<uint8_t>& a_data, transmission& a_output) {

	byte_buffer l_byte_buffer;

	if (m_inbound_confidential) {
		l_byte_buffer = byte_buffer(rsa_decrypt_in_chunks(a_data, m_inbound_private_key));
	}
	else {
		l_byte_buffer = byte_buffer(a_data);
	}

	a_output.m_full_data = l_byte_buffer.data();

	if (m_inbound_authenticated) {
		if (!unpack_signature(l_byte_buffer, a_output)) return false;
		if (!unpack_token(l_byte_buffer, a_output)) return false;
		if (!a_output.authentic(m_outbound_public_key, m_inbound_token.serialize())) return false;
		m_inbound_token++;
	}

	a_output.m_message_data = l_byte_buffer.data();

	return true;

}

bool transmission_security_manager::pack_signature(byte_buffer& a_data) {
	vector<uint8_t> l_signature;
	if (!rsa_try_sign(a_data.data(), m_inbound_private_key, l_signature)) return false;
	if (!a_data.push_front(l_signature)) return false;
	return true;
}

bool transmission_security_manager::unpack_signature(byte_buffer& a_data, transmission& a_output) {
	vector<uint8_t> l_signature;
	if (!a_data.pop_front(l_signature)) return false;

	// POPULATE FIELDS IN MESSAGE
	a_output.m_signature = l_signature;
	a_output.m_signed_data = a_data.data();

	return true;
}

bool transmission_security_manager::pack_token(byte_buffer& a_data) {
	if (!a_data.push_front(m_outbound_token.serialize())) return false;
	return true;
}

bool transmission_security_manager::unpack_token(byte_buffer& a_data, transmission& a_output) {

	vector<uint8_t> l_inbound_token;
	if (!a_data.pop_front(l_inbound_token)) return false;

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
