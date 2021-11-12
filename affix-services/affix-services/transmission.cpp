#include "transmission.h"

using affix_services::networking::transmission;
using namespace affix_base::cryptography;

bool transmission::authentic(const RSA::PublicKey& a_outbound_public_key, const vector<uint8_t>& a_inbound_token) const {
	bool l_token_valid =
		std::equal(a_inbound_token.begin(), a_inbound_token.end(),
		m_token.begin(), m_token.end());
	bool l_signature_valid = false;
	if (!rsa_try_verify(m_signed_data, m_signature, a_outbound_public_key, l_signature_valid)) return false;
	if (!l_token_valid || !l_signature_valid) return false;
	return true;
}
