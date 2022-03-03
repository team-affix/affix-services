#pragma once
#include "affix-base/pch.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/tree.h"
#include "message_types.h"
#include "message_header.h"

namespace affix_services
{
	class message_rqt_index_body : public affix_base::data::serializable
	{
	public:
		affix_base::data::tree<std::tuple<std::string, std::string>> m_agents;

	public:
		message_rqt_index_body(

		);

		message_rqt_index_body(
			const affix_base::data::tree<std::tuple<std::string, std::string>>& a_agents
		);

		message_rqt_index_body(
			const message_rqt_index_body& a_message_rqt_index_body
		);

		message_header create_message_header(

		) const;

	};
}
