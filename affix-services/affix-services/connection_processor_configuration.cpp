#include "connection_processor_configuration.h"
#include "json.hpp"
#include <fstream>
#include <iostream>
#include "affix-base/rsa.h"

using namespace affix_services;
using affix_base::data::cache;
using affix_base::cryptography::rsa_key_pair;
using namespace affix_base::cryptography;

connection_processor_configuration::connection_processor_configuration(
	const std::string& a_json_file_path
) :
	m_json_file_path(a_json_file_path)
{
	// Configure enable_pending_authentication_timeout cache
	m_enable_pending_authentication_timeout.set_pull(
		[&](bool& a_resource)
		{
			a_resource = m_resource["enable_pending_authentication_timeout"].get<bool>();
		});
	m_enable_pending_authentication_timeout.set_push(
		[&](bool& a_resource)
		{
			m_resource["enable_pending_authentication_timeout"] = a_resource;
		});
	m_enable_pending_authentication_timeout.set_import_failed_callback(
		[&](bool& a_resource, std::exception)
		{
			a_resource = true;
		});

	// Configure pending_authentication_timeout_in_seconds cache
	m_pending_authentication_timeout_in_seconds.set_pull(
		[&](uint64_t& a_resource)
		{
			a_resource = m_resource["pending_authentication_timeout_in_seconds"].get<uint64_t>();
		});
	m_pending_authentication_timeout_in_seconds.set_push(
		[&](uint64_t& a_resource)
		{
			m_resource["pending_authentication_timeout_in_seconds"] = a_resource;
		});
	m_pending_authentication_timeout_in_seconds.set_import_failed_callback(
		[&](uint64_t& a_resource, std::exception)
		{
			a_resource = 5;
		});

	// Configure enable_authenticated_connection_timeout cache
	m_enable_authenticated_connection_timeout.set_pull(
		[&](bool& a_resource)
		{
			a_resource = m_resource["enable_authenticated_connection_timeout"].get<bool>();
		});
	m_enable_authenticated_connection_timeout.set_push(
		[&](bool& a_resource)
		{
			m_resource["enable_authenticated_connection_timeout"] = a_resource;
		});
	m_enable_authenticated_connection_timeout.set_import_failed_callback(
		[&](bool& a_resource, std::exception)
		{
			a_resource = true;
		});

	// Configure authenticated_connection_timeout_in_seconds cache
	m_authenticated_connection_timeout_in_seconds.set_pull(
		[&](uint64_t& a_resource)
		{
			a_resource = m_resource["authenticated_connection_timeout_in_seconds"].get<uint64_t>();
		});
	m_authenticated_connection_timeout_in_seconds.set_push(
		[&](uint64_t& a_resource)
		{
			m_resource["authenticated_connection_timeout_in_seconds"] = a_resource;
		});
	m_authenticated_connection_timeout_in_seconds.set_import_failed_callback(
		[&](uint64_t& a_resource, std::exception)
		{
			a_resource = 21600;
		});

	// Configure local_key_pair cache
	m_local_key_pair.set_pull(
		[&](rsa_key_pair& a_resource)
		{
			// Read in base64 string versions of the RSA keys.
			std::string l_private_key_string = m_resource["local_private_key"].get<std::string>();
			std::string l_public_key_string = m_resource["local_public_key"].get<std::string>();

			// Import the RSA keys from the base64 strings
			if (!affix_base::cryptography::rsa_from_base64_string(
				a_resource.private_key,
				l_private_key_string
			))
			{
				throw std::exception("Failed to load RSA Private Key from base64 string.");
			}
			if (!affix_base::cryptography::rsa_from_base64_string(
				a_resource.public_key,
				l_public_key_string
			))
			{
				throw std::exception("Failed to load RSA Public Key from base64 string.");
			}

		});
	m_local_key_pair.set_push(
		[&](rsa_key_pair& a_resource)
		{
			std::string l_private_key_string = affix_base::cryptography::rsa_to_base64_string(a_resource.private_key);
			std::string l_public_key_string = affix_base::cryptography::rsa_to_base64_string(a_resource.public_key);

			// Write base64 strings to json structure
			m_resource["local_private_key"] = l_private_key_string;
			m_resource["local_public_key"] = l_public_key_string;

		});
	m_local_key_pair.set_validate(
		[&](rsa_key_pair& a_resource)
		{
			CryptoPP::AutoSeededRandomPool l_random;

			if (!a_resource.private_key.Validate(l_random, 3))
			{
				throw std::exception("Failed to validate private key integrity.");
			}
			if (!a_resource.public_key.Validate(l_random, 3))
			{
				throw std::exception("Failed to validate public key integrity.");
			}

		});
	m_local_key_pair.set_import_failed_callback(
		[&](rsa_key_pair& a_resource, std::exception)
		{
			a_resource = rsa_generate_key_pair(4096);
		});

	// Configure reconnect_delay_in_seconds cache
	m_reconnect_delay_in_seconds.set_pull(
		[&](uint64_t& a_resource)
		{
			a_resource = m_resource["reconnect_delay_in_seconds"].get<uint64_t>();
		});
	m_reconnect_delay_in_seconds.set_push(
		[&](uint64_t& a_resource)
		{
			m_resource["reconnect_delay_in_seconds"] = a_resource;
		});
	m_reconnect_delay_in_seconds.set_import_failed_callback(
		[&](uint64_t& a_resource, std::exception)
		{
			a_resource = 1;
		});

	// Configure this cache
	set_pull(
		[&](nlohmann::json& a_resource)
		{
			std::ifstream l_ifstream(m_json_file_path);
			l_ifstream >> a_resource;
			l_ifstream.close();

			// Import internal fields
			m_enable_pending_authentication_timeout.import_resource();
			m_pending_authentication_timeout_in_seconds.import_resource();
			m_enable_authenticated_connection_timeout.import_resource();
			m_authenticated_connection_timeout_in_seconds.import_resource();
			m_local_key_pair.import_resource();
			m_reconnect_delay_in_seconds.import_resource();

		});
	set_push(
		[&](nlohmann::json& a_resource)
		{
			// Export internal fields
			m_enable_pending_authentication_timeout.export_resource();
			m_pending_authentication_timeout_in_seconds.export_resource();
			m_enable_authenticated_connection_timeout.export_resource();
			m_authenticated_connection_timeout_in_seconds.export_resource();
			m_local_key_pair.export_resource();
			m_reconnect_delay_in_seconds.export_resource();

			std::ofstream l_ofstream(m_json_file_path);
			l_ofstream << a_resource.dump(1, '\t');
			l_ofstream.close();

		});
	set_import_failed_callback(
		[&](nlohmann::json& a_resource, std::exception)
		{
			// "Import" internal fields (will initialize them all to defaults since
			// pulling will fail)
			m_enable_pending_authentication_timeout.import_resource();
			m_pending_authentication_timeout_in_seconds.import_resource();
			m_enable_authenticated_connection_timeout.import_resource();
			m_authenticated_connection_timeout_in_seconds.import_resource();
			m_local_key_pair.import_resource();
			m_reconnect_delay_in_seconds.import_resource();
		});

}
