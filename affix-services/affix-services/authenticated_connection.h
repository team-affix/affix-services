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
#include "affix-base/threading.h"

namespace affix_services {
	namespace networking {

		class authenticated_connection
		{
		public:
			/// <summary>
			/// Security manager, in charge of all security when sending / receiving data.
			/// </summary>
			affix_services::security::transmission_security_manager m_transmission_security_manager;

			/// <summary>
			/// Actual network interface with the remote peer.
			/// </summary>
			affix_base::data::ptr<asio::ip::tcp::socket> m_socket;

			/// <summary>
			/// Boolean describing if the connection was established in an inbound fashion.
			/// </summary>
			bool m_inbound_connection = false;

			/// <summary>
			/// Boolean describing if the connection is still valid.
			/// </summary>
			affix_base::threading::guarded_resource<bool, affix_base::threading::cross_thread_mutex> m_connected = true;

			/// <summary>
			/// The endpoint which the socket is connected to.
			/// </summary>
			asio::ip::tcp::endpoint m_remote_endpoint;

			/// <summary>
			/// The endpoint which the socket is bound to.
			/// </summary>
			asio::ip::tcp::endpoint m_local_endpoint;

		protected:
			/// <summary>
			/// IO guard preventing concurrent reads/writes to the socket.
			/// </summary>
			affix_base::networking::socket_io_guard m_socket_io_guard;

			/// <summary>
			/// Time of creation of the authenticated_connection object.
			/// </summary>
			uint64_t m_start_time = 0;
			
			/// <summary>
			/// Vector of async_receive results.
			/// </summary>
			affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_async_receive_result>>, affix_base::threading::cross_thread_mutex>& m_receive_results;

		public:
			/// <summary>
			/// Destructor, handles deletion of resources.
			/// </summary>
			virtual ~authenticated_connection(

			);

			/// <summary>
			/// Constructor taking all necessary information.
			/// </summary>
			/// <param name="a_socket"></param>
			/// <param name="a_local_private_key"></param>
			/// <param name="a_local_token"></param>
			/// <param name="a_remote_public_key"></param>
			/// <param name="a_remote_token"></param>
			/// <param name="a_receive_results_mutex"></param>
			/// <param name="a_receive_results"></param>
			authenticated_connection(
				const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
				const asio::ip::tcp::endpoint& a_remote_endpoint,
				const asio::ip::tcp::endpoint& a_local_endpoint,
				const CryptoPP::RSA::PrivateKey& a_local_private_key,
				const affix_services::security::rolling_token& a_local_token,
				const CryptoPP::RSA::PublicKey& a_remote_public_key,
				const affix_services::security::rolling_token& a_remote_token,
				affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_async_receive_result>>, affix_base::threading::cross_thread_mutex>& a_receive_results,
				const bool& a_inbound_connection
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
