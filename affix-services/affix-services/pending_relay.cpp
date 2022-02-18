#include "pending_relay.h"

using namespace affix_services;
using namespace affix_base::data;
using namespace affix_base::threading;

pending_relay::pending_relay(
	const affix_services::message_rqt_relay& a_request,
	affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<pending_relay>>, affix_base::threading::cross_thread_mutex>& a_pending_relays,
	affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<relay_result>>, affix_base::threading::cross_thread_mutex>& a_relay_results
) :
	m_request(a_request),
	m_pending_relays(a_pending_relays),
	m_relay_results(a_relay_results)
{

	locked_resource l_pending_relays = m_pending_relays.lock();



}
