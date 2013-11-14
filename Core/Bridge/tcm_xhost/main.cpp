#include "stdafx.h"
#include "tcm_config.h"

#include "include/cef_app.h"
#include "include/cef_browser.h"
#include "include/cef_frame.h"
#include "include/cef_runnable.h"

#pragma comment(linker, "/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")  // NOLINT(whitespace/line_length)

using std::wstring;
using namespace tcm;

enum xHostReturnCode
{
	TCM_XHOST_RETURN_NORMAL = 0,
	TCM_XHOST_RETURN_INVALIDCMD = 1,
};

void CheckParam(LPWSTR* argv);
void ShowHelp();

int APIENTRY wWinMain(
	HINSTANCE hInstance, 
	HINSTANCE hPrevInstance, 
	LPWSTR lpCmdLine, 
	int nCmdShow)
{
	int argc = 0;
	LPWSTR* argv = CommandLineToArgvW(GetCommandLineW(),&argc);

	if(argc != 3)
	{
		ShowHelp();
		return TCM_XHOST_RETURN_INVALIDCMD;
	}

	CheckParam(argv);

	CefSettings settings;
	CefRefPtr<CefApp> app;

	// Populate the settings based on command line arguments.
	AppGetSettings(settings, app);

	// Initialize CEF.
	CefInitialize(settings, app);

	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_CEFCLIENT, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization
	if (!InitInstance (hInstance, nCmdShow))
		return FALSE;

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_CEFCLIENT));

	// Register the find event message.
	uFindMsg = RegisterWindowMessage(FINDMSGSTRING);

	int result = 0;

	if (!settings.multi_threaded_message_loop) {
		// Run the CEF message loop. This function will block until the application
		// recieves a WM_QUIT message.
		CefRunMessageLoop();
	} else {
		MSG msg;

		// Run the application message loop.
		while (GetMessage(&msg, NULL, 0, 0)) {
			// Allow processing of find dialog messages.
			if (hFindDlg && IsDialogMessage(hFindDlg, &msg))
				continue;

			if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg)) {
				TranslateMessage(&msg);
				DispatchMessage(&msg);
			}
		}

		result = static_cast<int>(msg.wParam);
	}

	// Shut down CEF.
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