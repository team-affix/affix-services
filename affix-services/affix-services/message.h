#pragma once
#include "message_header.h"

namespace affix_services
{
	/// <summary>
	/// A message structure, holding a message header and message body.
	/// </summary>
	/// <typeparam name="MESSAGE_TYPE"></typeparam>
	template<typename MESSAGE_TYPE>
	struct message
	{
	public:
		message_header m_message_header;
		MESSAGE_TYPE m_message_body;

	public:
		message(
			const message_header& a_message_header,
			const MESSAGE_TYPE& a_message_body
		) :
			m_message_header(a_message_header),
			m_message_body(a_message_body)
		{

		}

	};
}
