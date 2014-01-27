#include "vf_dev_inv.h"
#include "vf_debug.h"

#include "windows.h"

using std::cin;
using std::cout;
using std::endl;

using namespace vapula;

void Assert(bool condition)
{
	if(condition) cout<<"[OK]"<<endl;
	else
	{
		cout<<"[Fail]"<<endl;
		system("pause");
		exit(0);
	}
}

void Test1(Library* lib)
{
	cout<<"[get invoker] ... ";
	Invoker* inv = lib->CreateInvoker("context");
	Assert(inv != NULL);

	cout<<"[invoke function context] ... ";
	Assert(inv->Start());

	Stack* stack = inv->GetStack();
	Context* ctx = stack->GetContext();
	while(ctx->GetCurrentState() != VF_STATE_IDLE)
	{
		float prog = ctx->GetProgress();
		if(prog > 10)
		{
			cout<<"[pause] progress:"<<prog<<endl;
			inv->Pause(50);
			break;
		}
		Sleep(50);
	}
	int step = 0;
	cout<<"has paused, wait for a moment"<<endl;
	while(step < 20)
	{
		step++;
		Sleep(50);
	}
	inv->Resume();
	float prog = ctx->GetProgress();
	cout<<"[resume] progress:"<<prog<<endl;
	while(ctx->GetCurrentState() != VF_STATE_IDLE) 
		Sleep(50);
	cout<<"finished"<<endl;
}

void Test2(Library* lib)
{
	DbgMemleak dbg;
	cout<<"[get invoker] ... ";
	Invoker* inv = lib->CreateInvoker("math");
	Assert(inv != NULL);

	Stack* stack = inv->GetStack();
	Envelope* env = stack->GetEnvelope();

	env->WriteValue(1, 12);
	env->WriteValue(2, 23);

	//dbg.Begin();
	cout<<"[invoke function math] ... ";
	Assert(inv->Start());

	Context* ctx = stack->GetContext();
	while(ctx->GetCurrentState() != VF_STATE_IDLE) 
		Sleep(50);
	//dbg.End();
	
	int result = env->ReadValue<int>(3);
	cout<<"<valid> - out:"<<result<<endl;

	LARGE_INTEGER freq, t1, t2;
	QueryPerformanceFrequency(&freq);
	QueryPerformanceCounter(&t1);
	for (int i=0;i<10000;i++)
	{
		env->WriteValue(1, 12);
		env->WriteValue(2, 23);
		inv->Start();
		while(ctx->GetCurrentState() != VF_STATE_IDLE) 
			Sleep(0);
		int result = env->ReadValue<int>(3);
	}
	QueryPerformanceCounter(&t2);
	cout<<"adv time:"<<(t2.QuadPart - t1.QuadPart) * 1000.0 / (float)freq.QuadPart<<" (ms)"<<endl;
	Clear(inv);
}

void Test3(Library* lib)
{
	cout<<"[get invoker] ... ";
	Invoker* inv = lib->CreateInvoker("output");
	Assert(inv != NULL);
	Stack* stack = inv->GetStack();

	Context* ctx = stack->GetContext();
	Envelope* env = stack->GetEnvelope();

	cout<<"[invoke function output] ... ";
	Assert(inv->Start());
	while(ctx->GetCurrentState() != VF_STATE_IDLE)
		Sleep(50);
	ShowMsgbox(env->ReadCh16(1));
	Clear(inv);
}

void Test4()
{
	vector<int*> vec;
	int* d = new int[100];
	vec.push_back(d);
	vec.clear();
	Scoped<int> data(d);
}

int main()
{
	//link driver manually
	//actually you can load library directly
	DriverHub* drv_hub = DriverHub::GetInstance();
	cout<<"[register driver crt] ... ";
	Assert(drv_hub->Link("crt"));
	//cout<<"[register driver clr] ... ";
	//Assert(drv_hub->Link("clr"));

	cout<<"[load library] ... ";
	astr8 dir(GetAppDir());
	string path = dir.get();
	path += "sample_lib.library";
	
	Library* lib = Library::Load(path.c_str());
	Assert(lib != NULL);

	cout<<"[mount library] ... ";
	Assert(lib->Mount());

	//Test1(lib);
	Test2(lib);
	Test3(lib);

	cout<<"[unmount component]"<<endl;
	lib->Unmount();

	cout<<"[kick all driver]"<<endl;
	drv_hub->KickAll();

	//_CrtDumpMemoryLeaks();
	system("pause");
	return 0;
}