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
		std::string m_client_identity;
		std::string m_targeted_agent_identifier;
		std::vector<uint8_t> m_payload;

	public:
		std::vector<std::string> m_path;

	public:
		message_relay_body(

		);

		message_relay_body(
			const std::string& a_client_identity,
			const std::string& a_targeted_agent_identifier,
			const std::vector<uint8_t>& a_payload,
			const std::vector<std::string>& a_path
		);

		message_relay_body(
			const message_relay_body& a_message_rqt_relay_body
		);

		message_header<message_types, affix_base::details::semantic_version_number> create_message_header(

		) const;

	};
}
