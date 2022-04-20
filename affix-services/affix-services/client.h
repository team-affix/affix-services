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
#include "client_configuration.h"
#include "agent_information.h"
#include "messaging.h"
#include "client_information.h"
#include "remote_client.h"

namespace affix_services
{
	class client
	{
	public:
		/// <summary>
		/// Contains the configuration for this client instance; this object governs how to behave as a connection processor.
		/// </summary>
		affix_base::data::ptr<client_configuration> m_client_configuration;

		/// <summary>
		/// Holds necessary information about the client, such as the client identity and the agents.
		/// </summary>
		client_information l_local_client_information;

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

	protected:
		/// <summary>
		/// Vector of relay requests that are pending being processed.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body>>, affix_base::threading::cross_thread_mutex> m_relay_messages;

		/// <summary>
		/// Vector of client_path requests that are pending being processed.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_client_path_body>>, affix_base::threading::cross_thread_mutex> m_client_path_messages;

		/// <summary>
		/// Vector of reveal requests that are pending being processed.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_client_information_body>>, affix_base::threading::cross_thread_mutex> m_client_information_messages;

	protected:
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
		/// Vector of relayed messages that have been received and were destined for this module.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body>>, affix_base::threading::cross_thread_mutex> m_agent_received_messages;

		/// <summary>
		/// A vector of registered clients, along with paths to those clients,
		/// and agent information.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<remote_client>, affix_base::threading::cross_thread_mutex> m_remote_clients;

	public:
		/// <summary>
		/// Constructs the affix services client given an io context as well as an client configuration.
		/// </summary>
		/// <param name="a_local_key_pair"></param>
		client(
			asio::io_context& a_io_context,
			affix_base::data::ptr<client_configuration> a_client_configuration,
			const std::vector<agent_information>& a_local_agents
		);

		/// <summary>
		/// Runs main functions necessary for functionality of the Affix-Services module. This should be called repeatedly for ideal performance.
		/// </summary>
		void process(

		);

		/// <summary>
		/// A function that implements relaying of messages to a remote module.
		/// </summary>
		/// <param name="a_exclusive_path"></param>
		/// <param name="a_payload"></param>
		void relay(
			const std::vector<std::string>& a_path,
			const std::string& a_target_agent_identifier,
			const std::vector<uint8_t>& a_payload = {}
		);

		/// <summary>
		/// A function that implements relaying of messages to a remote module given only 
		/// the identity associated with that module.
		/// </summary>
		/// <param name="a_exclusive_path"></param>
		/// <param name="a_payload"></param>
		void relay(
			const std::string& a_identity,
			const std::string& a_target_agent_identifier,
			const std::vector<uint8_t>& a_payload = {}
		);

		/// <summary>
		/// Relays a message of any type message body, with any type acting as the 
		/// "message_types" enum.
		/// </summary>
		/// <typeparam name="MESSAGE_TYPES"></typeparam>
		/// <typeparam name="VERSION_TYPE"></typeparam>
		/// <typeparam name="MESSAGE_TYPE"></typeparam>
		/// <param name="a_identity"></param>
		/// <param name="a_message"></param>
		template<typename MESSAGE_HEADER_TYPE, typename MESSAGE_BODY_TYPE>
		void relay(
			const std::string& a_identity,
			const std::string& a_target_agent_identifier,
			const message<MESSAGE_HEADER_TYPE, MESSAGE_BODY_TYPE>& a_message
		)
		{
			// Temporary byte buffer which will be populated with message header & message body data
			affix_base::data::byte_buffer l_byte_buffer;

			if (!l_byte_buffer.push_back(a_message.m_message_header) ||
				!l_byte_buffer.push_back(a_message.m_message_body))
			{
				// Failed to serialize either the message header or body
				std::cerr << "[ APPLICATION ] Error: failed to serialize relayed message." << std::endl;

				return;

			}

			// Finally, relay the serialized message
			relay(a_identity, a_target_agent_identifier, l_byte_buffer.data());

		}

