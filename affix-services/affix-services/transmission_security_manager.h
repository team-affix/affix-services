#pragma once
#include "affix-base/pch.h"
#include "rolling_token.h"
#include "affix-base/rsa.h"
#include "affix-base/byte_buffer.h"
#include "transmission.h"
#include "transmission_result.h"

using namespace CryptoPP;
using affix_base::data::byte_buffer;
using affix_services::networking::transmission;
using affix_services::networking::transmission_result;

namespace affix_services {
	namespace security {
		class transmission_security_manager {
		public:
			RSA::PublicKey m_outbound_public_key;
			RSA::PrivateKey m_inbound_private_key;
			rolling_token m_outbound_token;
			rolling_token m_inbound_token;

		public:
			bool m_outbound_confidential;
			bool m_inbound_confidential;
			bool m_outbound_authenticated;
			bool m_inbound_authenticated;

		public:
			bool export_transmission(const vector<uint8_t>& a_message_data, vector<uint8_t>& a_output, transmission_result& a_result);
			bool import_transmission(const vector<uint8_t>& a_message_data, transmission& a_output, transmission_result& a_result);

		protected:
			bool pack_signature(byte_buffer& a_data, transmission_result& a_result);
			bool unpack_signature(byte_buffer& a_data, transmission& a_output, transmission_result& a_result);
			bool pack_token(byte_buffer& a_data, transmission_result& a_result);
			bool unpack_token(byte_buffer& a_data, transmission& a_output, transmission_result& a_result);

		public:
			bool secured() const;

		};
	}
}
