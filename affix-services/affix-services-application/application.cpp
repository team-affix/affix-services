#include "application.h"

using namespace affix_services_application;
using namespace asio::ip;
using std::vector;
using affix_base::data::ptr;
using affix_services::networking::connection;
using std::lock_guard;
using std::mutex;
using affix_base::threading::cross_thread_mutex;

void connection_processor::process_connections(

)
{
	process_new_connections();
	process_authentication_attempts();
	process_authenticated_connections();
}

void connection_processor::process_new_connections(

)
{
	// Lock the mutex, preventing changes to m_new_connections.
	lock_guard<cross_thread_mutex> l_lock_guard(m_new_connections_mutex);

}

void connection_processor::process_authentication_attempts(

)
{

}

void connection_processor::process_authenticated_connections(

)
{

}
