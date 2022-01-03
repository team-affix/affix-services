#pragma once
#include "affix-base/pch.h"
#include "message_types.h"
#include "transmission_result.h"
#include "version.h"
#include "affix-base/byte_buffer.h"
#include "rolling_token.h"
#include "affix-base/rsa.h"
#include <iostream>

#if 1
#define LOG(x) std::cout << x << std::endl
#else
#define LOG(x)
#endif

namespace affix_services {
	namespace messaging {

		class message_header {
		public:
			affix_base::details::semantic_version_number m_affix_services_version = affix_services::details::i_affix_services_version;

		public:
			uint32_t m_discourse_id = 0;
			message_types m_message_type = message_types::unknown;
			affix_services::networking::transmission_result m_transmission_result = affix_services::networking::transmission_result::unknown;

		public:
			message_header(
				const uint32_t& a_discourse_id,
				const message_types& a_message_type,
				const affix_services::networking::transmission_result& a_transmission_result
			);

		public:
			virtual bool serialize(
				affix_base::data::byte_buffer& a_output,
				affix_services::networking::transmission_result& a_result
			);
			static bool deserialize(
				affix_base::data::byte_buffer& a_input,
				message_header& a_output,
				affix_services::networking::transmission_result& a_result
			);

		};

	}
}
