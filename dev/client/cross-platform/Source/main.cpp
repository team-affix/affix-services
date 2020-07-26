#pragma once
#include "../Header/superHeader.h"
#include "../Header/startupwindow.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    StartUpWindow w;
    w.show();
    return a.exec();
}
