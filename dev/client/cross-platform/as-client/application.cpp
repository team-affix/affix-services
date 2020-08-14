#include "application.h"

namespace application{

void start(){
    ptr<QMainWindow> s = new start_up_window();
    active_windows.push_back(s);
    s->show();
    return;
}
void stop(){
    for(ptr<QMainWindow> p : active_windows){
        p->close();
    }
}

}
