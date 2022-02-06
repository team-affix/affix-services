#include "connection_processor_configuration.h"
#include "json.hpp"
#include <fstream>
#include <iostream>

using namespace affix_services;
using affix_base::data::cache;
using affix_base::cryptography::rsa_key_pair;

connection_processor_configuration::connection_processor_configuration(
	const std::string& a_json_file_path
) :
	m_json_file_path(a_json_file_path),
	cache<nlohmann::json>(
		[&](nlohmann::json& a_local)
		{
			// Pull method
			std::ifstream l_ifstream(m_json_file_path);
			l_ifstream >> a_local;
			l_ifstream.close();
		},
		[&](nlohmann::json& a_local)
		{
			// Push method
			std::ofstream l_ofstream(m_json_file_path);
			l_ofstream << a_local;
			l_ofstream.close();
		},
		[&](nlohmann::json& a_local)
		{
			// Validate method
		},
		nlohmann::json(),
		[&](nlohmann::json& a_local, std::exception a_exception)
		{
			// Pull error callback
			std::cerr << a_exception.what() << std::endl;
		},
		[&](nlohmann::json& a_local, std::exception a_exception)
		{
			// Push error callback
			std::cerr << a_exception.what() << std::endl;
		})
{
	m_enable_pending_authentication_timeout = new cache<bool>(
		[&](auto& a_local)
		{
			// Pull
			a_local = m_local["enable_pending_authentication_timeout"].get<bool>();
		},
		[&](auto& a_local)
		{
			// Push
			m_local["enable_pending_authentication_timeout"] = a_local;
		},
		[&](auto& a_local)
		{
			// Validate
		},
		true,
		[&](auto& a_local, std::exception a_exception)
		{
			// Import error
			m_local["enable_pending_authentication_timeout"] = m_enable_pending_authentication_timeout;
		});

	m_pending_authentication_timeout_in_seconds = new cache<uint64_t>(
		[&](auto& a_local)
		{
			// Pull
			a_local = m_local["pending_authentication_timeout"].get<uint64_t>();
		},
		[&](auto& a_local)
		{
			// Push
			m_local["pending_authentication_timeout"] = a_local;
		},
		[&](auto& a_local)
		{
			// Validate
		},
		10,
		[&](auto& a_local, std::exception a_exception)
		{
			// Import error
			m_local["pending_authentication_timeout"] = a_local;
		});

	m_enable_authenticated_connection_timeout = new cache<bool>(
		[&](auto& a_local)
		{
			// Pull
			a_local = m_local["enable_authenticated_connection_timeout"].get<bool>();
		},
		[&](auto& a_local)
		{
			// Push
			m_local["enable_authenticated_connection_timeout"] = a_local;
		},
		[&](auto& a_local)
		{
			// Validate
		},
		true,
		[&](auto& a_local, std::exception a_exception)
		{
			// Import error
			m_local["enable_authenticated_connection_timeout"] = a_local;
		});

	m_authenticated_connection_timeout_in_seconds = new cache<uint64_t>(
		[&](auto& a_local)
		{
			// Pull
			a_local = m_local["authenticated_connection_timeout_in_seconds"].get<uint64_t>();
		},
		[&](auto& a_local)
		{
			// Push
			m_local["authenticated_connection_timeout_in_seconds"] = a_local;
		},
		[&](auto& a_local)
		{
			// Validate
		},
		21600,
		[&](auto& a_local, std::exception a_exception)
		{
			// Import error
			m_local["authenticated_connection_timeout_in_seconds"] = a_local;
		});

	m_local_key_pair = new cache<rsa_key_pair>(
		[&](auto& a_local)
		{
			// Pull
			a_local = m_local["local_private_key"].get<std::string>();
		},
		[&](auto& a_local)
		{
			// Push
			m_local["authenticated_connection_timeout_in_seconds"] = a_local;
		},
		[&](auto& a_local)
		{
			// Validate
		},
		rsa_key_pair(),
		[&](auto& a_local, std::exception a_exception)
		{
			// Import error
			m_local["authenticated_connection_timeout_in_seconds"] = a_local;
		});

}
