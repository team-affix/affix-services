#include "agent_information.h"

using namespace affix_services;

agent_information_base::agent_information_base(

) :
	affix_base::data::serializable(m_agent_type_identifier, m_serialized_agent_specific_information, m_timestamp, m_disclosure_iteration)
{

}

agent_information_base::agent_information_base(
	const std::string& a_agent_type_identifier,
	const std::vector<uint8_t>& a_serialized_agent_specific_information,
	const uint64_t& a_timestamp,
	const uint64_t& a_disclosure_iteration
) :
	affix_base::data::serializable(m_agent_type_identifier, m_serialized_agent_specific_information, m_timestamp, m_disclosure_iteration),
	m_agent_type_identifier(a_agent_type_identifier),
	m_serialized_agent_specific_information(a_serialized_agent_specific_information),
	m_timestamp(a_timestamp),
	m_disclosure_iteration(a_disclosure_iteration)
{

}

agent_information_base::agent_information_base(
	const agent_information_base& a_agent_information
) :
	agent_information_base(
		a_agent_information.m_agent_type_identifier,
		a_agent_information.m_serialized_agent_specific_information,
		a_agent_information.m_timestamp,
		a_agent_information.m_disclosure_iteration)
{

}

bool agent_information_base::newer_than(
	const agent_information_base& a_agent_information
) const
{
	if (m_timestamp > a_agent_information.m_timestamp)
		return true;
	if (m_timestamp == a_agent_information.m_timestamp &&
		m_disclosure_iteration > a_agent_information.m_disclosure_iteration)
		return true;
	return false;
}
