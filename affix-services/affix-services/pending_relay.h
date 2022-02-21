#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "message_rqt_relay.h"
#include "message_rsp_relay.h"
#include "authenticated_connection.h"

namespace affix_services
{

	class application;

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

		/// <summary>
		/// Boolean describing whether or not the pending relay has finished.
		/// </summary>
		affix_base::threading::guarded_resource<bool, affix_base::threading::cross_thread_mutex> m_finished;

	public:
		/// <summary>
		/// Constructor for the pending relay. Takes all necessary arguments for instantiating all fields.
		/// </summary>
		/// <param name="a_application"></param>
		/// <param name="a_authenticated_connection"></param>
		/// <param name="a_request"></param>
		/// <param name="a_relay_received_callback"></param>
		pending_relay(
			application& a_application,
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_authenticated_connection,
			const affix_services::message_rqt_relay& a_request,
			const std::function<void(const std::vector<uint8_t>&)>& a_relay_received_callback
		);

		/// <summary>
		/// Callback for when a response is received by the remote peer.
		/// </summary>
		/// <param name="a_response"></param>
		void response_received(
			const affix_services::message_rsp_relay& a_response
		);

	};
}
