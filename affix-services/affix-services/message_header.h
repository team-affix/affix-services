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

	class message_header : public affix_base::data::serializable
	{
	public:
		/// <summary>
		/// The default size of discourse identifiers
		/// </summary>
		static size_t s_discourse_identifier_size;

	public:
		/// <summary>
		/// The version of affix services in the sender module
		/// </summary>
		affix_base::details::semantic_version_number m_affix_services_version = affix_services::i_affix_services_version;

		/// <summary>
		/// The type of message being sent
		/// </summary>
		message_types m_message_type = message_types::unknown;

		/// <summary>
		/// A random string used to identify all future messages corresponding to this one.
		/// </summary>
		std::string m_discourse_identifier;

	public:
		/// <summary>
		/// Default constructor. Initializes all fields to their types' default values.
		/// </summary>
		message_header(

		);

		/// <summary>
		/// Constructs the message header with the argued field values.
		/// </summary>
		/// <param name="a_discourse_id"></param>
		/// <param name="a_message_type"></param>
		/// <param name="a_transmission_result"></param>
		message_header(
			const message_types& a_message_type,
			const std::string& a_discourse_identifier
		);

		/// <summary>
		/// Manually define copy constructor since this class is serializable
		/// </summary>
		/// <param name="a_message_header"></param>
		message_header(
			const message_header& a_message_header
		);

		/// <summary>
		/// Generates a random discourse id, which must be unique for proper functionality.
		/// </summary>
		static std::string random_discourse_identifier(

		);

	};

}
