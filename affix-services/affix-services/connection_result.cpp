#include "connection_result.h"

using namespace affix_services;

connection_result::connection_result(
	const affix_base::data::ptr<connection_information>& a_connection_information,
	const bool& a_successful
) :
	m_connection_information(a_connection_information),
	m_successful(a_successful)
{

}
