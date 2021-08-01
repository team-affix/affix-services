#include <iostream>

#define _WIN32_WINNT 0x0A00
#define ASIO_STANDALONE

#include <asio.hpp>
#include <asio/ts/buffer.hpp>
#include <asio/ts/internet.hpp>

#include "interpret.h"

using namespace cfvi::interpretation;

int main() {

	interpreter i = interpreter();
	i.import({ "main.cpp" });

	asio::error_code ec;
	asio::io_context context;
	asio::ip::tcp::endpoint endpoint(asio::ip::make_address("93.184.216.34", ec), 80);
	asio::ip::tcp::socket socket(context);
	socket.connect(endpoint, ec);

	if (!ec) {
		std::cout << "CONNECTED" << std::endl;
	}
	else {
		std::cout << "ERROR" << std::endl;
	}

	return 0;
}
