#include "security_information.h"

using namespace affix_services;
using namespace affix_base::cryptography;

security_information::security_information(
	const affix_base::cryptography::rsa_key_pair& a_local_key_pair,
	const affix_services::security::rolling_token& a_local_token,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const affix_services::security::rolling_token& a_remote_token
) :
	m_local_key_pair(a_local_key_pair),
	m_local_token(a_local_token),
	m_remote_public_key(a_remote_public_key),
	m_remote_token(a_remote_token)
{
	if (!rsa_to_base64_string(a_remote_public_key, m_remote_identity))
		throw std::exception("Error converting RSA public key to base64 string format.");
}
