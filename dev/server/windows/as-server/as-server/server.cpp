#pragma once 
#include "server.h"
#include "process.h"

void server_receive() {

	while (appContinue) {

		// loop through all client sockets
		for (int i = 0; i < vector_clients.size(); i++) {
			
			// check and make sure the socket at the current index still exists
			if (i >= vector_clients.size()) {
				break;
			}

			// get the client socket
			client c = vector_clients.at(i);

			int clientRecvResult = c.receivestr();

			if (clientRecvResult == CLIENT_CODE_END_REQUEST) {
				
				log("--client-end-request--", log_result::success);
				process_client_recv_buffer(c);

			}
			else if (clientRecvResult == CLIENT_CODE_BUFFER_FULL) {

				// if the client recv buffer is maximum in size
				log("--client-buffer-full--", log_result::error);
				// set the buffer to size zero
				c.clientRecvBuffer = "";
			}
			else if (clientRecvResult == -1) {

			}
			else if (clientRecvResult == 0) {
				log("--socket-close--", log_result::success);
				// remove the socket, freeing local resources
				vector_clients.erase(vector_clients.begin() + i);
				// set the index back one, evaluating the socket that has taken this one's place upon removal
				i -= 1;
			}
			else {
				log("--received buffer--", log_result::success);
			}

		}
	}

}

int server_init() {

	WSAData wsData;
	WORD ver = MAKEWORD(2, 2);

	// start up WSA
	int wsOk = WSAStartup(ver, &wsData);
	if (wsOk == 0) {
		log("--winsock success--", log_result::success);
	}
	else {
		log("--winsock failure--", log_result::error);
		return EXIT_FAILURE;
	}

	// create socket
	socket_server = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (socket_server != INVALID_SOCKET) {
		log("--socket success--", log_result::success);
	}
	else {
		log("--socket failure--", log_result::error);
		return EXIT_FAILURE;
	}

	// create local end point
	sockaddr_in hint;
	hint.sin_family = AF_INET;
	hint.sin_port = htons(8091);
	hint.sin_addr.S_un.S_addr = INADDR_ANY;
	
	int bindResult = bind(socket_server, (sockaddr*)&hint, sizeof(hint));
	if (socket_server != SOCKET_ERROR) {
		log("--bind success--", log_result::success);
		log("server bound to address: " + to_string(INADDR_ANY) + " on port: " + to_string(ntohs(hint.sin_port)), log_result::none);
	}
	else {
		log("--bind failure--", log_result::error);
		return EXIT_FAILURE;
	}

	return EXIT_SUCCESS;

}

void server_main() {

	while (true) {
		int listenResult = listen(socket_server, 0);
		if (listenResult >= 0) {
			log("--listen success--", log_result::success);
		}
		else {
			log("--listen failure--", log_result::error);
			return;
		}

		sockaddr_in clientAddr;
		int addrLen = sizeof(clientAddr);

		SOCKET clientSocket = accept(socket_server, (sockaddr*)&clientAddr, &addrLen);
		if (clientSocket >= 0) {
			log("--accept success--", log_result::success);
		}
		else {
			log("--accept failure--", log_result::error);
			return;
		}

		char host[NI_MAXHOST];
		char service[NI_MAXSERV];

		int getNameInfoResult = getnameinfo((sockaddr*)&clientAddr, sizeof(clientAddr), host, NI_MAXHOST, service, NI_MAXSERV, 0);
		if (getNameInfoResult != 0) {
			inet_ntop(AF_INET, &clientAddr.sin_addr, host, NI_MAXHOST);
		}

		unsigned long blocking = 0;
		unsigned long nonBlocking = 1;

		int ioctlResult = ioctlsocket(clientSocket, FIONBIO, &nonBlocking);
		if (ioctlResult != SOCKET_ERROR) {
			log("--ioctl success--", log_result::success);
		}
		else {
			log("--ioctl failure--", log_result::error);
			return;
		}

		vector_clients.push_back(client(clientSocket, 4096));

	}

}