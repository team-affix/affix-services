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
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_StartUpWindow
{
public:
    QWidget *centralwidget;
    QPushButton *pushButton;
    QMenuBar *menubar;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *StartUpWindow)
    {
        if (StartUpWindow->objectName().isEmpty())
            StartUpWindow->setObjectName(QString::fromUtf8("StartUpWindow"));
        StartUpWindow->resize(800, 600);
        centralwidget = new QWidget(StartUpWindow);
        centralwidget->setObjectName(QString::fromUtf8("centralwidget"));
        pushButton = new QPushButton(centralwidget);
        pushButton->setObjectName(QString::fromUtf8("pushButton"));
        pushButton->setGeometry(QRect(290, 200, 111, 41));
        StartUpWindow->setCentralWidget(centralwidget);
        menubar = new QMenuBar(StartUpWindow);
        menubar->setObjectName(QString::fromUtf8("menubar"));
        menubar->setGeometry(QRect(0, 0, 800, 20));
        StartUpWindow->setMenuBar(menubar);
        statusbar = new QStatusBar(StartUpWindow);
        statusbar->setObjectName(QString::fromUtf8("statusbar"));
        StartUpWindow->setStatusBar(statusbar);

        retranslateUi(StartUpWindow);

        QMetaObject::connectSlotsByName(StartUpWindow);
    } // setupUi

    void retranslateUi(QMainWindow *StartUpWindow)
    {
        StartUpWindow->setWindowTitle(QCoreApplication::translate("StartUpWindow", "StartUpWindow", nullptr));
        pushButton->setText(QCoreApplication::translate("StartUpWindow", "PushButton", nullptr));
    } // retranslateUi

};

namespace Ui {
    class StartUpWindow: public Ui_StartUpWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_STARTUPWINDOW_H
