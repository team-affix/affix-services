#pragma once
#include "affix-base/pch.h"
#include "agent_information.h"

namespace affix_services
{
	class client_information
	{
	public:
		/// <summary>
		/// String representing the identity of the remote client.
		/// </summary>
		std::string m_identity;

		/// <summary>
		/// Information about the agent who owns the remote client.
		/// </summary>
		agent_information m_agent_information;

		/// <summary>
		/// The paths which are registered to the remote client
		/// </summary>
		std::vector<std::vector<std::string>> m_paths;

	public:
		/// <summary>
		/// Constructs the client_paths object with the argued values for the fields.
		/// </summary>
		/// <param name="a_identity"></param>
		/// <param name="a_agent_information"></param>
		client_information(
			const std::string& a_identity,
			const agent_information& a_agent_information
		);

	public:
		/// <summary>
		/// Registers a path. If the path is already in the registry,
		/// it simply updates the path registration timestamp.
		/// </summary>
		/// <param name="a_path"></param>
		void register_path(
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
