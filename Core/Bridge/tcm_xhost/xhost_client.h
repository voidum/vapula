#pragma once

#include "include/cef_client.h"

class xHostClient : 
	public CefClient
{
private:
	int _RefCount;
public:
	xHostClient();
	
	virtual int AddRef();
	
	virtual int Release();

	virtual int GetRefCt();
};