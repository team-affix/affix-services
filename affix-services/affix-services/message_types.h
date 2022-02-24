#pragma once
#include "affix-base/pch.h"

namespace affix_services {

	enum class message_types : uint8_t {

		unknown = 0,

		rqt_relay,				// relay data to connected party
		rsp_relay,

		rqt_index,
		rsp_index

	};
		
}
