#include "affix_services.h"
#include <iostream>

#include "asio.hpp"
#include "asio/ts/buffer.hpp"
#include <asio/ts/internet.hpp>

int main() {
	
	asio::error_code ec;
	asio::io_context context;
	std::string ip_addr_0 = "93.184.216.34";
	std::string ip_addr_1 = "170.3.3.3";
	asio::ip::tcp::endpoint ep(asio::ip::make_address(ip_addr_1, ec), 80);
	asio::ip::tcp::socket sock(context);
	
	sock.connect(ep, ec);

	if (!ec) {
		std::cout << "Connected" << std::endl;
		sock.close(ec);
	}

	return 0;
}