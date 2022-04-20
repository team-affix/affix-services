#pragma once
#include "affix-base/pch.h"
#include "agent_information.h"
#include "affix-base/serializable.h"
#include "affix-base/utc_time.h"

namespace affix_services
{
	class client_information : public affix_base::data::serializable
	{
	public:
		/// <summary>
		/// String representing the identity of the remote client.
		/// </summary>
		std::string m_identity;

		/// <summary>
		/// Information about the agent who owns the remote client.
		/// </summary>
		std::vector<agent_information> m_agents;

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
		/// Default constructor for client_information object.
		/// </summary>
		client_information(

		);

		/// <summary>
		/// Constructs the client_information object with the argued values for the fields.
		/// </summary>
		/// <param name="a_identity"></param>
		/// <param name="a_agents"></param>
		/// <param name="a_timestamp"></param>
		/// <param name="a_version_number"></param>
		client_information(
			const std::string& a_identity,
			const std::vector<agent_information>& a_agents,
			const uint64_t& a_timestamp = affix_base::timing::utc_time(),
			const uint64_t& a_disclosure_iteration = 0
		);

		/// <summary>
		/// Copy constructor for the client_information type.
		/// </summary>
		/// <param name="a_client_information"></param>
		client_information(
			const client_information& a_client_information
		);

		/// <summary>
		/// Returns whether or not this client information is newer than an argued client_information object.
		/// </summary>
		/// <param name="a_client_information"></param>
		/// <returns></returns>
		bool newer_than(
			const client_information& a_client_information
		) const;


	};
}
