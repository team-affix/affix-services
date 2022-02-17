#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "authenticated_connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"
#include "server.h"
#include "pending_authentication.h"
#include "connection_result.h"
#include "authentication_result.h"
#include "messaging.h"
#include "message_processor.h"
#include "pending_connection.h"
#include "connection_processor_configuration.h"

namespace affix_services
{
	class connection_processor
	{
	protected:
		/// <summary>
		/// Contains the configuration for this connection_processor instance; this object governs how to behave as a connection processor.
		/// </summary>
		affix_base::data::ptr<connection_processor_configuration> m_connection_processor_configuration;

		/// <summary>
		/// IO context which runs all the asynchronous networking functions.
		/// </summary>
		asio::io_context& m_io_context;

		/// <summary>
		/// A vector of all pending outbound connections.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<pending_connection>>, affix_base::threading::cross_thread_mutex> m_pending_outbound_connections;

		/// <summary>
		/// A vector of all newly established connections.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_result>>, affix_base::threading::cross_thread_mutex> m_connection_results;

		/// <summary>
		/// A vector of all current authentication attempts, which holds those for both inbound and outbound connections.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<pending_authentication>>, affix_base::threading::cross_thread_mutex> m_authentication_attempts;

		/// <summary>
		/// Vector holding results from authentication attempts.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<authentication_result>>, affix_base::threading::cross_thread_mutex> m_authentication_attempt_results;

		/// <summary>
		/// A vector of fully authenticated connections.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>>, affix_base::threading::cross_thread_mutex> m_authenticated_connections;

		/// <summary>
		/// A vector of all pending miscellaneous functions that need to be called after a certain delay, hence the uint64_t in the tuple.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<std::tuple<uint64_t, std::function<void()>>>, affix_base::threading::cross_thread_mutex> m_pending_function_calls;

		/// <summary>
		/// Receive results for all authenticated connections.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection_receive_result>>, affix_base::threading::cross_thread_mutex>& m_authenticated_connection_receive_results;

	public:
		/// <summary>
		/// Constructs the processor given the local key pair.
		/// </summary>
		/// <param name="a_local_key_pair"></param>
		connection_processor(
			asio::io_context& a_io_context,
			affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection_receive_result>>, affix_base::threading::cross_thread_mutex>& a_authenticated_connection_receive_results,
			affix_base::data::ptr<connection_processor_configuration> a_connection_processor_configuration
		);

	public:
		/// <summary>
		/// Starts a single pending outbound connection to a remote peer.
		/// </summary>
		/// <param name="a_outbound_connection_configuration"></param>
		void start_pending_outbound_connection(
			const uint16_t& a_bind_port,
			asio::ip::tcp::endpoint a_remote_endpoint,
			const bool& a_remote_localhost
		);

		/// <summary>
		/// Restarts a single pending outbound connection to a remote peer.
		/// </summary>
		/// <param name="a_remote_endpoint"></param>
		/// <param name="a_local_port"></param>
		void restart_pending_outbound_connection(
			const uint16_t& a_bind_port,
			asio::ip::tcp::endpoint a_remote_endpoint,
			const bool& a_remote_localhost
		);

		/// <summary>
		/// Describes whether the identity is approved.
		/// </summary>
		/// <param name="a_identity"></param>
		/// <returns></returns>
		bool identity_approved(
			const CryptoPP::RSA::PublicKey& a_identity
		);

		/// <summary>
		/// Processes all unauthenticated, authentication-in-progress, and fully
		/// authenticated connections, as well as all received messages.
		/// </summary>
		void process(

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
			std::vector<affix_base::data::ptr<pending_connection>>& a_pending_outbound_connections,
			std::vector<affix_base::data::ptr<pending_connection>>::iterator a_pending_outbound_connection
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
			std::vector<affix_base::data::ptr<connection_result>>& a_connection_results,
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
			std::vector<affix_base::data::ptr<pending_authentication>>& a_authentication_attempts,
			std::vector<affix_base::data::ptr<pending_authentication>>::iterator a_authentication_attempt
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
			std::vector<affix_base::data::ptr<authentication_result>>& a_authentication_attempt_results,
			std::vector<affix_base::data::ptr<authentication_result>>::iterator a_authentication_attempt_result
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
			std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>>& a_authenticated_connections,
			std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>>::iterator a_authenticated_connection
		);

		/// <summary>
		/// Processes all pending function call.
		/// </summary>
		void process_pending_function_calls(

		);

		/// <summary>
		/// Processes a single function call.
		/// </summary>
		/// <param name="a_pending_function"></param>
		void process_pending_function_call(
			std::vector<std::tuple<uint64_t, std::function<void()>>>& a_pending_function_calls,
			std::vector<std::tuple<uint64_t, std::function<void()>>>::iterator a_pending_function_call
		);

	public:
		/// <summary>
		/// Gets a reference to the connection results vector.
		/// </summary>
		/// <returns></returns>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_result>>, affix_base::threading::cross_thread_mutex>& connection_results(

		);

	};
}

