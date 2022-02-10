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
	affix_base::data::ptr<connection_information> a_connection_information,
	affix_base::data::ptr<security_information> a_security_information,
	affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<connection_async_receive_result>>, affix_base::threading::cross_thread_mutex>& a_receive_results
) :
	m_transmission_security_manager(a_security_information),
	m_connection_information(a_connection_information),
    m_socket_io_guard(*a_connection_information->m_socket),
	m_start_time(utc_time()),
	m_receive_results(a_receive_results)
{
	// Set the last interaction time to the current utc time, to avoid having it ever be set to zero.
	locked_resource l_last_interaction_time = m_last_interaction_time.lock();
	(*l_last_interaction_time) = utc_time();
}

void authenticated_connection::async_send(
	affix_base::data::byte_buffer& a_byte_buffer,
	const std::function<void(bool)>& a_callback
)
{
	// Set the last interaction time to the current utc time.
	locked_resource l_last_interaction_time = m_last_interaction_time.lock();
	(*l_last_interaction_time) = utc_time();

	vector<uint8_t> l_exported_transmission_data;

	transmission_result l_transmission_result = transmission_result::unknown;

	// TRY TO EXPORT MESSAGE DATA IN "TRANSMISSION" FORMAT
	if (!m_transmission_security_manager.export_transmission(a_byte_buffer.data(), l_exported_transmission_data, l_transmission_result)) {
		LOG_ERROR("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);

		// Close the connection.
		close();

		return;
	}

	// SEND TRANSMISSION
	m_socket_io_guard.async_send(l_exported_transmission_data, a_callback);

}

void authenticated_connection::async_receive(

)
{
	// ALLOCATE DYNAMIC VECTOR FOR CALLBACK LAMBDA FUNCTION TO ACCESS AFTER PROGRAM EXITS THIS FUNCTION'S SCOPE
	ptr<vector<uint8_t>> l_data = new vector<uint8_t>();

	// Try to receive data asynchronously
	m_socket_io_guard.async_receive(l_data.val(), [&, l_data](bool a_result) {

		// Set the last interaction time to the current utc time.
		locked_resource l_last_interaction_time = m_last_interaction_time.lock();
		(*l_last_interaction_time) = utc_time();

		// Lock the mutex preventing concurrent reads/writes to the vector
		locked_resource l_receive_results = m_receive_results.lock();

		// Dynamically allocate result
		ptr<connection_async_receive_result> l_result(new connection_async_receive_result(this));

		// Push dynamically allocated result into vector.
		l_receive_results->push_back(l_result);

		if (!a_result)
		{
			LOG_ERROR("[ CONNECTION ] Error receiving data.");

			// Close the connection.
			close();

			return; 
		}

		transmission_result l_transmission_result = transmission_result::unknown;

		vector<uint8_t> l_message_data;

		// TRY TO "IMPORT" THE TRANSMISSION DATA
		if (!m_transmission_security_manager.import_transmission(l_data.val(), l_message_data, l_transmission_result))
		{
			LOG_ERROR("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);

			// Close the connection.
			close();

			return;
		}

		byte_buffer l_message_data_buffer(l_message_data);

		// Save byte buffer for future use.
		l_result->m_byte_buffer = l_message_data_buffer;

	});

}

void authenticated_connection::close(

)
{
	// Lock mutex for m_connected boolean
	locked_resource l_connected = m_connected.lock();

	// Make sure the socket closes.
	m_connection_information->m_socket->close();

	// Record the fact that the connection is no longer valid.
	(*l_connected) = false;

}

uint64_t authenticated_connection::lifetime() const {
	return utc_time() - m_start_time;
}

uint64_t authenticated_connection::idletime() {
	locked_resource l_last_interaction_time = m_last_interaction_time.lock();
	return utc_time() - (*l_last_interaction_time);
}
