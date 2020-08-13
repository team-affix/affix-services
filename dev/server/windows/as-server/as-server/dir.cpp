#include "dir.h"


void fix_program_directory() {
	if (s_program_directory_state & dir_main_exists != 0) {
		log("dir_main missing.", log_result::warning);
	}
	if (s_program_directory_state & dir_server_exists != 0) {
		log("dir_server missing.", log_result::warning);
	}
	if (s_program_directory_state & dir_accounts_exists != 0) {
		log("dir_accounts missing.", log_result::warning);
	}
	if (s_program_directory_state & dir_accounts_registered_exists != 0) {
		log("dir_accounts_registered missing.", log_result::warning);
	}
	if (s_program_directory_state & dir_accounts_banned_exists != 0) {
		log("dir_accounts_banned missing.", log_result::warning);
	}
	if (s_program_directory_state & dir_machines_exists != 0) {
		log("dir_machines missing.", log_result::warning);
	}
	if (s_program_directory_state & dir_logs_exists != 0) {
		log("dir_logs missing.", log_result::warning);
	}
	if (s_program_directory_state & fil_log_exists != 0) {
		log("fil_log missing.", log_result::warning);
	}
}

void refresh_program_directory() {
	uint result = 0;
	if (fs::exists(dir_main)) {
		result |= dir_main_exists;
	}
	if (fs::exists(dir_server)) {
		result |= dir_server_exists;
	}
	if (fs::exists(dir_accounts)) {
		result |= dir_accounts_exists;
	}
	if (fs::exists(dir_accounts_registered)) {
		result |= dir_accounts_registered_exists;
	}
	if (fs::exists(dir_accounts_banned)) {
		result |= dir_accounts_banned_exists;
	}
	if (fs::exists(dir_machines)) {
		result |= dir_machines_exists;
	}
	if (fs::exists(dir_logs)) {
		result |= dir_logs_exists;
	}
	if (fs::exists(fil_log)) {
		result |= fil_log_exists;
	}
	s_program_directory_state = result;
}