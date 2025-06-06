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
#include "agent_information.h"
#include "messaging.h"
#include "client_information.h"
#include "agent_information.h"
#include "affix-base/data.h"
#include "json.hpp"

namespace affix_services
{
	class client
	{
	public:
		struct configuration
		{
		public:
			/// <summary>
			/// Path to the JSON file.
			/// </summary>
			std::string m_json_file_path;

			/// <summary>
			/// Boolean describing whether or not timing out is enabled for pending authentication attempts.
			/// </summary>
			bool m_enable_pending_authentication_timeout = true;

			/// <summary>
			/// Maximum time after which pending authentication attempts will be discarded.
			/// </summary>
			uint64_t m_pending_authentication_timeout_in_seconds = 5;

			/// <summary>
			/// Boolean describing whether or not to close sockets after the connections have idled for a maximum amount of time.
			/// </summary>
			bool m_enable_authenticated_connection_timeout = true;

			/// <summary>
			/// Maximum amount of time that connections can idle for before they must be closed and reestablished.
			/// </summary>
			uint64_t m_authenticated_connection_timeout_in_seconds = 21600;

			/// <summary>
			/// The local RSA key pair, used for all message security
			/// </summary>
			affix_base::cryptography::rsa_key_pair m_local_key_pair;

			/// <summary>
			/// The delay in seconds for which the connection processor should wait before reconnecting to a remote peer.
			/// </summary>
			uint64_t m_reconnect_delay_in_seconds = 16;

			/// <summary>
			/// Import all identities approved for using this module.
			/// </summary>
			std::vector<std::string> m_approved_identities;

			/// <summary>
			/// Remote endpoints which this module will connect to.
			/// </summary>
			std::vector<std::string> m_remote_endpoint_strings;

			/// <summary>
			/// Boolean describing whether or not the server should be enabled.
			/// </summary>
			bool m_enable_server = false;

			/// <summary>
			/// Endpoint to which the acceptor is bound.
			/// </summary>
			uint16_t m_server_bind_port = 0;

		public:
			/// <summary>
			/// Constructor which takes an argument for each field it is to populate.
			/// </summary>
			/// <param name="a_connection_enable_disconnect_after_maximum_idle_time"></param>
			/// <param name="a_connection_maximum_idle_time_in_seconds"></param>
			configuration(
				const std::string& a_json_file_path
			);

		};

	public:
		struct guarded_data
		{
			/// <summary>
			/// A vector of all inboxes used by agents registered with this client.
			/// </summary>
			std::map<std::string, std::vector<message<affix_services::message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body>>> m_local_agent_inboxes;

			/// <summary>
			/// A vector of all pending outbound connections.
			/// </summary>
			std::vector<affix_base::data::ptr<pending_connection>> m_pending_outbound_connections;

			/// <summary>
			/// A vector of all pending outbound connection results.
			/// </summary>
			std::vector<affix_base::data::ptr<connection_result>> m_connection_results;

			/// <summary>
			/// A vector of all current authentication attempts, which holds those for both inbound and outbound connections.
			/// </summary>
			std::vector<affix_base::data::ptr<pending_authentication>> m_authentication_attempts;

			/// <summary>
			/// Vector holding results from authentication attempts.
			/// </summary>
			std::vector<affix_base::data::ptr<authentication_result>> m_authentication_attempt_results;

			/// <summary>
			/// A vector of fully authenticated connections.
			/// </summary>
			std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection>> m_authenticated_connections;

			/// <summary>
			/// A vector of all the message bytes received from authenticated connections.
			/// </summary>
			std::vector<std::tuple<affix_base::data::ptr<affix_services::networking::authenticated_connection>, affix_base::data::ptr<std::vector<uint8_t>>>> m_received_messages;

			/// <summary>
			/// Vector of relay messages pending being processed.
			/// </summary>
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body>> m_relay_messages;

			/// <summary>
			/// Vector of client_path requests that are pending being processed.
			/// </summary>
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_client_path_body>> m_client_path_messages;

			/// <summary>
			/// Vector of reveal requests that are pending being processed.
			/// </summary>
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_agent_information_body>> m_agent_information_messages;

