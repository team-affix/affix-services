#pragma once
#include "superHeader.h"

using namespace std;

class startUpFrame : wxFrame{
public:
	startUpFrame();
	~startUpFrame();

public:
	void Show();
	void OBC(wxCommandEvent& wce);

public:
	wxButton* btn;
};