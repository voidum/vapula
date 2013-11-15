#include "stdafx.h"

#include "include/cef_app.h"
#include "include/cef_browser.h"
#include "include/cef_frame.h"
#include "include/cef_runnable.h"

#include "xhost_app.h"
#include "xhost_client.h"

#include "tcm_config.h"

#pragma comment(linker, "/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

using std::wstring;
using namespace tcm;

enum xHostReturnCode
{
	TCM_XHOST_RETURN_NORMAL = 0,
	TCM_XHOST_RETURN_INVALIDCMD = 1,
	TCM_XHOST_RETURN_FAILINIT = 2
};

void CheckParam(LPWSTR* argv);
void ShowHelp();

HWND hWnd;
UINT uFindMsg;  // Message identifier for find events.
HWND hFindDlg = NULL;  // Handle for the find dialog.

CefWindowInfo windowInfo;
xHostClient client;
CefRefPtr<CefBrowser> browser;

bool InitWindow(HINSTANCE, int);
void StartMessageLoop();
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(
	HINSTANCE hInstance, HINSTANCE hPrevInstance, 
	LPWSTR lpCmdLine, int nCmdShow)
{
	int argc = 0;
	LPWSTR* argv = CommandLineToArgvW(GetCommandLineW(),&argc);

	if(argc != 3)
	{
		ShowHelp();
		//return TCM_XHOST_RETURN_INVALIDCMD;
	}

	CheckParam(argv);

	CefRefPtr<CefApp> app(new xHostApp());
	//CefMainArgs main_args(hInstance);
	CefSettings settings;
	settings.multi_threaded_message_loop = false;
	
	/*
	int exit_code = CefExecuteProcess(main_args, &app);
	if (exit_code >= 0)
		return exit_code;
	*/
	CefInitialize(settings, app);


	if (!InitWindow(hInstance, nCmdShow))
		return TCM_XHOST_RETURN_FAILINIT;
	//StartMessageLoop();
	
	CefShutdown();

	return TCM_XHOST_RETURN_NORMAL;
}

void CheckParam(LPWSTR* argv)
{
}

void ShowHelp()
{
	wstring str = L"command lines:\n";
	str += L" tcm_xhost [library directory] [data id]\n";
	ShowMsgStr(str.c_str(),L"TCM xHost");
}

bool InitWindow(HINSTANCE hInstance, int nCmdShow)
{
	WNDCLASSEXW wclsex;
	RtlZeroMemory(&wclsex, sizeof(WNDCLASSEX));

	wclsex.cbSize = sizeof(WNDCLASSEX);
	wclsex.style = CS_HREDRAW | CS_VREDRAW;
	wclsex.lpfnWndProc = WndProc;
	wclsex.cbClsExtra = 0;
	wclsex.cbWndExtra = 0;
	wclsex.hInstance = hInstance;

	wclsex.hIcon = NULL;
	wclsex.hIconSm = NULL; 
	wclsex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wclsex.hbrBackground = (HBRUSH)COLOR_BTNFACE;

	wclsex.lpszMenuName = NULL;
	wclsex.lpszClassName = L"tcm_xhost_window";

	RegisterClassEx(&wclsex);

	HWND hWnd = CreateWindowEx(
		NULL,
		L"tcm_xhost_window", L"navigator",
		WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		NULL, NULL, hInstance, NULL);

	if (!hWnd)
		return false;

	ShowWindow(hWnd, SW_SHOWDEFAULT);
	//ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd); 
	return true;
}

void StartMessageLoop()
{
	BOOL ret;
	MSG msg;
	WndProc(hWnd, WM_CREATE, 0, 0);
	WndProc(hWnd, WM_NCCREATE, 0, 0);
	while((ret = GetMessage(&msg, NULL, 0, 0)) != 0)
	{
		if (ret == -1)
			break;
		else
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}
}

LRESULT CALLBACK WndProc(
	HWND hWnd, UINT uMsg, 
	WPARAM wParam, LPARAM lParam)
{
	RECT rt;
	switch(uMsg)
	{
	case WM_CREATE:
		GetClientRect(hWnd, &rt);
		windowInfo.SetAsChild(hWnd, rt);
		browser = CefBrowser::CreateBrowserSync(windowInfo, &client, CefString("http://www.hoverlees.com"), CefBrowserSettings());
		//CefRunMessageLoop();
		break;
	case WM_SIZE:
		GetClientRect(hWnd,&rt);
		SetWindowPos(browser->GetWindowHandle(),0,rt.left,rt.top,rt.right,rt.bottom,0);
		break;
	case WM_CLOSE:
		CefQuitMessageLoop();
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, uMsg, wParam, lParam);
	}
}