#pragma once
#include "affix-base/pch.h"
#include "rolling_token.h"
#include "affix-base/rsa.h"
#include "affix-base/byte_buffer.h"
#include "transmission_result.h"

namespace affix_services {
	namespace security {
		class transmission_security_manager {
		public:
			CryptoPP::RSA::PrivateKey m_local_private_key;
			affix_services::security::rolling_token m_local_token;

		public:
			CryptoPP::RSA::PublicKey m_remote_public_key;
			affix_services::security::rolling_token m_remote_token;

		public:
			transmission_security_manager(
				const CryptoPP::RSA::PrivateKey& a_local_private_key,
				const affix_services::security::rolling_token& a_local_token,
				const CryptoPP::RSA::PublicKey& a_remote_public_key,
				const affix_services::security::rolling_token& a_remote_token
			);

		public:
			bool export_transmission(
				const std::vector<uint8_t>& a_message_data,
				std::vector<uint8_t>& a_output,
				affix_services::networking::transmission_result& a_result
			);
			bool import_transmission(
				const std::vector<uint8_t>& a_message_data,
				std::vector<uint8_t>& a_output,
				affix_services::networking::transmission_result& a_result
			);

		protected:
			bool pack_token(
				affix_base::data::byte_buffer& a_data,
				affix_services::networking::transmission_result& a_result
			);
			bool unpack_token(
				affix_base::data::byte_buffer& a_data,
				std::vector<uint8_t>& a_output,
				affix_services::networking::transmission_result& a_result
			);
			bool pack_signature(
				affix_base::data::byte_buffer& a_data,
				affix_services::networking::transmission_result& a_result
			);
			bool unpack_signature(
				affix_base::data::byte_buffer& a_data,
				std::vector<uint8_t>& a_output,
				affix_services::networking::transmission_result& a_result
			);

		};
	}
}
