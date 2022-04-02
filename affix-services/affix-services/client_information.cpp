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

)
{
	return m_paths.size();
}

void client_information::clean_paths(
	const uint64_t& a_path_timeout_in_seconds
)
{
	uint64_t l_time_since_epoch = affix_base::timing::utc_time();

	for (int i = m_paths.size() - 1; i >= 0; i--)
	{
		auto l_it = m_paths.begin();
		std::advance(l_it, i);
		
		if (l_time_since_epoch - l_it->second >= a_path_timeout_in_seconds)
			m_paths.erase(l_it);
	}

}

void client_information::register_path(
	const std::vector<std::string>& a_path
)
{
	auto l_path = m_paths.find(a_path);

	uint64_t l_time_since_epoch = affix_base::timing::utc_time();

	if (l_path == m_paths.end())
	{
		auto l_insertion_iterator = m_paths.end();

		// Select the position at which to insert based off of path size.
		// Shortest paths should be first.
		for (auto i = m_paths.begin(); i != m_paths.end(); i++)
		{
			if (a_path.size() <= i->first.size())
			{
				l_insertion_iterator = i;
				break;
			}
		}
		m_paths.insert(l_insertion_iterator, { a_path, l_time_since_epoch });
	}
	else
	{
		l_path->second = l_time_since_epoch;
	}

}

std::vector<std::string> client_information::shortest_path(

)
{
	return m_paths.begin()->first;
}
