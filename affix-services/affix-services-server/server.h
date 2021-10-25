#pragma once
#include "affix-base/pch.h"
#include "asio.hpp"
#include "affix-services/rolling_token.h"
#include <map>

namespace affix_services {

	using std::deque;
	using std::vector;
	using std::map;
	using namespace asio;
	using namespace asio::ip;
	using security::rolling_token;

	const size_t AS_VERSION = 0;
	const size_t AS_ID_SIZE = 25;
	const size_t AS_MAX_INBOUND_DATA_SIZE = 1024;
	const size_t AS_MAX_OUTBOUND_DATA_SIZE = 1024;
	const size_t AS_SEED_SIZE = 25;
	
	class server {
	protected:
		// NETWORK OBJECTS
		tcp::acceptor m_acceptor;


	protected:
		// SERVER CONNECTIONS
		deque<connection> m_connections;

	public:


	};

}
