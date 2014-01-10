#include "vf_dev_inv.h"
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
	Invoker* inv = lib->CreateInvoker(5);
	Assert(inv != NULL);

	cout<<"[invoke function 5] ... ";
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
	cout<<"[get invoker] ... ";
	Invoker* inv = lib->CreateInvoker(1);
	Assert(inv != NULL);
	Stack* stack = inv->GetStack();

	cout<<"[get envelope] ... ";
	Envelope* env = stack->GetEnvelope();
	Assert(env != NULL);

	cout<<"[set params]"<<endl;
	env->WriteValue(1, 12);
	env->WriteValue(2, 23);

	cout<<"[Invoke function 0] ... ";
	Assert(inv->Start());

	Context* ctx = stack->GetContext();
	while(ctx->GetCurrentState() != VF_STATE_IDLE) 
		Sleep(50);
	
	int result = env->ReadValue<int>(3);
	cout<<"<valid> - out:"<<result<<endl;

	LARGE_INTEGER freq, t1, t2;
	QueryPerformanceFrequency(&freq);
	QueryPerformanceCounter(&t1);
	for (int i=0;i<2000;i++)
	{
		env->WriteValue(1, 12);
		env->WriteValue(2, 23);
		inv->Start();
		while(ctx->GetCurrentState() != VF_STATE_IDLE) 
			Sleep(0);
		int result = env->ReadValue<int>(3);
	}
	QueryPerformanceCounter(&t2);
	cout<<"PerfC time:"<<(t2.QuadPart - t1.QuadPart) * 1000.0 / (float)freq.QuadPart<<" (ms)"<<endl;
}

void Test3(Library* lib)
{
	Invoker* inv = lib->CreateInvoker(2);
	Stack* stack = inv->GetStack();
	Context* ctx = stack->GetContext();
	Envelope* env = stack->GetEnvelope();

	inv->Start();
	while(ctx->GetCurrentState() != VF_STATE_IDLE)
		Sleep(50);
	ShowMsgbox(env->ReadCh16(1));
}

int main()
{
	DriverHub* drv_hub = DriverHub::GetInstance();
	cout<<"[register driver crt] ... ";
	Assert(drv_hub->Link("E:\\Projects\\vapula\\Core\\OutDir\\Debug\\crt.vapula.driver"));
	//cout<<"[register driver clr] ... ";
	//Assert(drv_hub->Link("clr"));

	cout<<"[load library] ... ";
	LibraryHub* lib_hub = LibraryHub::GetInstance();
	Library* lib = lib_hub->Load("E:\\Projects\\vapula\\Core\\OutDir\\Debug\\sample_lib.library");
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

	system("pause");
	return 0;
}