			/// <summary>
			/// A vector of all pending miscellaneous functions that need to be called at a certain epoch time,
			/// hence the uint64_t in the tuple.
			/// </summary>
			std::vector<std::tuple<uint64_t, std::function<void()>>> m_pending_function_calls;
		
			/// <summary>
			/// A vector of registered clients, along with ALL paths to those clients,
			/// and agent information.
			/// </summary>
			std::vector<client_information> m_remote_clients;

		};

	public:
		/// <summary>
		/// Contains the configuration for this client instance; this object governs how to behave as a connection processor.
		/// </summary>
		configuration m_client_configuration;

		/// <summary>
		/// Base64 representation of the local client's identity.
		/// </summary>
		std::string m_local_identity;

		/// <summary>
		/// The client data that must be guarded in a thread-safe manner.
		/// </summary>
		affix_base::threading::guarded_resource<guarded_data> m_guarded_data;

	protected:
		/// <summary>
		/// IO context which runs all the asynchronous networking functions.
		/// </summary>
		asio::io_context& m_io_context;

		/// <summary>
		/// If the server has a dedicated port, the endpoint for this acceptor should include that port.
		/// If the server doesn't have a dedicated port, the endpoint should have port set to 0.
		/// </summary>
		affix_base::data::ptr<asio::ip::tcp::acceptor> m_acceptor;

	public:
		/// <summary>
		/// Constructs the affix services client given an io context as well as an client configuration.
		/// </summary>
		/// <param name="a_local_key_pair"></param>
		client(
			asio::io_context& a_io_context,
			const std::string& a_configuration_file_path
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
		bool relay(
			const std::string& a_remote_client_identity,
			const std::string& a_target_agent_type_identifier,
			const std::vector<uint8_t>& a_payload = {}
		);

		/// <summary>
		/// Creates an inbox for the local agent.
		/// </summary>
		/// <param name="a_agent_identifier"></param>
		void register_local_agent(
			const std::string& a_agent_identifier
		);

		/// <summary>
		/// Discloses information for a local agent associated with the client.
		/// </summary>
		/// <param name="a_agent_information"></param>
		void disclose_local_agent_information(
			const affix_services::agent_information& a_agent_information
		);

		/// <summary>
		/// Gets all agents who have the argued type identifier among all remote clients.
		/// </summary>
		/// <param name="a_agent_type_identifier"></param>
		/// <returns></returns>
		std::map<std::string, agent_information> get_remote_agents(
			const std::string& a_agent_type_identifier
		);

		/// <summary>
		/// Gets the inbox associated with the given local agent type identifier. 
		/// (Since there can only be one local agent of a given type registered with the local client,
		/// this identifies a single agent and resolves all ambiguity.)
		/// </summary>
		/// <param name="a_agent_type_identifier"></param>
		/// <returns></returns>
		std::vector<message<affix_services::message_header<message_types, affix_base::details::semantic_version_number>, message_relay_body>> pop_inbox(
			const std::string& a_agent_type_identifier
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
			std::scoped_lock l_lock(m_guarded_data);

			// Try to find a connection associated with the remote identity
			auto l_authenticated_connection = find_connection(m_guarded_data->m_authenticated_connections, a_remote_identity);

			if (l_authenticated_connection == m_guarded_data->m_authenticated_connections.end())
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
			std::scoped_lock l_lock(m_guarded_data);

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
		void process_agent_information_messages(

		);

		/// <summary>
		/// Processes a single received reveal request.
		/// </summary>
		/// <param name="a_reveal_requests"></param>
		/// <param name="a_reveal_request"></param>
		void process_agent_information_message(
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_agent_information_body>>& a_client_information_messages,
			std::vector<message<message_header<message_types, affix_base::details::semantic_version_number>, message_agent_information_body>>::iterator a_client_information_message
		);

		/// <summary>
		/// Processes all pending function calls.
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
			std::vector<client_information>& a_registered_clients,
			std::vector<client_information>::iterator a_registered_client
		);

	protected:
		/// <summary>
		/// Accepts one connection (which will later be authenticated) asynchronously.
		/// </summary>
		void async_accept_next(

		);

	};
}
