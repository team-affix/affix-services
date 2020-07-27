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
        mkdir(dirMain.c_str());
    }
    if((status & appColorsExists) == 0){
        mkdir(dirAppColors.c_str());
    }
    if((status & appColorsActiveExists) == 0){
        eAppColorsActive();
    }
    if((status & accountsExists) == 0){
        mkdir(dirAccounts.c_str());
    }
    if((status & accountsLoggedInExists) == 0){
        o.open(dirAccountsLoggedIn.c_str());
        o.close();
    }
    if((status & accountsActiveIndexExists) == 0){
        o.open(dirAccountsActiveIndex.c_str());
        o.close();
    }
    if((status & machineExists) == 0){
        mkdir(dirMachine.c_str());
    }
    if((status & machineActiveExists) == 0){
        o.open(dirMachineActive.c_str());
        o.close();
    }
}

void import(appFiles files){
    if((files & appColorsActiveFile) != 0){
        iAppColorsActive();
    }
}

bool pathExists(string path){
    struct stat buffer;
    return (stat (path.c_str(), &buffer) == 0);
}
