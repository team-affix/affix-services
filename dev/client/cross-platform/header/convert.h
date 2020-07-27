#pragma once
#include "superHeader.h"
#include "color.h"
#include "appColors.h"

// string

string toBase(string in, uint from, uint to, uint padding = 0);

// Directories

string getOSDir(string winDir);

// Colors

QColor toQColor(const color& c);

color toColor(const QColor& q);

color toColor(const string& s);

string toString(const color& c);

// AppColors

string toString(const appColors& a);

appColors toAppColors(const string& s);
