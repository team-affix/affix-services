#pragma once
#include "superHeader.h"
#include "ie.h"
#include "const.h"

enum appDirStatus{
    mainExists = 1,                 // main
    appColorsExists = 2,            // appColors
    appColorsActiveExists = 4,
    accountsExists = 8,             // accounts
    accountsLoggedInExists = 16,
    accountsActiveIndexExists = 32,
    machineExists = 64,             // machine
    machineActiveExists = 128,
};

enum appFiles{
    appColorsActiveFile = 1,           // appColors
    appAccountsLoggedInFile = 2,        // accounts
    appAccountsActiveIndexFile = 4,
    appMachineActiveFile = 8,           // machine
};

appDirStatus scanAppDirectories();

void fixAppDirectories(appDirStatus status);

void import(appFiles files);

bool pathExists(string path);
