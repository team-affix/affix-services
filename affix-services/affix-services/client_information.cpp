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

size_t client_information::path_count(

) const
{
	return m_paths.size();
}

void client_information::register_path(
	const std::vector<std::string>& a_path
)
{
	auto l_path = std::find(m_paths.begin(), m_paths.end(), a_path);

	if (l_path == m_paths.end())
		m_paths.push_back(a_path);

}

void client_information::deregister_path(
	const std::vector<std::string>& a_path
)
{
	auto l_path = std::find(m_paths.begin(), m_paths.end(), a_path);

	if (l_path != m_paths.end())
		m_paths.erase(l_path);

}

std::vector<std::string> client_information::fastest_path(

)
{
	return m_paths.front();
}
