#pragma once
#include "affix-base/pch.h"
#include "rolling_token.h"
#include "affix-base/rsa.h"
#include "affix-base/byte_buffer.h"
#include "authenticated_message.h"

using namespace CryptoPP;
using affix_base::data::byte_buffer;

namespace affix_services {
	namespace security {
		class message_security_manager {
		public:
			RSA::PublicKey m_outbound_public_key;
			RSA::PrivateKey m_inbound_private_key;
			rolling_token m_outbound_token;
			rolling_token m_inbound_token;

		public:
			bool try_export(const vector<uint8_t>& a_data, vector<uint8_t>& a_output);
			bool try_import(const vector<uint8_t>& a_data, vector<uint8_t>& a_output);

		protected:
			bool try_pack_security_header(byte_buffer& a_data);
			bool try_unpack_security_header(byte_buffer& a_data, authenticated_message& a_authenticated_message);
			bool try_pack_signature(byte_buffer& a_data);
			bool try_unpack_signature(byte_buffer& a_data, authenticated_message& a_authenticated_message);
			bool try_pack_token(byte_buffer& a_data);
			bool try_unpack_token(byte_buffer& a_data, authenticated_message& a_authenticated_message);

		protected:
			bool secured() const;

		};
	}
}
