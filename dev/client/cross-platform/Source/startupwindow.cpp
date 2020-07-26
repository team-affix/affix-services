#include "../../cross-platform/header/startupwindow.h"
#include "ui_startupwindow.h"

StartUpWindow::StartUpWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::StartUpWindow)
{
    ui->setupUi(this);
}

StartUpWindow::~StartUpWindow()
{
    delete ui;
}

