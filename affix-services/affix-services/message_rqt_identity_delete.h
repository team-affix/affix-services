#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "affix-base/rsa.h"
#include "affix-base/byte_buffer.h"
#include "transmission_result.h"

namespace affix_services
{
	class message_rqt_identity_delete
	{
	public:
		enum class serialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_validating_public_key,
			error_packing_public_key_data
		};
		enum class deserialization_status_response_type : uint8_t
		{
			unknown = 0,
			error_unpacking_public_key_data,
			error_importing_public_key,
			error_validating_public_key
		};
		enum class processing_status_response_type : uint8_t
		{
			unknown = 0,
			error_identity_not_registered
		};

	public:
		CryptoPP::RSA::PublicKey m_public_key;

	public:
		message_rqt_identity_delete(

		);
		message_rqt_identity_delete(
			const CryptoPP::RSA::PublicKey& a_public_key
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
