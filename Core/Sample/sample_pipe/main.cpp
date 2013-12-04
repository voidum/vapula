#include "tcm_pipe.h"

using namespace tcm;

int main()
{
	Pipe* pipe = new Pipe();
	pipe->Listen();

	str content = "";
	pipe->Write(content);
	str content = pipe->Read(true);
	return 0;
};