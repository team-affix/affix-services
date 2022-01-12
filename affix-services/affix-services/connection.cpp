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
using std::vector;
using affix_base::data::byte_buffer;
using affix_base::data::ptr;
using std::lock_guard;
using affix_base::threading::cross_thread_mutex;

connection::connection(
	asio::ip::tcp::socket& a_socket,
	const CryptoPP::RSA::PrivateKey& a_local_private_key,
	const affix_services::security::rolling_token& a_local_token,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const affix_services::security::rolling_token& a_remote_token,
	affix_base::threading::cross_thread_mutex& a_receive_results_mutex,
	std::vector<affix_base::data::ptr<connection_async_receive_result>>& a_receive_results
) :
	m_transmission_security_manager(a_local_private_key, a_local_token, a_remote_public_key, a_remote_token),
	m_socket(std::move(a_socket)),
    m_socket_io_guard(a_socket),
	m_start_time(utc_time()),
	m_receive_results_mutex(a_receive_results_mutex),
	m_receive_results(a_receive_results)
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
		std::cerr << "[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result] << std::endl;
		return;
	}

	// SEND TRANSMISSION
	m_socket_io_guard.async_send(l_final, a_callback);

}

void connection::async_receive(

)
{
	// ALLOCATE DYNAMIC VECTOR FOR CALLBACK LAMBDA FUNCTION TO ACCESS AFTER PROGRAM EXITS THIS FUNCTION'S SCOPE
	ptr<vector<uint8_t>> l_data = new vector<uint8_t>();

	m_socket_io_guard.async_receive(l_data.val(), [&, l_data](bool a_result) {

		// Lock the mutex preventing concurrent reads/writes to the vector
		lock_guard<cross_thread_mutex> l_lock_guard(m_receive_results_mutex);

		// Dynamically allocate result
		ptr<connection_async_receive_result> l_result(new connection_async_receive_result(this));

		// Push dynamically allocated result into vector.
		m_receive_results.push_back(l_result);

		if (!a_result) {
			std::cerr << "[ CONNECTION ] Error receiving data." << std::endl;
			return;
		}

		transmission_result l_transmission_result = transmission_result::unknown;

		vector<uint8_t> l_message_data;

		// TRY TO "IMPORT" THE TRANSMISSION DATA
		if (!m_transmission_security_manager.import_transmission(l_data.val(), l_message_data, l_transmission_result))
		{
			std::cerr << "[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result] << std::endl;
			return;
		}

		byte_buffer l_message_data_buffer(l_message_data);

		if (!l_message_data_buffer.pop_back(l_result->m_message_body_data))
		{
			std::cerr << "[ CONNECTION ] Failed to unpack message body: " << transmission_result_strings[transmission_result::error_unpacking_message_body] << std::endl;
			return;
		}

		if (!l_message_data_buffer.pop_back(l_result->m_message_header_data))
		{
			std::cerr << "[ CONNECTION ] Failed to unpack message header: " << transmission_result_strings[transmission_result::error_unpacking_message_header] << std::endl;
			return;
		}

	});

}

uint64_t connection::lifetime() const {
	return utc_time() - m_start_time;
}
