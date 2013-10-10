#include "tcm_xproc.h"

#include <iostream>

using namespace std;
using namespace tcm;

int main()
{
	Pipe* p = new Pipe();
	cout<<"listen(y/n)?";
	char cmd;
	cin>>cmd;

	if(cmd != 'y')
	{
		string id;
		cout<<"enter id:";
		cin>>id;
		if(!p->Connect(MbToWc(id.c_str())))
		{
			cout<<"fail to connect"<<endl;
			return -1;
		}
		while(true)
		{
			if(p->GetFlag(TCM_PIPE_GLOBAL))
			{
				cout<<"server close"<<endl;
				break;
			}
			if(p->GetFlag(TCM_PIPE_S2C))
			{
				int v;
				p->Read(&v);
				cout<<"recv:"<<v<<endl;
			}
			Sleep(50);
		}
		p->SetFlag(TCM_PIPE_GLOBAL, false);
		system("pause");
		int i=0;
		while(i<10)
		{
			int v = rand();
			p->Write(&v, 4);
			cout<<"send:"<<v<<endl;
			Sleep(500);
			i++;
		}
		p->SetFlag(TCM_PIPE_GLOBAL, true);
	}
	else
	{
		p->Listen();
		PCSTR id = tcm::WcToMb(p->GetPipeId());
		cout<<"id:"<<id<<endl;
		system("pause");
		int i=0;
		while(i<10)
		{
			int v = rand();
			p->Write(&v, 4);
			cout<<"send:"<<v<<endl;
			Sleep(500);
			i++;
		}
		p->SetFlag(TCM_PIPE_GLOBAL, true);
		system("pause");
		while(true)
		{
			if(p->GetFlag(TCM_PIPE_GLOBAL))
			{
				cout<<"server close"<<endl;
				break;
			}
			if(p->GetFlag(TCM_PIPE_C2S))
			{
				int v;
				p->Read(&v);
				cout<<"recv:"<<v<<endl;
			}
			Sleep(50);
		}
	}
	delete p;
	return 0;
}