#pragma once
#include "message_header.h"

namespace affix_services {
	namespace messaging {

		class message_identity_authenticate_0 {
		public:
			vector<uint8_t> m_seed;

		public:
			message_identity_authenticate_0(const vector<uint8_t>& a_seed);

		public:
			serialization_result try_serialize(byte_buffer& a_data);
			static serialization_result try_deserialize(byte_buffer& a_data, message_identity_authenticate_0& a_message);

		};

		enum class identity_authenticate_0_status_response_type : uint8_t {

			success = 0,
			invalid_seed_length = 1,

		};

	}
}
