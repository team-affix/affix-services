#pragma once
#include "affix-base/pch.h"
#include "affix-base/rsa.h"
#include "rolling_token.h"

namespace affix_services
{
	struct security_information
	{
	public:
		/// <summary>
		/// Private key, must remain private.
		/// </summary>
		CryptoPP::RSA::PrivateKey m_local_private_key;

		/// <summary>
		/// Local token, which is a repeat-attack prevention object.
		/// This token shifts by 1 after each sent message.
		/// </summary>
		affix_services::security::rolling_token m_local_token;

		/// <summary>
		/// The remote party's public key, using which all received data must be signed.
		/// </summary>
		CryptoPP::RSA::PublicKey m_remote_public_key;

		/// <summary>
		/// Remote token, which is a repeat-attack prevention object. If the remote can present this token to us, along with
		/// a valid signature, we know that the transmission is authentic. 
		/// This token shifts by 1 after each received message.
		/// </summary>
		affix_services::security::rolling_token m_remote_token;

	public:
		/// <summary>
		/// Constructor, which initializes all necessary security fields.
		/// </summary>
		/// <param name="a_local_private_key"></param>
		/// <param name="a_local_token"></param>
		/// <param name="a_remote_public_key"></param>
		/// <param name="a_remote_token"></param>
		security_information(
			const CryptoPP::RSA::PrivateKey& a_local_private_key,
			const affix_services::security::rolling_token& a_local_token,
			const CryptoPP::RSA::PublicKey& a_remote_public_key,
			const affix_services::security::rolling_token& a_remote_token
		);

	};
}
