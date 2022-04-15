#pragma once
#include "affix-base/pch.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/tree.h"
#include "message_types.h"
#include "message_header.h"
#include "agent_information.h"

namespace affix_services
{
	class message_agent_information_body : public affix_base::data::serializable
	{
	public:
		/// <summary>
		/// The path of all identities through whom this message has been mediated.
		/// </summary>
		std::string m_client_identity;
		
		/// <summary>
		/// The information about the agent who owns the source client.
		/// </summary>
		agent_information m_agent_information;

	public:
		/// <summary>
		/// Defualt constructor for the message body.
		/// </summary>
		message_agent_information_body(

		);

		/// <summary>
		/// Constructor for message body initializes the fields to the argued values.
		/// </summary>
		/// <param name="a_client_identity"></param>
		/// <param name="a_agent_information"></param>
		message_agent_information_body(
			const std::string& a_client_identity,
			const agent_information& a_agent_information
		);

		/// <summary>
		/// Copy constructor for the message body.
		/// </summary>
		/// <param name="a_message_rqt_index_body"></param>
		message_agent_information_body(
			const message_agent_information_body& a_message_rqt_index_body
		);

		/// <summary>
		/// Returns a message header with information about this message body and type.
		/// </summary>
		/// <returns></returns>
		message_header<message_types, affix_base::details::semantic_version_number> create_message_header(

		) const;

	};
}
