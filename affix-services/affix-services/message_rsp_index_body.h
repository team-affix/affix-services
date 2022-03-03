#pragma once
#include "affix-base/pch.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/tree.h"
#include "message_types.h"
#include "message_header.h"
#include "message_rqt_index_body.h"

namespace affix_services
{
	class message_rsp_index_body : public affix_base::data::serializable
	{
	public:
		affix_base::data::tree<std::string> m_identities;

	public:
		message_rsp_index_body(

		);

		message_rsp_index_body(
			const affix_base::data::tree<std::string> a_identities
		);

		message_rsp_index_body(
			const message_rsp_index_body& a_message_rsp_index_body
		);

		message_header create_message_header(
			const message_header& a_request_message_header
		) const;

	};
}
