#include "agent_information.h"

using namespace affix_services;

agent_information::agent_information(

) :
	affix_base::data::serializable(m_agent_type_identifier, m_agent_specific_information)
{

}

agent_information::agent_information(
	const std::string& a_agent_type_identifier,
	const std::vector<uint8_t>& a_agent_specific_information
) :
	affix_base::data::serializable(m_agent_type_identifier, m_agent_specific_information),
	m_agent_type_identifier(a_agent_type_identifier),
	m_agent_specific_information(a_agent_specific_information)
{

}

agent_information::agent_information(
	const agent_information& a_agent_information
) :
	agent_information(a_agent_information.m_agent_type_identifier, a_agent_information.m_agent_specific_information)
{

}
