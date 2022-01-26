#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "authenticated_connection.h"
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
		struct affix_services_process_message_declaration
		{
		public:
			affix_base::data::ptr<affix_services::networking::authenticated_connection> m_owner;
			affix_services::messaging::message_header m_message_header;
			MESSAGE_TYPE m_message_body;

		public:
			/// <summary>
			/// Status response types, indicating success/failure of
			/// deserialization of message AND processing of message.
			/// </summary>
			typename MESSAGE_TYPE::deserialization_status_response_type m_deserialization_status_response;
			
		public:
			/// <summary>
			/// Deserializes a message body, and then 
			/// encapsulates all necessary information for message processing.
			/// </summary>
			/// <param name="a_owner"></param>
			/// <param name="a_message_header"></param>
			/// <param name="a_body_byte_buffer"></param>
			affix_services_process_message_declaration(
				const affix_base::data::ptr<affix_services::networking::authenticated_connection>& a_owner,
				const affix_services::messaging::message_header& a_message_header,
				affix_base::data::byte_buffer& a_body_byte_buffer
			)
			{
				// Store owner connection.
				m_owner = a_owner;

				// Store message header.
				m_message_header = a_message_header;

				// Deserialize message body
				if (!m_message_body.deserialize(a_body_byte_buffer, m_deserialization_status_response))
				{
					// If unable to deserialize message body, just return.
					return;
				}

			}

		};

		/// <summary>
		/// Callback for when a relay is received whose destination is the submodule attached to this affix-services client.
		/// </summary>
		std::function<void(const std::vector<uint8_t>&)> m_relay_received_callback;

	public:
		/// <summary>
		/// Constructor for the message processor which takes as an argument 
		/// the callback for when a relay is received.
		/// </summary>
		/// <param name="a_relay_received_callback"></param>
		message_processor(
			const std::function<void(const std::vector<uint8_t>&)>& a_relay_received_callback = [](const std::vector<uint8_t>&) {}
		);

		/// <summary>
		/// Processes an async receive result (originating from the connection processor).
		/// </summary>
		/// <param name="a_connection_async_receive_result"></param>
		void process_async_receive_result(
			affix_base::data::ptr<affix_services::networking::connection_async_receive_result> a_connection_async_receive_result
		);

		/// <summary>
		/// Processes a single Affix Services message
		/// </summary>
		/// <typeparam name="MESSAGE_TYPE"></typeparam>
		/// <param name="a_process_message_declaration"></param>
		template<typename MESSAGE_TYPE>
		void process_message(
			const affix_services_process_message_declaration<MESSAGE_TYPE>& a_process_message_declaration
		)
		{

		}

		/// <summary>
		/// Processes a request to push an identity 
		/// (replacing the current identity they are authenticated as).
		/// </summary>
		/// <param name="a_process_message_declaration"></param>
		template<>
		void process_message<affix_services::messaging::message_rqt_identity_push>(
			const affix_services_process_message_declaration<affix_services::messaging::message_rqt_identity_push>& a_process_message_declaration
		)
		{

		}

		/// <summary>
		/// Processes a response to push an identity.
		/// </summary>
		/// <param name="a_process_message_declaration"></param>
		template<>
		void process_message<affix_services::messaging::message_rsp_identity_push>(
			const affix_services_process_message_declaration<affix_services::messaging::message_rsp_identity_push>& a_process_message_declaration
		)
		{

		}

		/// <summary>
		/// Processes a response to push an identity.
		/// </summary>
		/// <param name="a_process_message_declaration"></param>
		template<>
		void process_message<affix_services::message_rqt_identity_delete>(
			const affix_services_process_message_declaration<affix_services::message_rqt_identity_delete>& a_process_message_declaration
		)
		{

		}

	};
}
