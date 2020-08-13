#pragma once 
#include <string>
#include <vector>

#define uint unsigned int
#define uint8 uint8_t
#define uint16 uint16_t
#define uint32 uint32_t
#define uint64 uint64_t

#define REQUEST_TYPE_DEFAULT (uint64_t)0
#define REQUEST_TYPE_GET_VERSION (uint64_t)1

#define REQUEST_SERIALIZATION_SUCCESS 0
#define REQUEST_SERIALIZATION_ERROR 1

using namespace std;

struct request {
	string tag;
};

struct request_get_version {
	string tag;
};

uint16 serialized_size(request& src);
uint16 serialized_size(request_get_version& src);

int serialize(char* dst, request& src);
int serialize(char* dst, request_get_version& src);

int deserialize(request& dst, char* src);
int deserialize(request_get_version& dst, char* src);