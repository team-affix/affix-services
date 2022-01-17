#pragma once
#include "message_header.h"
#include "affix-base/rsa.h"

namespace affix_services
{
	namespace messaging
	{
		class rqt_identity_push
		{
		protected:
			CryptoPP::RSA::PublicKey m_public_key;

		public:
			rqt_identity_push(

			);
			rqt_identity_push(
				const CryptoPP::RSA::PublicKey& a_public_key
			);

		public:
			bool serialize(
				affix_base::data::byte_buffer& a_output,
				affix_services::networking::transmission_result& a_result
			);
			bool deserialize(
				affix_base::data::byte_buffer& a_input,
				affix_services::networking::transmission_result& a_result
			);

		};
		enum class identity_push_status_response_type : uint8_t
		{
			unknown = 0,
			error_invalid_public_key,
			error_identity_already_registered,
		};
	}
}
