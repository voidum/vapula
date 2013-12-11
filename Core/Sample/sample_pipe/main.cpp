#include "pipe.h"

int main()
{
	cout<<"[Vapula Pipe TEST]"<<endl;
	cout<<">mode:";
	string input;
	cin>>input;

	if(input == "server")
		Pipe_Server();
	else if(input == "client")
		Pipe_Client();
	return 0;
};