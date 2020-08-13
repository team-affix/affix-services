#include "process.h"

// buffer processing
void process_client_recv_buffer(client& c) {

	// get the request type for it to be parsed
	uint64 requestType;
	memcpy(&requestType, &c.clientRecvBuffer[0], sizeof(uint64));
	uint8 request_start_pos = sizeof(uint64);

	try {
		switch (requestType) {

			case(REQUEST_TYPE_DEFAULT): {
				request r;
				deserialize(r, &c.clientRecvBuffer[request_start_pos]);
				process_request(c, r);
			}

			case(REQUEST_TYPE_GET_VERSION): {
				request_get_version r;
				deserialize(r, &c.clientRecvBuffer[request_start_pos]);
				process_request_get_version(c, r);
				break;
			}

		}
	}
	catch (exception ex) {
		log(ex.what(), log_result::error);
	}
}

// request processing
void process_request(client& c, request& r) {

}
void process_request_get_version(client& c, request_get_version& r) {

}