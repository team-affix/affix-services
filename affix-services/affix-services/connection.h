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
#include "message_header.h"

namespace affix_services {
	namespace networking {

		class connection
		{
		public:
			affix_services::security::transmission_security_manager m_transmission_security_manager;

		public:
			asio::ip::tcp::socket m_socket;
			affix_base::networking::socket_io_guard m_socket_io_guard;

		protected:
			uint64_t m_start_time = 0;

		public:
			connection(
				asio::ip::tcp::socket& a_socket,
				const CryptoPP::RSA::PrivateKey& a_local_private_key,
				const affix_services::security::rolling_token& a_local_token,
				const CryptoPP::RSA::PublicKey& a_remote_public_key,
				const affix_services::security::rolling_token& a_remote_token
			);

		public:
			void async_send(
				const std::vector<uint8_t>& a_message_header_data,
				const std::vector<uint8_t>& a_message_body_data,
				const std::function<void(bool)>& a_callback
			);
			void async_receive(
				std::vector<uint8_t>& a_message_header_data,
				std::vector<uint8_t>& a_message_body_data,
				const std::function<void(bool)>& a_callback
			);

		public:
			uint64_t lifetime() const;

		};

	}
}
