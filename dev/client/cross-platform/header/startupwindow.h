#ifndef STARTUPWINDOW_H
#define STARTUPWINDOW_H

#include <QMainWindow>

QT_BEGIN_NAMESPACE
namespace Ui { class StartUpWindow; }
QT_END_NAMESPACE

class StartUpWindow : public QMainWindow
{
    Q_OBJECT

public:
    StartUpWindow(QWidget *parent = nullptr);
    ~StartUpWindow();

private:
    Ui::StartUpWindow *ui;
};
#endif // STARTUPWINDOW_H
