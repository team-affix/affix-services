#pragma once
#include "affix-base/pch.h"
#include "message_types.h"
#include "transmission_result.h"
#include "version.h"
#include "affix-base/byte_buffer.h"
#include "rolling_token.h"
#include "affix-base/rsa.h"
#include <iostream>
#include "affix-base/serializable.h"

namespace affix_services {

	template<typename MESSAGE_TYPES, typename VERSION_TYPE>
	class message_header : public affix_base::data::serializable
	{
	public:
		/// <summary>
		/// The type of message being sent
		/// </summary>
		MESSAGE_TYPES m_message_type = {};

		/// <summary>
		/// The version of the sender client/agent
		/// </summary>
		VERSION_TYPE m_version = {};

	public:
		/// <summary>
		/// Default constructor. Initializes all fields to their types' default values.
		/// </summary>
		message_header(

		) :
			affix_base::data::serializable(m_message_type, m_version)
		{

		}

		/// <summary>
		/// Constructs the message header with the argued field values.
		/// </summary>
		/// <param name="a_discourse_id"></param>
		/// <param name="a_message_type"></param>
		/// <param name="a_transmission_result"></param>
		message_header(
			const MESSAGE_TYPES& a_message_type,
			const VERSION_TYPE& a_version
		) :
			affix_base::data::serializable(m_message_type, m_version),
			m_message_type(a_message_type),
			m_version(a_version)
		{

		}

		/// <summary>
		/// Manually define copy constructor since this class is serializable
		/// </summary>
		/// <param name="a_message_header"></param>
		message_header(
			const message_header& a_message_header
		) :
			message_header(
				a_message_header.m_message_type,
				a_message_header.m_version)
		{

		}

		message_header& operator=(
			const message_header& a_message_header
		)
		{
			m_message_type = a_message_header.m_message_type;
			m_version = a_message_header.m_version;
			return *this;
		}

	};

}
