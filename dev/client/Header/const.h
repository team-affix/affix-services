#pragma once
#include "superHeader.h"
#include "convert.h"

#pragma region directories
const string dirMain = getOSDir("main\\");
const string dirAppColors = getOSDir(dirMain + "appColors\\");
const string dirAppColorsConfigs = getOSDir(dirAppColors + "configs");
const string dirAppColorsActiveConfigIndex = getOSDir(dirAppColors + "activeConfigIndex");
const string dirAccounts = getOSDir(dirMain + "accounts\\");
const string dirAccountsLoggedIn = getOSDir(dirAccounts + "loggedIn");
const string dirAccountsActiveIndex = getOSDir(dirAccounts + "activeIndex");
const string dirMachine = getOSDir(dirMain + "machine\\");
const string dirMachineActive = getOSDir(dirMachine + "active");
#pragma endregion