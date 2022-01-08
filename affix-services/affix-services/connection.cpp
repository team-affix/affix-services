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
using affix_services::networking::connection;

connection::connection(
	asio::ip::tcp::socket& a_socket,
	const CryptoPP::RSA::PrivateKey& a_local_private_key,
	const affix_services::security::rolling_token& a_local_token,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const affix_services::security::rolling_token& a_remote_token

) :
	m_transmission_security_manager(a_local_private_key, a_local_token, a_remote_public_key, a_remote_token),
	m_socket(std::move(a_socket)),
    m_socket_io_guard(m_socket),
	m_start_time(utc_time())
{
	
}

void connection::async_send(
	const std::vector<uint8_t>& a_message_header_data,
	const std::vector<uint8_t>& a_message_body_data,
	const std::function<void(bool)>& a_callback
)
{

	vector<uint8_t> l_final;

	transmission_result l_transmission_result = transmission_result::unknown;

	byte_buffer l_message_data_buffer;
	l_message_data_buffer.push_back(a_message_header_data);
	l_message_data_buffer.push_back(a_message_body_data);

	// TRY TO EXPORT MESSAGE DATA IN "TRANSMISSION" FORMAT
	if (!m_transmission_security_manager.export_transmission(l_message_data_buffer.data(), l_final, l_transmission_result)) {
		LOG("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);
		return;
	}

	// SEND TRANSMISSION
	m_socket_io_guard.async_send(l_final, a_callback);

}

void connection::async_receive(
	std::vector<uint8_t>& a_message_header_data,
	std::vector<uint8_t>& a_message_body_data,
	const std::function<void(bool)>& a_callback
)
{

	// ALLOCATE DYNAMIC VECTOR FOR CALLBACK LAMBDA FUNCTION TO ACCESS AFTER PROGRAM EXITS THIS FUNCTION'S SCOPE
	ptr<vector<uint8_t>> l_data = new vector<uint8_t>();
	
	m_socket_io_guard.async_receive(l_data.val(), [&, l_data, a_callback](bool a_result) {

		if (!a_result) {
			LOG("[ CONNECTION ] Error receiving data.");
			a_callback(false);
			return;
		}

		transmission_result l_transmission_result = transmission_result::unknown;

		vector<uint8_t> l_message_data;

		// TRY TO "IMPORT" THE TRANSMISSION DATA
		if (!m_transmission_security_manager.import_transmission(l_data.val(), l_message_data, l_transmission_result))
		{
			LOG("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);
			a_callback(false);
			return;
		}

		byte_buffer l_message_data_buffer(l_message_data);

		if (!l_message_data_buffer.pop_back(a_message_body_data))
		{
			LOG("[ CONNECTION ] Failed to unpack message body: " << transmission_result_strings[transmission_result::error_deserializing_data]);
			a_callback(false);
			return;
		}

		if (!l_message_data_buffer.pop_back(a_message_header_data))
		{
			LOG("[ CONNECTION ] Failed to unpack message header: " << transmission_result_strings[transmission_result::error_deserializing_data]);
			a_callback(false);
			return;
		}

		a_callback(true);

	});

}

uint64_t connection::lifetime() const {
	return utc_time() - m_start_time;
}
