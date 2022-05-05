#pragma once
#include "affix-base/guarded_resource.h"
#include "affix-base/ptr.h"
#include "client.h"
#include "agent_information.h"

namespace affix_services
{
	template<typename AGENT_SPECIFIC_INFORMATION_TYPE>
	class agent
	{
	public:
		/// <summary>
		/// The client which is being utilized by the agent for affix-services functionality.
		/// </summary>
		affix_services::client& m_local_client;

		/// <summary>
		/// Information pertaining to the local agent.
		/// </summary>
		parsed_agent_information<AGENT_SPECIFIC_INFORMATION_TYPE> m_local_agent_information;

		/// <summary>
		/// A vector of all data that has been received which was destined for this agent.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body>>> m_inbox;

	public:
		/// <summary>
		/// Value-initializing constructor for the agent object.
		/// </summary>
		/// <param name="a_client"></param>
		/// <param name="a_type_identifier"></param>
		/// <param name="a_agent_specific_information"></param>
		agent(
			affix_services::client& a_local_client,
			const std::string& a_type_identifier,
			const AGENT_SPECIFIC_INFORMATION_TYPE& a_agent_specific_information
		) :
			m_local_client(a_local_client),
			m_local_agent_information(
				a_type_identifier,
				a_agent_specific_information,
				affix_base::timing::utc_time(),
				0
			)
		{
			// Add this agent's information to the client on construction of this object
			affix_base::threading::locked_resource l_client_local_agent_inboxes = m_local_client.m_local_agent_inboxes.lock();

			// Lock the local agent information
			affix_base::threading::const_locked_resource l_local_agent_information = m_local_agent_information.m_agent_information.const_lock();

			// Try to find entry for an agent with the same type identifier
			auto l_agent_iterator = l_client_local_agent_inboxes->find(l_local_agent_information->m_agent_type_identifier);

			if (l_agent_iterator != l_client_local_agent_inboxes->end())
				// Throw exception if there is already an agent with the same type identifier registered.
				throw std::exception("Cannot register two or more agents with the same type identifier.");

			// Push the agent to the vector
			l_client_local_agent_inboxes->insert({ l_local_agent_information->m_agent_type_identifier, &m_inbox });

		}


		/// <summary>
		/// Discloses the agent information using the client.
		/// </summary>
		void disclose_agent_information(

		)
		{
			// Lock the local agent information
			affix_base::threading::locked_resource l_local_agent_information = m_local_agent_information.m_agent_information.lock();
			
			m_local_client.disclose_agent_information(*l_local_agent_information);

			// Increment the disclosure iteration.
			l_local_agent_information->m_disclosure_iteration++;

		}

	};

}
