#include "message_identity_authenticate_2.h"

using namespace affix_services::messaging;

message_identity_authenticate_2::message_identity_authenticate_2(identity_authenticate_1_status_response_type a_status) {
	m_status = a_status;
}

message_result message_identity_authenticate_2::try_serialize(byte_buffer& a_data) {

	try {
		a_data.push_back(m_status);
	}
	catch (std::exception& ex) {
		LOG("[ SERIALIZE ] Error: failed to serialize message_identity_authenticate_2: " << ex.what());
		return message_result::error_serializing_data;
	}

	return message_result::success;

}

message_result message_identity_authenticate_2::try_deserialize(byte_buffer& a_data, message_identity_authenticate_2& a_message) {

	try {
		a_data.pop_front(a_message.m_status);
	}
	catch (std::exception& ex) {
		LOG("[ SERIALIZE ] Error: failed to deserialize message_identity_authenticate_2: " << ex.what());
		return message_result::error_deserializing_data;
	}

	return message_result::success;

}
