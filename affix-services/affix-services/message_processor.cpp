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

message_processor::message_processor(
	const std::function<void(const std::vector<uint8_t>&)>& a_relay_received_callback
) :
	m_relay_received_callback(a_relay_received_callback)
{

}

void message_processor::process_async_receive_result(
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


	}

}
