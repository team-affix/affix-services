#include "client.h"

client::client(SOCKET _clientSocket, int _clientRecvBufferMaxSize) {

	clientSocket = _clientSocket;
	clientRecvBufferMaxSize = _clientRecvBufferMaxSize;

}

int client::receivestr() {

	// check if the client buffer is already of maximum length
	if (clientRecvBuffer.length() < clientRecvBufferMaxSize) {

		// get the number of bytes available to be received from the socket
		u_long bytesAvailable;
		ioctlsocket(clientSocket, FIONREAD, &bytesAvailable);

		// define the temporary buffer for recv
		int availableBuffSize = remainingRecvBufferSize();
		int recvBytes = (bytesAvailable < availableBuffSize) ? bytesAvailable : 0;
		char* buff = new char[recvBytes];

		// make sure the receive will not cause the buffer to be it's maximum size or larger
		if (availableBuffSize <= 0) {
			return CLIENT_CODE_BUFFER_FULL;
		}

		// receive the data
		int recvResult = recv(clientSocket, buff, recvBytes, 0);

		string strBuff = to_string(buff, recvBytes);

		// delete the temporary buffer for recv
		delete[] buff;

		// append temporary buffer to the client recv buffer
		if (recvResult != -1 && recvResult != 0) {
			clientRecvBuffer += strBuff;
		}

		// check for "end of request" token
		if (strBuff.find(CLIENT_TOKEN_END_REQUEST) != string::npos) {
			return CLIENT_CODE_END_REQUEST;
		}

		return recvResult;

	}
	// client buffer size is maximum
	else {
		return CLIENT_CODE_BUFFER_FULL;
	}

}

int client::sendstr(char* contents, int len) {

	return send(clientSocket, contents, len, 0);

}


int client::remainingRecvBufferSize() {

	return clientRecvBufferMaxSize - clientRecvBuffer.length();

}
