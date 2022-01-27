#include "authentication_attempt_result.h"

using namespace affix_services;

authentication_attempt_result::authentication_attempt_result(
	const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
	const bool& a_successful,
	const bool& a_inbound_connection,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const std::vector<uint8_t>& a_remote_seed,
	const std::vector<uint8_t>& a_local_seed
) :
	m_socket(a_socket),
	m_successful(a_successful),
	m_remote_public_key(a_remote_public_key),
	m_remote_seed(a_remote_seed),
	m_local_seed(a_local_seed),
	m_inbound_connection(a_inbound_connection)
{
	
}
