#include "affix-base/pch.h"
#include "client_information.h"
#include "affix-base/timing.h"

using namespace affix_services;

client_information::client_information(

) :
	affix_base::data::serializable(m_identity, m_agents, m_timestamp, m_disclosure_iteration)
{

}

client_information::client_information(
	const std::string& a_identity,
	const std::vector<agent_information>& a_agents,
	const uint64_t& a_timestamp,
	const uint64_t& a_disclosure_iteration
) :
	affix_base::data::serializable(m_identity, m_agents, m_timestamp, m_disclosure_iteration),
	m_identity(a_identity),
	m_agents(a_agents),
	m_timestamp(a_timestamp),
	m_disclosure_iteration(a_disclosure_iteration)
{

}

client_information::client_information(
	const client_information& a_client_information
) :
	client_information(
		a_client_information.m_identity,
		a_client_information.m_agents,
		a_client_information.m_timestamp,
		a_client_information.m_disclosure_iteration)
{

}

bool client_information::newer_than(
	const client_information& a_client_information
) const
{
	if (m_timestamp > a_client_information.m_timestamp)
		return true;
	if (m_timestamp == a_client_information.m_timestamp &&
		m_disclosure_iteration > a_client_information.m_disclosure_iteration)
		return true;
	return false;
}
