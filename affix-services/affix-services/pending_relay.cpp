#include "pending_relay.h"

using namespace affix_services;

pending_relay::pending_relay(
	affix_base::data::ptr<affix_services::networking::authenticated_connection> a_sender_authenticated_connection,
	affix_base::data::ptr<affix_services::networking::authenticated_connection> a_recipient_authenticated_connection
) :
	m_sender_authenticated_connection(a_sender_authenticated_connection),
	m_recipient_authenticated_connection(a_recipient_authenticated_connection)
{

}
