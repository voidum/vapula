#include "main.h"
#include <iostream>

//1st
void Process_Math()
{
	Stack* stack = Stack::Instance();
	Dataset* dataset = stack->GetDataset();
	Context* context = stack->GetContext();

	Record* record = (*dataset)[1];
	int* data = (int*)record->Read();
	uint32 count = record->GetSize() / sizeof(int);

	int result = 0;
	for (uint32 i = 0; i < count; i++)
		result += (data[i]);

	(*dataset)[2]->Write(&result, 1 * sizeof(int));

	context->SetProgress(100);
	context->SetReturnCode(VF_RETURN_NORMAL);
}

//2nd
void Process_Out()
{
	Stack* stack = Stack::Instance();
	Dataset* dataset = stack->GetDataset();
	Context* context = stack->GetContext();

	pcwstr cs16 = L"中文Engligh日本語テスト";
	pcstr cs8 = str::ToStr(cs16);
	(*dataset)[1]->Write((raw)cs8, strlen(cs8) + 1);

	context->SetProgress(100);
	context->SetReturnCode(VF_RETURN_NORMAL);
}

//3rd
void Process_Context()
{
	Stack* stack = Stack::Instance();
	Context* context = stack->GetContext();

	for(int i=0;i<1000;i++)
	{
		int code = context->GetControlCode();
		if (code == VF_CTRL_CANCEL)
		{
			context->SetProgress(100);
			context->SetReturnCode(VF_RETURN_CANCEL);
			return;
		}
		if(code == VF_CTRL_PAUSE)
		{
			context->SwitchHold();
			for(;;)
			{
				int code = context->GetControlCode();
				if(code == VF_CTRL_RESUME)
				{
					context->SwitchHold();
					break;
				}
				Sleep(20);
			}
		}
		context->SetProgress(i / 10.0f);
		Sleep(20);
	}

	context->SetProgress(100);
	context->SetReturnCode(VF_RETURN_NORMAL);
}

//4th
void Process_Context2()
{
	Stack* stack = Stack::Instance();
	Context* context = stack->GetContext();

	int custom_error = 1001;
	for(int i=0; i<1000; i++)
	{
		if(i == 500)
			Error::Throw(custom_error);
		context->SetProgress(i / 10.0f);
		Sleep(20);
	}
}

//4th rollback
void Rollback_Context2()
{
	Stack* stack = Stack::Instance();
	Context* context = stack->GetContext();
	float progress = context->GetProgress();
	for (float i = progress; i > 0; i--)
	{
		context->SetProgress(i);
		Sleep(20);
	}
}

//5th
void Process_Protect()
{
	int* err_ptr = null;
	err_ptr[0] = 42;
}

//5th rollback
void Rollback_Protect()
{
	int* err_ptr = null;
	err_ptr[0] = 42;
}

//6th
void Process_AOP()
{
	Runtime* runtime = Runtime::Instance();

	Stack* stack = Stack::Instance();
	Dataset* dataset = stack->GetDataset();
	Record* record = (*dataset)[1];
	if (record->GetSize() > 0)
	{
		uint32* data = (uint32*)record->Read();
		Stack* stack_attached = (Stack*)data[0];
		ShowMsgbox(data[0]);
	}
	runtime->Reach("aop");
}