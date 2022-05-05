#include "authenticated_connection.h"
#include "affix-base/pch.h"
#include "affix-base/utc_time.h"
#include "affix-base/rsa.h"
#include "affix-base/catch_friendly_assert.h"
#include "client.h"

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
	affix_base::data::ptr<security_information> a_security_information
) :
	m_transmission_security_manager(a_security_information),
	m_connection_information(a_connection_information),
    m_socket_io_guard(*a_connection_information->m_socket),
	m_start_time(utc_time())
{
	// Set the last interaction time to the current utc time, to avoid having it ever be set to zero.
	locked_resource l_last_interaction_time = m_last_interaction_time.lock();
	(*l_last_interaction_time) = utc_time();
}

void authenticated_connection::async_send(
	const affix_base::data::byte_buffer& a_byte_buffer
)
{
	// Set the last interaction time to the current utc time.
	locked_resource l_last_interaction_time = m_last_interaction_time.lock();
	(*l_last_interaction_time) = utc_time();

	// Lock the transmission_security_manager, keeping the rolling tokens embedded inside it valid
	locked_resource l_transmission_security_manager = m_transmission_security_manager.lock();

	vector<uint8_t> l_exported_transmission_data;

	transmission_result l_transmission_result = transmission_result::unknown;

	// TRY TO EXPORT MESSAGE DATA IN "TRANSMISSION" FORMAT
	if (!l_transmission_security_manager->export_transmission(a_byte_buffer.data(), l_exported_transmission_data, l_transmission_result)) {
		LOG_ERROR("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);

		// Close the connection.
		close();

		return;
	}

	// SEND TRANSMISSION
	m_socket_io_guard.async_send(l_exported_transmission_data, 
		m_send_dispatcher.dispatch([&](bool a_result)
		{
			if (!a_result)
			{
				LOG_ERROR("[ CONNECTION ] Error sending data.");

				// Close the connection.
				close();

				return;
			}

		}));

}

void authenticated_connection::async_receive(
	std::vector<uint8_t>& a_received_message_data,
	const std::function<void()>& a_callback
)
{
	// DYNAMICALLY ALLOCATE VECTOR SO IT CAN STAY IN SCOPE FOR LAMBDA CALLBACKS
	ptr<std::vector<uint8_t>> l_received_exported_message_data = new std::vector<uint8_t>();

	// Try to receive data asynchronously
	m_socket_io_guard.async_receive(*l_received_exported_message_data,
		m_receive_dispatcher.dispatch([&, l_received_exported_message_data, a_callback](bool a_result)
		{

			// Set the last interaction time to the current utc time.
			locked_resource l_last_interaction_time = m_last_interaction_time.lock();
			(*l_last_interaction_time) = utc_time();

			// Lock the transmission_security_manager to prevent concurrent reads/writes to the rolling_tokens involved
			locked_resource l_transmission_security_manager = m_transmission_security_manager.lock();

			// The result from trying to import the message
			transmission_result l_transmission_result = transmission_result::unknown;

			// If the receive call was unsuccessful, close the connection, then return.
			if (!a_result || !l_transmission_security_manager->import_transmission(l_received_exported_message_data.val(), a_received_message_data, l_transmission_result))
			{
				LOG_ERROR("[ CONNECTION ] Error receiving data.");
				LOG_ERROR("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);

				// Close the connection.
				close();

				return;
			}

			// Call the argued callback function
			a_callback();

		}));

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

const std::string& authenticated_connection::remote_identity(

)
{
	locked_resource l_transmission_security_manager = m_transmission_security_manager.lock();
	return l_transmission_security_manager->m_security_information->m_remote_identity;
}
