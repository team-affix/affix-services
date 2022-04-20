#pragma once
#include "affix-base/pch.h"

namespace affix_services {

	enum class message_types : uint8_t {

		unknown = 0,

		relay,				// relay data to connected party
		client_information,
		client_path

	};
		
}
