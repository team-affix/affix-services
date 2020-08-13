#pragma once
#include "superHeader.h"
#include "convert.h"
#include "appColors.h"

inline const string dirMain = getOSDir("main\\");
inline const string dirAppColors = getOSDir(dirMain + "appColors\\");
inline const string dirAppColorsActive = getOSDir(dirAppColors + "active");
inline const string dirAccounts = getOSDir(dirMain + "accounts\\");
inline const string dirAccountsLoggedIn = getOSDir(dirAccounts + "loggedIn");
inline const string dirAccountsActiveIndex = getOSDir(dirAccounts + "activeIndex");
inline const string dirMachine = getOSDir(dirMain + "machine\\");
inline const string dirMachineActive = getOSDir(dirMachine + "active");
inline const string dirImages = getOSDir("..\\..\\..\\..\\image\\");
inline const string dirLogos = getOSDir(dirImages + "logo\\");
inline const string dirBackgrounds = getOSDir(dirImages + "background\\");

//inline const appColors appColorsDark;
//inline const appColors appColorsBright;
//inline const appColors* appColorsDefault = &appColorsDark;
