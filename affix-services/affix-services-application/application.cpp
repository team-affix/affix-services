#include "application.h"

using namespace affix_services_application;
using namespace asio::ip;
using std::vector;
using affix_base::data::ptr;
using affix_services::networking::connection;
using std::lock_guard;
using std::mutex;

application::~application(

)
{

}

application::application(

)
{

}

void application::process_connections(

)
{
	lock_guard<mutex> l_lock(s_connections_mutex);
	for (int i = s_connections.size() - 1; i >= 0; i--)
	{
		process_connection(s_connections.begin() + i);
	}
}

void application::process_connection(
	vector<ptr<connection>>::iterator a_connection
)
{

}

void application::async_accept(

)
{

}
