#include "message_identity_authenticate_1.h"
#include "message_header.h"

using namespace affix_services::messaging;

message_identity_authenticate_1::message_identity_authenticate_1(identity_authenticate_0_status_response_type a_status, const vector<uint8_t>& a_public_key) {
	m_status = a_status;
	m_public_key = a_public_key;
}

serialization_result message_identity_authenticate_1::try_serialize(byte_buffer& a_data) {

	try {
		a_data << m_status;
		a_data << m_public_key;
	}
	catch (std::exception& ex) {
		LOG("[ SERIALIZE ] Error: failed to serialize message_identity_authenticate_1: " << ex.what());
		return serialization_result::error_serializing_data;
	}

	return serialization_result::success;

}

serialization_result message_identity_authenticate_1::try_deserialize(byte_buffer& a_data, message_identity_authenticate_1& a_message) {

	try {
		a_data >> a_message.m_status;
		a_data >> a_message.m_public_key;
	}
	catch (std::exception& ex) {
		LOG("[ DESERIALIZE ] Error: failed to deserialize message_identity_authenticate_1: " << ex.what());
		return serialization_result::error_deserializing_data;
	}

	return serialization_result::success;

}
