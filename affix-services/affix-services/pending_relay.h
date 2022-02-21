#pragma once
#include "authenticated_connection.h"
#include "message_rqt_relay.h"

namespace affix_services
{
	class pending_relay
	{
	public:
		/// <summary>
		/// Authenticated connection with the sender
		/// </summary>
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_sender_authenticated_connection;

		/// <summary>
		/// Authenticated connection with the recipient
		/// </summary>
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_recipient_authenticated_connection;

		/// <summary>
		/// Boolean describing whether sending the request has finished.
		/// </summary>
		affix_base::threading::guarded_resource<bool, affix_base::threading::cross_thread_mutex> m_send_request_started = false;

		/// <summary>
		/// Boolean describing whether sending the response has finished.
		/// </summary>
		affix_base::threading::guarded_resource<bool, affix_base::threading::cross_thread_mutex> m_send_response_started = false;

		/// <summary>
		/// Dispatcher for sending the request to the recipient
		/// </summary>
		affix_base::callback::dispatcher<affix_base::threading::cross_thread_mutex, void, bool> m_request_dispatcher;

		/// <summary>
		/// Dispatcher for sending the response to the original sender
		/// </summary>
		affix_base::callback::dispatcher<affix_base::threading::cross_thread_mutex, void, bool> m_response_dispatcher;

	public:
		/// <summary>
		/// Constructs a pending relay between two connections.
		/// </summary>
		/// <param name="a_sender_authenticated_connection"></param>
		/// <param name="a_recipient_authenticated_connection"></param>
		pending_relay(
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_sender_authenticated_connection,
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_recipient_authenticated_connection
		);

		/// <summary>
		/// Sends a relay request to the recipient.
		/// </summary>
		/// <param name="a_request"></param>
		void send_request(
			const affix_services::message_rqt_relay& a_request
		);

		/// <summary>
		/// This gets called when 
		/// </summary>
		/// <param name="a_response"></param>
		void send_response(
			const affix_services::message_rsp_relay& a_response
		);

	};
}
