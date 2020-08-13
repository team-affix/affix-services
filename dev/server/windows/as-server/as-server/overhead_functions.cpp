#include "overhead_functions.h"

string to_string(char* a, int len) {

	string result = "";
	for (int i = 0; i < len; i++) {
		char c = *(a + i);
		result += c;
	}
	return result;

}

void app_exit() {

	// application is exiting
	appContinue = false;
	// wait for threads to exit
	for (thread& t : vector_threads) {
		t.join();
	}
	return;

}