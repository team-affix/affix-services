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

void pending_relay::send_request(
	const affix_services::message_rqt_relay& a_request
)
{
	m_application.async_send_message(m_recipient_authenticated_connection, a_request, m_request_dispatcher.dispatch(
		[&](bool a_result)
		{
			if (!a_result)
			{
				locked_resource l_finished = m_finished.lock();
				(*l_finished) = true;
				return;
			}

			locked_resource l_response_expected = m_response_expected.lock();
			(*l_response_expected) = true;

		}));

}

void pending_relay::send_response(
	const affix_services::message_rsp_relay& a_response
)
{
	m_application.async_send_message(m_sender_authenticated_connection, a_response, m_response_dispatcher.dispatch(
		[&](bool)
		{
			locked_resource l_finished = m_finished.lock();
			(*l_finished) = true;
		}));

}
