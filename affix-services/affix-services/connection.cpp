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

connection::connection(tcp::socket& a_socket) : m_socket(std::move(a_socket)), m_socket_io_guard(m_socket), m_start_time(utc_time()) {

}

void connection::async_send(const vector<uint8_t>& a_message_data, const RSA::PrivateKey& a_private_key, const function<void(bool)>& a_callback) {

	vector<uint8_t> l_final;

	transmission_result l_transmission_result = transmission_result::unknown;

	// TRY TO EXPORT MESSAGE DATA IN "TRANSMISSION" FORMAT
	if (!m_transmission_security_manager.export_transmission(a_message_data, l_final, l_transmission_result)) {
		LOG("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);
		return;
	}

	// SEND TRANSMISSION
	m_socket_io_guard.async_send(l_final, a_callback);

}

void connection::async_receive(transmission& a_transmission, const RSA::PrivateKey& a_private_key, const function<void(bool)>& a_callback) {

	// ALLOCATE DYNAMIC VECTOR FOR CALLBACK LAMBDA FUNCTION TO ACCESS AFTER PROGRAM EXITS THIS FUNCTION'S SCOPE
	ptr<vector<uint8_t>> l_data = new vector<uint8_t>();

	m_socket_io_guard.async_receive(l_data.val(), [&, l_data, a_private_key, a_callback] (bool a_result) {

		if (!a_result) {
			LOG("[ CONNECTION ] Error receiving data.");
			a_callback(false);
			return;
		}

		transmission_result l_transmission_result = transmission_result::unknown;

		// TRY TO "IMPORT" THE TRANSMISSION DATA
		if (!m_transmission_security_manager.import_transmission(l_data.val(), a_transmission, l_transmission_result)) {
			LOG("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);
			a_callback(false);
			return;
		}

		a_callback(true);

	});

}

bool connection::secured() const {
	return m_transmission_security_manager.secured();
}

uint64_t connection::lifetime() const {
	return utc_time() - m_start_time;
}
