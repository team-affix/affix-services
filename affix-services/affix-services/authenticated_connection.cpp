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
	affix_base::threading::guarded_resource<std::vector<affix_base::data::ptr<authenticated_connection_receive_result>>, affix_base::threading::cross_thread_mutex>& a_receive_results
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

	// Lock number_of_sends_in_progress mutex
	locked_resource l_number_of_sends_in_progress = m_number_of_sends_in_progress.lock();

	// Increment number of sends in progress
	l_number_of_sends_in_progress.resource()++;

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
	m_socket_io_guard.async_send(l_exported_transmission_data, 
		[&](bool a_result)
		{
			// Lock the mutex describing whether or not a send is in progress.
			locked_resource l_lambda_number_of_sends_in_progress = m_number_of_sends_in_progress.lock();

			// Let the program know that there is not currently a send request in progress.
			l_lambda_number_of_sends_in_progress.resource()--;

			if (!a_result)
			{
				LOG_ERROR("[ CONNECTION ] Error sending data.");

				// Close the connection.
				close();

				return;
			}

		});

}

void authenticated_connection::async_receive(

)
{
	// Lock mutex describing whether or not there is an async receive request in progress.
	locked_resource l_receive_in_progress = m_receive_in_progress.lock();


	// ALLOCATE DYNAMIC VECTOR FOR CALLBACK LAMBDA FUNCTION TO ACCESS AFTER PROGRAM EXITS THIS FUNCTION'S SCOPE
	ptr<vector<uint8_t>> l_data = new vector<uint8_t>();

	// Try to receive data asynchronously
	m_socket_io_guard.async_receive(l_data.val(), [&, l_data](bool a_result) {

		// Set the last interaction time to the current utc time.
		locked_resource l_last_interaction_time = m_last_interaction_time.lock();
		(*l_last_interaction_time) = utc_time();
		
		// Lock the mutex preventing concurrent reads/writes to the vector
		locked_resource l_receive_results = m_receive_results.lock();

		// The result from trying to import the message
		transmission_result l_transmission_result = transmission_result::unknown;

		// The decrypted message data.
		vector<uint8_t> l_message_data;

		// If the receive call was unsuccessful, close the connection, then return.
		if (!a_result || !m_transmission_security_manager.import_transmission(l_data.val(), l_message_data, l_transmission_result))
		{
			LOG_ERROR("[ CONNECTION ] Error receiving data.");
			LOG_ERROR("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);

			// Close the connection.
			close();

			return;
		}

		byte_buffer l_message_data_buffer(l_message_data);

		// Write the byte buffer to the vector of receive results
		l_receive_results->push_back(
			new authenticated_connection_receive_result(
			this, l_message_data_buffer
			)
		);

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
