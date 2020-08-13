#pragma once
#include <string>
#include <iostream>
#include <fstream>
#include <vector>

#include <winsock2.h>
#include <WS2tcpip.h>
#pragma comment (lib, "ws2_32.lib")

#include <thread>
#include <cassert>
#include <filesystem>
#include <chrono>
#include <ctime>

#define uint unsigned int
#define uint8 uint8_t
#define uint16 uint16_t
#define uint32 uint32_t
#define uint64 uint64_t

#define ulong ULONG
#define ulong32 ULONG32
#define ulong64 ULONG64

using namespace std;




// Application Values


inline bool appContinue = true;