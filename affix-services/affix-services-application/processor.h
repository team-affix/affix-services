#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "affix-services/connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"
#include "server.h"
#include "authentication_attempt.h"
#include "unauthenticated_connection.h"

namespace affix_services_application
{
	class processor
	{
	public:
		/// <summary>
		/// A mutex guarding m_unauthenticated_connecitons.
		/// </summary>
		affix_base::threading::cross_thread_mutex m_unauthenticated_connections_mutex;

		/// <summary>
		/// A vector of all newly established connections.
		/// </summary>
		std::vector<affix_base::data::ptr<unauthenticated_connection>> m_unauthenticated_connections;

	protected:
		/// <summary>
		/// A vector of all current authentication attempts, which holds those for both inbound and outbound connections.
		/// </summary>
		std::vector<affix_base::data::ptr<authentication_attempt>> m_authentication_attempts;

		/// <summary>
		/// A vector of fully authenticated connections.
		/// </summary>
		std::vector<affix_base::data::ptr<affix_services::networking::connection>> m_authenticated_connections;

		/// <summary>
		/// Mutex that prevents concurrent reads/writes to m_connection_async_receive_results.
		/// </summary>
		affix_base::threading::cross_thread_mutex m_connection_async_receive_results_mutex;

		/// <summary>
		/// Receive results for all authenticated connections.
		/// </summary>
		std::vector<affix_base::data::ptr<affix_services::networking::connection_async_receive_result>> m_connection_async_receive_results;
		
		/// <summary>
		/// The local RSA key pair, used for all message security
		/// </summary>
		affix_base::cryptography::rsa_key_pair m_local_key_pair;

	public:
		/// <summary>
		/// Constructs the processor given the local key pair.
		/// </summary>
		/// <param name="a_local_key_pair"></param>
		processor(
			const affix_base::cryptography::rsa_key_pair& a_local_key_pair
		);

	public:
		/// <summary>
		/// Processes all unauthenticated, authentication-in-progress, and fully
		/// authenticated connections, as well as all received messages.
		/// </summary>
		void process(

		);

	protected:
		/// <summary>
		/// Processes all new (unauthenticated) connections.
		/// </summary>
		void process_unauthenticated_connections(

		);

		/// <summary>
		/// Processes a single unauthenticated connection.
		/// </summary>
		/// <param name="a_unauthenticated_connection"></param>
		void process_unauthenticated_connection(
			std::vector<affix_base::data::ptr<unauthenticated_connection>>::iterator a_unauthenticated_connection
		);

		/// <summary>
		/// Processes all authentication attempts, looping through, checking if any have been successful,
		/// and stopping any if they've expired.
		/// </summary>
		void process_authentication_attempts(

		);

		/// <summary>
		/// Processes a single authentication attempt. If the attempt has expired, the connection is closed and it is disposed of.
		/// However, if it has authenticated successfully, the connection is bumped into m_authenticated_connections.
		/// </summary>
		void process_authentication_attempt(
			std::vector<affix_base::data::ptr<authentication_attempt>>::iterator a_authentication_attempt
		);

		/// <summary>
		/// Processes all authenticated connections, checking if any have disconnected, and disposing
		/// of the allocated resources if so.
		/// </summary>
		void process_authenticated_connections(

		);

		/// <summary>
		/// Process a single authenticated connection, disposing of it when the socket closes.
		/// </summary>
		/// <param name="a_authenticated_connection"></param>
		void process_authenticated_connection(
			std::vector<affix_base::data::ptr<affix_services::networking::connection>>::iterator a_authenticated_connection
		);

		/// <summary>
		/// Processes all async receive results in the vector.
		/// </summary>
		void process_async_receive_results(

		);

		/// <summary>
		/// Processes a single async_receive_result from the vector.
		/// </summary>
		void process_async_receive_result(
			std::vector<affix_base::data::ptr<affix_services::networking::connection_async_receive_result>>::iterator a_async_receive_result
		);

	protected:
		/// <summary>
		/// Processes a single inbound message.
		/// </summary>
		/// <param name="a_message_header_data"></param>
		/// <param name="a_message_body_data"></param>
		/// <param name="a_connection"></param>
		void process_message_data(
			const std::vector<uint8_t>& a_message_header_data,
			const std::vector<uint8_t>& a_message_body_data,
			const affix_base::data::ptr<affix_services::networking::connection>& a_connection
		);

	};
}

