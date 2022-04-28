#include "agent.h"

using namespace affix_services;
using namespace affix_base::threading;

agent::agent(
	affix_services::client& a_local_client,
	const std::string& a_type_identifier,
	const std::vector<uint8_t>& a_agent_specific_information
) :
	m_local_client(a_local_client),
	m_type_identifier(a_type_identifier),
	m_agent_specific_information(a_agent_specific_information),
	m_timestamp(affix_base::timing::utc_time()),
	m_disclosure_iteration(0)
{
	a_local_client.register_agent(this);
}

void agent::disclose_agent_information(

)
{
	// Lock the vector of agent information messages
	locked_resource l_agent_information_messages = m_local_client.m_agent_information_messages.lock();

	// Create the message body
	message_agent_information_body l_message_body(m_local_client.m_local_identity, 
		agent_information(m_type_identifier, m_agent_specific_information, m_timestamp, m_disclosure_iteration));

	// Create the message
	message l_message(l_message_body.create_message_header(), l_message_body);

	// Add the message to the vector
	l_agent_information_messages->push_back(l_message);

	// Increment the disclosure iteration.
	m_disclosure_iteration++;

}
