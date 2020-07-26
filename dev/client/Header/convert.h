#pragma once
#include "superHeader.h"
#include "color.h"

#pragma region directories
string getOSDir(string winDir);
#pragma endregion
///////////////////
#pragma region colors
QColor toQColor(const color& c);
color toColor(const QColor& w);
#pragma endregion
