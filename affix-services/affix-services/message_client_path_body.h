#pragma once
#include "message_header.h"

namespace affix_services
{
	class message_client_path_body : public affix_base::data::serializable
	{
	public:
		/// <summary>
		/// The path to a source client.
		/// </summary>
		std::vector<std::string> m_client_path;

		/// <summary>
		/// Boolean describing whether or not the path should be validated.
		/// </summary>
		bool m_register = false;

	public:
		/// <summary>
		/// Default constructor for the message body.
		/// </summary>
		message_client_path_body(

		);

		/// <summary>
		/// Value-initializing constructor for the message body.
		/// </summary>
		/// <param name="a_client_path"></param>
		message_client_path_body(
			const std::vector<std::string>& a_client_path,
			const bool& a_register
		);

		/// <summary>
		/// Copy constructor for the message body.
		/// </summary>
		/// <param name="a_message_client_path_body"></param>
		message_client_path_body(
			const message_client_path_body& a_message_client_path_body
		);

		/// <summary>
		/// Returns a message header populated with information regarding this message body.
		/// </summary>
		/// <returns></returns>
		message_header<message_types, affix_base::details::semantic_version_number> create_message_header(

		) const;

	};
}
