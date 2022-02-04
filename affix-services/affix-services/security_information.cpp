#include "security_information.h"

using namespace affix_services;

security_information::security_information(
	const CryptoPP::RSA::PrivateKey& a_local_private_key,
	const affix_services::security::rolling_token& a_local_token,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const affix_services::security::rolling_token& a_remote_token
) :
	m_local_private_key(a_local_private_key),
	m_local_token(a_local_token),
	m_remote_public_key(a_remote_public_key),
	m_remote_token(a_remote_token)
{

}
