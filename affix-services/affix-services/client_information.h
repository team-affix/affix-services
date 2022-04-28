#pragma once
#include "affix-base/pch.h"
#include "agent_information.h"
#include "affix-base/serializable.h"
#include "affix-base/utc_time.h"

namespace affix_services
{
	class client_information : public affix_base::data::serializable
	{
	public:
		/// <summary>
		/// String representing the identity of the remote client.
		/// </summary>
		std::string m_identity;

		/// <summary>
		/// Information about the agent who owns the remote client.
		/// </summary>
		std::vector<agent_information> m_agents;

		/// <summary>
		/// The paths which are registered to the remote client
		/// </summary>
		std::vector<std::vector<std::string>> m_paths;

	public:
		/// <summary>
		/// Default constructor for client_information object.
		/// </summary>
		client_information(

		);

		/// <summary>
		/// Constructs the client_information object with the argued values for the fields.
		/// </summary>
		/// <param name="a_identity"></param>
		/// <param name="a_agents"></param>
		/// <param name="a_timestamp"></param>
		/// <param name="a_version_number"></param>
		client_information(
			const std::string& a_identity,
			const std::vector<agent_information>& a_agents = {},
			const std::vector<std::vector<std::string>>& a_paths = {}
		);

		/// <summary>
		/// Copy constructor for the client_information type.
		/// </summary>
		/// <param name="a_client_information"></param>
		client_information(
			const client_information& a_client_information
		);

		/// <summary>
		/// Registers a path. If the path is already in the registry,
		/// it simply updates the path registration timestamp.
		/// </summary>
		/// <param name="a_path"></param>
		bool register_path(
			const std::vector<std::string>& a_path
		);

		/// <summary>
		/// Deregisters a path. If the path is already in the registry,
		/// it removes the path.
		/// </summary>
		/// <param name="a_path"></param>
		void deregister_paths_starting_with(
			const std::vector<std::string>& a_subpath
		);

		/// <summary>
		/// Returns the shortest registered path.
		/// </summary>
		/// <returns></returns>
		std::vector<std::string> fastest_path(

		);

	};
}
