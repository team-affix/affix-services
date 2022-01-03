#pragma once
#include "affix-base/pch.h"

namespace affix_services {
	namespace security {

		const size_t AS_SEED_SIZE = 25;

		class rolling_token {
		public:
			std::vector<uint8_t> m_seed;
			uint64_t m_index = 0;

		public:
			rolling_token();
			rolling_token(const std::vector<uint8_t>& a_seed, const uint64_t& a_index = 0);

		public:
			void operator++();

		public:
			std::vector<uint8_t> serialize() const;

		public:
			bool initialized() const;

		};

	}
}
