#pragma once
#include "affix-base/pch.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/tree.h"
#include "message_types.h"
#include "message_header.h"
#include "agent_information.h"

namespace affix_services
{
	class message_index_body : public affix_base::data::serializable
	{
	public:
		std::vector<std::string> m_client_identity_path;
		affix_services::agent_information m_agent_information;

	public:
		message_index_body(

		);

		message_index_body(
			const std::vector<std::string>& a_client_identity_path,
			const affix_services::agent_information& a_agent_information
		);

		message_index_body(
			const message_index_body& a_message_rqt_index_body
		);

		message_header create_message_header(

		) const;

	};
}
