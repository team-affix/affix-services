#pragma once
#include "affix-base/async_authenticate.h"
#include "authentication_attempt_result.h"
#include "affix-base/threading.h"

namespace affix_services
{
	struct authentication_attempt
	{
	protected:
		/// <summary>
		/// Defines the inclusive minimum lifetime (in seconds) for the authentication attempt to be considered expired.
		/// </summary>
		static uint64_t s_expire_time;

		/// <summary>
		/// Holds the UTC time for when this authentication request was created.
		/// </summary>
		uint64_t m_start_time = 0;

		/// <summary>
		/// From affix-base, object which performs actual asynchronous authentication of both the local and the remote.
		/// </summary>
		affix_base::data::ptr<affix_base::networking::async_authenticate> m_async_authenticate;

		/// <summary>
		/// The endpoint which the socket is connected to.
		/// </summary>
		asio::ip::tcp::endpoint m_remote_endpoint;

		/// <summary>
		/// The endpoint which the socket is bound to.
		/// </summary>
		asio::ip::tcp::endpoint m_local_endpoint;

	public:
		/// <summary>
		/// Guard preventing concurrent reads/writes to the socket.
		/// </summary>
		affix_base::networking::socket_io_guard m_socket_io_guard;

		/// <summary>
		/// Boolean describing whether the asynchronous authenticate request has finished.
		/// </summary>
		affix_base::threading::guarded_resource<bool, affix_base::threading::cross_thread_mutex> m_finished = false;

		/// <summary>
		/// Boolean describing whether the connection was established in an inbound fashion.
		/// </summary>
		bool m_inbound_connection = false;

	public:
		virtual ~authentication_attempt(

		);
		/// <summary>
		/// Constructor which receives all necessary authentication initialization data.
		/// </summary>
		/// <param name="a_socket"></param>
		/// <param name="a_remote_seed"></param>
		/// <param name="a_local_key_pair"></param>
		/// <param name="a_authenticate_remote_first"></param>
		authentication_attempt(
			affix_base::data::ptr<asio::ip::tcp::socket> a_socket,
			const asio::ip::tcp::endpoint& a_remote_endpoint,
			const asio::ip::tcp::endpoint& a_local_endpoint,
			const std::vector<uint8_t>& a_remote_seed,
			const affix_base::cryptography::rsa_key_pair& a_local_key_pair,
			const bool& a_inbound_connection,
			affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<authentication_attempt_result>>, affix_base::threading::cross_thread_mutex>& a_authentication_attempt_results
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
