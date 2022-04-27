#include "agent.h"

using namespace affix_services;
using namespace affix_base::threading;

agent::agent(
	affix_services::client& a_client,
	const std::string& a_type_identifier,
	const std::vector<uint8_t>& a_agent_specific_information
) :
	m_local_client(a_client),
	m_type_identifier(a_type_identifier),
	m_agent_specific_information(a_agent_specific_information),
	m_timestamp(affix_base::timing::utc_time()),
	m_disclosure_iteration(0)
{

}
