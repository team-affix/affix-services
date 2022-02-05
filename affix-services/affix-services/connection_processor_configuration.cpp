#include "connection_processor_configuration.h"
#include "json.hpp"
#include <fstream>
#include <iostream>

using namespace affix_services;

connection_processor_configuration::connection_processor_configuration(

)
{

}

connection_processor_configuration::connection_processor_configuration(
	const std::string& a_json_file_path
) :
	m_json_file_path(a_json_file_path)
{

}

void connection_processor_configuration::export_to_file(

)
{
	// Write fields to JSON structure
	nlohmann::json l_json;
	l_json["enable_pending_authentication_timeout"] = m_enable_pending_authentication_timeout;
	l_json["pending_authentication_maximum_idle_time_in_seconds"] = m_pending_authentication_maximum_idle_time_in_seconds;
	l_json["enable_authenticated_connection_disconnect_after_maximum_idle_time"] = m_enable_authenticated_connection_disconnect_after_maximum_idle_time;
	l_json["authenticated_connection_maximum_idle_time_in_seconds"] = m_authenticated_connection_maximum_idle_time_in_seconds;
	l_json["local_private_key"] = affix_base::cryptography::rsa_to_base64_string(m_local_key_pair.private_key);
	l_json["local_public_key"] = affix_base::cryptography::rsa_to_base64_string(m_local_key_pair.public_key);

	while (true)
	{
		try
		{
			// Write JSON to file
			std::ofstream l_ofstream(m_json_file_path, std::ios::out | std::ios::trunc);
			l_ofstream << l_json.dump(1, '\t');
			l_ofstream.close();
			break;
		}
		catch (std::exception a_exception)
		{
			std::cerr << a_exception.what() << std::endl;
		}
	}

}

void connection_processor_configuration::import_from_file(

)
{
	// Read JSON from file
	std::ifstream l_ifstream(m_json_file_path, std::ios::in);

	if (!l_ifstream.is_open())
	{
		// Generate RSA key pair
		m_local_key_pair = affix_base::cryptography::rsa_generate_key_pair(4096);

		// Export the configuration to a JSON file.
		export_to_file();

	}
	else
	{
		while (true)
		{
			try
			{
				nlohmann::json l_json;
				l_ifstream >> l_json;
				l_ifstream.close();

				// Read fields from JSON structure
				m_enable_pending_authentication_timeout = l_json["enable_pending_authentication_timeout"].get<bool>();
				m_pending_authentication_maximum_idle_time_in_seconds = l_json["pending_authentication_maximum_idle_time_in_seconds"].get<uint64_t>();
				m_enable_authenticated_connection_disconnect_after_maximum_idle_time = l_json["enable_authenticated_connection_disconnect_after_maximum_idle_time"].get<bool>();
				m_authenticated_connection_maximum_idle_time_in_seconds = l_json["authenticated_connection_maximum_idle_time_in_seconds"].get<uint64_t>();

				bool l_rsa_key_pair_loaded_successfully = true;

				// Try to import RSA key pair
				if (!affix_base::cryptography::rsa_from_base64_string(m_local_key_pair.private_key, l_json["local_private_key"].get<std::string>()))
				{
					std::cerr << "[ CONNECTION PROCESSOR CONFIGURATION ] Error reading RSA Private Key from JSON file." << std::endl;
					l_rsa_key_pair_loaded_successfully = false;
				}
				if (!affix_base::cryptography::rsa_from_base64_string(m_local_key_pair.public_key, l_json["local_public_key"].get<std::string>()))
				{
					std::cerr << "[ CONNECTION PROCESSOR CONFIGURATION ] Error reading RSA Public Key from JSON file." << std::endl;
					l_rsa_key_pair_loaded_successfully = false;
				}

				break;
			}
			catch (std::exception a_exception)
			{
				std::cerr << a_exception.what() << std::endl;
			}
		}

	}

}
