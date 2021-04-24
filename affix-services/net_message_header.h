#pragma once
#include "pch.h"

using std::vector;

namespace affix_services {
	namespace net_common {
		struct message_header {
			uint32_t id{};
			uint32_t size = 0;
		};
	}
}