		/// <summary>
		/// Recursively causes all neighboring machines to register all paths to all locally registered clients.
		/// </summary>
		void disclose_local_index(
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_authenticated_connection
		);

		/// <summary>
		/// Recursively deregisters all paths through you to your neighbor.
		/// </summary>
		/// <param name="a_neighbor_identity"></param>
		void deregister_neighbor_index(
			const std::string& a_neighbor_identity
		);

		/// <summary>
		/// Discloses client information with all neighbors and the message recurs outward.
		/// </summary>
		void disclose_client_information(

		);

		/// <summary>
		/// Returns the shortest path in terms of connecting nodes to a remote identity.
		/// </summary>
		/// <param name="a_identity"></param>
		/// <returns></returns>
		std::vector<std::string> fastest_path_to_identity(
			const std::string& a_identity
		);

	protected:
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
		template<typename MESSAGE_BODY_TYPE>
		void async_send_message(
			const std::string& a_remote_identity,
			const message<message_header<message_types, affix_base::details::semantic_version_number>, MESSAGE_BODY_TYPE>& a_message
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
				return;
			}

			async_send_message((*l_authenticated_connection), a_message);

		}

		/// <summary>
		/// Sends a message to the authenticated connection asynchronously.
		/// </summary>
		/// <typeparam name="MESSAGE_TYPE"></typeparam>
		/// <param name="a_authenticated_connection"></param>
		/// <param name="a_message_body"></param>
		/// <param name="a_callback"></param>
		template<typename MESSAGE_BODY_TYPE>
		void async_send_message(
			affix_base::data::ptr<affix_services::networking::authenticated_connection> a_authenticated_connection,
			const message<message_header<message_types, affix_base::details::semantic_version_number>, MESSAGE_BODY_TYPE>& a_message
		)
		{
			// Lock mutex preventing concurrent reads/writes to the connections vector
			affix_base::threading::locked_resource l_authenticated_connections = m_authenticated_connections.lock();

			// The byte buffer into which the message header data is to be stored
			affix_base::data::byte_buffer l_message_byte_buffer;

			if (!l_message_byte_buffer.push_back(a_message.m_message_header) ||
				!l_message_byte_buffer.push_back(a_message.m_message_body))
			{
				// Failed to serialize message.
				std::cerr << "[ APPLICATION ] Error: failed to serialize message." << std::endl;

				// Close the conection.
				a_authenticated_connection->close();

				// Just return on failure
				return;

			}

			// Finally, send the message data
			a_authenticated_connection->async_send(l_message_byte_buffer);

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
		void process_relay_messages(

		);

		/// <summary>
		/// Processes a single relay request that has been received.
		/// </summary>
		/// <param name="a_relay_requests"></param>
		/// <param name="a_relay_request"></param>
		void process_relay_message(
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body>>& a_relay_messages,
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body>>::iterator a_relay_message
		);

		/// <summary>
		/// Processes all client_path messages.
		/// </summary>
		void process_client_path_messages(

		);

		/// <summary>
		/// Processes a single client_path message.
		/// </summary>
		/// <param name="a_client_path_messages"></param>
		/// <param name="a_client_path_message"></param>
		void process_client_path_message(
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_client_path_body>>& a_client_path_messages,
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_client_path_body>>::iterator a_client_path_message
		);

		/// <summary>
		/// Processes every received reveal request.
		/// </summary>
		void process_client_information_messages(

		);

		/// <summary>
		/// Processes a single received reveal request.
		/// </summary>
		/// <param name="a_reveal_requests"></param>
		/// <param name="a_reveal_request"></param>
		void process_client_information_message(
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_client_information_body>>& a_client_information_messages,
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_client_information_body>>::iterator a_client_information_message
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

		/// <summary>
		/// Cleans all expired client paths from the registry.
		/// </summary>
		void process_registered_clients(

		);

		/// <summary>
		/// Cleans all expired client paths from the registry.
		/// </summary>
		void process_registered_client(
			std::vector<remote_client>& a_registered_clients,
			std::vector<remote_client>::iterator a_registered_client
		);

	protected:
		void async_accept_next(

		);

	};
}

