#include "connection_processor_configuration.h"

using namespace affix_services;

connection_processor_configuration::connection_processor_configuration(

)
{

}

connection_processor_configuration::connection_processor_configuration(
	const bool& a_connection_enable_disconnect_after_maximum_idle_time,
	const uint64_t& a_connection_maximum_idle_time_in_seconds
) :
	m_connection_enable_disconnect_after_maximum_idle_time(a_connection_enable_disconnect_after_maximum_idle_time),
	m_connection_maximum_idle_time_in_seconds(a_connection_maximum_idle_time_in_seconds)
{

}

connection_processor_configuration connection_processor_configuration::import_from_file(
	const std::string& a_file_path
)
{
	return connection_processor_configuration{};
}
