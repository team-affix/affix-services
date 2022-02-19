#include "pending_relay.h"

using namespace affix_services;
using namespace affix_base::data;
using namespace affix_base::threading;

pending_relay::pending_relay(
	affix_base::data::ptr<affix_services::networking::authenticated_connection> a_authenticated_connection,
	const affix_services::message_rqt_relay& a_request,
	const std::function<void(const std::vector<uint8_t>&)>& a_relay_received_callback
)
{
	
	if (affix_base::cryptography::rsa_to_base64_string(a_authenticated_connection->m_transmission_security_manager.m_security_information->m_local_key_pair.public_key)
		!= a_request.m_path[a_request.m_path_index])
	{
		// We have an issue, the destination identity does not match our identity. Respond with an error
		message_rsp_relay l_response(message_rqt_relay::processing_status_response_type::error_identity_not_reached);
		// Send the response message
		a_authenticated_connection->async_send_message(l_response, m_dispatcher.dispatch([](bool){}));

		// Just return on failure
		return;

	}

	if (a_request.m_path_index == a_request.m_path.size() - 1)
	{
		// This module is the destination module of this relay request,

		// Trigger relay received callback
		a_relay_received_callback(a_request.m_payload);

		// Create the response
		message_rsp_relay l_response(message_rqt_relay::processing_status_response_type::success);
		
		// Send the response message
		a_authenticated_connection->async_send_message(l_response, m_dispatcher.dispatch([](bool){}));
	}
	else
	{
		// Create the request intended to be relayed to the remote peer.
		message_rqt_relay l_relayed_request(a_request.m_path, a_request.m_path_index + 1, a_request.m_payload);

		std::string l_next_identity_in_path = a_request.m_path[a_request.m_path_index + 1];



	}

}
