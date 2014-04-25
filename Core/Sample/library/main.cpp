#include "main.h"
#include <iostream>

//1st
void Process_Math()
{
	Stack* stack = Stack::GetInstance();
	Dataset* ds = stack->GetDataset();
	Context* ctx = stack->GetContext();

	int a = (*ds)[1]->ReadAt<int>();
	int b = (*ds)[2]->ReadAt<int>();

	int c = a + b;

	(*ds)[3]->WriteAt(c);

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//2nd
void Process_Out()
{
	Stack* stack = Stack::GetInstance();
	Dataset* ds = stack->GetDataset();
	Context* ctx = stack->GetContext();

	pcwstr str = L"中文Engligh日本語テスト";
	(*ds)[1]->Write(str);

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//3rd
void Process_Array()
{
	Stack* stack = Stack::GetInstance();
	Dataset* ds = stack->GetDataset();
	Context* ctx = stack->GetContext();

	Record* rec = (*ds)[1];
	int* data = (int*)(rec->Read());
	int count = rec->GetSize() / sizeof(int);

	int result = 0;
	for(int i=0; i<count; i++)
		result += data[i];

	(*ds)[2]->WriteAt(result);

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//4th
void Process_Object()
{
	Stack* stack = Stack::GetInstance();
	Dataset* ds = stack->GetDataset();
	Context* ctx = stack->GetContext();

	ClassA* obj = (ClassA*)(*ds)[1]->Read();
	bool ifinc = (*ds)[2]->ReadAt<bool>();

	if(ifinc) obj->Inc();
	else obj->Dec();

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//5th
void Process_Context()
{
	Stack* stack = Stack::GetInstance();
	Context* ctx = stack->GetContext();

	for(int i=0;i<1000;i++)
	{
		int ctrl = ctx->GetCtrlCode();
		if(ctrl == VF_CTRL_CANCEL)
		{
			ctx->SetProgress(100);
			ctx->SetReturnCode(VF_RETURN_CANCEL);
			return;
		}
		if(ctrl == VF_CTRL_PAUSE)
		{
			ctx->SwitchHold();
			for(;;)
			{
				int ctrl = ctx->GetCtrlCode();
				if(ctrl == VF_CTRL_RESUME)
				{
					ctx->SwitchHold();
					break;
				}
				Sleep(20);
			}
		}
		ctx->SetProgress(i / 10.0f);
		Sleep(20);
	}

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//6th
void Process_Context2()
{
	Stack* stack = Stack::GetInstance();
	Context* ctx = stack->GetContext();

	int custom_error = 1001;
	for(int i=0; i<1000; i++)
	{
		if(i == 500)
			Error::Throw(custom_error);
		ctx->SetProgress(i / 10.0f);
		Sleep(20);
	}
}

//6th rollback
void Rollback_Context2()
{
	Stack* stack = Stack::GetInstance();
	Context* ctx = stack->GetContext();
	float prog = ctx->GetProgress();
	for(float i = prog; i > 0; i--)
	{
		ctx->SetProgress(i);
		Sleep(20);
	}
}

//7th
void Process_Protect()
{
	Weaver* weaver = Weaver::GetInstance();
	weaver->Reach("msg");
	int* err_ptr = null;
	err_ptr[0] = 42;
}

//7th rollback
void Rollback_Protect()
{
	int* err_ptr = null;
	err_ptr[0] = 42;
}

//8th
void Process_Msgbox()
{
	Stack* stack = Stack::GetInstance();
	Dataset* ds = stack->GetDataset();
	uint32 target = (*ds)[1]->ReadAt<uint32>();

	StackHub* stack_hub = StackHub::GetInstance();
	Stack* stack_tar = stack_hub->GetStack(target);

	pcstr ptrs = str::Value((uint32)stack_tar);
	ShowMsgbox(ptrs);
	ShowMsgbox("hello, world.");
}