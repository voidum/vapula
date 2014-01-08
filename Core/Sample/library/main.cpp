#include "main.h"
#include <iostream>

int Run()
{
	Stack* stack = Stack::GetInstance();
	int ret = 0;
	switch(stack->GetFunctionId())
	{
	case 1:
		ret = Function_Math();
		break;
	case 2:
		ret = Function_Out();
		break;
	case 3:
		ret = Function_TestArray();
		break;
	case 4:
		ret = Function_TestObject();
		break;
	case 5:
		ret = Function_TestContext();
		break;
	default:
		ret = VF_RETURN_NULLENTRY;
	}
	return ret;
}

//1st
int Function_Math()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	int a = env->ReadValue<int>(1);
	int b = env->ReadValue<int>(2);

	int c = a + b;

	ctx->SetProgress(100);
	env->WriteValue(3, c);
	return VF_RETURN_NORMAL;
}

//2nd
int Function_Out()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	cstr16 str = L"中文Engligh日本語テスト";
	ctx->SetProgress(100);
	env->WriteCh16(1, str);
	return VF_RETURN_NORMAL;
}

//3rd
int Function_TestArray()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	int count = env->GetLength(1);
	int* data = env->ReadArray<int>(1);

	int result = 0;
	for(int i=0;i<count;i++)
		result += data[i];

	ctx->SetProgress(100);
	env->WriteValue(1, result);
	return VF_RETURN_NORMAL;
}

//4th
int Function_TestObject()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	TestClassA* obj = (TestClassA*)env->ReadObject(1);
	bool ifinc = env->ReadValue<bool>(2);

	if(ifinc) obj->Inc();
	else obj->Dec();

	ctx->SetProgress(100);
	env->WriteObject(3, obj, sizeof(TestClassA));
	return VF_RETURN_NORMAL;
}

//5th
int Function_TestContext()
{
	Stack* stack = Stack::GetInstance();
	Envelope* env = stack->GetEnvelope();
	Context* ctx = stack->GetContext();

	for(int i=0;i<1000;i++)
	{
		int ctrl = ctx->GetCtrlCode();
		if(ctrl == VF_CTRL_CANCEL)
			return VF_RETURN_CANCELBYMSG;
		if(ctrl == VF_CTRL_PAUSE)
		{
			ctx->ReplyCtrlCode();
			for(;;)
			{
				int ctrl = ctx->GetCtrlCode();
				if(ctrl == VF_CTRL_RESUME)
				{
					ctx->ReplyCtrlCode();
					break;
				}
				Sleep(25);
			}
		}
		ctx->SetProgress(i / 10.0f);
		Sleep(25);
	}
	return VF_RETURN_NORMAL;
}