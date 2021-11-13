#pragma once
#include "affix-base/pch.h"
#include "message_identity_authenticate_1.h"

namespace affix_services {
	namespace messaging {

		class message_identity_authenticate_2 {
		public:
			identity_authenticate_1_status_response_type m_status;

		public:
			message_identity_authenticate_2(identity_authenticate_1_status_response_type a_status);

		public:
			message_result try_serialize(byte_buffer& a_data);
			static message_result try_deserialize(byte_buffer& a_data, message_identity_authenticate_2& a_message);

		};

	}
}
