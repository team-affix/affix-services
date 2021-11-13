#pragma once
#include "affix-base/pch.h"
#include <map>

namespace affix_services {
	namespace networking {

		using std::map;
		using std::string;

		enum class transmission_result : uint16_t {
			unknown = 0,
			success,
			error_encrypting_data,
			error_signing_data,
			error_decrypting_data,
			error_authenticating_data,
			error_packing_message_data,
			error_packing_signature,
			error_unpacking_signature,
			error_packing_token,
			error_unpacking_token,
		};

		inline map<transmission_result, string> transmission_result_strings = {
			{transmission_result::unknown, "UNKNOWN"},
			{transmission_result::success, "SUCCESS"},
			{transmission_result::error_encrypting_data, "ERROR_ENCRYPTING_DATA"},
			{transmission_result::error_signing_data, "ERROR_SIGNING_DATA"},
			{transmission_result::error_decrypting_data, "ERROR_DECRYPTING_DATA"},
			{transmission_result::error_authenticating_data, "ERROR_AUTHENTICATING_DATA"},
			{transmission_result::error_packing_message_data, "ERROR_PACKING_MESSAGE_DATA"},
			{transmission_result::error_packing_signature, "ERROR_PACKING_SIGNATURE"},
			{transmission_result::error_unpacking_signature, "ERROR_UNPACKING_SIGNATURE"},
			{transmission_result::error_packing_token, "ERROR_PACKING_TOKEN"},
			{transmission_result::error_unpacking_token, "ERROR_UNPACKING_TOKEN"},
		};

	}
}
