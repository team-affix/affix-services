#pragma once
#include "affix-base/pch.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/tree.h"
#include "message_types.h"
#include "message_header.h"
#include "agent_information.h"

namespace affix_services
{
	class message_reveal_body : public affix_base::data::serializable
	{
	public:
		std::string m_client_identity;
		agent_information m_agent_information;

	public:
		std::vector<std::string> m_path;

	public:
		message_reveal_body(

		);

		message_reveal_body(
			const std::string& a_client_identity,
			const agent_information& a_agent_information,
			const std::vector<std::string>& a_path = {}
		);

		message_reveal_body(
			const message_reveal_body& a_message_rqt_index_body
		);

		message_header create_message_header(

		) const;

	};
}
