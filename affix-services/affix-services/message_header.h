#pragma once
#include "affix-base/pch.h"
#include "message_types.h"
#include "transmission_result.h"
#include "message_result.h"
#include "version.h"
#include "affix-base/byte_buffer.h"
#include <iostream>

#if 1
#define LOG(x) std::cout << x << std::endl
#else
#define LOG(x)
#endif

namespace affix_services {
	namespace messaging {

		using std::vector;
		using affix_base::details::semantic_version_number;
		using details::affix_services_version;
		using affix_base::data::byte_buffer;
		using affix_services::networking::transmission_result;

		class message_header {
		protected:
			semantic_version_number m_affix_services_version = affix_services_version;

		protected:
			uint32_t m_discourse_id = 0;
			message_types m_message_type = message_types::unknown;
			transmission_result m_transmission_status_type = transmission_result::unknown;

		public:
			message_header(uint32_t a_discourse_id, message_types a_message_type, transmission_result a_transmission_status_type);

		public:
			virtual message_result try_serialize(byte_buffer& a_data);
			static message_result try_deserialize(byte_buffer& a_data, message_header& a_message);

		};

	}
}
