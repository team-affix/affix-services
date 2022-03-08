#pragma once
#include "affix-base/pch.h"
#include "affix-base/rsa.h"
#include "affix-base/byte_buffer.h"
#include "message_types.h"
#include "message_header.h"
#include "agent_information.h"

namespace affix_services
{
	class message_relay_body : public affix_base::data::serializable
	{
	public:
		std::vector<std::string> m_path;
		std::vector<uint8_t> m_payload;

		std::string m_client_identity;
		agent_information m_agent_information;

	public:
		message_relay_body(

		);

		message_relay_body(
			const std::vector<std::string>& a_path,
			const std::vector<uint8_t>& a_payload,
			const std::string a_client_identity,
			const agent_information& a_agent_information
		);

		message_relay_body(
			const message_relay_body& a_message_rqt_relay_body
		);

		message_header create_message_header(

		) const;

	};
}
