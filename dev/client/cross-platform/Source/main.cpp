#pragma once
#include "../../cross-platform/header/superHeader.h"
#include "../../cross-platform/header/startupwindow.h"
#include "../../cross-platform/header/convert.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    StartUpWindow w;
    string od = getOSDir("test\\123");
    w.show();
    return a.exec();
}
