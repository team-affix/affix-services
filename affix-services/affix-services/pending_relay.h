#pragma once
#include "affix-base/pch.h"
#include "message_rqt_relay.h"
#include "message_rsp_relay.h"
#include "authenticated_connection.h"
#include "inbound_message.h"

namespace affix_services
{
	class pending_relay
	{
	protected:
		/// <summary>
		/// The actual received request.
		/// </summary>
		affix_services::message_rqt_relay m_request;

		/// <summary>
		/// Vector of all pending relay objects.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<pending_relay>>, affix_base::threading::cross_thread_mutex>& m_pending_relays;

		/// <summary>
		/// Vector into which the result of this async object will push it's results.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<relay_result>>, affix_base::threading::cross_thread_mutex>& m_relay_results;
	public:
		pending_relay(
			const affix_services::message_rqt_relay& a_request,
			affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<pending_relay>>, affix_base::threading::cross_thread_mutex>& a_pending_relays,
			affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<relay_result>>, affix_base::threading::cross_thread_mutex>& a_relay_results
		);

		bool finished(

		);

	};
}
