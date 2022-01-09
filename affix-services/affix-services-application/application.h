#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "affix-services/connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"

namespace affix_services_application
{
	class application
	{
	protected:
		asio::ip::tcp::endpoint s_acceptor_endpoint;
		asio::io_context s_acceptor_context;
		asio::ip::tcp::acceptor s_acceptor;
		affix_base::threading::persistent_thread s_acceptor_context_thread;

	protected:
		std::mutex s_connections_mutex;
		std::vector<affix_base::data::ptr<affix_services::networking::connection>> s_connections;
		
	public:
		virtual ~application(

		);
		application(

		);

	public:
		void process_connections(

		);

	protected:
		void process_connection(
			std::vector<affix_base::data::ptr<affix_services::networking::connection>>::iterator a_connection
		);

	protected:
		void async_accept(

		);

	};
}

