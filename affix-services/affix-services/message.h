#pragma once
#include "message_header.h"

namespace affix_services
{
	/// <summary>
	/// A message structure, holding a message header and message body.
	/// </summary>
	/// <typeparam name="MESSAGE_TYPE"></typeparam>
	template<typename MESSAGE_HEADER_TYPE, typename MESSAGE_BODY_TYPE>
	struct message
	{
	public:
		/// <summary>
		/// The message header associated with the message.
		/// </summary>
		MESSAGE_HEADER_TYPE m_message_header;
		
		/// <summary>
		/// The message body associated with the message.
		/// </summary>
		MESSAGE_BODY_TYPE m_message_body;

	public:
		/// <summary>
		/// Default constructor for the message structure.
		/// </summary>
		message(

		)
		{

		}

		/// <summary>
		/// The constructor for the structure, which takes in a message header and message body
		/// </summary>
		/// <param name="a_message_header"></param>
		/// <param name="a_message_body"></param>
		message(
			const MESSAGE_HEADER_TYPE& a_message_header,
			const MESSAGE_BODY_TYPE& a_message_body
		) :
			m_message_header(a_message_header),
			m_message_body(a_message_body)
		{

		}

	};
}
