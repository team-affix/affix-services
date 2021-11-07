#pragma once
#include "affix-base/pch.h"

namespace affix_services {
	namespace messaging {

		enum class transmission_status_types : uint16_t {

			unknown = 0,
			success = 1,
			error_decrypting_data = 2,
			error_token_invalid = 3,
			error_signature_invalid = 4,
			error_deserializing_data = 5,

		};

	}
}
