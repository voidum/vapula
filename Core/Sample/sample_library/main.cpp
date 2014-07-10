#include "main.h"
#include <iostream>

//1st
void Process_Math()
{
	Stack* stack = Stack::Instance();
	Dataset* dataset = stack->GetDataset();
	Context* context = stack->GetContext();

	Pointer* pointer = new Pointer();
	Record* record1 = (*dataset)[1];
	pointer->Capture(record1->Read(), record1->GetSize());
	int* input = null;
	uint32 count = pointer->ReadArray(input);

	uint32 output = 0;
	for (uint32 i = 0; i < count; i++)
		output += (input[i]);

	Record* record2 = (*dataset)[2];
	//also we can write data in the following way:
	//record2->Write(&output, 1 * sizeof(uint32));

	//pointer make it more flexible
	pointer->Release();
	pointer->WriteValue(output);
	raw data = null;
	count = pointer->ReadArray(data);
	record2->Write(data, count);
	delete pointer;

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
		ShowMsgbox(data[0]);
	}
	runtime->Reach("aop");
}