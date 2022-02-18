#pragma once
#include "affix-base/pch.h"
#include "message_rqt_relay.h"
#include "message_rsp_relay.h"
#include "authenticated_connection.h"

namespace affix_services
{
	class pending_relay
	{
	protected:

		/// <summary>
		/// Vector of all pending relay objects.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<pending_relay>>, affix_base::threading::cross_thread_mutex>& m_pending_relays;

		affix_base::data::ptr<

		/// <summary>
		/// Vector of all relay results.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<relay_result>>, affix_base::threading::cross_thread_mutex>& m_relay_results;

		/// <summary>
		/// The connection who is to receive the relayed message.
		/// </summary>
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_recipient_connection;

	public:
		pending_relay(

		)

	};
}
