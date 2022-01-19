#pragma once
#include "affix-base/pch.h"
#include "message_header.h"
#include "message_rqt_identity_push.h"

namespace affix_services
{
	namespace messaging
	{
		class rsp_identity_push
		{
		public:
			identity_push_status_response_type m_status_response = identity_push_status_response_type::unknown;

		public:
			rsp_identity_push(

			);
			rsp_identity_push(
				const identity_push_status_response_type& a_status_response
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
	}
}
