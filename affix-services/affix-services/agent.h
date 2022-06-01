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
	protected:
		class data
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
			/// Information about the remote agents of the same type as the local agent.
			/// </summary>
			std::map<std::string, affix_base::data::ptr<affix_services::parsed_agent_information<AGENT_SPECIFIC_INFORMATION_TYPE>>> m_remote_agents;

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

			/// <summary>
			/// Constructs the agent data structure with argued field values.
			/// </summary>
			/// <param name="a_client"></param>
			/// <param name="a_type_identifier"></param>
			/// <param name="a_agent_specific_information"></param>
			data(
				client& a_client,
				const std::string& a_type_identifier,
				const AGENT_SPECIFIC_INFORMATION_TYPE& a_agent_specific_information
			) :
				m_client(a_client),
				m_local_agent_information(
					a_type_identifier,
					a_agent_specific_information,
					affix_base::timing::utc_time(),
					0
				)
			{

			}

		};

		/// <summary>
		/// The data for the local agent.
		/// </summary>
		affix_base::threading::guarded_resource<data> m_agent_data;

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
			m_agent_data(
				data(
					a_local_client,
					a_type_identifier,
					a_agent_specific_information
				)
			)
		{
			affix_base::threading::locked_resource l_agent_data = m_agent_data.lock();
			affix_base::threading::locked_resource l_client_data = l_agent_data->m_local_client.m_client_data.lock();

			// Add this agent's information to the client on construction of this object
			affix_base::threading::locked_resource l_client_local_agent_inboxes = l_client_data->m_local_agent_inboxes.lock();

			// Lock the local agent information
			affix_base::threading::const_locked_resource l_local_agent_information = l_agent_data->m_local_agent_information.m_agent_information.const_lock();

			// Try to find entry for an agent with the same type identifier
			auto l_agent_iterator = l_client_local_agent_inboxes->find(l_local_agent_information->m_agent_type_identifier);

			if (l_agent_iterator != l_client_local_agent_inboxes->end())
				// Throw exception if there is already an agent with the same type identifier registered.
				throw std::exception("Cannot register two or more agents with the same type identifier.");

			// Push the agent to the vector
			l_client_local_agent_inboxes->insert({ l_local_agent_information->m_agent_type_identifier, {} });

		}

		/// <summary>
		/// Discloses the agent information using the client.
		/// </summary>
		void disclose_agent_information(

		)
		{
			// Lock the local agent information
			affix_base::threading::locked_resource l_agent_data = m_agent_data.lock();
			
			l_agent_data->m_local_client.disclose_agent_information(*l_local_agent_information);

			// Increment the disclosure iteration.
			l_agent_data->l_local_agent_information->m_disclosure_iteration++;

		}

		template<typename ... SERIALIZABLE_PARAMETER_TYPES>
		void invoke(
			const std::string& a_remote_client_identity,
			const FUNCTION_IDENTIFIER_TYPE& a_function_identifier,
			SERIALIZABLE_PARAMETER_TYPES ... a_args
		)
		{
			// Lock the local agent information
			affix_base::threading::locked_resource l_agent_data = m_agent_data.lock();

			// Get the local agent information in a thread-safe manner
			std::string l_agent_type_identifier = l_agent_data->m_local_agent_information.m_agent_information.const_lock()->m_agent_type_identifier;

			// Serialize the function invocation
			affix_base::data::byte_buffer l_serialized_invocation = l_agent_data->m_remote_function_invoker.serialize_invocation(
				a_function_identifier,
				a_args...
			);

			// Relay the serialized invocation to the remote agent.
			l_agent_data->m_local_client.relay(
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
			process_registered_clients();
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
		/// Processes all remote client entries that the local client has.
		/// </summary>
		void process_registered_clients(

		)
		{
			affix_base::threading::const_locked_resource l_client_informations = m_local_client.m_remote_clients.const_lock();

			// Get the local agent information in a thread-safe manner
			affix_base::threading::const_locked_resource l_local_agent_information = m_local_agent_information.m_agent_information.const_lock();

			for (int i = l_client_informations->size() - 1; i >= 0; i--)
				process_registered_client(*l_local_agent_information, l_client_informations->at(i));

		}

		/// <summary>
		/// Processes a single remote client entry.
		/// </summary>
		/// <param name="a_client_information"></param>
		/// <param name=""></param>
		void process_registered_client(
			const affix_services::agent_information& a_local_agent_information,
			const affix_services::client_information& a_client_information
		)
		{
			affix_base::threading::locked_resource l_remote_agents = m_remote_agents.lock();


			// Get whether or not the remote client currently HAS a similar agent.
			auto l_agent_information_iterator =
				std::find_if(a_client_information.m_agents.begin(), a_client_information.m_agents.end(),
					[&](affix_services::agent_information a_agent_information)
					{
						return a_agent_information.m_agent_type_identifier == a_local_agent_information.m_agent_type_identifier;
					});

			if (l_agent_information_iterator == a_client_information.m_agents.end())
				// Do NOTHING, registered client has no similar agent.
				return;

			// Get whether or not the client is currently registered as having an agent of the same type as this
			auto l_registered_agent = l_remote_agents->find(a_client_information.m_identity);

			// Boolean suggesting whether or not the remote agent information has changed
			bool l_registered_agent_information_changed = false;

			if (l_registered_agent == l_remote_agents->end() &&
				l_agent_information_iterator != a_client_information.m_agents.end())
			{
				// If the agent is not registered, 
				// and the client has an agent of the local type associated with it, register it.
				l_registered_agent = l_remote_agents->insert(
					{
						a_client_information.m_identity,
						new affix_services::parsed_agent_information<AGENT_SPECIFIC_INFORMATION_TYPE>(*l_agent_information_iterator)
					}).first;

				// Call method notfying of a change to the agents
				on_remote_agent_connect(l_registered_agent->first);

				// Notify those outside scope that the information changed
				l_registered_agent_information_changed = true;

			}
			else
			{
				// Get the currently registered agent_information
				affix_base::threading::locked_resource l_registered_agent_information = l_registered_agent->second->m_agent_information.lock();

				if (l_agent_information_iterator->newer_than(*l_registered_agent_information))
				{
					// If the agent information received by the client is newer than the currently registered agent information, replace it.
					*l_registered_agent_information = *l_agent_information_iterator;

					// Notify those outside scope that the information changed
					l_registered_agent_information_changed = true;

				}

			}

			if (l_registered_agent_information_changed)
			{
				// Get registered agent specific information
				affix_base::threading::const_locked_resource l_remote_agent_specific_information = l_registered_agent->second->m_parsed_agent_specific_information.const_lock();

				// Call method notfying of a change to the agent information
				on_remote_agent_information_changed(l_registered_agent->first, *l_remote_agent_specific_information);


			}

		}

		/// <summary>
		/// Processes every registered agent.
		/// </summary>
		void process_registered_agents(

		)
		{
			affix_base::threading::locked_resource l_remote_agents = m_remote_agents.lock();

			for (int i = l_remote_agents->size() - 1; i >= 0; i--)
			{
				auto l_registered_agent_iterator = l_remote_agents->begin();
				std::advance(l_registered_agent_iterator, i);
				process_registered_agent(l_remote_agents.resource(), l_registered_agent_iterator);
			}

		}

		/// <summary>
		/// Processes a single registered agent.
		/// </summary>
		/// <param name="a_remote_agents"></param>
		/// <param name="a_remote_agent"></param>
		void process_registered_agent(
			std::map<std::string, affix_base::data::ptr<affix_services::parsed_agent_information<AGENT_SPECIFIC_INFORMATION_TYPE>>>& a_remote_agents,
			typename std::map<std::string, affix_base::data::ptr<affix_services::parsed_agent_information<AGENT_SPECIFIC_INFORMATION_TYPE>>>::iterator a_remote_agent
		)
		{
			affix_base::threading::const_locked_resource l_registered_clients = m_local_client.m_remote_clients.const_lock();

			auto l_registered_client =
				std::find_if(l_registered_clients->begin(), l_registered_clients->end(),
					[&](const affix_services::client_information& a_client_information)
					{
						return a_client_information.m_identity == a_remote_agent->first;
					});

			if (l_registered_client == l_registered_clients->end())
			{
				// Call method notfying of a change to the agents
				on_remote_agent_disconnect(a_remote_agent->first);

				// Deregister the agent, since it was deregistered from the client
				a_remote_agents.erase(a_remote_agent);
				return;

			}

		}

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
