#pragma once
#include "overhead_include.h"
#include "overhead_functions.h"

#define CLIENT_TOKEN_END_REQUEST "REQEND"
#define CLIENT_CODE_BUFFER_FULL -2
#define CLIENT_CODE_END_REQUEST -3

class client {
public:
	SOCKET clientSocket;
	string clientRecvBuffer;

	int clientRecvBufferMaxSize;

public:
	client(SOCKET _clientSocket, int _clientRecvBufferMaxSize);
	int receivestr();
	int sendstr(char* contents, int len);

private:
	int remainingRecvBufferSize();
};