#include "main.h"
#include <iostream>

//1st
void Process_Math()
{
	Stack* stack = Stack::Instance();
	Dataset* dataset = stack->GetDataset();
	Context* context = stack->GetContext();

	int a = (*dataset)[1]->ReadAt<int>();
	int b = (*dataset)[2]->ReadAt<int>();

	int c = a + b;

	(*dataset)[3]->WriteAt(c);

	context->SetProgress(100);
	context->SetReturnCode(VF_RETURN_NORMAL);
}

//2nd
void Process_Out()
{
	Stack* stack = Stack::Instance();
	Dataset* dataset = stack->GetDataset();
	Context* context = stack->GetContext();

	pcwstr str = L"中文Engligh日本語テスト";
	(*dataset)[1]->Write(str);

	context->SetProgress(100);
	context->SetReturnCode(VF_RETURN_NORMAL);
}

//3rd
void Process_Array()
{
	Stack* stack = Stack::Instance();
	Dataset* dataset = stack->GetDataset();
	Context* context = stack->GetContext();

	Record* record = (*dataset)[1];
	int* data = (int*)(record->Read());
	int count = record->GetSize() / sizeof(int);

	int result = 0;
	for(int i=0; i<count; i++)
		result += data[i];

	(*dataset)[2]->WriteAt(result);

	context->SetProgress(100);
	context->SetReturnCode(VF_RETURN_NORMAL);
}

//4th
void Process_Object()
{
	Stack* stack = Stack::Instance();
	Dataset* dataset = stack->GetDataset();
	Context* context = stack->GetContext();

	ClassA* object = (ClassA*)(*dataset)[1]->Read();
	bool inc = (*dataset)[2]->ReadAt<bool>();

	if (inc) object->Inc();
	else object->Dec();

	context->SetProgress(100);
	context->SetReturnCode(VF_RETURN_NORMAL);
}

//5th
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

//6th
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

//6th rollback
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

//7th
void Process_Protect()
{
	Runtime* runtime = Runtime::Instance();
	runtime->Reach("msg");
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
	Stack* stack = Stack::Instance();
	Dataset* dataset = stack->GetDataset();
	pcstr target = (pcstr)(*dataset)[1]->Read();

	Runtime* runtime = Runtime::Instance();
	Stack* stack_tar = (Stack*)runtime->SelectObject(VF_CORE_STACK, target);
}