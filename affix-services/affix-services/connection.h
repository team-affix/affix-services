#pragma once
#include "affix-base/pch.h"
#include "rolling_token.h"
#include "asio.hpp"
#include "cryptopp/rsa.h"
#include "transmission_security_manager.h"
#include "affix-base/ts_deque.h"
#include "affix-base/ptr.h"
#include "affix-base/networking.h"
#include "affix-base/transmission.h"

namespace affix_services {
	namespace networking {

		using std::vector;
		using security::rolling_token;
		using security::transmission_security_manager;
		using networking::transmission;
		using affix_base::data::ts_deque;
		using affix_base::data::ptr;
		using namespace asio::ip;
		using CryptoPP::RSA;
		using affix_base::networking::socket_io_guard;
		using std::mutex;
		using std::lock_guard;
		using std::deque;
		using std::function;

		const size_t AS_ID_SIZE = 25;
		const size_t AS_MAX_INBOUND_DATA_SIZE = 4096;
		const size_t AS_MAX_OUTBOUND_DATA_SIZE = 4096;

		class connection {
		public:
			// SECURITY FIELDS
			transmission_security_manager m_transmission_security_manager;

			// NETWORKING FIELDS
			tcp::socket m_socket;
			socket_io_guard m_socket_io_guard;

		protected:
			vector<uint8_t> m_inbound_data;


		protected:
			uint64_t m_start_time = 0;

		public:
			connection(tcp::socket& a_socket);

		public:
			void async_send(const vector<uint8_t>& a_message_data, const function<void(bool)>& a_callback);
			void async_receive(transmission& a_transmission, const function<void(bool)>& a_callback);

		public:
			bool secured() const;

		public:
			uint64_t lifetime() const;

		};

	}
}
