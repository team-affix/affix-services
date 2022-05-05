#include "agent.h"

using namespace affix_services;
using namespace affix_base::threading;

agent::agent(
	affix_services::client& a_local_client,
	const std::string& a_type_identifier
) :
	m_local_client(a_local_client),
	m_local_agent_information(agent_information(a_type_identifier))
{
	// Add this agent's information to the client on construction of this object
	locked_resource l_client_local_agents = m_local_client.m_local_agents.lock();

	// Lock the local agent information
	const_locked_resource l_local_agent_information = m_local_agent_information.const_lock();

	// Try to find entry for an agent with the same type identifier
	std::vector<agent*>::iterator l_agent_iterator =
		std::find_if(l_client_local_agents->begin(), l_client_local_agents->end(),
			[&](agent* a_agent_entry)
			{
				const_locked_resource l_agent_entry_information = a_agent_entry->m_local_agent_information.const_lock();
				return l_local_agent_information->m_agent_type_identifier == l_agent_entry_information->m_agent_type_identifier;
			});

	if (l_agent_iterator != l_client_local_agents->end())
		// Throw exception if there is already an agent with the same type identifier registered.
		throw std::exception("Cannot register two or more agents with the same type identifier.");

	// Push the agent to the vector
	l_client_local_agents->push_back(this);

}
