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
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_StartUpWindow
{
public:
    QWidget *centralwidget;
    QGridLayout *gridLayout;

    void setupUi(QMainWindow *StartUpWindow)
    {
        if (StartUpWindow->objectName().isEmpty())
            StartUpWindow->setObjectName(QString::fromUtf8("StartUpWindow"));
        StartUpWindow->resize(794, 640);
        StartUpWindow->setStyleSheet(QString::fromUtf8("background-color: rgb(20, 20, 20);"));
        centralwidget = new QWidget(StartUpWindow);
        centralwidget->setObjectName(QString::fromUtf8("centralwidget"));
        gridLayout = new QGridLayout(centralwidget);
        gridLayout->setObjectName(QString::fromUtf8("gridLayout"));
        StartUpWindow->setCentralWidget(centralwidget);

        retranslateUi(StartUpWindow);

        QMetaObject::connectSlotsByName(StartUpWindow);
    } // setupUi

    void retranslateUi(QMainWindow *StartUpWindow)
    {
        StartUpWindow->setWindowTitle(QCoreApplication::translate("StartUpWindow", "StartUpWindow", nullptr));
    } // retranslateUi

};

namespace Ui {
    class StartUpWindow: public Ui_StartUpWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_STARTUPWINDOW_H
