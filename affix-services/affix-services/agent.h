#pragma once
#include "affix-base/guarded_resource.h"
#include "affix-base/ptr.h"
#include "client.h"
#include "agent_information.h"

namespace affix_services
{
	class agent
	{
	public:
		/// <summary>
		/// Information pertaining to the local agent.
		/// </summary>
		affix_base::threading::guarded_resource<agent_information> m_local_agent_information;

		/// <summary>
		/// The client which is being utilized by the agent for affix-services functionality.
		/// </summary>
		affix_services::client& m_local_client;

		/// <summary>
		/// A vector of all data that has been received which was destined for this agent.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body>>, affix_base::threading::cross_thread_mutex> m_received_messages;

	public:
		/// <summary>
		/// Value-initializing constructor for the agent object.
		/// </summary>
		/// <param name="a_client"></param>
		/// <param name="a_type_identifier"></param>
		/// <param name="a_agent_specific_information"></param>
		agent(
			affix_services::client& a_local_client,
			const std::string& a_type_identifier
		);

		/// <summary>
		/// Discloses the agent information using the client.
		/// </summary>
		void disclose_agent_information(

		)
		{
			// Lock the vector of agent information messages
			affix_base::threading::locked_resource l_agent_information_messages = m_local_client.m_agent_information_messages.lock();

			// Lock the local agent information
			affix_base::threading::locked_resource l_local_agent_information = m_local_agent_information.lock();

			// Create the message body
			message_agent_information_body l_message_body(
				m_local_client.m_local_identity,
				*l_local_agent_information);

			// Create the message
			message l_message(l_message_body.create_message_header(), l_message_body);

			// Add the message to the vector
			l_agent_information_messages->push_back(l_message);

			// Increment the disclosure iteration.
			l_local_agent_information->m_disclosure_iteration++;

		}

	};

	template<typename AGENT_SPECIFIC_INFORMATION_TYPE>
	class parsed_agent : public affix_services::agent
	{
	public:
		affix_base::threading::synchronized_resource<AGENT_SPECIFIC_INFORMATION_TYPE, agent_information> m_parsed_agent_specific_information;

	public:
		parsed_agent(
			affix_services::client& a_local_client,
			const std::string& a_type_identifier,
			const AGENT_SPECIFIC_INFORMATION_TYPE& a_agent_specific_information
		) :
			affix_services::agent(a_local_client, a_type_identifier),
			m_parsed_agent_specific_information(
				agent::m_local_agent_information,
				[](const agent_information& a_remote, AGENT_SPECIFIC_INFORMATION_TYPE& a_local)
				{
					affix_base::data::byte_buffer l_byte_buffer(a_remote.m_agent_specific_information);
					if (!l_byte_buffer.pop_front(a_local))
						throw std::exception("Error deserializing agent specific information");
				},
				[](const AGENT_SPECIFIC_INFORMATION_TYPE& a_local, agent_information& a_remote)
				{
					affix_base::data::byte_buffer l_byte_buffer;
					if (!l_byte_buffer.push_back(a_local))
						throw std::exception("Error serializing agent specific information");
					a_remote.m_agent_specific_information = l_byte_buffer.data();
				},
				a_agent_specific_information)
		{

		}

	};

}
