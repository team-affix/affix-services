// OS Specific Values
#ifdef _WIN32
#define OS_PATH_CONVERT \
return winDir;
#else
#define OS_PATH_CONVERT \
replace(winDir.begin(), winDir.end(), '\\', '/'); \
return winDir;
#endif

// Application Values


#pragma once

#include "wx/wx.h"
#include <functional>
#include <iostream>
#include <fstream>
#include <string>

using namespace std;