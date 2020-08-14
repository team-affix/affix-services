/********************************************************************************
** Form generated from reading UI file 'start_up_window.ui'
**
** Created by: Qt User Interface Compiler version 5.15.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_START_UP_WINDOW_H
#define UI_START_UP_WINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QGraphicsView>
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QLabel>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_start_up_window
{
public:
    QWidget *centralwidget;
    QHBoxLayout *horizontalLayout;
    QVBoxLayout *verticalLayout_center;
    QGraphicsView *graphicsView_center;
    QLabel *label_affix_services;
    QMenuBar *menubar;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *start_up_window)
    {
        if (start_up_window->objectName().isEmpty())
            start_up_window->setObjectName(QString::fromUtf8("start_up_window"));
        start_up_window->resize(583, 462);
        start_up_window->setStyleSheet(QString::fromUtf8("background-color: rgb(255, 255, 255);"));
        centralwidget = new QWidget(start_up_window);
        centralwidget->setObjectName(QString::fromUtf8("centralwidget"));
        horizontalLayout = new QHBoxLayout(centralwidget);
        horizontalLayout->setObjectName(QString::fromUtf8("horizontalLayout"));
        verticalLayout_center = new QVBoxLayout();
        verticalLayout_center->setObjectName(QString::fromUtf8("verticalLayout_center"));
        graphicsView_center = new QGraphicsView(centralwidget);
        graphicsView_center->setObjectName(QString::fromUtf8("graphicsView_center"));
        graphicsView_center->setStyleSheet(QString::fromUtf8("border-top: 2px solid;\n"
"border-top-color: rgb(0, 170, 255);"));

        verticalLayout_center->addWidget(graphicsView_center);

        label_affix_services = new QLabel(centralwidget);
        label_affix_services->setObjectName(QString::fromUtf8("label_affix_services"));
        QFont font;
        font.setFamily(QString::fromUtf8("Segoe UI"));
        font.setPointSize(10);
        label_affix_services->setFont(font);
        label_affix_services->setStyleSheet(QString::fromUtf8("color: rgb(0, 170, 255);"));
        label_affix_services->setTextFormat(Qt::AutoText);
        label_affix_services->setAlignment(Qt::AlignCenter);

        verticalLayout_center->addWidget(label_affix_services);


        horizontalLayout->addLayout(verticalLayout_center);

        start_up_window->setCentralWidget(centralwidget);
        menubar = new QMenuBar(start_up_window);
        menubar->setObjectName(QString::fromUtf8("menubar"));
        menubar->setGeometry(QRect(0, 0, 583, 20));
        start_up_window->setMenuBar(menubar);
        statusbar = new QStatusBar(start_up_window);
        statusbar->setObjectName(QString::fromUtf8("statusbar"));
        start_up_window->setStatusBar(statusbar);

        retranslateUi(start_up_window);

        QMetaObject::connectSlotsByName(start_up_window);
    } // setupUi

    void retranslateUi(QMainWindow *start_up_window)
    {
        start_up_window->setWindowTitle(QCoreApplication::translate("start_up_window", "start_up_window", nullptr));
        label_affix_services->setText(QCoreApplication::translate("start_up_window", "Affix Services", nullptr));
    } // retranslateUi

};

namespace Ui {
    class start_up_window: public Ui_start_up_window {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_START_UP_WINDOW_H
