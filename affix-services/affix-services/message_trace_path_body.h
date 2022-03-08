#pragma once
#include "affix-base/pch.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/tree.h"
#include "message_types.h"
#include "message_header.h"
#include "agent_information.h"

namespace affix_services
{
	class message_trace_path_body : public affix_base::data::serializable
	{
	public:
		std::vector<std::string> m_path;

	public:
		message_trace_path_body(
			const std::vector<std::string>& a_path = {}
		);

		message_trace_path_body(
			const message_trace_path_body& a_message_rqt_index_body
		);

		message_header create_message_header(

		) const;

	};
}
