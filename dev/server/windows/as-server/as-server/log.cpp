#include "log.h"

void log(const string data, log_result result) {

	// check what type of log_result result is
	if ((result & log_result::error) != 0) {
		
		// if result is an error

		// write result to console
		if ((i_log_dst & log_dst::console_error) != 0) {
			cout << "[ERROR][" << time(0) << "]" << endl;
			cout << data << endl;
		}
		// write result to file
		if ((i_log_dst & log_dst::file_error) != 0) {
			if (!fil_log.empty() && ofs_log.is_open()) {
				ofs_log << "[ERROR][" << time(0) << "]" << endl;
				ofs_log << data << endl;
			}
		}
	}
	if ((result & log_result::success) != 0) {

		// if result is a success

		// write result to console
		if ((i_log_dst & log_dst::console_success) != 0) {
			cout << "[SUCCESS][" << time(0) << "]" << endl;
			cout << data << endl;
		}
		// write result to file
		if ((i_log_dst & log_dst::file_success) != 0) {
			if (!fil_log.empty() && ofs_log.is_open()) {
				ofs_log << "[SUCCESS][" << time(0) << "]" << endl;
				ofs_log << data << endl;
			}
		}
	}
	if ((result & log_result::warning) != 0) {

		// if result is a warning

		// write result to console
		if ((i_log_dst & log_dst::console_warning) != 0) {
			cout << "[WARNING][" << time(0) << "]" << endl;
			cout << data << endl;
		}
		// write result to file
		if ((i_log_dst & log_dst::file_warning) != 0) {
			if (!fil_log.empty() && ofs_log.is_open()) {
				ofs_log << "[WARNING][" << time(0) << "]" << endl;
				ofs_log << data << endl;
			}
		}
	}
	if (result == log_result::none) {
		// if result is a warning

		// write result to console
		cout << "[" << time(0) << "]" << "]" << endl;
		cout << data << endl;

		// write result to file
		if (!fil_log.empty() && ofs_log.is_open()) {
			ofs_log << time(0) << endl;
			ofs_log << data << endl;
		}
	}

}