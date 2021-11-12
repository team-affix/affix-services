#include "connection.h"
#include "affix-base/pch.h"
#include "affix-base/utc_time.h"
#include "affix-base/rsa.h"
#include "affix-base/catch_friendly_assert.h"

using namespace affix_services::networking;
using affix_base::timing::utc_time;
using asio::ip::tcp;
using namespace affix_base::networking;
using namespace affix_base::cryptography;

#if 1
#define LOG(x) std::cout << x <<std::endl
#else
#define LOG(x)
#endif

connection::connection(tcp::socket& a_socket) : m_socket(std::move(a_socket)), m_socket_io_guard(m_socket), m_start_time(utc_time()) {

}

bool connection::async_send(const vector<uint8_t>& a_message_data, const RSA::PrivateKey& a_private_key, const function<void(bool)>& a_callback) {

	vector<uint8_t> l_final;

	if (!m_message_security_manager.export_transmission(a_message_data, l_final)) return false;
	LOG("[ CONNECTION ] Exported message without error.");

	m_socket_io_guard.async_send(l_final, a_callback);
	LOG("[ CONNECTION ] Began async send request.");

	return true;

}

void connection::async_receive(transmission& a_message, const RSA::PrivateKey& a_private_key, const function<void(bool)>& a_callback) {

	ptr<vector<uint8_t>> l_data = new vector<uint8_t>();

	m_socket_io_guard.async_receive(l_data.val(), [&, l_data, a_private_key, a_callback] (bool a_result) {

		if (!a_result) {
			LOG("[ CONNECTION ] Error receiving data.");
			a_callback(false);
			return;
		}

		if (!m_message_security_manager.import_transmission(l_data.val(), a_message)) {
			a_callback(false);
			return;
		}

		a_callback(true);

	});
	LOG("[ CONNECTION ] Began async receive request.");

}

bool connection::secured() const {
	return m_message_security_manager.secured();
}

uint64_t connection::lifetime() const {
	return utc_time() - m_start_time;
}
