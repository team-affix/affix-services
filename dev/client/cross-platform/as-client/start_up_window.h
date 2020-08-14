#ifndef START_UP_WINDOW_H
#define START_UP_WINDOW_H

#pragma once
#include "overhead_include.h"
#include <QMainWindow>

QT_BEGIN_NAMESPACE
namespace Ui { class start_up_window; }
QT_END_NAMESPACE

class start_up_window : public QMainWindow
{
    Q_OBJECT

public:
    start_up_window(QWidget *parent = nullptr);
    ~start_up_window();

private:
    Ui::start_up_window *ui;
};
#endif // START_UP_WINDOW_H
