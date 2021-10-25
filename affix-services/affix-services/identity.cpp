#include "identity.h"
#include "cryptopp/osrng.h"

using affix_services::security::identity;
using CryptoPP::AutoSeededRandomPool;

bool identity::initialized() const {
	AutoSeededRandomPool l_random;
	return m_public_key.Validate(l_random, 3);
}
