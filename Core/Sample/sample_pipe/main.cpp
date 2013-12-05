#include "tcm_pipe.h"

using namespace tcm;
using namespace std;

int main()
{
	Pipe* pipe = new Pipe();
	pipe->Listen();
	str id = pipe->GetPipeId();
	cout<<"id:"<<id<<endl;

	string input;
	for(;;)
	{
		cin>>input;
		cout<<input;
	}
	return 0;
};