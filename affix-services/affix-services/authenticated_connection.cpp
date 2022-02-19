#include "authenticated_connection.h"
#include "affix-base/pch.h"
#include "affix-base/utc_time.h"
#include "affix-base/rsa.h"
#include "affix-base/catch_friendly_assert.h"
#include "application.h"

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
	application& a_application,
	affix_base::data::ptr<connection_information> a_connection_information,
	affix_base::data::ptr<security_information> a_security_information
) :
	m_transmission_security_manager(a_security_information),
	m_connection_information(a_connection_information),
    m_socket_io_guard(*a_connection_information->m_socket),
	m_start_time(utc_time()),
	m_application(a_application)
{
	// Set the last interaction time to the current utc time, to avoid having it ever be set to zero.
	locked_resource l_last_interaction_time = m_last_interaction_time.lock();
	(*l_last_interaction_time) = utc_time();
}

void authenticated_connection::async_receive_message(

)
{
	// DYNAMICALLY ALLOCATE VECTOR SO IT CAN STAY IN SCOPE FOR LAMBDA CALLBACKS
	ptr<std::vector<uint8_t>> l_received_message_data = new std::vector<uint8_t>();

	async_receive_message_data(
		l_received_message_data.val(),
		[&, l_received_message_data](bool a_result)
		{
			if (!a_result)
				return;

			// Create byte buffer from which all message-specific data will be unpacked
			affix_base::data::byte_buffer l_message_data_byte_buffer(*l_received_message_data);

			// Vector where all message header data lives
			std::vector<uint8_t> l_message_header_data;

			// Vector where all message body data lives
			std::vector<uint8_t> l_message_body_data;

			if (!l_message_data_byte_buffer.pop_front(l_message_header_data) ||
				!l_message_data_byte_buffer.pop_front(l_message_body_data))
			{
				LOG_ERROR("[ AUTHENTICATED CONNECTION ] Error unpacking message header (or) body.");
				close();
				return;
			}

			// Create a message header byte buffer, which holds serialized contents of message header
			byte_buffer l_message_header_byte_buffer(l_message_header_data);

			// Create a message body byte buffer, which holds serialized contents of message body
			byte_buffer l_message_body_byte_buffer(l_message_body_data);

			// Create actual message header
			affix_services::messaging::message_header l_message_header;

			// Deserialization status response for the message header only
			affix_services::messaging::message_header::deserialization_status_response_type l_message_header_deserialization_status_response;

			if (!l_message_header.deserialize(l_message_header_byte_buffer, l_message_header_deserialization_status_response))
			{
				LOG_ERROR("[ AUTHENTICATED CONNECTION ] Error deserializing message header.");
				close();
				return;
			}

			switch (l_message_header.m_message_type)
			{
				case messaging::message_types::rqt_relay:
				{
					// Lock the mutex preventing concurrent reads/writes to the vector
					locked_resource l_received_relay_requests = m_application.m_received_relay_requests.lock();

					message_rqt_relay l_message_body;

					// Create a deserialization status response
					message_rqt_relay::deserialization_status_response_type l_message_body_deserialization_status_response;

					if (!l_message_body.deserialize(l_message_body_byte_buffer, l_message_body_deserialization_status_response))
					{
						LOG_ERROR("[ AUTHENTICATED CONNECTION ] Error deserializing the body of message_rqt_relay.");
						close();
						return;
					}

					// Push the received relay request onto the vector
					l_received_relay_requests->push_back(
						new std::tuple(
							this,
							l_message_body
						));

					break;
				}
				case messaging::message_types::rsp_relay:
				{
					// Lock the mutex preventing concurrent reads/writes to the vector
					locked_resource l_received_relay_responses = m_application.m_received_relay_responses.lock();

					message_rsp_relay l_message_body;

					// Create a deserialization status response
					message_rsp_relay::deserialization_status_response_type l_message_body_deserialization_status_response;

					if (!l_message_body.deserialize(l_message_body_byte_buffer, l_message_body_deserialization_status_response))
					{
						LOG_ERROR("[ AUTHENTICATED CONNECTION ] Error deserializing the body of message_rsp_relay.");
						close();
						return;
					}

					// Push the received relay request onto the vector
					l_received_relay_responses->push_back(
						new std::tuple(
							this,
							l_message_body
						));

					break;
				}
				case messaging::message_types::rqt_index:
				{
					// Lock the mutex preventing concurrent reads/writes to the vector
					locked_resource l_received_index_requests = m_application.m_received_index_requests.lock();

					message_rqt_index l_message_body;

					// Create a deserialization status response
					message_rqt_index::deserialization_status_response_type l_message_body_deserialization_status_response;

					if (!l_message_body.deserialize(l_message_body_byte_buffer, l_message_body_deserialization_status_response))
					{
						LOG_ERROR("[ AUTHENTICATED CONNECTION ] Error deserializing the body of message_rqt_index.");
						close();
						return;
					}

					// Push the received relay request onto the vector
					l_received_index_requests->push_back(
						new std::tuple(
							this,
							l_message_body
						));

					break;
				}
				case messaging::message_types::rsp_index:
				{
					// Lock the mutex preventing concurrent reads/writes to the vector
					locked_resource l_received_index_responses = m_application.m_received_index_responses.lock();

					message_rsp_index l_message_body;

					// Create a deserialization status response
					message_rsp_index::deserialization_status_response_type l_message_body_deserialization_status_response;

					if (!l_message_body.deserialize(l_message_body_byte_buffer, l_message_body_deserialization_status_response))
					{
						LOG_ERROR("[ AUTHENTICATED CONNECTION ] Error deserializing the body of message_rsp_index.");
						close();
						return;
					}

					// Push the received relay request onto the vector
					l_received_index_responses->push_back(
						new std::tuple(
							this,
							l_message_body
						));
					
					break;
				}
				default:
				{
					LOG_ERROR("[ AUTHENTICATED CONNECTION ] Error: message_type was invalid.");
					close();
					return;
				}
			}

		});
}

