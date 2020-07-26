#include "../Header/convert.h"


#pragma region directories
string getOSDir(string winDir) {
	OS_PATH_CONVERT
}
#pragma endregion
///////////////////
#pragma region colors
QColor toWXColor(const color& c) {
    QColor result = QColor(c.R, c.G, c.B, c.A);
	return result;
}
color toColor(const QColor& q) {
    color result = color(q.red(), q.green(), q.blue(), q.alpha());
	return result;
}
#pragma endregion
