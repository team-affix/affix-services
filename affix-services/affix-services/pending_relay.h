#pragma once
#include "authenticated_connection.h"
#include "message_rqt_relay.h"

namespace affix_services
{
	class pending_relay
	{
	public:
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_sender_authenticated_connection;
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_recipient_authenticated_connection;

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
		void response_received(
			const affix_services::message_rsp_relay& a_response
		);

	};
}
