#pragma once
#include "affix-base/pch.h"
#include "message_types.h"
#include "transmission_status_types.h"
#include "serialization_result.h"
#include "version.h"
#include "affix-base/byte_buffer.h"
#include <iostream>

#ifdef _DEBUG
#define LOG(x) std::cout << x << std::endl
#else
#define LOG(x)
#endif

namespace affix_services {
	namespace messaging {

		using std::vector;
		using affix_base::details::semantic_version_number;
		using details::affix_services_version;
		using affix_base::networking::byte_buffer;

		class message_header {
		protected:
			semantic_version_number m_affix_services_version = affix_services_version;

		protected:
			uint32_t m_discourse_id = 0;
			message_types m_message_type = message_types::unknown;
			transmission_status_types m_transmission_status_type = transmission_status_types::unknown;

		public:
			message_header(uint32_t a_discourse_id, message_types a_message_type, transmission_status_types a_transmission_status_type);

		public:
			virtual serialization_result try_serialize(byte_buffer& a_data);
			static serialization_result try_deserialize(byte_buffer& a_data, message_header& a_message);

		};

	}
}
