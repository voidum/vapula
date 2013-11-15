#include "stdafx.h"
#include "xhost_app.h"

xHostApp::xHostApp()
{
	_RefCount = 1;
}

int xHostApp::AddRef()
{
	_RefCount++;
	return _RefCount;
}

int xHostApp::Release()
{
	_RefCount--;
	return _RefCount;
}

int xHostApp::GetRefCt()
{
	return _RefCount;
}