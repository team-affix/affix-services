#pragma once
#include "affix-base/pch.h"
#include "rolling_token.h"
#include "asio.hpp"
#include "cryptopp/rsa.h"
#include "identity.h"
#include "affix-base/ts_deque.h"
#include "affix-base/ptr.h"
#include "affix-base/networking.h"

namespace affix_services {
	namespace networking {

		using std::vector;
		using security::rolling_token;
		using security::identity;
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
		const size_t AS_MAX_INBOUND_DATA_SIZE = 1024;
		const size_t AS_MAX_OUTBOUND_DATA_SIZE = 1024;

		class connection {
		public:
			struct security_header {
			public:
				identity m_identity;
				rolling_token m_inbound_token;
				rolling_token m_outbound_token;

			};
			struct network_header {
			public:
				tcp::socket m_socket;
				socket_io_guard m_socket_io_guard;

			public:
				network_header(tcp::socket& a_socket);

			};

		protected:
			uint64_t m_start_time = 0;

		public:
			security_header m_security_header;
			network_header m_network_header;

		public:
			connection(tcp::socket& a_socket);

		public:
			void async_send_securely(const vector<uint8_t>& a_data, const function<void(bool)>& a_callback);
			void async_receive_securely(vector<uint8_t>& a_data, const RSA::PrivateKey& a_private_key, const function<void(bool)>& a_callback);

		public:
			void async_send(const vector<uint8_t>& a_data, const function<void(bool)>& a_callback);
			void async_receive(vector<uint8_t>& a_data, const function<void(bool)>& a_callback);

		public:
			bool outbound_secured() const;
			bool inbound_secured() const;

		public:
			uint64_t lifetime() const;

		};

	}
}
