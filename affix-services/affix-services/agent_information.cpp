#include "agent_information.h"

using namespace affix_services;

agent_information::agent_information(

) :
	affix_base::data::serializable(m_agent_type_identifier, m_agent_specific_information, m_timestamp, m_version_number)
{

}

agent_information::agent_information(
	const std::string& a_agent_type_identifier,
	const std::vector<uint8_t>& a_agent_specific_information,
	const uint64_t& a_timestamp,
	const uint64_t& a_version_number
) :
	affix_base::data::serializable(m_agent_type_identifier, m_agent_specific_information, m_timestamp, m_version_number),
	m_agent_type_identifier(a_agent_type_identifier),
	m_agent_specific_information(a_agent_specific_information),
	m_timestamp(a_timestamp),
	m_version_number(a_version_number)
{

}

agent_information::agent_information(
	const agent_information& a_agent_information
) :
	agent_information(
		a_agent_information.m_agent_type_identifier,
		a_agent_information.m_agent_specific_information,
		a_agent_information.m_timestamp,
		a_agent_information.m_version_number)
{

}

bool agent_information::newer_than(
	const agent_information& a_agent_information
)
{
	if (m_timestamp > a_agent_information.m_timestamp)
		return true;
	if (m_timestamp == a_agent_information.m_timestamp &&
		m_version_number > a_agent_information.m_version_number)
		return true;
	return false;
}
