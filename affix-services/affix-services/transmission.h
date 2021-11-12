#pragma once
#include "affix-base/pch.h"
#include "affix-base/rsa.h"
#include "rolling_token.h"

using std::vector;
using affix_services::security::rolling_token;
using CryptoPP::RSA;

namespace affix_services {
	namespace networking {
		struct transmission {
			vector<uint8_t> m_full_data;
			vector<uint8_t> m_signature;
			vector<uint8_t> m_signed_data;
			vector<uint8_t> m_token;
			vector<uint8_t> m_message_data;
			bool authentic(const RSA::PublicKey& a_outbound_public_key, const vector<uint8_t>& a_inbound_token) const;
		};
	}
}
