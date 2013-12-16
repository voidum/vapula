#include "pipe.h"

void Pipe_Server()
{
	Pipe* pipe = new Pipe();
	pipe->Listen();
	cstr8 id = pipe->GetPipeId();
	cout<<"id:"<<id<<endl;
	cout<<"[pause] <you can start another pipe as client>"<<endl;
	string input;
	cin>>input;
	cout<<"-------------"<<endl;
	for(;;)
	{
		cout<<"input:";
		cin>>input;
		if(input == "quit")
			break;
		pipe->Write(str::EncodeCh8(input.c_str(), null, "utf8"));
	}
	pipe->Close();
}

void Pipe_Client()
{
	Pipe* pipe = new Pipe();
	string input;
	cout<<"id:";
	cin>>input;
	if(!pipe->Connect(input.c_str()))
	{
		cout<<"[pause] <fail to connect pipe>"<<endl;
		cin>>input;
		return;
	}
	cout<<"-------------"<<endl;
	for(;;)
	{
		if(pipe->IsClose())
			break;
		if(pipe->HasNewData())
		{
			cstr8 msg = pipe->Read();
			cout<<"recv: "<<msg<<endl;
		}
		Sleep(20);
	}
	cout<<"-- close --"<<endl;
}