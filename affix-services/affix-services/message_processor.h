#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "connection.h"
#include "messaging.h"

namespace affix_services
{
	class message_processor
	{
	protected:
		/// <summary>
		/// A message process call declaration type.
		/// This type holds all deserialized parameters necessary for processing messages.
		/// </summary>
		/// <typeparam name="MESSAGE_TYPE"></typeparam>
		template<typename MESSAGE_TYPE>
		struct process_message_declaration
		{
			affix_base::data::ptr<affix_services::networking::connection> a_owner;
			affix_services::messaging::message_header a_message_header;
			MESSAGE_TYPE a_message_body;
		};

	public:
		/// <summary>
		/// Processes a single message.
		/// </summary>
		/// <param name="a_connection_async_receive_result"></param>
		virtual void process(
			affix_base::data::ptr<affix_services::networking::connection_async_receive_result> a_connection_async_receive_result
		);

		/// <summary>
		/// Deserializes a message body, and then processes the whole message (header and all).
		/// This function gets called AFTER the header has been deserialized, so we can read the type of 
		/// message out from the message header.
		/// </summary>
		/// <typeparam name="MESSAGE_TYPE"></typeparam>
		/// <param name="a_body_byte_buffer"></param>
		/// <param name="a_owner"></param>
		/// <param name="a_message_header"></param>
		template<typename MESSAGE_TYPE>
		void deserialize_and_process_affix_services_message(
			affix_base::data::byte_buffer& a_body_byte_buffer,
			const affix_base::data::ptr<affix_services::networking::connection>& a_owner,
			const affix_services::messaging::message_header& a_message_header

		);

		/// <summary>
		/// Processes a single Affix Services message
		/// </summary>
		/// <typeparam name="MESSAGE_TYPE"></typeparam>
		/// <param name="a_process_message_declaration"></param>
		template<typename MESSAGE_TYPE>
		void process_affix_services_message(
			const process_message_declaration<MESSAGE_TYPE>& a_process_message_declaration
		);

	};
}
