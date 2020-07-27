#pragma once
#include "../../cross-platform/header/superHeader.h"
#include "../../cross-platform/header/startupwindow.h"
#include "../../cross-platform/header/dir.h"
#include "../../cross-platform/header/convert.h"

int main(int argc, char *argv[])
{
    // Fix the application "main" directory and all subdirectories
    fixAppDirectories(scanAppDirectories());
    iAppColorsActive();

    QApplication a(argc, argv);
    StartUpWindow w;
    w.show();
    return a.exec();
}

