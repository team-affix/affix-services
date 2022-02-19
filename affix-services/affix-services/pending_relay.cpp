#include "pending_relay.h"

using namespace affix_services;
using namespace affix_base::data;
using namespace affix_base::threading;

pending_relay::pending_relay(
	const std::function<void(const std::vector<uint8_t>&)>& a_relay_received_callback,
	const affix_base::data::ptr<affix_services::networking::authenticated_connection>& a_authenticated_connection,
	const affix_services::message_rqt_relay& a_request
)
{
	if (a_request.m_path.size() == 1)
	{
		// This module is the destination module of this relay request,
		// trigger relay received callback
		a_relay_received_callback(a_request.m_payload);



	}
	else
	{
		// Create the request intended to be relayed to the remote peer.
		message_rqt_relay l_relayed_request(a_request.m_path, a_request.m_payload);
		
		// Remove the first identity from the path, seeing as that is the local identity
		l_relayed_request.m_path.erase(l_relayed_request.m_path.begin());



	}

}
