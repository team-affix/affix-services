#pragma once
#include "affix-base/pch.h"
#include "message_rqt_relay.h"

namespace affix_services
{
	class message_rsp_relay
	{
	public:
		enum class serialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_packing_deserialization_status_response,
			error_packing_processing_status_response,
		};
		enum class deserialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_unpacking_deserialization_status_response,
			error_unpacking_processing_status_response,
		};
		enum class processing_status_response_type : uint8_t
		{
			unknown = 0,

		};

	public:
		message_rqt_relay::deserialization_status_response_type m_deserialization_status_response =
			message_rqt_relay::deserialization_status_response_type::unknown;
		message_rqt_relay::processing_status_response_type m_processing_status_response =
			message_rqt_relay::processing_status_response_type::unknown;

	public:
		message_rsp_relay(

		);
		message_rsp_relay(
			message_rqt_relay::deserialization_status_response_type a_deserialization_status_response,
			message_rqt_relay::processing_status_response_type a_processing_status_response
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
