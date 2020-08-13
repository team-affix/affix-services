#pragma once
#include "overhead_include.h"
#include "overhead_classes.h"
#include "client.h"
#include "log.h"

inline int clientBufferMaxSize = 4096;

inline SOCKET socket_server;
inline vector<client> vector_clients;

void server_receive();
int server_init();
void server_main();