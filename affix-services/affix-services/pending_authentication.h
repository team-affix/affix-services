#pragma once
#include "affix-base/async_authenticate.h"
#include "authentication_result.h"
#include "affix-base/threading.h"
#include "connection_information.h"

namespace affix_services
{
	class client;

	struct pending_authentication
	{
	protected:
		/// <summary>
		/// Boolean describing the enabled status of a timeout procedure.
		/// </summary>
		bool m_enable_timeout;

		/// <summary>
		/// Defines the inclusive minimum lifetime (in seconds) for the authentication attempt to be considered expired.
		/// </summary>
		uint64_t m_timeout_in_seconds;

		/// <summary>
		/// Holds the UTC time for when this authentication request was created.
		/// </summary>
		uint64_t m_start_time = 0;

		/// <summary>
		/// From affix-base, object which performs actual asynchronous authentication of both the local and the remote.
		/// </summary>
		affix_base::data::ptr<affix_base::networking::async_authenticate> m_async_authenticate;

	public:
		/// <summary>
		/// Guard preventing concurrent reads/writes to the socket.
		/// </summary>
		affix_base::networking::socket_io_guard m_socket_io_guard;

		/// <summary>
		/// Boolean describing whether the asynchronous authenticate request has finished.
		/// </summary>
		affix_base::threading::guarded_resource<bool> m_finished = false;

		/// <summary>
		/// Holds relevant information about the connection.
		/// </summary>
		affix_base::data::ptr<connection_information> m_connection_information;

	public:
		virtual ~pending_authentication(

		);

		/// <summary>
		/// Constructor which receives all necessary authentication initialization data.
		/// </summary>
		/// <param name="a_connection_information"></param>
		/// <param name="a_remote_seed"></param>
		/// <param name="a_local_key_pair"></param>
		/// <param name="a_authentication_attempt_results"></param>
		pending_authentication(
			affix_base::data::ptr<connection_information> a_connection_information,
			const std::vector<uint8_t>& a_remote_seed,
			const affix_base::cryptography::rsa_key_pair& a_local_key_pair,
			client& a_client,
			const bool& a_enable_timeout,
			const uint64_t& a_timeout_in_seconds
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
