#pragma once
#include "affix-base/guarded_resource.h"
#include "affix-base/ptr.h"
#include "client.h"
#include "agent_information.h"
#include "affix-base/distributed_computing.h"

namespace affix_services
{
	template<typename AGENT_SPECIFIC_INFORMATION_TYPE, typename FUNCTION_IDENTIFIER_TYPE>
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

		/// <summary>
		/// The remote function invoker, used to invoke functions on remote agents of the same type as this.
		/// </summary>
		affix_base::distributed_computing::remote_function_invoker<FUNCTION_IDENTIFIER_TYPE> m_remote_function_invoker;

		/// <summary>
		/// The remote invocation processor, used to process invocations raised by remote agents of the same type as the local agent.
		/// </summary>
		affix_base::distributed_computing::remote_invocation_processor<
			FUNCTION_IDENTIFIER_TYPE,
			std::string /*(used for client identity)*/> m_remote_invocation_processor;

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

		template<typename ... SERIALIZABLE_PARAMETER_TYPES>
		void invoke(
			const std::string& a_remote_client_identity,
			const FUNCTION_IDENTIFIER_TYPE& a_function_identifier,
			SERIALIZABLE_PARAMETER_TYPES ... a_args
		)
		{
			// Get the local agent information in a thread-safe manner
			affix_base::threading::const_locked_resource l_agent_information = m_local_agent_information.m_agent_information.const_lock();

			// Serialize the function invocation
			affix_base::data::byte_buffer l_serialized_invocation = m_remote_function_invoker.serialize_invocation(
				a_function_identifier,
				a_args
			);

			// Relay the serialized invocation to the remote agent.
			m_local_client.relay(
				a_remote_client_identity,
				l_agent_information->m_agent_type_identifier,
				l_serialized_invocation.data()
			);

		}

		/// <summary>
		/// Processes all messages and handles all repetative functionality.
		/// </summary>
		void process(

		)
		{
			process_received_messages();
			agent_specific_process();
		}

	protected:
		/// <summary>
		/// The entry point for customizable processing in the agent.
		/// </summary>
		virtual void agent_specific_process(

		)
		{

		}

	private:
		/// <summary>
		/// Processes each and every received message.
		/// </summary>
		void process_received_messages(

		)
		{
			affix_base::threading::locked_resource l_inbox = m_inbox.lock();

			for (int i = l_inbox->size() - 1; i >= 0; i--)
			{
				// Extract the message from the iterator
				message<message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body> l_message = l_inbox->at(i);

				// Erase the element at the iterator
				l_inbox->erase(l_inbox->begin() + i);

				// Process the received message
				process_received_message(
					l_message.m_message_body.m_source_client_identity,
					affix_base::data::byte_buffer(l_message.m_message_body.m_payload)
				);

			}

		}

		/// <summary>
		/// Processes a single received message.
		/// </summary>
		void process_received_message(
			const std::string& a_remote_client_identity,
			const affix_base::data::byte_buffer& a_byte_buffer
		)
		{
			// Process the invocation using agent-specific-defined functions.
			m_remote_invocation_processor.process(a_remote_client_identity, a_byte_buffer);
		}

	};

}
