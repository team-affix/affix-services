#pragma once
#include "superHeader.h"
#include "color.h"

#pragma region directories
string getOSDir(string winDir);
#pragma endregion
///////////////////
#pragma region colors
wxColor toWXColor(const color& c);
color toColor(const wxColor& w);
#pragma endregion
