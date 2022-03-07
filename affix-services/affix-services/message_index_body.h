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
		affix_base::data::tree<std::string> m_client_identities;
		std::vector<std::string> m_current_client_identity_path;
		std::map<std::string, affix_services::agent_information> m_agents;

	public:
		message_index_body(

		);

		message_index_body(
			const affix_base::data::tree<std::string>& a_client_identities,
			const std::vector<std::string>& a_current_client_identity_resource_path,
			const std::map<std::string, affix_services::agent_information> a_agents
		);

		message_index_body(
			const message_index_body& a_message_rqt_index_body
		);

		message_header create_message_header(

		) const;

	};
}
