/********************************************************************************
** Form generated from reading UI file 'startupwindow.ui'
**
** Created by: Qt User Interface Compiler version 5.15.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_STARTUPWINDOW_H
#define UI_STARTUPWINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QLabel>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QStackedWidget>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_StartUpWindow
{
public:
    QWidget *centralwidget;
    QHBoxLayout *horizontalLayout;
    QStackedWidget *swMain;
    QWidget *pgLoading;
    QWidget *gridLayoutWidget;
    QGridLayout *gridLayout;
    QLabel *lblAffixLogo;
    QWidget *pgLogin;
    QWidget *pgSignUp;
    QWidget *pgHome;

    void setupUi(QMainWindow *StartUpWindow)
    {
        if (StartUpWindow->objectName().isEmpty())
            StartUpWindow->setObjectName(QString::fromUtf8("StartUpWindow"));
        StartUpWindow->resize(951, 640);
        StartUpWindow->setStyleSheet(QString::fromUtf8("background-color: rgb(20, 20, 20);"));
        centralwidget = new QWidget(StartUpWindow);
        centralwidget->setObjectName(QString::fromUtf8("centralwidget"));
        horizontalLayout = new QHBoxLayout(centralwidget);
        horizontalLayout->setObjectName(QString::fromUtf8("horizontalLayout"));
        swMain = new QStackedWidget(centralwidget);
        swMain->setObjectName(QString::fromUtf8("swMain"));
        swMain->setEnabled(true);
        pgLoading = new QWidget();
        pgLoading->setObjectName(QString::fromUtf8("pgLoading"));
        gridLayoutWidget = new QWidget(pgLoading);
        gridLayoutWidget->setObjectName(QString::fromUtf8("gridLayoutWidget"));
        gridLayoutWidget->setGeometry(QRect(-10, 150, 951, 241));
        gridLayout = new QGridLayout(gridLayoutWidget);
        gridLayout->setObjectName(QString::fromUtf8("gridLayout"));
        gridLayout->setContentsMargins(0, 0, 0, 0);
        lblAffixLogo = new QLabel(gridLayoutWidget);
        lblAffixLogo->setObjectName(QString::fromUtf8("lblAffixLogo"));
        lblAffixLogo->setMaximumSize(QSize(500, 16777215));
        lblAffixLogo->setStyleSheet(QString::fromUtf8("background-color: rgb(20, 20, 20);"));

        gridLayout->addWidget(lblAffixLogo, 0, 0, 1, 1);

        swMain->addWidget(pgLoading);
        pgLogin = new QWidget();
        pgLogin->setObjectName(QString::fromUtf8("pgLogin"));
        swMain->addWidget(pgLogin);
        pgSignUp = new QWidget();
        pgSignUp->setObjectName(QString::fromUtf8("pgSignUp"));
        swMain->addWidget(pgSignUp);
        pgHome = new QWidget();
        pgHome->setObjectName(QString::fromUtf8("pgHome"));
        swMain->addWidget(pgHome);

        horizontalLayout->addWidget(swMain);

        StartUpWindow->setCentralWidget(centralwidget);

        retranslateUi(StartUpWindow);

        swMain->setCurrentIndex(0);


        QMetaObject::connectSlotsByName(StartUpWindow);
    } // setupUi

    void retranslateUi(QMainWindow *StartUpWindow)
    {
        StartUpWindow->setWindowTitle(QCoreApplication::translate("StartUpWindow", "StartUpWindow", nullptr));
        lblAffixLogo->setText(QString());
    } // retranslateUi

};

namespace Ui {
    class StartUpWindow: public Ui_StartUpWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_STARTUPWINDOW_H
