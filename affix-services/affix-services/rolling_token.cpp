#include "rolling_token.h"
#include "cryptopp/files.h"
#include "cryptopp/modes.h"
#include "cryptopp/osrng.h"
#include "cryptopp/rsa.h"
#include "cryptopp/sha.h"
#include "cryptopp/pssr.h"
#include "affix-base/byte_buffer.h"

using namespace affix_services::security;
using CryptoPP::AutoSeededRandomPool;
using affix_base::data::byte_buffer;

rolling_token::rolling_token() {

}

rolling_token::rolling_token(const vector<uint8_t>& a_seed, const uint64_t& a_index) : m_seed(a_seed), m_index(a_index) {

}

void rolling_token::operator++() {
	m_index++;
}

vector<uint8_t> rolling_token::serialize() const {

	byte_buffer result;
	result.push_back(m_seed);
	result.push_back(m_index);
	return result.data();

}

bool rolling_token::initialized() const {
	return m_seed.size() > 0;
}
