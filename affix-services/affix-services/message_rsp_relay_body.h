#pragma once
#include "affix-base/pch.h"
#include "message_rqt_relay_body.h"
#include "message_types.h"
#include "message_header.h"

namespace affix_services
{
	class message_rsp_relay_body : public affix_base::data::serializable
	{
	public:
		std::vector<std::string> m_path;
		size_t m_path_index = 0;
		message_rqt_relay_body::processing_status_response_type m_processing_status_response =
			message_rqt_relay_body::processing_status_response_type::unknown;

	public:
		message_rsp_relay_body(

		);
		
		message_rsp_relay_body(
			const std::vector<std::string>& a_path,
			const size_t& a_path_index,
			message_rqt_relay_body::processing_status_response_type a_processing_status_response
		);

		message_rsp_relay_body(
			const message_rsp_relay_body& a_message_rsp_relay_body
		);

		message_header create_message_header(
			const message_header& a_request_message_header
		) const;

	};
}
