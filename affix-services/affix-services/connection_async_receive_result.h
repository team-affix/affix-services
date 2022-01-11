#pragma once
#include "affix-base/ptr.h"

namespace affix_services
{
	namespace networking
	{
		class connection;

		struct connection_async_receive_result
		{
		public:
			affix_base::data::ptr<connection> m_owner;
			std::vector<uint8_t> m_message_header_data;
			std::vector<uint8_t> m_message_body_data;
			bool m_successful;

		public:
			connection_async_receive_result(
				const affix_base::data::ptr<connection>& a_owner,
				const std::vector<uint8_t>& a_message_header_data = {},
				const std::vector<uint8_t>& a_message_body_data = {}
			);

		};

	}
}
