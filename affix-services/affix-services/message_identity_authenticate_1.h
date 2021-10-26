#pragma once
#include "message_identity_authenticate_0.h"

namespace affix_services {
	namespace messaging {

		class message_identity_authenticate_1 {
		public:
			identity_authenticate_0_status_response_type m_status;
			vector<uint8_t> m_public_key;

		public:
			message_identity_authenticate_1(identity_authenticate_0_status_response_type a_status, const vector<uint8_t>& a_public_key);

		public:
			serialization_result try_serialize(byte_buffer& a_data);
			static serialization_result try_deserialize(byte_buffer& a_data, message_identity_authenticate_1& a_message);

		};

		enum class identity_authenticate_1_status_response_type : uint8_t {

			success = 0,
			error_public_key_invalid = 1,

		};

	}
}
