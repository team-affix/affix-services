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
		/// Timestamp for the client information object, which is measured in seconds since January 1, 1970.
		/// </summary>
		uint64_t m_timestamp = 0;

		/// <summary>
		/// Version number which can be incremented each time a new version of this client's information is sent out.
		/// </summary>
		uint64_t m_disclosure_iteration = 0;

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
			const uint64_t& a_timestamp,
			const uint64_t& a_disclosure_iteration,
			const std::vector<uint8_t>& a_agent_specific_information = {}
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

		template<typename AGENT_SPECIFIC_INFORMATION_TYPE>
		bool get_parsed_agent_specific_information(
			AGENT_SPECIFIC_INFORMATION_TYPE& a_agent_specific_information
		)
		{
			affix_base::data::byte_buffer l_byte_buffer(m_agent_specific_information);
			return l_byte_buffer.pop_front(a_agent_specific_information);
		}

		template<typename AGENT_SPECIFIC_INFORMATION_TYPE>
		bool set_parsed_agent_specific_information(
			const AGENT_SPECIFIC_INFORMATION_TYPE& a_agent_specific_information
		)
		{
			affix_base::data::byte_buffer l_byte_buffer;
			if (!l_byte_buffer.push_back(a_agent_specific_information))
				return false;
			m_agent_specific_information = l_byte_buffer.data();
			return true;
		}

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
