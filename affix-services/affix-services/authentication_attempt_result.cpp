#include "authentication_attempt_result.h"

using namespace affix_services;

authentication_attempt_result::authentication_attempt_result(
	affix_base::data::ptr<connection_information> a_connection_information,
	const bool& a_successful,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const std::vector<uint8_t>& a_remote_seed,
	const std::vector<uint8_t>& a_local_seed
) :
	m_connection_information(a_connection_information),
	m_successful(a_successful),
	m_remote_public_key(a_remote_public_key),
	m_remote_seed(a_remote_seed),
	m_local_seed(a_local_seed)
{
	
}
