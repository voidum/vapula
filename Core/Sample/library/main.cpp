#include "main.h"
#include <iostream>

//1st
void Process_Math()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	//TI2 will ignore invalid operation
	//stack->SetEnvelope(null);

	int a = env->ReadValue<int>(1);
	int b = env->ReadValue<int>(2);

	int c = a + b;

	env->WriteValue(3, c);

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//2nd
void Process_Out()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	pcwstr str = L"中文Engligh日本語テスト";
	env->WriteStrW(1, str);

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//3rd
void Process_Array()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	int count = env->GetLength(1);
	int* data = env->ReadArray<int>(1);

	int result = 0;
	for(int i=0;i<count;i++)
		result += data[i];

	env->WriteValue(1, result);

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//4th
void Process_Object()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	ClassA* obj = (ClassA*)env->ReadObject(1);
	bool ifinc = env->ReadValue<bool>(2);

	if(ifinc) obj->Inc();
	else obj->Dec();

	env->WriteObject(3, obj, sizeof(ClassA));

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
	int* err_ptr = null;
	err_ptr[0] = 42;
}

//7th rollback
void Rollback_Protect()
{
	int* err_ptr = null;
	err_ptr[0] = 42;
}