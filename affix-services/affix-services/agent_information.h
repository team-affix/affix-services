#pragma once
#include "affix-base/pch.h"
#include "affix-base/serializable.h"
#include "affix-base/utc_time.h"
#include "affix-base/guarded_resource.h"
#include "affix-base/synchronized_resource.h"

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
			const std::vector<uint8_t>& a_agent_specific_information,
			const uint64_t& a_timestamp,
			const uint64_t& a_disclosure_iteration
		);

		/// <summary>
		/// Copy constructor for the agent_information type.
		/// </summary>
		/// <param name="a_agent_information"></param>
		agent_information(
			const agent_information& a_agent_information
		);

		/// <summary>
		/// Copy operator method.
		/// </summary>
		/// <param name="a_agent_information"></param>
		/// <returns></returns>
		agent_information& operator=(
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

	template<typename AGENT_SPECIFIC_INFORMATION_TYPE>
	class parsed_agent_information
	{
	protected:
		const std::function<void(const agent_information&, AGENT_SPECIFIC_INFORMATION_TYPE&)> s_pull =
			[](const agent_information& a_remote, AGENT_SPECIFIC_INFORMATION_TYPE& a_local)
			{
				affix_base::data::byte_buffer l_byte_buffer(a_remote.m_agent_specific_information);
				if (!l_byte_buffer.pop_front(a_local))
					throw std::exception("Failed to deserialize agent specific information.");
			};
		const std::function<void(const AGENT_SPECIFIC_INFORMATION_TYPE&, agent_information&)> s_push =
			[](const AGENT_SPECIFIC_INFORMATION_TYPE& a_local, agent_information& a_remote)
			{
				affix_base::data::byte_buffer l_byte_buffer;
				if (!l_byte_buffer.push_back(a_local))
					throw std::exception("Failed to serialize agent specific information.");
				a_remote.m_agent_specific_information = l_byte_buffer.data();
			};

	public:
		affix_base::threading::guarded_resource<agent_information> m_agent_information;
		affix_base::threading::synchronized_resource<AGENT_SPECIFIC_INFORMATION_TYPE, agent_information> m_parsed_agent_specific_information;

	public:
		parsed_agent_information(
			const agent_information& a_agent_information
		) :
			m_agent_information(a_agent_information),
			m_parsed_agent_specific_information(m_agent_information, s_pull, s_push)
		{

		}

		parsed_agent_information(
			const std::string& a_agent_type_identifier,
			const AGENT_SPECIFIC_INFORMATION_TYPE& a_agent_specific_information,
			const uint64_t& a_timestamp,
			const uint64_t& a_disclosure_iteration
		) :
			m_agent_information(
				agent_information(
					a_agent_type_identifier,
					{},
					a_timestamp,
					a_disclosure_iteration
				)
			),
			m_parsed_agent_specific_information(m_agent_information, s_pull, s_push, a_agent_specific_information)
		{

		}

	};

}
