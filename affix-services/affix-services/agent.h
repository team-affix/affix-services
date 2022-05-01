#pragma once
#include "affix-base/guarded_resource.h"
#include "affix-base/ptr.h"
#include "client.h"

namespace affix_services
{
	class agent
	{
	public:
		/// <summary>
		/// Specifies the type of agent running the client.
		/// </summary>
		std::string m_type_identifier;

		/// <summary>
		/// Holds the serialized version of the agent specific information.
		/// </summary>
		std::vector<uint8_t> m_agent_specific_information;

		/// <summary>
		/// Timestamp for the agent object, which is measured in seconds since January 1, 1970.
		/// </summary>
		uint64_t m_timestamp = 0;

		/// <summary>
		/// Version number which can be incremented each time a new version of this agent's information is sent out.
		/// </summary>
		uint64_t m_disclosure_iteration = 0;

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
			const std::string& a_type_identifier,
			const std::vector<uint8_t>& a_agent_specific_information = {}
		);

		/// <summary>
		/// Discloses the agent information using the client.
		/// </summary>
		void disclose_agent_information(

		)
		{
			// Lock the vector of agent information messages
			affix_base::threading::locked_resource l_agent_information_messages = m_local_client.m_agent_information_messages.lock();

			// Create the message body
			message_agent_information_body l_message_body(
				m_local_client.m_local_identity,
				agent_information(
					m_type_identifier,
					m_agent_specific_information,
					m_timestamp,
					m_disclosure_iteration));

			// Create the message
			message l_message(l_message_body.create_message_header(), l_message_body);

			// Add the message to the vector
			l_agent_information_messages->push_back(l_message);

			// Increment the disclosure iteration.
			m_disclosure_iteration++;

		}

	};

}
