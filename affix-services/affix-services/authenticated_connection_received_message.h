#pragma once
#include "affix-base/ptr.h"
#include "affix-base/byte_buffer.h"
#include "message_header.h"

namespace affix_services
{
	namespace networking
	{
		class authenticated_connection;

		template<typename MESSAGE_TYPE>
		struct authenticated_connection_received_message
		{
		public:
			affix_base::data::ptr<authenticated_connection> m_authenticated_connection;
			affix_services::messaging::message_header m_message_header;
			MESSAGE_TYPE m_message_body;

		public:
			authenticated_connection_received_message(
				const affix_base::data::ptr<authenticated_connection>& a_authenticated_connection,
				const affix_services::messaging::message_header& a_message_header,
				const MESSAGE_TYPE& a_message_body
			) :
				m_authenticated_connection(a_authenticated_connection),
				m_message_header(a_message_header),
				m_message_body(a_message_body)
			{

			}

		};

	}
}
