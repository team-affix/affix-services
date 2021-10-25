#pragma once
#include "affix-base/pch.h"
#include "message_types.h"
#include "transmission_status_types.h"
#include "version.h"

namespace affix_services {
	namespace messaging {

		using std::vector;
		using affix_base::details::semantic_version_number;
		using details::affix_services_version;

		class message {
			// S BLOCK
		protected:
			vector<uint8_t> m_signature;
			vector<uint8_t> m_token;

			// V BLOCK
		protected:
			semantic_version_number m_affix_services_version = affix_services_version;

			// T BLOCK
		protected:
			uint32_t m_discourse_id = 0;
			message_types m_message_type = message_types::unknown;
			transmission_status_types m_transmission_status_type = transmission_status_types::unknown;

		};

		class msg_identity_authenticate_0 : public message {
		public:
			msg_identity_authenticate_0(uint32_t a_discourse_id) {
				m_message_type = message_types::identity_authenticate_0;
				m_transmission_status_type = transmission_status_types::unknown;
			}



		};

	}
}
