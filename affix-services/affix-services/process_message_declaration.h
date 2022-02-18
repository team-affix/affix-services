#pragma once

namespace affix_services
{
	/// <summary>
	/// A message process call declaration type.
	/// This type holds all deserialized parameters necessary for processing messages.
	/// </summary>
	/// <typeparam name="MESSAGE_TYPE"></typeparam>
	template<typename MESSAGE_TYPE>
	struct process_message_declaration
	{
	public:
		/// <summary>
		/// The connection from which this message originated.
		/// </summary>
		affix_base::data::ptr<affix_services::networking::authenticated_connection> m_originating_connection;

		/// <summary>
		/// The message header which contains useful information about the message.
		/// </summary>
		affix_services::messaging::message_header m_message_header;

		/// <summary>
		/// The message body which contains all of the fields specific to the message type.
		/// </summary>
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
		process_message_declaration(
			const affix_base::data::ptr<affix_services::networking::authenticated_connection>& a_originating_connection,
			const affix_services::messaging::message_header& a_message_header,
			affix_base::data::byte_buffer& a_body_byte_buffer
		)
		{
			// Store owner connection.
			m_originating_connection = a_originating_connection;

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

}
