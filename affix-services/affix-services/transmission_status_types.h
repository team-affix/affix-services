#pragma once
#include "affix-base/pch.h"

namespace affix_services {
	namespace messaging {

		enum class transmission_status_types : uint16_t {

			unknown = 0,
			success = 1,
			error_encrypting_data = 2,
			error_signing_data = 3,
			error_decrypting_data = 4,
			error_token_invalid = 5,
			error_signature_invalid = 6,

		};

	}
}
