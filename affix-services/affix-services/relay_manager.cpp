#include "relay_manager.h"
#include "application.h"

using namespace affix_services;
using namespace affix_base::data;
using namespace affix_base::threading;
using namespace affix_services::networking;

relay_manager::relay_manager(
	affix_base::data::ptr<networking::authenticated_connection> a_authenticated_connection
) :
	m_authenticated_connection(a_authenticated_connection)
{

}
