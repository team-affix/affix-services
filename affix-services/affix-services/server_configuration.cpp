#include "server_configuration.h"
#include "affix-base/byte_buffer.h"
#include "affix-base/files.h"
#include "json.hpp"

using namespace affix_services;
using affix_base::data::byte_buffer;
using namespace asio::ip;

server_configuration::server_configuration(
	const std::string& a_server_configuration_file_path
) :
	m_server_configuration_file_path(a_server_configuration_file_path)
{

}

void server_configuration::export_to_file(

)
{
	// Write fields to JSON structure
	nlohmann::json l_json;
	l_json["enable"] = m_enable;
	l_json["bind_port"] = m_bind_endpoint.port();
	l_json["bound_port"] = m_bound_endpoint.port();

	// Write JSON to file
	std::ofstream l_ofstream(m_server_configuration_file_path, std::ios::out | std::ios::trunc);
	l_ofstream << l_json.dump(1, '\t');
	l_ofstream.close();

}

void server_configuration::import_from_file(

)
{
	// Read JSON from file
	std::ifstream l_ifstream(m_server_configuration_file_path, std::ios::in);

	if (!l_ifstream.is_open())
	{
		export_to_file();
	}
	else
	{
		nlohmann::json l_json;
		l_ifstream >> l_json;
		l_ifstream.close();

		// Read fields from JSON structure
		m_enable = l_json["enable"].get<bool>();
		m_bind_endpoint = tcp::endpoint(tcp::v4(), l_json["bind_port"].get<uint16_t>());
		m_bound_endpoint = tcp::endpoint(tcp::v4(), l_json["bound_port"].get<uint16_t>());
	}

}
