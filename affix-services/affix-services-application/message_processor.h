#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "affix-services/connection_async_receive_result.h"
#include "affix-base/cross_thread_mutex.h"

namespace affix_services_application
{
	class message_processor
	{
	public:
		/// <summary>
		/// Mutex preventing concurrent reads/writes to m_connection_async_receive_results
		/// </summary>
		affix_base::threading::cross_thread_mutex m_connection_async_receive_results_mutex;

		/// <summary>
		/// Results of receiving data from multiple connections. These results contain information about 
		/// the connection of origin, and the success/failure of the receive call.
		/// </summary>
		std::vector<affix_base::data::ptr<affix_services::networking::connection_async_receive_result>> m_connection_async_receive_results;


	};
}
