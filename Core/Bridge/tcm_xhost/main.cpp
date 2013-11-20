#include "stdafx.h"
#include "main.h"

CefRefPtr<CefApp> app(new App());
CefRefPtr<CefClient> client(new Client());
CefRefPtr<CefBrowser> browser;

tcm::xhost::Config config;

int APIENTRY wWinMain(
	HINSTANCE hInstance, HINSTANCE hPrevInstance, 
	LPWSTR lpCmdLine, int nCmdShow)
{
	int argc = 0;
	LPWSTR* argv = CommandLineToArgvW(GetCommandLineW(),&argc);

	if(argc != 3)
	{
		ShowHelp();
		return TCM_XHOST_RETURN_INVALIDCMD;
	}

	if(!CheckParam(argv))
	{
		return TCM_XHOST_RETURN_INVALIDCMD;
	}

	CefSettings settings;
	settings.multi_threaded_message_loop = false;
	CefInitialize(settings, app);

	CefRefPtr<CefRequest> req = CefRequest::CreateRequest();

	if (!InitWindow(hInstance, nCmdShow))
		return TCM_XHOST_RETURN_FAILINIT;
	CefRunMessageLoop();
	
	//CefShutdown();
	return TCM_XHOST_RETURN_NORMAL;
}

bool CheckParam(LPWSTR* argv)
{
	int port = atoi(WcToMb(argv[1]));
	if(port < 0 || port > 65535)
	{
		ShowMsgStr("unsupported port", "TCM xHost");
		return false;
	}
	config.Port(port);
	config.AppId(argv[2]);
	return true;
}

void ShowHelp()
{
	string str = "command lines:\n";
	str += " tcm_xhost [port] [app-id]\n";
	str += "  - port : port to specific service provider\n";
	str += "  - app-id : id of application from provider\n";
	ShowMsgStr(str.c_str(), "TCM xHost");
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
	wclsex.lpszClassName = L"tcm_xhost";

	RegisterClassEx(&wclsex);

	HWND hWnd = CreateWindowEx(
		NULL,
		L"tcm_xhost", L"navigator",
		WS_OVERLAPPED | WS_CLIPCHILDREN |
		WS_CAPTION | WS_BORDER |
		WS_SYSMENU | WS_MINIMIZEBOX,
		200, 10, 500, 400,
		NULL, NULL, hInstance, NULL);

	if (!hWnd)
		return false;

	ShowWindow(hWnd, SW_SHOWNORMAL);
	UpdateWindow(hWnd); 
	return true;
}

void WndProc_WM_CREATE(HWND hWnd)
{
	RECT rect;
	CefWindowInfo wi;
	GetClientRect(hWnd, &rect);
	wi.SetAsChild(hWnd, rect);
	wstring str = L"http://localhost:";
	str += MbToWc(ValueToStr(config.Port()));
	str += L"/app/";
	str += config.AppId();
	ShowMsgStr(str.c_str());
	browser = CefBrowser::CreateBrowserSync(
		wi, client, 
		CefString(str), 
		CefBrowserSettings());
}

void WndProc_WM_SIZE(HWND hWnd)
{
	RECT rect;
	GetClientRect(hWnd, &rect);
	SetWindowPos(
		browser->GetWindowHandle(), 
		null, 
		rect.left, rect.top,
		rect.right, rect.bottom, 
		0);
}

void WndProc_WM_CLOSE(HWND hWnd)
{
	CefQuitMessageLoop();
	PostQuitMessage(0);
}

LRESULT CALLBACK WndProc(
	HWND hWnd, UINT uMsg, 
	WPARAM wParam, LPARAM lParam)
{
	switch(uMsg)
	{
	case WM_CREATE:
		WndProc_WM_CREATE(hWnd);
		break;
	case WM_SIZE:
		WndProc_WM_SIZE(hWnd);
		break;
	case WM_CLOSE:
		WndProc_WM_CLOSE(hWnd);
		break;
	}
	return DefWindowProc(hWnd, uMsg, wParam, lParam);
}