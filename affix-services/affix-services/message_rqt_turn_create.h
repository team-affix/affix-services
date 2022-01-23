#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "affix-base/byte_buffer.h"

namespace affix_services
{
	class message_rqt_turn_create
	{
	public:
		enum class serialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_packing_turn_name
		};
		enum class deserialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_unpacking_turn_name
		};
		enum class processing_status_response_type : uint8_t
		{
			unknown = 0,
			error_turn_already_exists,
			error_invalid_turn_name
		};

	public:
		std::string m_turn_name;

	public:
		message_rqt_turn_create(

		);
		message_rqt_turn_create(
			const std::string& a_turn_name
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
