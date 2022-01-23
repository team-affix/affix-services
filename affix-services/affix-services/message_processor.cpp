#include "message_processor.h"

#if 1
#define LOG_ERROR(x) std::cerr << x << std::endl;
#else
#define LOG_ERROR(x)
#endif

using namespace affix_services;
using affix_base::data::ptr;
using affix_base::data::byte_buffer;
using namespace affix_services::networking;
using namespace affix_services::messaging;

void message_processor::process(
	affix_base::data::ptr<affix_services::networking::connection_async_receive_result> a_connection_async_receive_result
)
{
	std::vector<uint8_t> l_message_header_data;

	// Try to unpack message header data.
	if (!a_connection_async_receive_result->m_byte_buffer.pop_front(l_message_header_data))
	{
		LOG_ERROR("[ MESSAGE PROCESSOR ] Error: unable to unpack message header from received data.");
		return;
	}

	// Transmission result regarding ability to understand the message header
	transmission_result l_transmission_result;

	// Byte buffer from which elements of the message header will be unpacked
	byte_buffer l_message_header_byte_buffer(l_message_header_data);

	message_header l_message_header;

	// Try to deserialize the message header.
	if (!l_message_header.deserialize(l_message_header_byte_buffer, l_transmission_result))
	{
		LOG_ERROR("[ MESSAGE PROCESSOR ] Error:" << transmission_result_strings[l_transmission_result]);
		return;
	}

	std::vector<uint8_t> l_message_body_data;

	// Try to unpack message body data.
	if (!a_connection_async_receive_result->m_byte_buffer.pop_front(l_message_body_data))
	{
		LOG_ERROR("[ MESSAGE PROCESSOR ] Error: unable to unpack message body from received data.");
		return;
	}

	// Byte buffer from which elements of the message body will be unpacked
	byte_buffer l_message_body_byte_buffer(l_message_body_data);

	switch (l_message_header.m_message_type)
	{
		case affix_services::messaging::message_types::rqt_identity_push:
		{
			deserialize_and_process_affix_services_message<affix_services::messaging::message_rqt_identity_push>(
				l_message_body_byte_buffer,
				a_connection_async_receive_result->m_owner,
				l_message_header
			);
			break;
		}
		case affix_services::messaging::message_types::rsp_identity_push:
		{
			deserialize_and_process_affix_services_message<affix_services::messaging::message_rsp_identity_push>(
				l_message_body_byte_buffer,
				a_connection_async_receive_result->m_owner,
				l_message_header
			);
			break;
		}

	}


}

template<typename MESSAGE_TYPE>
void message_processor::deserialize_and_process_affix_services_message(
	affix_base::data::byte_buffer& a_body_byte_buffer,
	const affix_base::data::ptr<affix_services::networking::connection>& a_owner,
	const affix_services::messaging::message_header& a_message_header

)
{
	MESSAGE_TYPE l_message_body;
	typename MESSAGE_TYPE::deserialization_status_response_type l_deserialization_status_response;

	if (!l_message_body.deserialize(a_body_byte_buffer, l_deserialization_status_response))
	{
		return;
	}

	typename MESSAGE_TYPE::processing_status_response_type l_processing_status_response;

	process_message_declaration<MESSAGE_TYPE> l_process_declaration({ a_owner, a_message_header, l_message_body } );
	process_affix_services_message(l_process_declaration);

}

template<typename MESSAGE_TYPE>
void message_processor::process_affix_services_message(
	const process_message_declaration<MESSAGE_TYPE>& a_process_message_declaration
)
{

}

template<>
void message_processor::process_affix_services_message<affix_services::messaging::message_rqt_identity_push>(
	const process_message_declaration<affix_services::messaging::message_rqt_identity_push>& a_process_message_declaration
)
{

}

template<>
void message_processor::process_affix_services_message<affix_services::messaging::message_rsp_identity_push>(
	const process_message_declaration<affix_services::messaging::message_rsp_identity_push>& a_process_message_declaration
)
{

}
