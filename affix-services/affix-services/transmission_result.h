#pragma once
#include "affix-base/pch.h"
#include <map>

namespace affix_services {
	namespace networking {

		enum class transmission_result : uint16_t {
			unknown = 0,
			success,
			error_encrypting_data,
			error_signing_data,
			error_decrypting_data,
			error_authenticating_data,
			error_packing_signature,
			error_unpacking_signature,
			error_packing_token,
			error_unpacking_token,
			error_version_mismatch,
			error_serializing_data,
			error_deserializing_data,
			error_unpacking_message_body,
			error_unpacking_message_header
		};

		inline std::map<transmission_result, std::string> transmission_result_strings = {
			{transmission_result::unknown, "UNKNOWN"},
			{transmission_result::success, "SUCCESS"},
			{transmission_result::error_encrypting_data, "ERROR_ENCRYPTING_DATA"},
			{transmission_result::error_signing_data, "ERROR_SIGNING_DATA"},
			{transmission_result::error_decrypting_data, "ERROR_DECRYPTING_DATA"},
			{transmission_result::error_authenticating_data, "ERROR_AUTHENTICATING_DATA"},
			{transmission_result::error_packing_signature, "ERROR_PACKING_SIGNATURE"},
			{transmission_result::error_unpacking_signature, "ERROR_UNPACKING_SIGNATURE"},
			{transmission_result::error_packing_token, "ERROR_PACKING_TOKEN"},
			{transmission_result::error_unpacking_token, "ERROR_UNPACKING_TOKEN"},
			{transmission_result::error_version_mismatch, "ERROR_VERSION_MISMATCH"},
			{transmission_result::error_serializing_data, "ERROR_SERIALIZING_DATA"},
			{transmission_result::error_deserializing_data, "ERROR_DESERIALIZING_DATA"},
			{transmission_result::error_unpacking_message_body, "ERROR_UNPACKING_MESSAGE_BODY"},
			{transmission_result::error_unpacking_message_header, "ERROR_UNPACKING_MESSAGE_HEADER"},
		};

	}
}
