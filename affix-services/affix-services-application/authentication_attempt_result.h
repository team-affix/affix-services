#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "asio.hpp"
#include "affix-base/rsa.h"

namespace affix_services_application
{
	struct authentication_attempt_result
	{
	public:
		/// <summary>
		/// The actual network interface.
		/// </summary>
		affix_base::data::ptr<asio::ip::tcp::socket> m_socket;

		/// <summary>
		/// Boolean describing whether the asynchronous authentication attempt was successful.
		/// </summary>
		bool m_successful;

		/// <summary>
		/// RSA key holding the public parameters for communication with the remote peer.
		/// </summary>
		CryptoPP::RSA::PublicKey m_remote_public_key;

		/// <summary>
		/// Vector of bytes which holds the seed that the peer will use in all further correspondance with us.
		/// </summary>
		std::vector<uint8_t> m_remote_seed;

		/// <summary>
		/// Vector of bytes which holds the seed that we will use in all further correspondance with the peer.
		/// </summary>
		std::vector<uint8_t> m_local_seed;

	public:
		/// <summary>
		/// Initializes the structure with all necessary information regarding the authentication.
		/// </summary>
		/// <param name="a_socket"></param>
		/// <param name="a_successful"></param>
		/// <param name="a_remote_public_key"></param>
		/// <param name="a_remote_seed"></param>
		/// <param name="a_local_seed"></param>
		authentication_attempt_result(
			const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
			const bool& a_successful,
			const CryptoPP::RSA::PublicKey& a_remote_public_key = {},
			const std::vector<uint8_t>& a_remote_seed = {},
			const std::vector<uint8_t>& a_local_seed = {}
		);

	};
}
