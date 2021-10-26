#pragma once
#include "affix-base/pch.h"

namespace affix_services {
	namespace messaging {

		enum class serialization_result : uint8_t {

			success = 0,
			error_version_mismatch = 1,
			error_deserializing_data = 2,
			error_serializing_data = 3,

		};

	}
}
