#pragma once
#include "affix-base/pch.h"
#include <map>

using std::map;
using std::string;

namespace affix_services {
	namespace messaging {

		enum class message_result : uint16_t {
			unknown = 0,
			success,
			error_version_mismatch,
			error_serializing_data,
			error_deserializing_data,
		};

		inline map<message_result, string> message_result_strings = {
			{message_result::unknown, "UNKNOWN"},
			{message_result::success, "SUCCESS"},
			{message_result::error_version_mismatch, "ERROR_VERSION_MISMATCH"},
			{message_result::error_serializing_data, "ERROR_SERIALIZING_DATA"},
			{message_result::error_deserializing_data, "ERROR_DESERIALIZING_DATA"},
		};

	}
}
