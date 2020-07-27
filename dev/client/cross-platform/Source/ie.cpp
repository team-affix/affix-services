#include "../../cross-platform/header/ie.h"

// AppColors

void iAppColorsActive() {

    ifstream i(dirAppColorsActive);

    char line[24];

    i.read(line, 24);

}
void eAppColorsActive(){

    ofstream o(dirAppColorsActive);

    o << toString(appColorsActive);

}
