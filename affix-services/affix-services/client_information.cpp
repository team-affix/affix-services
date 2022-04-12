#include "affix-base/pch.h"
#include "client_information.h"
#include "affix-base/timing.h"

using namespace affix_services;

client_information::client_information(
	const std::string& a_identity,
	const agent_information& a_agent_information
) :
	m_identity(a_identity),
	m_agent_information(a_agent_information)
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
		if (std::equal(a_subpath.begin(), a_subpath.end(), m_paths[i].begin()))
			// This path begins with the subpath, therefore deregister it.
			m_paths.erase(m_paths.begin() + i);
	}

}

std::vector<std::string> client_information::fastest_path(

)
{
	return m_paths.front();
}
