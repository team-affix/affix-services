#pragma once
#include <string>
#include <vector>

#define uint unsigned int
#define uint8 uint8_t
#define uint16 uint16_t
#define uint32 uint32_t
#define uint64 uint64_t

#define RESPONSE_TYPE_DEFAULT (uint64_t)0

#define RESPONSE_SERIALIZATION_SUCCESS 0
#define RESPONSE_SERIALIZATION_ERROR 1

using namespace std;

struct response {
	string tag;
};

struct response_get_version {
	string tag;
	uint16 version_major;
	uint16 version_minor;
	uint16 version_patch;
};

uint16 serialized_size(response& src);
uint16 serialized_size(response_get_version& src);

int serialize(char* dst, response& src);
int serialize(char* dst, response_get_version& src);

int deserialize(response& dst, char* src);
int deserialize(response_get_version& dst, char* src);