#include "../../header/request-response/response.h"

uint16 serialized_size(response& src) {
	uint16 result = 0;
	result += sizeof(src.tag.length());
	result += src.tag.length();
	return result;
}
uint16 serialized_size(response_get_version& src) {
	uint16 result = 0;
	result += sizeof(src.tag.length());
	result += src.tag.length();
	result += sizeof(src.version_major);
	result += sizeof(src.version_minor);
	result += sizeof(src.version_patch);
	return result;
}

int serialize(char* dst, response& src) {
	try {

		uint64 tag_len = src.tag.length();
		memcpy(dst, &tag_len, sizeof(uint64));
		dst += sizeof(uint64);

		memcpy(dst, src.tag.data(), tag_len);
		dst += tag_len;

		return RESPONSE_SERIALIZATION_SUCCESS;
	}
	catch (exception ex) {
		return RESPONSE_SERIALIZATION_ERROR;
	}
}
int serialize(char* dst, response_get_version& src) {
	try {

		uint64 tag_len = src.tag.length();
		memcpy(dst, &tag_len, sizeof(uint64));
		dst += sizeof(uint64);

		memcpy(dst, src.tag.data(), tag_len);
		dst += tag_len;

		memcpy(dst, &src.version_major, sizeof(src.version_major));
		dst += sizeof(src.version_major);

		memcpy(dst, &src.version_minor, sizeof(src.version_minor));
		dst += sizeof(src.version_minor);

		memcpy(dst, &src.version_patch, sizeof(src.version_patch));
		dst += sizeof(src.version_patch);

		return RESPONSE_SERIALIZATION_SUCCESS;
	}
	catch (exception ex) {
		return RESPONSE_SERIALIZATION_ERROR;
	}
}

int deserialize(response& dst, char* src) {
	try {

		uint64 tag_len;
		memcpy(&tag_len, src, sizeof(uint64));
		src += sizeof(uint64);

		dst.tag.assign(src, tag_len);
		src += tag_len;

		return RESPONSE_SERIALIZATION_SUCCESS;
	}
	catch (exception ex) {
		return RESPONSE_SERIALIZATION_ERROR;
	}
}
int deserialize(response_get_version& dst, char* src) {
	try {

		uint64 tag_len;
		memcpy(&tag_len, src, sizeof(uint64));
		src += sizeof(uint64);

		dst.tag.assign(src, tag_len);
		src += tag_len;

		memcpy(&dst.version_major, src, sizeof(dst.version_major));
		src += sizeof(dst.version_major);

		memcpy(&dst.version_minor, src, sizeof(dst.version_minor));
		src += sizeof(dst.version_minor);

		memcpy(&dst.version_patch, src, sizeof(dst.version_patch));
		src += sizeof(dst.version_patch);

		return RESPONSE_SERIALIZATION_SUCCESS;
	}
	catch (exception ex) {
		return RESPONSE_SERIALIZATION_ERROR;
	}
}