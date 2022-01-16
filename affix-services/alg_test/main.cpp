#include "affix-services/connection.h"
#include "affix-base/async_authenticate.h"
#include "affix-base/rsa.h"
#include "affix-base/networking.h"
#include "affix-services/rolling_token.h"
#include "affix-base/nat.h"
#include <fstream>


int main()
{
	std::ofstream nullstream;
	std::clog.rdbuf(nullstream.rdbuf());



	return 0;
}
