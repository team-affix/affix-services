#pragma once
#include "affix-base/ptr.h"
#include "affix-base/byte_buffer.h"

namespace affix_services
{
	namespace networking
	{
		class authenticated_connection;

		struct connection_async_receive_result
		{
		public:
			affix_base::data::ptr<authenticated_connection> m_owner;
			affix_base::data::byte_buffer m_byte_buffer;

		public:
			connection_async_receive_result(
				const affix_base::data::ptr<authenticated_connection>& a_owner,
				affix_base::data::byte_buffer a_byte_buffer = {}
			);

			bool successful(

			) const;

		};

	}
}
