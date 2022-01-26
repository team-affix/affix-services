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
#include "connection_async_receive_result.h"
#include "affix-base/cross_thread_mutex.h"

namespace affix_services {
	namespace networking {

		class authenticated_connection
		{
		public:
			affix_services::security::transmission_security_manager m_transmission_security_manager;

		public:
			affix_base::data::ptr<asio::ip::tcp::socket> m_socket;

		protected:
			affix_base::networking::socket_io_guard m_socket_io_guard;

		protected:
			uint64_t m_start_time = 0;
			
			/// <summary>
			/// Mutex preventing concurrent reads/writes to m_received_messages.
			/// </summary>
			affix_base::threading::cross_thread_mutex& m_receive_results_mutex;

			/// <summary>
			/// Vector of async_receive results.
			/// </summary>
			std::vector<affix_base::data::ptr<connection_async_receive_result>>& m_receive_results;

		public:
			virtual ~authenticated_connection(

			);
			authenticated_connection(
				const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
				const CryptoPP::RSA::PrivateKey& a_local_private_key,
				const affix_services::security::rolling_token& a_local_token,
				const CryptoPP::RSA::PublicKey& a_remote_public_key,
				const affix_services::security::rolling_token& a_remote_token,
				affix_base::threading::cross_thread_mutex& a_receive_results_mutex,
				std::vector<affix_base::data::ptr<connection_async_receive_result>>& a_receive_results
			);

		public:
			void async_send(
				affix_base::data::byte_buffer& a_byte_buffer,
				const std::function<void(bool)>& a_callback
			);
			void async_receive(
			
			);

		public:
			uint64_t lifetime() const;

		};

	}
}
