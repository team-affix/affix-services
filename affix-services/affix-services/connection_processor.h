#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "authenticated_connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"
#include "server.h"
#include "authentication_attempt.h"
#include "connection_result.h"
#include "authentication_attempt_result.h"
#include "messaging.h"
#include "message_processor.h"
#include "pending_outbound_connection.h"

namespace affix_services
{
	class connection_processor
	{
	public:
		/// <summary>
		/// Mutex guarding m_pending_outbound_connections.
		/// </summary>
		affix_base::threading::cross_thread_mutex m_pending_outbound_connections_mutex;

		/// <summary>
		/// A vector of all pending outbound connections.
		/// </summary>
		std::vector<affix_base::data::ptr<pending_outbound_connection>> m_pending_outbound_connections;

		/// <summary>
		/// A mutex guarding m_unauthenticated_connecitons.
		/// </summary>
		affix_base::threading::cross_thread_mutex m_connection_results_mutex;

		/// <summary>
		/// A vector of all newly established connections.
		/// </summary>
		std::vector<affix_base::data::ptr<connection_result>> m_connection_results;

	protected:
		/// <summary>
		/// A vector of all current authentication attempts, which holds those for both inbound and outbound connections.
		/// </summary>
		std::vector<affix_base::data::ptr<authentication_attempt>> m_authentication_attempts;

		/// <summary>
		/// Mutex guarding m_authentication_attempt_results from concurrent actions.
		/// </summary>
		affix_base::threading::cross_thread_mutex m_authentication_attempt_results_mutex;

		/// <summary>
		/// Vector holding results from authentication attempts.
		/// </summary>
		std::vector<affix_base::data::ptr<authentication_attempt_result>> m_authentication_attempt_results;

	public:
		/// <summary>
		/// A vector of fully authenticated connections.
		/// </summary>
		std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>> m_authenticated_connections;

	protected:
		/// <summary>
		/// Mutex that prevents concurrent reads/writes to m_connection_async_receive_results.
		/// </summary>
		affix_base::threading::cross_thread_mutex m_connection_async_receive_results_mutex;

		/// <summary>
		/// Receive results for all authenticated connections.
		/// </summary>
		std::vector<affix_base::data::ptr<affix_services::networking::connection_async_receive_result>> m_connection_async_receive_results;
		
		/// <summary>
		/// Processor responsible for handling inbound messages.
		/// </summary>
		message_processor& m_message_processor;
		
		/// <summary>
		/// The local RSA key pair, used for all message security
		/// </summary>
		affix_base::cryptography::rsa_key_pair m_local_key_pair;

	public:
		/// <summary>
		/// Constructs the processor given the local key pair.
		/// </summary>
		/// <param name="a_local_key_pair"></param>
		connection_processor(
			message_processor& a_message_processor,
			const affix_base::cryptography::rsa_key_pair& a_local_key_pair
		);

	public:
		/// <summary>
		/// Processes all unauthenticated, authentication-in-progress, and fully
		/// authenticated connections, as well as all received messages.
		/// </summary>
		void process(

		);

		/// <summary>
		/// Starts a single pending outbound connection to a remote peer.
		/// </summary>
		/// <param name="a_outbound_connection_configuration"></param>
		void start_pending_outbound_connection(
			const affix_base::data::ptr<outbound_connection_configuration>& a_outbound_connection_configuration
		);

	protected:
		/// <summary>
		/// Processes all pending outbound connections.
		/// </summary>
		void process_pending_outbound_connections(

		);

		/// <summary>
		/// Processes a single pending outbound connection.
		/// </summary>
		void process_pending_outbound_connection(
			std::vector<affix_base::data::ptr<pending_outbound_connection>>::iterator a_pending_outbound_connection
		);

		/// <summary>
		/// Processes all new (unauthenticated) connections.
		/// </summary>
		void process_connection_results(

		);

		/// <summary>
		/// Processes a single unauthenticated connection.
		/// </summary>
		/// <param name="a_unauthenticated_connection"></param>
		void process_connection_result(
			std::vector<affix_base::data::ptr<connection_result>>::iterator a_connection_result
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
		/// Processes all authentication attempt results.
		/// </summary>
		void process_authentication_attempt_results(

		);

		/// <summary>
		/// Processes a single authentication attempt result.
		/// </summary>
		/// <param name="a_authentication_attempt_result"></param>
		void process_authentication_attempt_result(
			std::vector<affix_base::data::ptr<authentication_attempt_result>>::iterator a_authentication_attempt_result
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
			std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>>::iterator a_authenticated_connection
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

	};
}

