#pragma once
#include "overhead_include.h"
#include "overhead_vars.h"

enum log_dst {
	console_error = 1,
	console_success = 2,
	console_warning = 4,
	file_error = 8,
	file_success = 16,
	file_warning = 32,
};

inline log_dst operator|(log_dst a, log_dst b)
{
	return static_cast<log_dst>(static_cast<int>(a) | static_cast<int>(b));
}

enum log_result {
	none = 0,
	success = 1,
	error = 2,
	warning = 4,
};

inline log_result operator|(log_result a, log_result b)
{
	return static_cast<log_result>(static_cast<int>(a) | static_cast<int>(b));
}

inline log_dst i_log_dst;

void log(const string data, log_result result);