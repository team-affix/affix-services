#pragma once
#include "affix-base/pch.h"
#include "affix-base/serializable.h"
#include "affix-base/utc_time.h"
#include "affix-base/guarded_resource.h"

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

		/// <summary>
		/// Timestamp for the client information object, which is measured in seconds since January 1, 1970.
		/// </summary>
		uint64_t m_timestamp = 0;

		/// <summary>
		/// Version number which can be incremented each time a new version of this client's information is sent out.
		/// </summary>
		uint64_t m_disclosure_iteration = 0;

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
			const std::vector<uint8_t>& a_agent_specific_information = {},
			const uint64_t& a_timestamp = affix_base::timing::utc_time(),
			const uint64_t& a_disclosure_iteration = 0
		);

		/// <summary>
		/// Copy constructor for the agent_information type.
		/// </summary>
		/// <param name="a_agent_information"></param>
		agent_information(
			const agent_information& a_agent_information
		);

		/// <summary>
		/// Returns whether or not this agent information is newer than an argued agent_information object.
		/// </summary>
		/// <param name="a_client_information"></param>
		/// <returns></returns>
		bool newer_than(
			const agent_information& a_agent_information
		) const;

	};

}
