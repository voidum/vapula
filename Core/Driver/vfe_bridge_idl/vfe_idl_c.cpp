#include "vfe_idl_c.h"

int vfeEntry_IDL(int argc, object argv[])
{
	ShowMsgbox(argc);
	for(int i=0; i<argc; i++)
	{
		IDL_STRING* v = (IDL_STRING*)(argv[i]);
		pcstr cs8 = str::Copy(v->s);
		ShowMsgbox(cs8);
	}
	return 0;
}