#pragma once
#include "affix-base/pch.h"
#include "affix-base/ptr.h"
#include "asio.hpp"
#include "affix-base/rsa.h"
#include "connection_information.h"
#include "security_information.h"

namespace affix_services
{
	struct authentication_result
	{
	public:
		/// <summary>
		/// Holds relevant information about the connection.
		/// </summary>
		affix_base::data::ptr<connection_information> m_connection_information;

		/// <summary>
		/// Holds relevant information about the security for the connection.
		/// </summary>
		affix_base::data::ptr<security_information> m_security_information;

		/// <summary>
		/// Boolean describing whether the asynchronous authentication attempt was successful.
		/// </summary>
		bool m_successful;

	public:
		/// <summary>
		/// Initializes the structure with all necessary information regarding the authentication.
		/// </summary>
		/// <param name="a_connection_information"></param>
		/// <param name="a_security_information"></param>
		/// <param name="a_successful"></param>
		authentication_result(
			affix_base::data::ptr<connection_information> a_connection_information,
			affix_base::data::ptr<security_information> a_security_information,
			const bool& a_successful
		);

	};
}
