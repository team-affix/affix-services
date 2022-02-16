#pragma once
#include "affix-base/pch.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/tree.h"

namespace affix_services
{
	class message_rsp_index
	{
	public:
		enum class serialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_packing_identities
		};
		enum class deserialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_unpacking_identities
		};
		enum class processing_status_response_type : uint8_t
		{
			unknown = 0,

		};

	public:
		affix_base::data::tree<std::string> m_identities;

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
