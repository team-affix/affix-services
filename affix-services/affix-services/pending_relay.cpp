#include "pending_relay.h"

using namespace affix_services;
using namespace affix_base::threading;

pending_relay::pending_relay(
	affix_base::data::ptr<affix_services::networking::authenticated_connection> a_sender_authenticated_connection,
	affix_base::data::ptr<affix_services::networking::authenticated_connection> a_recipient_authenticated_connection
) :
	m_sender_authenticated_connection(a_sender_authenticated_connection),
	m_recipient_authenticated_connection(a_recipient_authenticated_connection)
{

}

void pending_relay::send_request(
	const affix_services::message_rqt_relay& a_request
)
{
	// Lock the mutex preventing concurrent reads/writes to the boolean
	locked_resource l_send_request_started = m_send_request_started.lock();

	m_recipient_authenticated_connection->async_send_message(a_request, m_request_dispatcher.dispatch([&](bool) {}));

	// Indicate that the request has begun sending
	(*l_send_request_started) = true;

}

void pending_relay::send_response(
	const affix_services::message_rsp_relay& a_response
)
{
	// Lock the mutex preventing concurrent reads/writes to the boolean
	locked_resource l_send_response_started = m_send_response_started.lock();

	m_sender_authenticated_connection->async_send_message(a_response, m_response_dispatcher.dispatch([&](bool) {}));

	// Indicate that the response has begun sending
	(*l_send_response_started) = true;

}
