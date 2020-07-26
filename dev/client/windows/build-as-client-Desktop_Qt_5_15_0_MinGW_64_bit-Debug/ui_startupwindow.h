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
#include <QtWidgets/QPushButton>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_StartUpWindow
{
public:
    QWidget *centralwidget;
    QGridLayout *gridLayout;
    QPushButton *pushButton;

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
        pushButton = new QPushButton(centralwidget);
        pushButton->setObjectName(QString::fromUtf8("pushButton"));
        pushButton->setMinimumSize(QSize(0, 100));
        pushButton->setMaximumSize(QSize(300, 16777215));
        pushButton->setStyleSheet(QString::fromUtf8("border-style: outset;\n"
"border-width: 0px;\n"
"border-radius: 20px;\n"
"border-color: rgb(30, 30, 30);\n"
"background-color: rgb(30, 30, 30);\n"
"color: rgb(123, 255, 156);\n"
"font: 25 14pt \"Segoe UI\";"));

        gridLayout->addWidget(pushButton, 0, 0, 1, 1);

        StartUpWindow->setCentralWidget(centralwidget);

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
