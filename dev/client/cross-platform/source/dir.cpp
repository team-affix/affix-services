#pragma once
#include "../../cross-platform/header/dir.h"
#include "../../cross-platform/header/appVals.h"

appDirStatus scanAppDirectories(){
    uint result = 0;

    if(pathExists(dirMain)){
        result |= mainExists;
    }
    if(pathExists(dirAppColors)){
        result |= appColorsExists;
    }
    if(pathExists(dirAppColorsActive)){
        result |= appColorsActiveExists;
    }
    if(pathExists(dirAccounts)){
        result |= accountsExists;
    }
    if(pathExists(dirAccountsLoggedIn)){
        result |= accountsLoggedInExists;
    }
    if(pathExists(dirAccountsActiveIndex)){
        result |= accountsActiveIndexExists;
    }
    if(pathExists(dirMachine)){
        result |= machineExists;
    }
    if(pathExists(dirMachineActive)){
        result |= machineActiveExists;
    }

    return (appDirStatus)result;
}

void fixAppDirectories(appDirStatus status){

    ofstream o;

    if((status & mainExists) == 0){
        createDirectory(dirMain);
    }
    if((status & appColorsExists) == 0){
        createDirectory(dirAppColors);
    }
    if((status & appColorsActiveExists) == 0){
        eAppColorsActive();
    }
    if((status & accountsExists) == 0){
        createDirectory(dirAccounts);
    }
    if((status & accountsLoggedInExists) == 0){
        o.open(dirAccountsLoggedIn);
        o.close();
    }
    if((status & accountsActiveIndexExists) == 0){
        o.open(dirAccountsActiveIndex);
        o.close();
    }
    if((status & machineExists) == 0){
        createDirectory(dirMachine);
    }
    if((status & machineActiveExists) == 0){
        o.open(dirMachineActive);
        o.close();
    }
}

void import(appFiles files){
    if((files & appColorsActiveFile) != 0){
        iAppColorsActive();
    }
}

void createDirectory(const string& path){
    OS_CREATE_DIRECTORY
}

bool pathExists(string path){
    struct stat buffer;
    return (stat (path.c_str(), &buffer) == 0);
}
