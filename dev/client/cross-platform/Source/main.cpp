#pragma once
#include "../../cross-platform/header/superHeader.h"
#include "../../cross-platform/header/startupwindow.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    StartUpWindow w;
    w.show();
    return a.exec();
}
