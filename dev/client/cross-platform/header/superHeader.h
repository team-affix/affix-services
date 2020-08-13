// OS Specific Includes and Values
#ifdef _WIN32
#define OS_PATH_CONVERT \
    return winDir;
#define OS_CREATE_DIRECTORY \
    mkdir(path.c_str());
#else
#define OS_PATH_CONVERT \
replace(winDir.begin(), winDir.end(), '\\', '/'); \
    return winDir;
#define OS_CREATE_DIRECTORY \
    mkdir(path.c_str(), S_IWUSR);
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
#include <math.h>
#include <QPixmap>

using namespace std;
