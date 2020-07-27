#include "../../cross-platform/header/convert.h"


// string

string toBase(string in, uint from, uint to, uint padding){

    string baseConv = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_-+=";

    // Convert to base 10
    uint b10 = 0;

    int inLength = in.length();
    for(int i = 0; i < inLength; i++){
        b10 += baseConv.find(in[i]) * pow(from, inLength - i - 1);
    }

    // Convert to base "to"
    string result;

    while(b10 > 0){
        result = baseConv[b10 % to] + result;
        b10 /= to;
    }

    int resultLength = result.length();

    for(int i = padding; i > resultLength; i--){
        result = "0" + result;
    }
    return result;

}

// Directories

string getOSDir(string winDir) {
	OS_PATH_CONVERT
}

// Colors

QColor toQColor(const color& c) {
    QColor result = QColor(c.R, c.G, c.B, c.A);
	return result;
}

color toColor(const QColor& q) {
    color result = color();
    result.R = q.red();
    result.G = q.green();
    result.B = q.blue();
    result.A = q.alpha();
	return result;
}

color toColor(const string& s){

    color result = color();
    result.R = (uint8_t)stoul(toBase(s.substr(0, 2), 16, 10, 0));
    result.G = (uint8_t)stoul(toBase(s.substr(2, 2), 16, 10, 0));
    result.B = (uint8_t)stoul(toBase(s.substr(4, 2), 16, 10, 0));
    result.A = (uint8_t)stoul(toBase(s.substr(6, 2), 16, 10, 0));
    return result;

}

string toString(const color& c){

    string result =
            toBase(to_string(c.R), 10, 16, 2) +
            toBase(to_string(c.G), 10, 16, 2) +
            toBase(to_string(c.B), 10, 16, 2) +
            toBase(to_string(c.A), 10, 16, 2);

    return result;
}

// AppColors

string toString(const appColors& a){

    string result =
            toString(a.backColor0) +
            toString(a.backColor1) +
            toString(a.backColor2) +
            toString(a.panelColor0) +
            toString(a.panelColor1) +
            toString(a.panelColor2) +
            toString(a.textColor0) +
            toString(a.textColor1) +
            toString(a.textColor2) +
            toString(a.outlineColor0) +
            toString(a.outlineColor1) +
            toString(a.outlineColor2);
    return result;

}

appColors toAppColors(const string& s){

    appColors result;
    result.backColor0 = toColor(s.substr(0, 2));
    result.backColor1 = toColor(s.substr(2, 2));
    result.backColor2 = toColor(s.substr(4, 2));
    result.panelColor0 = toColor(s.substr(6, 2));
    result.panelColor1 = toColor(s.substr(8, 2));
    result.panelColor2 = toColor(s.substr(10, 2));
    result.textColor0 = toColor(s.substr(12, 2));
    result.textColor1 = toColor(s.substr(14, 2));
    result.textColor2 = toColor(s.substr(16, 2));
    result.outlineColor0 = toColor(s.substr(18, 2));
    result.outlineColor1 = toColor(s.substr(20, 2));
    result.outlineColor2 = toColor(s.substr(22, 2));
    return result;

}
