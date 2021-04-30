#pragma once
#include "deque_mt.h"
#include "net_common.h"
#include "asio/ts/internet.hpp"

using namespace affix_services::net_common;
using affix_base::data::deque_mt;
using asio::ip::tcp;
using asio::ip::make_address;

namespace as_server {
	namespace program {
		static class server {
		protected:
			asio::io_context m_context{};
			tcp::socket m_socket{m_context};
			tcp::acceptor m_acceptor{m_context};
			tcp::endpoint m_local_endpoint{tcp::endpoint(make_address("0.0.0.0"), 8090)};
			vector<tcp::socket> m_connections;
			
		protected:
			friend class processor;

		protected:
			deque_mt<message> m_deque;

		public:
			virtual ~server();
			server();

		public:
			void start_up();
			void shut_down();

		protected:
			void message_client(message& a_msg);
			void message_all_clients(message& a_msg);
			

		};
	}
}