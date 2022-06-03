#pragma once
#include "affix-base/pch.h"
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
		struct guarded_data
		{
			/// <summary>
			/// Information pertaining to the local agent.
			/// </summary>
			agent_information m_local_agent_information;

			/// <summary>
			/// A map of all registered agents of a similar type to the local agent.
			/// </summary>
			std::map<std::string, affix_services::agent_information> m_registered_agents;

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

		};
		
	public:
		/// <summary>
		/// The client which is being utilized by the agent for affix-services functionality.
		/// </summary>
		affix_services::client& m_local_client;

		/// <summary>
		/// The data specific to this agent which must be guarded in a thread-safe manner.
		/// </summary>
		affix_base::threading::guarded_resource<guarded_data> m_guarded_data;

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
			m_guarded_data(
				{
					agent_information(
						a_type_identifier,
						affix_base::timing::utc_time(),
						0
					)
				}
			)
		{
			// Lock the local agent information
			affix_base::threading::locked_resource l_guarded_data = m_guarded_data.lock();

			if (!l_guarded_data->m_local_agent_information.set_parsed_agent_specific_information(a_agent_specific_information))
			{
				throw std::exception("[ AGENT ] Error: unable to set the parsed agent specific information.");
			}

			// Registers the local agent with the local client.
			m_local_client.register_local_agent(a_type_identifier);

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
			std::string l_agent_type_identifier = m_local_agent_information.m_agent_information.const_lock()->m_agent_type_identifier;

			// Serialize the function invocation
			affix_base::data::byte_buffer l_serialized_invocation = m_remote_function_invoker.serialize_invocation(
				a_function_identifier,
				a_args...
			);

			// Relay the serialized invocation to the remote agent.
			m_local_client.relay(
				a_remote_client_identity,
				l_agent_type_identifier,
				l_serialized_invocation.data()
			);

		}

		/// <summary>
		/// Processes all messages and handles all repetative functionality.
		/// </summary>
		void process(

		)
		{
			process_registered_agents();
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

		/// <summary>
		/// An overridable method which is called when an agent is newly registered.
		/// </summary>
		virtual void on_remote_agent_connect(
			const std::string& a_remote_client_identity
		)
		{

		}

		/// <summary>
		/// An overridable method which is called when an agent is newly deregistered.
		/// </summary>
		virtual void on_remote_agent_disconnect(
			const std::string& a_remote_client_identity
		)
		{

		}

		/// <summary>
		/// An overridable method which is called when a remote agent's information is updated.
		/// </summary>
		virtual void on_remote_agent_information_changed(
			const std::string& a_remote_client_identity,
			const AGENT_SPECIFIC_INFORMATION_TYPE& a_agent_specific_information
		)
		{

		}

	private:
		/// <summary>
		/// Processes a single remote client entry.
		/// </summary>
		/// <param name="a_client_information"></param>
		/// <param name=""></param>
		void process_registered_agents(

		)
		{
			affix_base::threading::const_locked_resource l_guarded_data = m_guarded_data.const_lock();

			std::map<std::string, affix_services::agent_information> l_remote_agents = m_local_client.get_remote_agents(
				l_guarded_data->m_local_agent_information.m_agent_type_identifier
			);

			std::map<std::string, affix_services::agent_information>& l_registered_agents = l_guarded_data->m_registered_agents;

			for (int i = 0; i < l_remote_agents.size(); i++)
			{
				auto l_remote_agent = l_remote_agents.begin();
				std::advance(l_remote_agent, i);

				// Boolean suggesting whether or not the remote agent information has changed
				bool l_registered_agent_information_changed = false;
				
				// Get an entry for the remote agent, if any exist.
				auto l_registered_agent = l_registered_agents.find(l_remote_agent->first);

				if (l_registered_agent == l_registered_agents.end())
				{
					// If the agent is not registered,
					// and the client has an agent of the local type associated with it, register it.
					l_registered_agent = l_registered_agents.insert(l_remote_agent).first;

					// Call method notfying of a change to the agents
					on_remote_agent_connect(l_registered_agent->first);

					// Notify those outside scope that the information changed
					l_registered_agent_information_changed = true;

				}
				else
				{
					if (l_remote_agent->second.newer_than(l_registered_agent->second))
					{
						// If the agent information received by the client is newer than the currently registered agent information, replace it.
						l_registered_agent->second = l_remote_agent->second;

						// Notify those outside scope that the information changed
						l_registered_agent_information_changed = true;

					}

				}

				if (l_registered_agent_information_changed)
				{
					AGENT_SPECIFIC_INFORMATION_TYPE l_agent_specific_information;

					if (!l_remote_agent->second.get_parsed_agent_specific_information(l_agent_specific_information))
					{
						std::clog << "[ AGENT ] Error: unable to parse agent specific information for identity: " << l_remote_agent->first << std::endl;
						continue;
					}

					// Call method notfying of a change to the agent information
					on_remote_agent_information_changed(l_remote_agent->first, l_agent_specific_information);

				}

			}

			for (int i = l_registered_agents.size() - 1; i >= 0; i--)
			{
				auto l_registered_agent = l_registered_agents.begin();
				std::advance(l_registered_agent, i);

				auto l_remote_agent = l_remote_agents.find(l_registered_agent->first);

				if (l_remote_agent == l_remote_agents.end())
				{
					// Call method notfying of a change to the agents
					on_remote_agent_disconnect(l_registered_agent->first);

					// Deregister the agent, since it was deregistered from the client
					l_registered_agents.erase(l_registered_agent);
					return;

				}
			}

		}

		/// <summary>
		/// Processes each and every received message.
		/// </summary>
		void process_received_messages(

		)
		{
			std::string l_agent_identifier = m_guarded_data.const_lock()->m_local_agent_information.m_agent_type_identifier;

			auto l_inbox = m_local_client.pop_inbox(l_agent_identifier);

			for (int i = l_inbox.size() - 1; i >= 0; i--)
			{
				// Extract the message from the iterator
				message l_message = l_inbox.at(i);

				// Erase the element at the iterator
				l_inbox.erase(l_inbox.begin() + i);

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
			locked_resource l_guarded_data = m_guarded_data.lock();

			// Process the invocation using agent-specific-defined functions.
			l_guarded_data->m_remote_invocation_processor.process(a_remote_client_identity, a_byte_buffer);

		}

	};

}
