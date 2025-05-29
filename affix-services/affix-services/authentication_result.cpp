#include "authentication_result.h"

using namespace affix_services;

authentication_result::authentication_result(
	affix_base::data::ptr<connection_information> a_connection_information,
	affix_base::data::ptr<security_information> a_security_information,
	const bool& a_successful
) :
	m_connection_information(a_connection_information),
	m_security_information(a_security_information),
	m_successful(a_successful)
{
	
}
