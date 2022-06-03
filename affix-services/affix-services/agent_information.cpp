#include "agent_information.h"

using namespace affix_services;

agent_information::agent_information(

) :
	affix_base::data::serializable(m_agent_type_identifier, m_timestamp, m_disclosure_iteration, m_agent_specific_information)
{

}

agent_information::agent_information(
	const std::string& a_agent_type_identifier,
	const uint64_t& a_timestamp,
	const uint64_t& a_disclosure_iteration,
	const std::vector<uint8_t>& a_agent_specific_information
) :
	affix_base::data::serializable(m_agent_type_identifier, m_timestamp, m_disclosure_iteration, m_agent_specific_information),
	m_agent_type_identifier(a_agent_type_identifier),
	m_timestamp(a_timestamp),
	m_disclosure_iteration(a_disclosure_iteration),
	m_agent_specific_information(a_agent_specific_information)
{

}

agent_information::agent_information(
	const agent_information& a_agent_information
) :
	agent_information(
		a_agent_information.m_agent_type_identifier,
		a_agent_information.m_timestamp,
		a_agent_information.m_disclosure_iteration,
		a_agent_information.m_agent_specific_information
	)
{

}

agent_information& agent_information::operator=(
	const agent_information& a_agent_information
)
{
	m_agent_type_identifier = a_agent_information.m_agent_type_identifier;
	m_timestamp = a_agent_information.m_timestamp;
	m_disclosure_iteration = a_agent_information.m_disclosure_iteration;
	m_agent_specific_information = a_agent_information.m_agent_specific_information;

	return *this;

}

bool agent_information::newer_than(
	const agent_information& a_agent_information
) const
{
	if (m_timestamp > a_agent_information.m_timestamp)
		return true;
	if (m_timestamp == a_agent_information.m_timestamp &&
		m_disclosure_iteration > a_agent_information.m_disclosure_iteration)
		return true;
	return false;
}
