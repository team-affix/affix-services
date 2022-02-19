#pragma once
#include "affix-base/pch.h"
#include "affix-base/rsa.h"
#include "affix-base/byte_buffer.h"
#include "message_types.h"

namespace affix_services
{
	class message_rqt_relay
	{
	public:
		static affix_services::messaging::message_types s_message_type;

	public:
		enum class serialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_packing_path,
			error_packing_path_index,
			error_packing_payload
		};
		enum class deserialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_unpacking_path,
			error_unpacking_path_index,
			error_unpacking_payload
		};
		enum class processing_status_response_type : uint8_t
		{
			unknown = 0,
			success,
			error_identity_not_connected,
			error_identity_not_reached
		};

	public:
		std::vector<std::string> m_path;
		size_t m_path_index = 0;

		std::vector<uint8_t> m_payload;

	public:
		message_rqt_relay(

		);
		message_rqt_relay(
			const std::vector<std::string>& a_path,
			const size_t& a_path_index,
			const std::vector<uint8_t>& a_payload
		);

	public:
		bool serialize(
			affix_base::data::byte_buffer& a_output,
			serialization_status_response_type& a_result
		);
		bool deserialize(
			affix_base::data::byte_buffer& a_input,
			deserialization_status_response_type& a_result
		);

	};
}
