#pragma once
#include "authenticated_connection.h"
#include "message_rqt_relay_body.h"

namespace affix_services
{
	class application;

	class pending_relay
	{
	public:
		/// <summary>
		/// The owner application.
		/// </summary>
		affix_services::application& m_application;

		/// <summary>
		/// Authenticated connection with the sender
		/// </summary>
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_sender_authenticated_connection;

		/// <summary>
		/// Authenticated connection with the recipient
		/// </summary>
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_recipient_authenticated_connection;

		/// <summary>
		/// Boolean describing whether sending the request has finished. (does not indicate the success nature of the send)
		/// </summary>
		affix_base::threading::guarded_resource<bool, affix_base::threading::cross_thread_mutex> m_relay_request_sent = false;

		/// <summary>
		/// Dispatcher for sending the request to the recipient
		/// </summary>
		affix_base::callback::dispatcher<affix_base::threading::cross_thread_mutex, void, bool> m_request_dispatcher;

		/// <summary>
		/// Dispatcher for sending the response to the original sender
		/// </summary>
		affix_base::callback::dispatcher<affix_base::threading::cross_thread_mutex, void, bool> m_response_dispatcher;

		/// <summary>
		/// The message which was sent to us (the mediators). (This is the original request received from a sender)
		/// </summary>
		message<message_rqt_relay_body> m_original_request;

		/// <summary>
		/// The message which relayed to the recipient from us (the mediators).
		/// </summary>
		message<message_rqt_relay_body> m_relayed_request;

	public:
		/// <summary>
		/// Constructs a pending relay between two connections.
		/// </summary>
		/// <param name="a_sender_authenticated_connection"></param>
		/// <param name="a_recipient_authenticated_connection"></param>
		pending_relay(
			affix_services::application& a_application,
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_sender_authenticated_connection,
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_recipient_authenticated_connection
		);

		/// <summary>
		/// Sends a relay request to the recipient.
		/// </summary>
		/// <param name="a_request"></param>
		void relay_request(
			const affix_services::message<affix_services::message_rqt_relay_body>& a_request
		);

		/// <summary>
		/// This gets called when 
		/// </summary>
		/// <param name="a_response"></param>
		void relay_response(
			const affix_services::message<affix_services::message_rsp_relay_body>& a_response
		);

	};
}
