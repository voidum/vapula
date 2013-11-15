#pragma once

#include "include/cef_app.h"

class xHostApp : 
	public CefApp
{
private:
	int _RefCount;
public:
	xHostApp();

	virtual int AddRef();

	virtual int Release();

	virtual int GetRefCt();
};