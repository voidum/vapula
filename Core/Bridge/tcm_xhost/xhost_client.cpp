#include "stdafx.h"
#include "xhost_client.h"

xHostClient::xHostClient()
{
	_RefCount = 1;
}

int xHostClient::AddRef()
{
	_RefCount++;
	return _RefCount;
}

int xHostClient::Release()
{
	_RefCount--;
	return _RefCount;
}

int xHostClient::GetRefCt()
{
	return _RefCount;
}