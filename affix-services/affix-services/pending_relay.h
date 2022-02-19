#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "message_rqt_relay.h"
#include "message_rsp_relay.h"
#include "authenticated_connection.h"

namespace affix_services
{
	class pending_relay
	{
	protected:
		/// <summary>
		/// Dispatcher for the callback functions to be used (send, receive callbacks)
		/// </summary>
		affix_base::callback::dispatcher<affix_base::threading::cross_thread_mutex, void, bool> m_dispatcher;

		/// <summary>
		/// Authenticated connection from which this request originated
		/// </summary>
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_authenticated_connection;

		/// <summary>
		/// The received relay request
		/// </summary>
		affix_services::message_rqt_relay m_request;

		/// <summary>
		/// Callback for when a relay is received (one whos destination is this module)
		/// </summary>
		std::function<void(const std::vector<uint8_t>&)> m_relay_received_callback;

	public:
		pending_relay(
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_authenticated_connection,
			const affix_services::message_rqt_relay& a_request,
			const std::function<void(const std::vector<uint8_t>&)>& a_relay_received_callback
		);

		void respond(

		)
		{

		}

		bool finished(

		)
		{

		}

	};
}