void authenticated_connection::async_send_message_data(
	const affix_base::data::byte_buffer& a_byte_buffer,
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

		// Trigger the callback with a failure response.
		a_callback(false);

		// Close the connection.
		close();

		return;
	}

	// SEND TRANSMISSION
	m_socket_io_guard.async_send(l_exported_transmission_data, 
		m_dispatcher.dispatch([&, a_callback](bool a_result)
		{
			if (!a_result)
			{
				LOG_ERROR("[ CONNECTION ] Error sending data.");

				// Trigger the callback with a failure response.
				a_callback(false);

				// Close the connection.
				close();

				return;
			}

			// Trigger the argued callback function
			a_callback(true);

		}));

}

void authenticated_connection::async_receive_message_data(
	std::vector<uint8_t>& a_received_message_data,
	const std::function<void(bool)>& a_callback
)
{
	// DYNAMICALLY ALLOCATE VECTOR SO IT CAN STAY IN SCOPE FOR LAMBDA CALLBACKS
	ptr<std::vector<uint8_t>> l_received_exported_message_data = new std::vector<uint8_t>();

	// Try to receive data asynchronously
	m_socket_io_guard.async_receive(a_received_message_data, 
		m_dispatcher.dispatch([&, l_received_exported_message_data, a_callback](bool a_result) {

			// Set the last interaction time to the current utc time.
			locked_resource l_last_interaction_time = m_last_interaction_time.lock();
			(*l_last_interaction_time) = utc_time();

			// The result from trying to import the message
			transmission_result l_transmission_result = transmission_result::unknown;

			// If the receive call was unsuccessful, close the connection, then return.
			if (!a_result || !m_transmission_security_manager.import_transmission(l_received_exported_message_data.val(), a_received_message_data, l_transmission_result))
			{
				LOG_ERROR("[ CONNECTION ] Error receiving data.");
				LOG_ERROR("[ TRANSMISSION SECURITY MANAGER ] " << transmission_result_strings[l_transmission_result]);

				// Trigger the callback with a failure response.
				a_callback(false);

				// Close the connection.
				close();

				return;
			}

			// Call the argued callback function
			a_callback(true);

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
