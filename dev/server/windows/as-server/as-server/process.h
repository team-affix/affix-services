#pragma once
#include "overhead_include.h"
#include "client.h"
#include "server.h"
#include "../../../both/header/request-response/request.h"
#include "../../../both/header/request-response/response.h"

using namespace std;

// buffer processing
void process_client_recv_buffer(client& c);

// request processing
void process_request(client& c, request& r);
void process_request_get_version(client& c, request_get_version& r);