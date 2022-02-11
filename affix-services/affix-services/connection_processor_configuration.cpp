#include "connection_processor_configuration.h"
#include "json.hpp"
#include <fstream>
#include <iostream>
#include "affix-base/rsa.h"
#include "affix-base/string_extensions.h"

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
				throw std::exception("Failed to load local RSA Private Key from base64 string.");
			}
			if (!affix_base::cryptography::rsa_from_base64_string(
				a_resource.public_key,
				l_public_key_string
			))
			{
				throw std::exception("Failed to load local RSA Public Key from base64 string.");
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
			a_resource = 16;
		});

	// Configure approved_identities cache
	m_approved_identities.set_pull(
		[&](std::vector<std::string>& a_resource)
		{
			// Get base64 versions of the identities
			a_resource = m_resource["approved_identities"].get<std::vector<std::string>>();
		});
	m_approved_identities.set_push(
		[&](std::vector<std::string>& a_resource)
		{
			// Save list to json
			m_resource["approved_identities"] = a_resource;

		});
	m_approved_identities.set_import_failed_callback(
		[&](std::vector<std::string>& a_resource, std::exception)
		{
			
		});

	// Configure remote_endpoints cache
	m_remote_endpoints.set_pull(
		[&](std::vector<asio::ip::tcp::endpoint>& a_resource)
		{
			std::vector<std::string> l_remote_endpoints = m_resource["remote_endpoints"].get<std::vector<std::string>>();

			a_resource.resize(l_remote_endpoints.size());

			for (int i = 0; i < l_remote_endpoints.size(); i++)
			{
				std::vector<std::string> l_remote_endpoint = affix_base::data::string_split(l_remote_endpoints[i], ':');

				a_resource[i].address(asio::ip::make_address(l_remote_endpoint[0]));
				a_resource[i].port(std::stoi(l_remote_endpoint[1]));

			}

		});
	m_remote_endpoints.set_push(
		[&](std::vector<asio::ip::tcp::endpoint>& a_resource)
		{
			std::vector<std::string> l_remote_endpoints(a_resource.size());

			for (int i = 0; i < a_resource.size(); i++)
			{
				l_remote_endpoints[i] = a_resource[i].address().to_string() + ":" + std::to_string(a_resource[i].port());
			}

			m_resource["remote_endpoints"] = l_remote_endpoints;

		});
	m_remote_endpoints.set_import_failed_callback(
		[&](std::vector<asio::ip::tcp::endpoint>& a_resource, std::exception)
		{

		});

	// Configure this cache
	set_pull(
		[&](nlohmann::ordered_json& a_resource)
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

			m_approved_identities.import_resource();

			m_remote_endpoints.import_resource();

		});
	set_push(
		[&](nlohmann::ordered_json& a_resource)
		{
			// Wipe JSON clean before exporting fields (this removes unnecessary fields)
			a_resource.clear();

			// Export internal fields
			m_enable_pending_authentication_timeout.export_resource();
			m_pending_authentication_timeout_in_seconds.export_resource();

			m_enable_authenticated_connection_timeout.export_resource();
			m_authenticated_connection_timeout_in_seconds.export_resource();

			m_local_key_pair.export_resource();

			m_reconnect_delay_in_seconds.export_resource();

			m_approved_identities.export_resource();

			m_remote_endpoints.export_resource();

			std::ofstream l_ofstream(m_json_file_path);
			l_ofstream << a_resource.dump(1, '\t');
			l_ofstream.close();

		});
	set_import_failed_callback(
		[&](nlohmann::ordered_json& a_resource, std::exception)
		{
			// "Import" internal fields (will initialize them all to defaults since
			// pulling will fail)

			m_enable_pending_authentication_timeout.import_resource();
			m_pending_authentication_timeout_in_seconds.import_resource();

			m_enable_authenticated_connection_timeout.import_resource();
			m_authenticated_connection_timeout_in_seconds.import_resource();

			m_local_key_pair.import_resource();

			m_reconnect_delay_in_seconds.import_resource();

			m_approved_identities.import_resource();

			m_remote_endpoints.import_resource();

		});

}
