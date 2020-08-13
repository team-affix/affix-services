#pragma once
#include "overhead_include.h"
#include "overhead_vars.h"
#include "log.h"

namespace fs = filesystem;

inline const uint dir_main_exists = 1;
inline const uint dir_server_exists = 2;
inline const uint dir_accounts_exists = 4;
inline const uint dir_accounts_registered_exists = 8;
inline const uint dir_accounts_banned_exists = 16;
inline const uint dir_machines_exists = 32;
inline const uint dir_logs_exists = 64;
inline const uint fil_log_exists = 128;

inline uint s_program_directory_state;

void fix_program_directory();

void refresh_program_directory();