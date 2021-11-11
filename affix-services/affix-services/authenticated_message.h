#pragma once
#include "affix-base/pch.h"

using std::vector;

namespace affix_services {
	namespace security {
		struct authenticated_message {
			vector<uint8_t> m_signature;
			vector<uint8_t> m_token;
			vector<uint8_t> m_payload;
		};
	}
}
