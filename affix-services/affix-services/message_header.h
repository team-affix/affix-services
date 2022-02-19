#pragma once
#include "affix-base/pch.h"
#include "message_types.h"
#include "transmission_result.h"
#include "version.h"
#include "affix-base/byte_buffer.h"
#include "rolling_token.h"
#include "affix-base/rsa.h"
#include <iostream>

namespace affix_services {
	namespace messaging {

		class message_header
		{
		public:
			enum class serialization_status_response_type : uint8_t
			{
				unknown = 0,
				error_packing_affix_services_version,
				error_packing_message_type,
			};
			enum class deserialization_status_response_type : uint8_t
			{
				unknown = 0,
				error_unpacking_affix_services_version,
				error_affix_services_version_mismatch,
				error_unpacking_message_type,
			};

		public:
			/// <summary>
			/// The version of affix services in the sender module
			/// </summary>
			affix_base::details::semantic_version_number m_affix_services_version = affix_services::details::i_affix_services_version;

			/// <summary>
			/// The type of message being sent
			/// </summary>
			message_types m_message_type = message_types::unknown;

		public:
			/// <summary>
			/// Default constructor. Initializes all fields to their types' default values.
			/// </summary>
			message_header(

			);

			/// <summary>
			/// Constructs the message header with the argued field values.
			/// </summary>
			/// <param name="a_discourse_id"></param>
			/// <param name="a_message_type"></param>
			/// <param name="a_transmission_result"></param>
			message_header(
				const message_types& a_message_type
			);

			/// <summary>
			/// Serializes the message header, returning a boolean suggesting the success of the operation (true == success)
			/// </summary>
			/// <param name="a_output"></param>
			/// <param name="a_result"></param>
			/// <returns></returns>
			virtual bool serialize(
				affix_base::data::byte_buffer& a_output,
				serialization_status_response_type& a_result
			);
			/// <summary>
			/// Deserializes the message header, returning a boolean suggesting the success of the operation (true == success)
			/// </summary>
			/// <param name="a_output"></param>
			/// <param name="a_result"></param>
			/// <returns></returns>
			virtual bool deserialize(
				affix_base::data::byte_buffer& a_input,
				deserialization_status_response_type& a_result
			);

		};

	}
}
