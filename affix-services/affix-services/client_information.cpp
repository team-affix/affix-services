#include "affix-base/pch.h"
#include "client_information.h"
#include "affix-base/timing.h"

using namespace affix_services;

client_information::client_information(

) :
	affix_base::data::serializable(m_identity, m_agents, m_paths)
{

}

client_information::client_information(
	const std::string& a_identity,
	const std::vector<agent_information>& a_agents,
	const std::vector<std::vector<std::string>>& a_paths
) :
	affix_base::data::serializable(m_identity, m_agents, m_paths),
	m_identity(a_identity),
	m_agents(a_agents),
	m_paths(a_paths)
{

}

client_information::client_information(
	const client_information& a_client_information
) :
	client_information(
		a_client_information.m_identity,
		a_client_information.m_agents,
		a_client_information.m_paths)
{

}

bool client_information::register_path(
	const std::vector<std::string>& a_path
)
{
	auto l_path = std::find(m_paths.begin(), m_paths.end(), a_path);

	if (l_path == m_paths.end())
	{
		m_paths.push_back(a_path);
		return true;
	}

	return false;

}

void client_information::deregister_paths_starting_with(
	const std::vector<std::string>& a_subpath
)
{
	for (int i = m_paths.size() - 1; i >= 0; i--)
	{
		if (m_paths[i].size() < a_subpath.size())
			continue;

		if (std::equal(a_subpath.begin(), a_subpath.end(), m_paths[i].begin(), m_paths[i].begin() + a_subpath.size()))
			// This path begins with the subpath, therefore deregister it.
			m_paths.erase(m_paths.begin() + i);

	}

}

std::vector<std::string> client_information::fastest_path(

)
{
	return m_paths.front();
}
