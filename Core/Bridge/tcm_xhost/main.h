#pragma once

#include "include/cef_app.h"
#include "include/cef_browser.h"
#include "include/cef_frame.h"
#include "include/cef_runnable.h"

#include "xhost_app.h"
#include "xhost_client.h"
#include "xhost_config.h"

#include "tcm_config.h"

#pragma comment(linker, "/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

using std::wstring;
using namespace tcm;
using namespace tcm::xhost;

enum xHostReturnCode
{
	TCM_XHOST_RETURN_NORMAL = 0,
	TCM_XHOST_RETURN_INVALIDCMD = 1,
	TCM_XHOST_RETURN_FAILINIT = 2
};

bool CheckParam(LPWSTR* argv);
void ShowHelp();

bool InitWindow(HINSTANCE, int);
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);