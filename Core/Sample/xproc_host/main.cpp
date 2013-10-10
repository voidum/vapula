#include "tcm_context.h"
#include "tcm_pipe.h"
#include "tcm_xml.h"

#include <iostream>

using namespace std;
using namespace tcm;

void CreateHost()
{
	STARTUPINFO si;
	PROCESS_INFORMATION pi;
	ZeroMemory(&si, sizeof(si));
	si.cb = sizeof(si);
	ZeroMemory(&pi, sizeof(pi));

	LPWSTR cmd = _wcsdup(L"\"E:\\Projects\\TCM\\tcm_bridge\\OutDir\\debug-vc10\\tcm_host.exe\" \"E:\\Projects\\TCM\\tcm_bridge\\OutDir\\debug-vc10\\task.xml\"");
	CreateProcessW(NULL,cmd,NULL,NULL,FALSE,NULL,NULL,NULL,&si,&pi);
}

int ReadState(LPVOID data)
{
	int ret = ((int*)((ULONG)data+1))[0];
	return ret;
}

float ReadProg(LPVOID data)
{
	float ret = ((float*)((ULONG)data+1))[1];
	return ret;
}

int main()
{
	Pipe* p = new Pipe();
	p->Listen();
	PCSTR id = tcm::WcToMb(p->GetPipeId());
	cout<<"id:"<<id<<endl;

	system("pause");
	cout<<"==== Stage A ===="<<endl;

	//CreateHost();
	LPVOID pipe_r = NULL;
	LPVOID pipe_w = NULL;

	pipe_r = p->WaitRead(0);
	if(pipe_r == NULL || ((char*)pipe_r)[0] != 'A')
	{
		cout<<"fail to wait for stage A"<<endl;
		system("pause");
		return 0;
	}
	delete pipe_r;
	pipe_w = new int[1];
	*((int*)pipe_w) = 0;
	p->Write(pipe_w, 4);

	cout<<"==== Stage B ===="<<endl;


	HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
	//COORD coord = {0, 0};
	CONSOLE_SCREEN_BUFFER_INFO csbi;
	GetConsoleScreenBufferInfo(hConsole, &csbi);

	pipe_r = p->WaitRead(10000);
	if(pipe_r == NULL || *((char*)pipe_r) != 'B')
	{
		cout<<"fail to wait for stage B"<<endl;
		system("pause");
		return 0;
	}
	while(ReadState(pipe_r) != TCM_STATE_IDLE)
	{
		int state = ReadState(pipe_r);
		float prog = ReadProg(pipe_r);
		p->Read(pipe_r);
		SetConsoleCursorPosition(hConsole, csbi.dwCursorPosition);;
		cout<<"state:"<<state<<" progress:"<<prog<<endl;
		*((int*)pipe_w) = TCM_CTRL_NULL;
		p->Write(pipe_w, 4);
		Sleep(50);
	}

	cout<<"==== Stage C ===="<<endl;
	pipe_r = p->WaitRead(10000);
	if(pipe_r == NULL || *((char*)pipe_r) != 'C')
	{
		cout<<"fail to wait for stage C"<<endl;
		system("pause");
		return 0;
	}
	std::string str = (PCSTR)pipe_r;
	str = str.substr(1);
	cout<<"addition:\n"<<str<<endl;
	system("pause");

	return 0;
}