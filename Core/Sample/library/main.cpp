#include "main.h"
#include <iostream>

int Run(int function, Envelope* envelope, Context* context)
{
	Context* ctx = context;
	int ret = 0;
	switch(function)
	{
	case 1:
		ret = Function_Math(envelope, ctx);
		break;
	case 2:
		ret = Function_Out(envelope, ctx);
		break;
	case 3:
		ret = Function_TestArray(envelope,ctx);
		break;
	case 4:
		ret = Function_TestObject(envelope,ctx);
		break;
	case 5:
		ret = Function_TestContext(envelope,ctx);
		break;
	default:
		ret = VF_RETURN_NULLENTRY;
	}
	return ret;
}

//第一个任务
int Function_Math(Envelope* envelope, Context* context)
{
	int a = envelope->ReadValue<int>(1);
	int b = envelope->ReadValue<int>(2);

	int c = a + b;

	context->SetProgress(100);
	envelope->WriteValue(3, c);
	return VF_RETURN_NORMAL;
}

//第二个任务
int Function_Out(Envelope* envelope,Context* context)
{
	cstr16 str = L"中文Engligh日本語テスト";
	context->SetProgress(100);
	envelope->WriteCh16(1, str);
	return VF_RETURN_NORMAL;
}

//第三个任务
int Function_TestArray(Envelope* envelope,Context* context)
{
	int count = envelope->GetLength(1);
	int* data = envelope->ReadArray<int>(1);

	int result = 0;
	for(int i=0;i<count;i++)
		result += data[i];

	envelope->WriteValue(1, result);
	return VF_RETURN_NORMAL;
}

//第四个任务
int Function_TestObject(Envelope* envelope,Context* context)
{
	TestClassA* obj = (TestClassA*)envelope->ReadObject(1);
	bool ifinc = envelope->ReadValue<bool>(2);

	if(ifinc) obj->Inc();
	else obj->Dec();

	envelope->WriteObject(3, obj, sizeof(TestClassA));
	return VF_RETURN_NORMAL;
}

//第五个任务
int Function_TestContext(Envelope* envelope,Context* context)
{
	for(int i=0;i<1000;i++)
	{
		int ctrl = context->GetCtrlCode();
		if(ctrl == VF_CTRL_CANCEL)
			return VF_RETURN_CANCELBYMSG;
		if(ctrl == VF_CTRL_PAUSE)
		{
			context->ReplyCtrlCode();
			for(;;)
			{
				int ctrl = context->GetCtrlCode();
				if(ctrl == VF_CTRL_RESUME)
				{
					context->ReplyCtrlCode();
					break;
				}
				Sleep(25);
			}
		}
		context->SetProgress(i / 10.0f);
		Sleep(25);
	}
	return VF_RETURN_NORMAL;
}