#pragma once
#include "affix-base/semantic_version_number.h"

namespace affix_services {
	namespace details {

		using affix_base::details::semantic_version_number;

		inline semantic_version_number affix_services_version = {0, 0, 0};

	}
}
