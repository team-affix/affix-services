#include "start_up_window.h"
#include "ui_start_up_window.h"

start_up_window::start_up_window(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::start_up_window)
{
    ui->setupUi(this);
}

start_up_window::~start_up_window()
{
    delete ui;
}

