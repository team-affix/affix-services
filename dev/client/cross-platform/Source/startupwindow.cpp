#include "../../cross-platform/header/startupwindow.h"
#include "ui_startupwindow.h"

StartUpWindow::StartUpWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::StartUpWindow)
{
    ui->setupUi(this);
    QPixmap affixLogo((dirLogos + "AffixLogoWhite.png").c_str());
    ui->lblAffixLogo->setPixmap(affixLogo.scaled(ui->lblAffixLogo->width(), ui->lblAffixLogo->height(), Qt::KeepAspectRatio));
}

StartUpWindow::~StartUpWindow()
{
    delete ui;
}

