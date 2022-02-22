#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "authenticated_connection.h"
#include "affix-base/persistent_thread.h"
#include "asio.hpp"
#include "pending_authentication.h"
#include "connection_result.h"
#include "authentication_result.h"
#include "messaging.h"
#include "pending_connection.h"
#include "application_configuration.h"
#include "pending_relay.h"

namespace affix_services
{
	class application
	{
	public:
		/// <summary>
		/// Contains the configuration for this application instance; this object governs how to behave as a connection processor.
		/// </summary>
		affix_base::data::ptr<application_configuration> m_application_configuration;

		/// <summary>
		/// The local public key exported into base64 format.
		/// </summary>
		std::string m_local_identity;

		/// <summary>
		/// IO context which runs all the asynchronous networking functions.
		/// </summary>
		asio::io_context& m_io_context;

		/// <summary>
		/// If the server has a dedicated port, the endpoint for this acceptor should include that port.
		/// If the server doesn't have a dedicated port, the endpoint should have port set to 0.
		/// </summary>
		affix_base::data::ptr<asio::ip::tcp::acceptor> m_acceptor;

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
		/// A vector of all the message data received from authenticated connections.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, affix_base::data::ptr<std::vector<uint8_t>>>>, affix_base::threading::cross_thread_mutex> m_received_messages;

	public:
		/// <summary>
		/// Vector of asynchronously received relay requests.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, message_rqt_relay>>, affix_base::threading::cross_thread_mutex> m_relay_requests;

		/// <summary>
		/// Vector of asynchronously received relay responses.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, message_rsp_relay>>, affix_base::threading::cross_thread_mutex> m_relay_responses;

		/// <summary>
		/// Vector of asynchronously received index requests.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, message_rqt_index>>, affix_base::threading::cross_thread_mutex> m_index_requests;

		/// <summary>
		/// Vector of asynchronously received index responses.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, message_rsp_index>>, affix_base::threading::cross_thread_mutex> m_index_responses;

	protected:
		/// <summary>
		/// A vector of all pending relays
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<affix_services::pending_relay>>, affix_base::threading::cross_thread_mutex> m_pending_relays;

		/// <summary>
		/// A vector of all pending miscellaneous functions that need to be called after a certain delay, hence the uint64_t in the tuple.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<std::tuple<uint64_t, std::function<void()>>>, affix_base::threading::cross_thread_mutex> m_pending_function_calls;


		///// <summary>
		///// A vector of all pending indexes
		///// </summary>
		//affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<pending_index>>, affix_base::threading::cross_thread_mutex> m_pending_indexes;

	public:
		/// <summary>
		/// Vector of relayed payloads that have been received and were destined for this module.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<std::vector<uint8_t>>, affix_base::threading::cross_thread_mutex> m_received_relay_payloads;

	public:
		/// <summary>
		/// Constructs the affix services application given an io context as well as an application configuration.
		/// </summary>
		/// <param name="a_local_key_pair"></param>
		application(
			asio::io_context& a_io_context,
			affix_base::data::ptr<application_configuration> a_application_configuration
		);

		/// <summary>
		/// Starts the server associated with this affix-services module.
		/// </summary>
		void start_server(

		);

		/// <summary>
		/// Starts the pending outbound connections associated with this affix-services-module
		/// </summary>
		void start_pending_outbound_connections(

		);

	public:
		/// <summary>
		/// Starts a single pending outbound connection to a remote peer.
		/// </summary>
		/// <param name="a_outbound_connection_configuration"></param>
		void start_pending_outbound_connection(
			asio::ip::tcp::endpoint a_remote_endpoint,
			const bool& a_remote_localhost
		);

		/// <summary>
		/// Restarts a single pending outbound connection to a remote peer.
		/// </summary>
		/// <param name="a_remote_endpoint"></param>
		/// <param name="a_local_port"></param>
		void restart_pending_outbound_connection(
			asio::ip::tcp::endpoint a_remote_endpoint,
			const bool& a_remote_localhost
		);

		/// <summary>
		/// Sends a message to the authenticated connection asynchronously, where the connection is identified by the argued remote identity.
		/// </summary>
		/// <typeparam name="MESSAGE_TYPE"></typeparam>
		/// <param name="a_remote_identity"></param>
		/// <param name="a_message_body"></param>
		/// <param name="a_callback"></param>
		template<typename MESSAGE_TYPE>
		void async_send_message(
			const std::string& a_remote_identity,
			MESSAGE_TYPE a_message_body,
			const std::function<void(bool)>& a_callback = [](bool) {}
		)
		{
			// Lock mutex preventing concurrent reads/writes to the connections vector
			affix_base::threading::locked_resource l_authenticated_connections = m_authenticated_connections.lock();

			// Try to find a connection associated with the remote identity
			std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>>::iterator l_authenticated_connection =
				find_connection(l_authenticated_connections.resource(), a_remote_identity);

			if (l_authenticated_connection == l_authenticated_connections->end())
			{
				// No associated connection was found.
				a_callback(false);
				return;
			}

			async_send_message((*l_authenticated_connection), a_message_body, a_callback);

		}

