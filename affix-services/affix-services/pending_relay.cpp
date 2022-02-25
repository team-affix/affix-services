#include "pending_relay.h"
#include "application.h"

using namespace affix_services;
using namespace affix_base::threading;

pending_relay::pending_relay(
	affix_services::application& a_application,
	affix_base::data::ptr<affix_services::networking::authenticated_connection> a_sender_authenticated_connection,
	affix_base::data::ptr<affix_services::networking::authenticated_connection> a_recipient_authenticated_connection
) :
	m_application(a_application),
	m_sender_authenticated_connection(a_sender_authenticated_connection),
	m_recipient_authenticated_connection(a_recipient_authenticated_connection)
{

}

void pending_relay::relay_request(
	const affix_services::message<affix_services::message_rqt_relay_body>& a_request
)
{
	// Save the original request
	m_original_request = a_request;

	// Construct the request body which is to be sent to the recipient
	message_rqt_relay_body l_recipient_request_body(a_request.m_message_body.m_path, a_request.m_message_body.m_path_index + 1, a_request.m_message_body.m_payload);

	// Construct the recipient request
	m_relayed_request = message(l_recipient_request_body.create_message_header(), l_recipient_request_body);
	
	m_application.async_send_message(m_recipient_authenticated_connection, m_relayed_request, m_request_dispatcher.dispatch(
		[&](bool a_result)
		{
			locked_resource l_relay_request_sent = m_relay_request_sent.lock();
			(*l_relay_request_sent) = true;

		}));

}

void pending_relay::relay_response(
	const affix_services::message<affix_services::message_rsp_relay_body>& a_response
)
{
	// Construct the response which is to arrive at the original request sender's inbox
	message l_relayed_response(a_response.m_message_body.create_message_header(m_original_request.m_message_header), a_response.m_message_body);

	m_application.async_send_message(m_sender_authenticated_connection, l_relayed_response, m_response_dispatcher.dispatch([&](bool){}));

}
