#include "pending_relay.h"
#include "application.h"

using namespace affix_services;
using namespace affix_base::data;
using namespace affix_base::threading;
using namespace affix_services::networking;

pending_relay::pending_relay(
	application& a_application,
	affix_base::data::ptr<affix_services::networking::authenticated_connection> a_authenticated_connection,
	const affix_services::message_rqt_relay& a_request,
	const std::function<void(const std::vector<uint8_t>&)>& a_relay_received_callback
) :
	m_authenticated_connection(a_authenticated_connection),
	m_request(a_request),
	m_relay_received_callback(a_relay_received_callback)
{
	if (affix_base::cryptography::rsa_to_base64_string(a_authenticated_connection->m_transmission_security_manager.m_security_information->m_local_key_pair.public_key)
		!= a_request.m_path[a_request.m_path_index])
	{
		// We have an issue, the current identity in the path does not match our identity. Respond with an error
		message_rsp_relay l_response(message_rqt_relay::processing_status_response_type::error_identity_not_reached);

		// Send the response message
		a_authenticated_connection->async_send_message(l_response, m_dispatcher.dispatch([](bool){}));

		// Just return on failure
		return;

	}

	if (a_request.m_path_index == a_request.m_path.size() - 1)
	{
		// This module is the destination module of this relay request,

		// Create the response
		message_rsp_relay l_response(message_rqt_relay::processing_status_response_type::success);
		
		// Send the response message
		a_authenticated_connection->async_send_message(l_response, m_dispatcher.dispatch([](bool){}));

		// Trigger relay received callback
		a_relay_received_callback(a_request.m_payload);

	}
	else
	{
		// Lock the mutex preventing concurrent reads/writes to application's authenticated_connections vector.
		locked_resource l_authenticated_connections = a_application.m_authenticated_connections.lock();

		// Index of next identity (in the path).
		size_t l_recipient_identity_index = a_request.m_path_index + 1;

		// The identity of module to which this message should be relayed
		std::string l_recipient_identity = a_request.m_path[l_recipient_identity_index];

		// The recipient authenticated connection, to whom this request should be relayed
		std::vector<ptr<authenticated_connection>>::iterator l_recipient_authenticated_connection =
			std::find_if(l_authenticated_connections->begin(), l_authenticated_connections->end(),
				[&](ptr<authenticated_connection> a_recipient_authenticated_connection)
				{
					return a_recipient_authenticated_connection->m_transmission_security_manager.m_security_information->m_remote_identity == l_recipient_identity;
				});

		if (l_recipient_authenticated_connection == l_authenticated_connections->end())
		{
			// Recipient is not online
			message_rsp_relay l_response(message_rqt_relay::processing_status_response_type::error_identity_not_connected);

			// Send the response message
			a_authenticated_connection->async_send_message(l_response, m_dispatcher.dispatch([](bool) {}));

			// Just return on failure
			return;
		}

		// Request intended to be relayed to the remote module.
		message_rqt_relay l_relay_request(a_request.m_path, l_recipient_identity_index, a_request.m_payload);
		
		// Send relay request
		(*l_recipient_authenticated_connection)->async_send_message(l_relay_request, m_dispatcher.dispatch([](bool){}));

	}

}
