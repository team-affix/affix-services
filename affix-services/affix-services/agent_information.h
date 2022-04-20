#pragma once
#include "affix-base/pch.h"
#include "affix-base/serializable.h"
#include "affix-base/utc_time.h"

namespace affix_services
{
	struct agent_information : public affix_base::data::serializable
	{
	public:
		/// <summary>
		/// Specifies the type of agent running the client.
		/// </summary>
		std::string m_agent_type_identifier;

		/// <summary>
		/// Additional information for any remote client or agent curious.
		/// </summary>
		std::vector<uint8_t> m_agent_specific_information;

	public:
		/// <summary>
		/// Default constructor for the agent_information object.
		/// </summary>
		agent_information(

		);

		/// <summary>
		/// Value-initializing constructor for the agent_information object.
		/// </summary>
		/// <param name="a_agent_type_identifier"></param>
		/// <param name="a_agent_specific_information"></param>
		/// <param name="a_timestamp"></param>
		/// <param name="a_version_number"></param>
		agent_information(
			const std::string& a_agent_type_identifier,
			const std::vector<uint8_t>& a_agent_specific_information = {}
		);

		/// <summary>
		/// Copy constructor for the agent_information type.
		/// </summary>
		/// <param name="a_agent_information"></param>
		agent_information(
			const agent_information& a_agent_information
		);

	};
}
