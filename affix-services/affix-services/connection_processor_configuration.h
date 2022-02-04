#pragma once
#include "affix-base/pch.h"

namespace affix_services
{
	struct connection_processor_configuration
	{
	public:
		/// <summary>
		/// Boolean describing whether or not to close sockets after the connections have gone stale.
		/// </summary>
		bool m_connection_enable_disconnect_after_maximum_idle_time;

		/// <summary>
		/// Maximum time after which connections should be closed if they been idling.
		/// (if m_connection_enable_disconnect_after_maximum_idle_time is false, this will not take effect)
		/// </summary>
		uint64_t m_connection_maximum_idle_time_in_seconds;

	};
}

