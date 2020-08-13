#include "main.h"

int main() {

	i_log_dst = log_dst::console_error | log_dst::console_success;

	int serverInitResult = server_init();
	if (serverInitResult == EXIT_SUCCESS) {
		log("--server initialization success--", log_result::success);
		vector_threads.push_back(thread(server_receive));
		server_main();
	}
	else {
		log("--server initialization error--", log_result::error);
	}

	log("--application shutting down--", log_result::none);
	app_exit();
	log("--application shut down--", log_result::success);

	return 0;

}