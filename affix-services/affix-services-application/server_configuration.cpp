#include "server_configuration.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/files.h"

using namespace affix_services_application;
using affix_base::data::byte_buffer;
using namespace asio::ip;

server_configuration::server_configuration(
	const uint16_t& a_port
) :
	m_acceptor(m_acceptor_context, tcp::endpoint(tcp::v4(), a_port))
{

}

void server_configuration::export_connection_information(
	const std::string& a_file_path
) const
{
	// Create byte buffer which we will populate with the public connection information.
	byte_buffer l_byte_buffer;

	l_byte_buffer.push_back(m_acceptor.local_endpoint().port());

	// Get l_byte_buffer raw data
	std::vector<uint8_t> l_byte_buffer_data = l_byte_buffer.data();

	// Open ofstream to output file
	std::ofstream l_ofstream(a_file_path, std::ios::out | std::ios::binary | std::ios::trunc);

	// Write l_byte_buffer_data to stream
	l_ofstream.write((const char*)l_byte_buffer_data.data(), l_byte_buffer.size());

	// Close the ofstream
	l_ofstream.close();

}

bool server_configuration::try_import(
	const std::string& a_file_path,
	affix_base::data::ptr<server_configuration> a_result
)
{
	// Read all bytes from the file
	std::vector<uint8_t> l_file_contents;
	affix_base::files::file_read(a_file_path, l_file_contents);

	// Construct a byte buffer object based on the file contents
	byte_buffer l_byte_buffer(l_file_contents);

	// Get default port for the server
	uint16_t l_port = 0;
	if (!l_byte_buffer.pop_back(l_port))
	{
		std::cerr << "Unable to unpack the default port from the server config file." << std::endl;
		return false;
	}

	// Construct resulting server_configuration instance
	a_result = new server_configuration(
		l_port
	);

	return true;
	
}
