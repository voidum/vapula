#include "main.h"
#include <iostream>

void Run()
{
	Stack* stack = Stack::GetInstance();
	switch(stack->GetMethodId())
	{
	case 1: Function_Math(); break;
	case 2: Function_Out(); break;
	case 3: Function_TestArray(); break;
	case 4: Function_TestObject(); break;
	case 5: Function_TestContext(); break;
	default: 
		stack->GetContext()->SetReturnCode(VF_RETURN_NULLENTRY);
		break;
	}
}

//1st
void Function_Math()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	int a = env->ReadValue<int>(1);
	int b = env->ReadValue<int>(2);

	int c = a + b;

	env->WriteValue(3, c);

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//2nd
void Function_Out()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();
	stack->SetEnvelope(null);

	cstr16 str = L"中文Engligh日本語テスト";
	env->WriteCh16(1, str);

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//3rd
void Function_TestArray()
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
void Function_TestObject()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	TestClassA* obj = (TestClassA*)env->ReadObject(1);
	bool ifinc = env->ReadValue<bool>(2);

	if(ifinc) obj->Inc();
	else obj->Dec();

	env->WriteObject(3, obj, sizeof(TestClassA));

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}

//5th
void Function_TestContext()
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
				Sleep(25);
			}
		}
		ctx->SetProgress(i / 10.0f);
		Sleep(25);
	}

	ctx->SetProgress(100);
	ctx->SetReturnCode(VF_RETURN_NORMAL);
}