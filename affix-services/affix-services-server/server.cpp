#include "server.h"

using namespace affix_services;

void server::start_accept() {
	m_acceptor.async_accept([&](asio::error_code a_ec, tcp::socket a_socket) { accept_callback(a_ec, std::move(a_socket)); });
}

void server::stop_accept() {
	m_acceptor.cancel();
}

void server::accept_callback(asio::error_code a_ec, tcp::socket a_socket) {

}

void server::clean_connections() {
	for (int i = 0; i < m_connections.size(); i++) {
		if (m_connections[i])
	}
}

void server::clean_connection(deque<ptr<connection>>::iterator a_connection) {

}


void server::process_connections() {

}

void server::process_connection(deque<ptr<connection>>::iterator a_connection) {

}
