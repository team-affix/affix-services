#include "message_identity_authenticate_0.h"

using namespace affix_services::messaging;

message_identity_authenticate_0::message_identity_authenticate_0(const vector<uint8_t>& a_seed) {
	m_seed = a_seed;
}

serialization_result message_identity_authenticate_0::try_serialize(byte_buffer& a_data) {

	try {
		a_data << m_seed;
	}
	catch (std::exception& ex) {
		LOG("[ SERIALIZE ] Error: failed to serialize message_identity_authenticate_0: " << ex.what());
		return serialization_result::error_serializing_data;
	}

	return serialization_result::success;

}

serialization_result message_identity_authenticate_0::try_deserialize(byte_buffer& a_data, message_identity_authenticate_0& a_message) {

	try {
		a_data >> a_message.m_seed;
	}
	catch (std::exception& ex) {
		LOG("[ DESERIALIZE ] Error: failed to deserialize message_identity_authenticate_0: " << ex.what());
		return serialization_result::error_deserializing_data;
	}

	return serialization_result::success;

}
