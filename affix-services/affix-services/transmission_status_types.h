#pragma once
#include "affix-base/pch.h"

namespace affix_services {
	namespace messaging {

		enum class transmission_status_types {

			unknown = 0,
			success,
			error_decrypting_data,
			error_token_invalid,
			error_signature_invalid,

		};

	}
}
