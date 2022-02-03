#include "authenticated_connection.h"
#include "affix-base/pch.h"
#include "affix-base/utc_time.h"
#include "affix-base/rsa.h"
#include "affix-base/catch_friendly_assert.h"

#if 1
#define LOG(x) std::clog << x << std::endl
#define LOG_ERROR(x) std::cerr << x << std::endl
#else
#define LOG(x)
#define LOG_ERROR(x)
#endif

using namespace affix_services::networking;
using affix_base::timing::utc_time;
using asio::ip::tcp;
using namespace affix_base::networking;
using namespace affix_base::cryptography;
using affix_services::networking::authenticated_connection;
using std::vector;
using affix_base::data::byte_buffer;
using affix_base::data::ptr;
using std::lock_guard;
using affix_base::threading::cross_thread_mutex;
using namespace affix_base::threading;

authenticated_connection::~authenticated_connection(

)
{

}

authenticated_connection::authenticated_connection(
	const affix_base::data::ptr<asio::ip::tcp::socket>& a_socket,
	const asio::ip::tcp::endpoint& a_remote_endpoint,
	const CryptoPP::RSA::PrivateKey& a_local_private_key,
	const affix_services::security::rolling_token& a_local_token,
	const CryptoPP::RSA::PublicKey& a_remote_public_key,
	const affix_services::security::rolling_token& a_remote_token,
	affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_async_receive_result>>, affix_base::threading::cross_thread_mutex>& a_receive_results,
	const bool& a_inbound_connection
) :
	m_transmission_security_manager(a_local_private_key, a_local_token, a_remote_public_key, a_remote_token),
	m_socket(a_socket),
	m_remote_endpoint(a_remote_endpoint),
    m_socket_io_guard(*a_socket),
	m_start_time(utc_time()),
	m_receive_results(a_receive_results),
	m_inbound_connection(a_inbound_connection)
{
	
}

void authenticated_connection::async_send(
	affix_base::data::byte_buffer& a_byte_buffer,
	const std::function<void(bool)>& a_callback
)
{
	vector<uint8_t> l_exported_transmission_data;

	transmission_result l_transmission_result = transmission_result::unknown;

	// TRY TO EXPORT MESSAGE DATA IN "TRANSMISSION" FORMAT
	if (!m_transmission_security_manager.export_transmission(a_byte_buffer.data(), l_exported_transmission_data, l_transmission_result)) {
		LOG_ERROR("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);
		return;
	}

	try
	{
		// SEND TRANSMISSION
		m_socket_io_guard.async_send(l_exported_transmission_data, a_callback);
	}
	catch (std::exception a_exception)
	{
		LOG_ERROR("[ CONNECTION ] Error sending data: " << a_exception.what());

		// Most likely the connection is no longer valid.
		m_connected = false;
	}

}

void authenticated_connection::async_receive(

)
{
	// ALLOCATE DYNAMIC VECTOR FOR CALLBACK LAMBDA FUNCTION TO ACCESS AFTER PROGRAM EXITS THIS FUNCTION'S SCOPE
	ptr<vector<uint8_t>> l_data = new vector<uint8_t>();

	try
	{
		// Try to receive data asynchronously
		m_socket_io_guard.async_receive(l_data.val(), [&, l_data](bool a_result) {

			// Lock the mutex preventing concurrent reads/writes to the vector
			locked_resource l_receive_results = m_receive_results.lock();

			// Dynamically allocate result
			ptr<connection_async_receive_result> l_result(new connection_async_receive_result(this));

			// Push dynamically allocated result into vector.
			l_receive_results->push_back(l_result);

			if (!a_result)
			{
				LOG_ERROR("[ CONNECTION ] Error receiving data.");

				// Record the fact that the connection is no longer valid.
				m_connected = false;

				return; 
			}

			transmission_result l_transmission_result = transmission_result::unknown;

			vector<uint8_t> l_message_data;

			// TRY TO "IMPORT" THE TRANSMISSION DATA
			if (!m_transmission_security_manager.import_transmission(l_data.val(), l_message_data, l_transmission_result))
			{
				LOG_ERROR("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);
				return;
			}

			byte_buffer l_message_data_buffer(l_message_data);

			// Save byte buffer for future use.
			l_result->m_byte_buffer = l_message_data_buffer;

		});

	}
	catch (std::exception a_exception)
	{
		LOG_ERROR("[ CONNECTION ] Error receiving data: " << a_exception.what());

		// Most likely the connection is no longer valid.
		m_connected = false;
	}


}

uint64_t authenticated_connection::lifetime() const {
	return utc_time() - m_start_time;
}
