#pragma once
#include "affix-base/async_authenticate.h"

namespace affix_services_application
{
	struct authentication_attempt
	{
	protected:
		/// <summary>
		/// Defines the inclusive minimum lifetime (in seconds) for the authentication attempt to be considered expired.
		/// </summary>
		static uint64_t s_expire_time;

		/// <summary>
		/// Guard for IO operations on the socket, prevents concurrent async read/write requests.
		/// </summary>
		affix_base::networking::socket_io_guard m_socket_io_guard;

		/// <summary>
		/// Holds the UTC time for when this authentication request was created.
		/// </summary>
		uint64_t m_start_time = 0;

	public:
		/// <summary>
		/// Socket, the connection interface.
		/// </summary>
		affix_base::data::ptr<asio::ip::tcp::socket> m_socket;

		/// <summary>
		/// Actual asynchronous authentication object. Handles authenticating the local and the remote.
		/// </summary>
		affix_base::data::ptr<affix_base::networking::async_authenticate> m_async_authenticate;

		/// <summary>
		/// Boolean suggesting whether or not authentication of both local and remote has succeeded.
		/// </summary>
		affix_base::data::ptr<bool> m_authenticated = new bool(false);

	public:
		/// <summary>
		/// Constructor which receives all necessary authentication initialization data.
		/// </summary>
		/// <param name="a_socket"></param>
		/// <param name="a_remote_seed"></param>
		/// <param name="a_local_key_pair"></param>
		/// <param name="a_authenticate_remote_first"></param>
		authentication_attempt(
			const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
			const std::vector<uint8_t>& a_remote_seed,
			const affix_base::cryptography::rsa_key_pair& a_local_key_pair,
			const bool& a_authenticate_remote_first
		);

	public:
		/// <summary>
		/// Returns whether or not the authentication attempt has expired.
		/// </summary>
		/// <returns></returns>
		bool expired(

		) const;

	protected:
		/// <summary>
		/// Returns the lifetime of this authentication request in seconds.
		/// </summary>
		/// <returns></returns>
		uint64_t lifetime(

		) const;

	};
}
