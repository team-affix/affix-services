// OS Specific Includes
#ifdef _WIN32
#define OS_PATH_CONVERT \
return winDir;
#else
#define OS_PATH_CONVERT \
replace(winDir.begin(), winDir.end(), '\\', '/'); \
return winDir;
#endif

// Application Includes

#pragma once

#include <QApplication>
#include <QColor>
#include <functional>
#include <iostream>
#include <fstream>
#include <string>
#include <sys/stat.h>
#include <sys/types.h>
#include <direct.h>
#include <math.h>

using namespace std;
