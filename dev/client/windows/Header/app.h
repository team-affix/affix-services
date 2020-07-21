#pragma once
#include "superHeader.h"
#include "startUpFrame.h"
#include "appColors.h"

class app : public wxApp
{
public:
	app();
	~app();

private:
	startUpFrame* m_frame1 = nullptr;

public:
	virtual bool OnInit();

public:
	static vector<appColors> appColorsConfigs;
	static UINT64 appColorsActiveConfigIndex;
};