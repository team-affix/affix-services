#include "message_header.h"

using affix_services::messaging::message_header;
using namespace affix_services::messaging;

message_header::message_header(uint32_t a_discourse_id, message_types a_message_type, transmission_result a_transmission_status_type) {

	m_affix_services_version = details::affix_services_version;
	m_discourse_id = a_discourse_id;
	m_message_type = a_message_type;
	m_transmission_status_type = a_transmission_status_type;

}

message_result message_header::try_serialize(byte_buffer& a_data) {

	try {
		a_data.push_back(m_affix_services_version);
		a_data.push_back(m_discourse_id);
		a_data.push_back(m_message_type);
		a_data.push_back(m_transmission_status_type);
	}
	catch (std::exception& ex) {
		LOG("[ SERIALIZE ] Error: failed to serialize message_header: " << ex.what());
		return message_result::error_serializing_data;
	}

	return message_result::success;

}

message_result message_header::try_deserialize(byte_buffer& a_data, message_header& a_message) {

	try {

		a_data.pop_front(a_message.m_affix_services_version);

		if (a_message.m_affix_services_version.m_major != details::affix_services_version.m_major ||
			a_message.m_affix_services_version.m_minor != details::affix_services_version.m_minor) {
			return message_result::error_version_mismatch;
		}

		a_data.pop_front(a_message.m_discourse_id);
		a_data.pop_front(a_message.m_message_type);
		a_data.pop_front(a_message.m_transmission_status_type);

	}
	catch (std::exception& ex) {
		LOG("[ DESERIALIZE ] Error: failed to deserialize message_header: " << ex.what());
		return message_result::error_deserializing_data;
	}

	return message_result::success;

}
