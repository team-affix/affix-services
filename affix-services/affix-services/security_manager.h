#pragma once
#include "affix-base/pch.h"
#include "rolling_token.h"
#include "affix-base/rsa.h"
#include "affix-base/byte_buffer.h"
#include "message.h"

using namespace CryptoPP;
using affix_base::data::byte_buffer;
using affix_services::networking::message;

namespace affix_services {
	namespace security {
		class security_manager {
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
			bool export_message(const vector<uint8_t>& a_message_data, vector<uint8_t>& a_output);
			bool import_message(const vector<uint8_t>& a_message_data, message& a_output);

		protected:
			bool pack_signature(byte_buffer& a_data);
			bool unpack_signature(byte_buffer& a_data, message& a_output);
			bool pack_token(byte_buffer& a_data);
			bool unpack_token(byte_buffer& a_data, message& a_output);

		};
	}
}
