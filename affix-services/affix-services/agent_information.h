#pragma once
#include "affix-base/pch.h"
#include "affix-base/serializable.h"

namespace affix_services
{
	struct agent_information : public affix_base::data::serializable
	{
	public:
		std::string m_agent_type_identifier;
		std::vector<uint8_t> m_agent_specific_information;

	public:
		agent_information(

		);

		agent_information(
			const std::string& a_agent_type_identifier,
			const std::vector<uint8_t>& a_agent_specific_information = {}
		);

		agent_information(
			const agent_information& a_agent_information
		);

	};
}