		/// <summary>
		/// Sends a message to the authenticated connection asynchronously.
		/// </summary>
		/// <typeparam name="MESSAGE_TYPE"></typeparam>
		/// <param name="a_authenticated_connection"></param>
		/// <param name="a_message_body"></param>
		/// <param name="a_callback"></param>
		template<typename MESSAGE_TYPE>
		void async_send_message(
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_authenticated_connection,
			MESSAGE_TYPE a_message_body,
			const std::function<void(bool)>& a_callback = [](bool) {}
		)
		{
			// Lock mutex preventing concurrent reads/writes to the connections vector
			affix_base::threading::locked_resource l_authenticated_connections = m_authenticated_connections.lock();

			// Create the message header from the message body's message type
			affix_services::messaging::message_header l_message_header(
				MESSAGE_TYPE::s_message_type
			);

			// The byte buffer into which the message header data is to be stored
			affix_base::data::byte_buffer l_message_header_byte_buffer;

			// The serialization status response for the message header.
			affix_services::messaging::message_header::serialization_status_response_type l_message_header_serialization_status_response;

			if (!l_message_header.serialize(l_message_header_byte_buffer, l_message_header_serialization_status_response))
			{
				// Failed to serialize message header.
				std::cerr << "[ AUTHENTICATED CONNECTION ] Error: failed to serialize message header." << std::endl;

				// Trigger the callback with a failure response
				a_callback(false);

				// Close the conection.
				a_authenticated_connection->close();

				// Just return on failure
				return;

			}

			// The byte buffer into which the message body will be serialized
			affix_base::data::byte_buffer l_message_body_byte_buffer;

			// The serialization status response for the message body.
			typename MESSAGE_TYPE::serialization_status_response_type l_message_body_serialization_status_response;

			if (!a_message_body.serialize(l_message_body_byte_buffer, l_message_body_serialization_status_response))
			{
				// Failed to serialize message header.
				std::cerr << "[ AUTHENTICATED CONNECTION ] Error: failed to serialize message body." << std::endl;

				// Trigger the callback with a failure response
				a_callback(false);

				// Close the conection.
				a_authenticated_connection->close();

				// Just return on failure
				return;

			}


			// The final message data byte buffer
			affix_base::data::byte_buffer l_message_data_byte_buffer;

			// Push the contents of the message header byte buffer into the final byte buffer
			l_message_data_byte_buffer.push_back(l_message_header_byte_buffer.data());

			// Push the contents of the message body byte buffer into the final byte buffer
			l_message_data_byte_buffer.push_back(l_message_body_byte_buffer.data());

			// Finally, send the message data
			a_authenticated_connection->async_send(l_message_data_byte_buffer, a_callback);

		}

		/// <summary>
		/// Begins receiving messages from an authenticated connection, the data from which will appear in the received_messages vector
		/// </summary>
		/// <param name="a_authenticated_connection"></param>
		void async_receive_message(
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_authenticated_connection
		);

		/// <summary>
		/// Finds an authenticated connection object given a remote identity.
		/// </summary>
		/// <param name="a_authenticated_connections"></param>
		/// <param name="a_remote_identity"></param>
		/// <returns></returns>
		std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>>::iterator find_connection(
			std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>>& a_authenticated_connections,
			const std::string& a_remote_identity
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
		/// Runs main functions necessary for functionality of the Affix-Services module. This should be called repeatedly for ideal performance.
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
		/// Processes all received messages (which are in their serialized form)
		/// </summary>
		void process_received_messages(

		);

		/// <summary>
		/// Processes a single received message (which is in a serialized form)
		/// </summary>
		/// <param name="a_received_messages"></param>
		/// <param name="a_received_message"></param>
		void process_received_message(
			std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, affix_base::data::ptr<std::vector<uint8_t>>>>& a_received_messages,
			std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, affix_base::data::ptr<std::vector<uint8_t>>>>::iterator a_received_message
		);

		/// <summary>
		/// Processes all relay requests that have been received.
		/// </summary>
		void process_relay_requests(

		);

		/// <summary>
		/// Processes a single relay request that has been received.
		/// </summary>
		/// <param name="a_relay_requests"></param>
		/// <param name="a_relay_request"></param>
		void process_relay_request(
			std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, message_rqt_relay>>& a_relay_requests,
			std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, message_rqt_relay>>::iterator a_relay_request
		);

		/// <summary>
		/// Processes all relay responses that have been received.
		/// </summary>
		void process_relay_responses(

		);

		/// <summary>
		/// Processes a single received relay response.
		/// </summary>
		/// <param name="a_relay_responses"></param>
		/// <param name="a_relay_response"></param>
		void process_relay_response(
			std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, message_rsp_relay>>& a_relay_responses,
			std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, message_rsp_relay>>::iterator a_relay_response
		);

		/// <summary>
		/// Processes all received relay requests in the vector.
		/// </summary>
		void process_pending_relays(

		);

		/// <summary>
		/// Processes a single received relay request.
		/// </summary>
		/// <param name="a_received_relay_requests"></param>
		/// <param name="a_received_relay_request"></param>
		void process_pending_relay(
			std::vector<affix_base::data::ptr<pending_relay>>& a_pending_relays,
			std::vector<affix_base::data::ptr<pending_relay>>::iterator a_pending_relay
		);

		///// <summary>
		///// Processes all received index requests in the vector.
		///// </summary>
		//void process_pending_indexes(

		//);

		///// <summary>
		///// Processes a single received index request.
		///// </summary>
		//void process_pending_index(
		//	std::vector<affix_base::data::ptr<pending_index>>& a_pending_index,
		//	std::vector<affix_base::data::ptr<pending_index>>::iterator a_pending_indexes
		//);

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

	protected:
		void async_accept_next(

		);

	};
}

