#pragma once
#include "affix-base/pch.h"
#include "asio.hpp"
#include "affix-services/rolling_token.h"
#include "affix-services/connection.h"
#include <map>

namespace affix_services {

	using std::deque;
	using std::vector;
	using std::map;
	using namespace asio;
	using namespace asio::ip;
	using security::rolling_token;
	using std::mutex;
	using affix_services::networking::connection;
	using affix_base::data::ptr;
	using std::function;

	const size_t AS_MAX_INBOUND_DATA_SIZE = 4096;
	const size_t AS_SEED_SIZE = 25;
	
	class server {
	protected:
		tcp::acceptor m_acceptor;

	protected:
		deque<ptr<connection>> m_connections;

	public:
		function<void(const vector<uint8_t>&)> m_process_inbound_transmission_data;

	public:
		void start_accept();
		void stop_accept();

	protected:
		void accept_callback(asio::error_code a_ec, tcp::socket a_socket);

	public:
		void clean_connections();
		void clean_connection(deque<ptr<connection>>::iterator a_connection);

	public:
		void process_connections();
		void process_connection(deque<ptr<connection>>::iterator a_connection);

	};

}
