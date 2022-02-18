#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "authenticated_connection.h"
#include "messaging.h"

namespace affix_services
{
	class message_processor
	{
	protected:
		



		/// <summary>
		/// Callback for when a relay is received whose destination is the submodule attached to this affix-services client.
		/// </summary>
		std::function<void(const std::vector<uint8_t>&)> m_relay_received_callback;

		/// <summary>
		/// Authenticated connections' receive results.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection_receive_result>>, affix_base::threading::cross_thread_mutex> m_authenticated_connection_receive_results;

		/// <summary>
		/// A vector of all received relay requests.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<process_message_declaration<message_rqt_relay>>>, affix_base::threading::cross_thread_mutex> m_received_relay_requests;

		/// <summary>
		/// A vector of all received relay responses.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<process_message_declaration<message_rsp_relay>>>, affix_base::threading::cross_thread_mutex> m_received_relay_responses;

		/// <summary>
		/// A vector of all received index requests.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<process_message_declaration<message_rqt_index>>>, affix_base::threading::cross_thread_mutex> m_received_index_requests;

		/// <summary>
		/// A vector of all received index responses.
		/// </summary>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<process_message_declaration<message_rsp_index>>>, affix_base::threading::cross_thread_mutex> m_received_index_responses;

	public:
		/// <summary>
		/// Constructor for the message processor which takes as an argument 
		/// the callback for when a relay is received.
		/// </summary>
		/// <param name="a_relay_received_callback"></param>
		message_processor(
			const std::function<void(const std::vector<uint8_t>&)>& a_relay_received_callback = [](const std::vector<uint8_t>&) {}
		);

		/// <summary>
		/// Processes an async receive result (originating from the connection processor).
		/// </summary>
		/// <param name="a_connection_async_receive_result"></param>
		void process(

		);

		/// <summary>
		/// Processes all receive results originating from authenticated connections.
		/// </summary>
		void process_authenticated_connection_receive_results(

		);

		/// <summary>
		/// Processes a single receive result originating from an authenticated connection.
		/// </summary>
		/// <param name="a_authenticated_connection_receive_results"></param>
		/// <param name="a_authenticated_connection_receive_result"></param>
		void process_authenticated_connection_receive_result(
			std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection_receive_result>>& a_authenticated_connection_receive_results,
			std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection_receive_result>>::iterator a_authenticated_connection_receive_result
		);

		/// <summary>
		/// Processes all received relay requests in the vector.
		/// </summary>
		void process_received_relay_requests(

		);

		/// <summary>
		/// Processes a single received relay request.
		/// </summary>
		/// <param name="a_received_relay_requests"></param>
		/// <param name="a_received_relay_request"></param>
		void process_received_relay_request(
			std::vector<affix_base::data::ptr<process_message_declaration<message_rqt_relay>>>& a_received_relay_requests,
			std::vector<affix_base::data::ptr<process_message_declaration<message_rqt_relay>>>::iterator a_received_relay_request
		);

		/// <summary>
		/// Processes all received relay responses.
		/// </summary>
		void process_received_relay_responses(

		);

		/// <summary>
		/// Processes a single received relay response.
		/// </summary>
		void process_received_relay_response(
			std::vector<affix_base::data::ptr<process_message_declaration<message_rsp_relay>>>& a_received_relay_responses,
			std::vector<affix_base::data::ptr<process_message_declaration<message_rsp_relay>>>::iterator a_received_relay_response
		);

		/// <summary>
		/// Processes all received index requests in the vector.
		/// </summary>
		void process_received_index_requests(

		);

		/// <summary>
		/// Processes a single received index request.
		/// </summary>
		void process_received_index_request(
			std::vector<affix_base::data::ptr<process_message_declaration<message_rqt_index>>>& a_received_index_requests,
			std::vector<affix_base::data::ptr<process_message_declaration<message_rqt_index>>>::iterator a_received_index_request
		);

		/// <summary>
		/// Processes all received index responses.
		/// </summary>
		void process_received_index_responses(

		);

		/// <summary>
		/// Processes a single received index response.
		/// </summary>
		void process_received_index_response(
			std::vector<affix_base::data::ptr<process_message_declaration<message_rsp_index>>>& a_received_index_responses,
			std::vector<affix_base::data::ptr<process_message_declaration<message_rsp_index>>>::iterator a_received_index_response
		);

	public:
		/// <summary>
		/// Gets a reference to the receive results vector.
		/// </summary>
		/// <returns></returns>
		affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<affix_services::networking::authenticated_connection_receive_result>>, affix_base::threading::cross_thread_mutex>& authenticated_connection_receive_results(

		);

	};
}
