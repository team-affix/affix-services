#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "connection.h"
#include "messaging.h"

namespace affix_services
{
	class message_processor
	{
	public:
		/// <summary>
		/// Processes a single message.
		/// </summary>
		/// <param name="a_connection_async_receive_result"></param>
		virtual void process(
			affix_base::data::ptr<affix_services::networking::connection_async_receive_result> a_connection_async_receive_result
		);

		/// <summary>
		/// Processes an affix services default message type. (rqt_identity_push)
		/// </summary>
		/// <param name="a_owner"></param>
		/// <param name="a_message_header"></param>
		/// <param name="a_message_body"></param>
		virtual void process_rqt_identity_push(
			const affix_base::data::ptr<affix_services::networking::connection>& a_owner,
			const affix_services::messaging::message_header& a_message_header,
			const affix_services::messaging::rqt_identity_push& a_message_body
		);

		/// <summary>
		/// Processes an affix services default message type. (rsp_identity_push)
		/// </summary>
		/// <param name="a_owner"></param>
		/// <param name="a_message_header"></param>
		/// <param name="a_message_body"></param>
		virtual void process_rsp_identity_push(
			const affix_base::data::ptr<affix_services::networking::connection>& a_owner,
			const affix_services::messaging::message_header& a_message_header,
			const affix_services::messaging::rsp_identity_push& a_message_body
		);

	};
}
