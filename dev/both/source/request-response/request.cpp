#include "../../header/request-response/request.h"

uint16 serialized_size(request& src) {
	uint16 result = 0;
	result += sizeof(uint64);
	result += src.tag.length();
	return result;
}
uint16 serialized_size(request_get_version& src) {
	uint16 result = 0;
	result += sizeof(src.tag.length());
	result += src.tag.length();
	return result;
}

int serialize(char* dst, request& src) {
	try {

		uint64 tag_len = src.tag.length();
		memcpy(dst, &tag_len, sizeof(uint64));
		dst += sizeof(uint64);

		memcpy(dst, src.tag.data(), tag_len);
		dst += tag_len;

		return REQUEST_SERIALIZATION_SUCCESS;
	}
	catch (exception ex) {
		return REQUEST_SERIALIZATION_ERROR;
	}
}
int serialize(char* dst, request_get_version& src) {
	try {

		uint64 tag_len = src.tag.length();
		memcpy(dst, &tag_len, sizeof(uint64));
		dst += sizeof(uint64);

		memcpy(dst, src.tag.data(), tag_len);
		dst += tag_len;

		return REQUEST_SERIALIZATION_SUCCESS;
	}
	catch (exception ex) {
		return REQUEST_SERIALIZATION_ERROR;
	}
}

int deserialize(request& dst, char* src) {
	try {

		uint64 tag_len;
		memcpy(&tag_len, src, sizeof(uint64));
		src += sizeof(uint64);

		dst.tag.assign(src, tag_len);
		src += tag_len;

		return REQUEST_SERIALIZATION_SUCCESS;
	}
	catch (exception ex) {
		return REQUEST_SERIALIZATION_ERROR;
	}
}
int deserialize(request_get_version& dst, char* src) {
	try {

		uint64 tag_len;
		memcpy(&tag_len, src, sizeof(uint64));
		src += sizeof(uint64);

		dst.tag.assign(src, tag_len);
		src += tag_len;

		return REQUEST_SERIALIZATION_SUCCESS;
	}
	catch (exception ex) {
		return REQUEST_SERIALIZATION_ERROR;
	}
}