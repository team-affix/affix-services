#include "convert.h"


#pragma region directories
string getOSDir(string winDir) {
	OS_PATH_CONVERT
}
#pragma endregion
///////////////////
#pragma region colors
wxColor toWXColor(const color& c) {
	wxColor result = wxColor(c.R, c.G, c.B, c.A);
	return result;
}
color toColor(const wxColor& w) {
	color result = color(w.Red(), w.Green(), w.Blue(), w.Alpha());
	return result;
}
#pragma endregion
