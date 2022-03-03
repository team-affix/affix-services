#pragma once
#include "affix-base/pch.h"
#include "affix-base/rsa.h"
#include "affix-base/byte_buffer.h"
#include "message_types.h"
#include "message_header.h"

namespace affix_services
{
	class message_rqt_relay_body : public affix_base::data::serializable
	{
	public:
		enum class processing_status_response_type
		{
			unknown = 0,
			success,
			error_identity_not_connected,
			error_identity_not_reached,

		};

	public:
		std::vector<std::string> m_path;
		std::vector<uint8_t> m_payload;
		size_t m_path_index = 1;

	public:
		message_rqt_relay_body(

		);

		message_rqt_relay_body(
			const std::vector<std::string>& a_path,
			const std::vector<uint8_t>& a_payload,
			const size_t& a_path_index = 1
		);

		message_rqt_relay_body(
			const message_rqt_relay_body& a_message_rqt_relay_body
		);

		message_header create_message_header(

		) const;

	};
}
