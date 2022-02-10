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
	m_json_file_path(a_server_configuration_file_path)
{
	// Configure m_enable cache
	m_enable.set_pull(
		[&](bool& a_resource)
		{
			a_resource = m_resource["enable"].get<bool>();
		});
	m_enable.set_push(
		[&](bool& a_resource)
		{
			m_resource["enable"] = a_resource;
		});
	m_enable.set_import_failed_callback(
		[&](bool& a_resource, std::exception)
		{
			a_resource = false;
		});
	
	// Configure bind_port cache
	m_bind_port.set_pull(
		[&](uint16_t& a_resource)
		{
			a_resource = m_resource["bind_port"].get<uint16_t>();
		});
	m_bind_port.set_push(
		[&](uint16_t& a_resource)
		{
			m_resource["bind_port"] = a_resource;
		});
	m_bind_port.set_import_failed_callback(
		[&](uint16_t& a_resource, std::exception)
		{
			a_resource = 0;
		});

	// Configure bound_port cache
	m_bound_port.set_pull(
		[&](uint16_t& a_resource)
		{
			a_resource = m_resource["bound_port"].get<uint16_t>();
		});
	m_bound_port.set_push(
		[&](uint16_t& a_resource)
		{
			m_resource["bound_port"] = a_resource;
		});
	m_bound_port.set_import_failed_callback(
		[&](uint16_t& a_resource, std::exception)
		{
			a_resource = 0;
		});

	// Configure this cache
	set_pull(
		[&](nlohmann::json& a_resource)
		{
			std::ifstream l_ifstream(m_json_file_path);
			l_ifstream >> a_resource;
			l_ifstream.close();

			// Import individual fields
			m_enable.import_resource();
			m_bind_port.import_resource();
			m_bound_port.import_resource();

		});
	set_push(
		[&](nlohmann::json& a_resource)
		{
			// Wipe JSON clean before exporting fields (this removes unnecessary fields)
			a_resource.clear();

			// Export individual fields
			m_enable.export_resource();
			m_bind_port.export_resource();
			m_bound_port.export_resource();

			std::ofstream l_ofstream(m_json_file_path);
			l_ofstream << a_resource.dump(1, '\t');
			l_ofstream.close();

		});
	set_import_failed_callback(
		[&](nlohmann::json& a_resource, std::exception)
		{
			// "Import" internal fields (will initialize them all to defaults since
			// pulling will fail)
			m_enable.import_resource();
			m_bind_port.import_resource();
			m_bound_port.import_resource();

		});

}
