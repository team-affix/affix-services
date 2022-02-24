#pragma once
#include "affix-base/pch.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/tree.h"
#include "message_types.h"
#include "message_header.h"
#include "message_rqt_index_body.h"

namespace affix_services
{
	class message_rsp_index_body
	{
	public:
		enum class serialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_packing_message_header,
			error_packing_identities,
		};
		enum class deserialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_unpacking_message_header,
			error_unpacking_identities,
		};
		enum class processing_status_response_type : uint8_t
		{
			unknown = 0,

		};

	public:
		affix_base::data::tree<std::string> m_identities;

	public:
		message_rsp_index_body(

		);

		message_rsp_index_body(
			const affix_base::data::tree<std::string> a_identities
		);

		message_header create_message_header(
			const message_header& a_request_message_header
		) const;

	public:
		bool serialize(
			affix_base::data::byte_buffer& a_output,
			serialization_status_response_type& a_result
		) const;
		bool deserialize(
			affix_base::data::byte_buffer& a_input,
			deserialization_status_response_type& a_result
		);

	};
}
