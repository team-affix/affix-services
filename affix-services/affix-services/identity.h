#pragma once
#include "affix-base/pch.h"
#include "cryptopp/rsa.h"

namespace affix_services {
	namespace security {

		using std::vector;
		using CryptoPP::RSA;

		class identity {
		public:
			RSA::PublicKey m_public_key;

		public:
			bool initialized() const;

		};

	}
}
