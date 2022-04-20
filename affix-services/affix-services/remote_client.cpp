#include "remote_client.h"

using namespace affix_services;

remote_client::remote_client(
	const client_information& a_client_information
) :
	m_client_information(a_client_information)
{

}

bool remote_client::register_path(
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

void remote_client::deregister_paths_starting_with(
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

std::vector<std::string> remote_client::fastest_path(

)
{
	return m_paths.front();
}

