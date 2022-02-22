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
#include "affix-base/cross_thread_mutex.h"
#include "affix-base/threading.h"
#include "connection_information.h"
#include "affix-base/dispatcher.h"
#include "messaging.h"

namespace affix_services {

	class application;

	namespace networking {


		class authenticated_connection
		{
		public:
			/// <summary>
			/// Holds relevant information about the connection.
			/// </summary>
			affix_base::data::ptr<connection_information> m_connection_information;

			/// <summary>
			/// Security manager, in charge of all security when sending / receiving data.
			/// </summary>
			affix_services::security::transmission_security_manager m_transmission_security_manager;

			/// <summary>
			/// Boolean describing if the connection is still valid.
			/// </summary>
			affix_base::threading::guarded_resource<bool, affix_base::threading::cross_thread_mutex> m_connected = true;

			/// <summary>
			/// Dispatcher which tracks the progress of the send and receive callback functions.
			/// </summary>
			affix_base::callback::dispatcher<affix_base::threading::cross_thread_mutex, void, bool> m_send_dispatcher;

			/// <summary>
			/// Dispatcher which tracks the progress of the send and receive callback functions.
			/// </summary>
			affix_base::callback::dispatcher<affix_base::threading::cross_thread_mutex, void, bool> m_receive_dispatcher;
			
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
			/// Time of last interaction between either of the parties.
			/// </summary>
			affix_base::threading::guarded_resource<uint64_t, affix_base::threading::cross_thread_mutex> m_last_interaction_time = 0;

			///// <summary>
			///// Boolean describing whether there is a receive in progress currently.
			///// </summary>
			//affix_base::threading::guarded_resource<bool, affix_base::threading::cross_thread_mutex> m_receive_in_progress = false;

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
				affix_base::data::ptr<connection_information> a_connection_information,
				affix_base::data::ptr<security_information> a_security_information
			);

		public:
			/// <summary>
			/// Sends data over the socket, where the data first goes through
			/// the transmission_security_manager, then the
			/// socket_io_guard to ensure that all transmissions remain secure.
			/// </summary>
			/// <param name="a_byte_buffer"></param>
			/// <param name="a_callback"></param>
			void async_send(
				const affix_base::data::byte_buffer& a_byte_buffer,
				const std::function<void(bool)>& a_callback
			);

			/// <summary>
			/// Receives data over the socket, where the received data goes through
			/// both the socket_io_guard and the transmission_security_manager before having
			/// the result of the async_receive be pushed back onto a vector.
			/// </summary>
			void async_receive(
				std::vector<uint8_t>& a_received_message_data,
				const std::function<void(bool)>& a_callback
			);

		public:
			/// <summary>
			/// Closes the connection.
			/// </summary>
			void close(

			);

		public:
			/// <summary>
			/// Returns the total lifetime of this connection.
			/// </summary>
			/// <returns></returns>
			uint64_t lifetime() const;

			/// <summary>
			/// Returns the total idletime of this connection. (Time since last send call or receive callback).
			/// </summary>
			/// <returns></returns>
			uint64_t idletime();

			/// <summary>
			/// Returns the remote identity of the authenticated connection.
			/// </summary>
			/// <returns></returns>
			const std::string& remote_identity(

			) const;

		};

	}
}
