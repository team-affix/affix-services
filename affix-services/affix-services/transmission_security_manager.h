#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "rolling_token.h"
#include "affix-base/rsa.h"
#include "affix-base/byte_buffer.h"
#include "transmission_result.h"
#include "security_information.h"

namespace affix_services {
	namespace security {
		class transmission_security_manager {
		public:
			/// <summary>
			/// Structure which holds all necessary connection security information.
			/// </summary>
			affix_base::data::ptr<security_information> m_security_information;

		public:
			transmission_security_manager(
				affix_base::data::ptr<security_information> a_security_information
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

		protected:
			std::vector<uint8_t> remote_aes_key(

			) const;
			std::vector<uint8_t> local_aes_key(

			) const;

		};
	}
}
