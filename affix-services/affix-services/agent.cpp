#include "agent.h"

using namespace affix_services;
using namespace affix_base::threading;

agent::agent(
	affix_services::client& a_local_client,
	const std::string& a_type_identifier
) :
	m_local_client(a_local_client),
	m_type_identifier(a_type_identifier),
	m_timestamp(affix_base::timing::utc_time()),
	m_disclosure_iteration(0)
{
	// Add this agent's information to the client on construction of this object
	locked_resource l_client_local_agents = m_local_client.m_local_agents.lock();

	// Try to find entry for an agent with the same type identifier
	std::vector<agent*>::iterator l_agent_iterator =
		std::find_if(l_client_local_agents->begin(), l_client_local_agents->end(),
			[&](const agent* a_agent_entry)
			{
				return m_type_identifier == a_agent_entry->m_type_identifier;
			});

	if (l_agent_iterator != l_client_local_agents->end())
		// Throw exception if there is already an agent with the same type identifier registered.
		throw std::exception("Cannot register two or more agents with the same type identifier.");

	// Push the agent to the vector
	l_client_local_agents->push_back(this);

}
