#include "vf_dev_invoker.h"
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

void Test1(Library* library)
{
	cout << "[create task] ... ";
	Task* task = library->CreateTask("context");
	Assert(task != NULL);

	cout << "[invoke function context]" << endl;
	task->Start();

	Stack* stack = task->GetStack();
	Context* context = stack->GetContext();
	while (context->GetCurrentState() != VF_STATE_IDLE)
	{
		float progress = context->GetProgress();
		if (progress > 10)
		{
			cout << "[pause] progress:" << progress << endl;
			task->Pause(50);
			break;
		}
		Sleep(50);
	}
	int step = 0;
	cout << "paused, wait for a moment" << endl;
	while (step < 20)
	{
		step++;
		Sleep(50);
	}
	task->Resume();
	float progress = context->GetProgress();
	cout << "[resume] progress:" << progress << endl;
	while (context->GetCurrentState() != VF_STATE_IDLE)
		Sleep(50);
	cout << "finished" << endl;
	Clear(task);
}

void Test2(Library* library)
{
	cout << "[create task] ... ";
	Task* task = library->CreateTask("math");
	Assert(task != NULL);

	Stack* stack = task->GetStack();
	Dataset* dataset = stack->GetDataset();

	(*dataset)[1]->WriteAt(12);
	(*dataset)[2]->WriteAt(23);

	cout << "[invoke function math]" << endl;
	task->Start();

	Context* context = stack->GetContext();
	while (context->GetCurrentState() != VF_STATE_IDLE)
		Sleep(50);

	int result = (*dataset)[3]->ReadAt<int>();
	cout << "<valid> - out:" << result << endl;



	LARGE_INTEGER freq, t1, t2;
	QueryPerformanceFrequency(&freq);
	QueryPerformanceCounter(&t1);

	list<Task*> tasks;
	for (int i = 0; i < 10000; i++)
	{
		Task* task = library->CreateTask("math");
		tasks.push_back(task);
		Stack* stack = task->GetStack();
		Dataset* dataset = stack->GetDataset();
		(*dataset)[1]->WriteAt(12);
		(*dataset)[2]->WriteAt(23);
		task->Start();
	}
	typedef list<Task*>::iterator iter;
	for (iter i = tasks.begin(); i != tasks.end(); i++)
	{
		Task* task = *i;
		Stack* stack = task->GetStack();
		Dataset* dataset = stack->GetDataset();
		Context* context = stack->GetContext();
		while (context->GetCurrentState() != VF_STATE_IDLE)
			Sleep(0);
		int result = (*dataset)[3]->ReadAt<int>();
		//cout << "<valid> - out:" << result << endl;
	}

	QueryPerformanceCounter(&t2);
	cout << "adv time:" << (t2.QuadPart - t1.QuadPart) * 1000.0 / (float)freq.QuadPart << " (ms)" << endl;
	Clear(task);
}

void Test3(Library* library)
{
	cout << "[create task] ... ";
	Task* task = library->CreateTask("output");
	Assert(task != NULL);
	Stack* stack = task->GetStack();

	Context* context = stack->GetContext();
	Dataset* dataset = stack->GetDataset();

	cout << "[invoke function output]" << endl;
	task->Start();
	while (context->GetCurrentState() != VF_STATE_IDLE)
		Sleep(50);
	ShowMsgbox((pcstr)(*dataset)[1]->Read());
	Clear(task);
}

void Test4(Library* library)
{
	cout << "[create task] ... ";
	Task* task = library->CreateTask("context2");
	Assert(task != NULL);
	Stack* stack = task->GetStack();

	Context* context = stack->GetContext();

	cout << "[invoke function context2]" << endl;
	task->Start();
	while (context->GetCurrentState() != VF_STATE_IDLE)
	{
		cout << context->GetProgress() << endl;
		Sleep(50);
	}
	Clear(task);
}

void Test5(Library* library)
{
	cout << "[load aspect] ... ";
	Aspect* aspect = Aspect::Load("E:\\Projects\\vapula\\Core\\OutDir\\Debug\\aspect.xml");
	Assert(aspect != null);
	Runtime* runtime = Runtime::Instance();
	runtime->LinkObject(VF_CORE_ASPECT, aspect);
	Task* task = library->CreateTask("protect");
	task->Start();
	Stack* stack = task->GetStack();
	Context* context = stack->GetContext();
	while (context->GetCurrentState() != VF_STATE_IDLE)
		Sleep(50);
}

void Test6()
{
	int* data = new int[123];
	for (int i = 0; i < 123; i++)
		data[i] = i;
	pcstr str = RawToBase64(data, 123 * sizeof(int));
	cout << "data:" << str << endl;
	int* data2 = (int*)Base64ToRaw(str);
	delete str;
	cout << "data:" << data2[122] << endl;
}

void Test7()
{
	pcstr data = "-23";
	uint8 i = (uint8)atoi(data);
	cout << (uint32)i << endl;
}

int main()
{
	//activate runtime
	cout << "[activate runtime]" << endl;
	Runtime* runtime = Runtime::Instance();
	runtime->Activate();

	//load & link driver manually
	cout << "[load & link driver crt] ... ";
	ostringstream oss;
	oss << runtime->GetProcessDir() << "crt.driver";
	Driver* driver = Driver::Load(oss.str().c_str());
	Assert(driver != null);
	runtime->LinkObject(VF_CORE_DRIVER, driver);
	//cout<<"[register driver clr] ... ";
	//Assert(drv_hub->Link("clr"));

	cout << "[load library] ... ";
	oss.str("");
	oss << runtime->GetProcessDir() << "sample_lib.library";
	Library* library = Library::Load(oss.str().c_str());
	Assert(library != NULL);
	runtime->LinkObject(VF_CORE_LIBRARY, library);

	cout << "[mount library] ... ";
	Assert(library->Mount());

	//Test1(library);
	Test2(library);
	//Test3(library);
	//Test4(library);
	//Test5(library);
	//Test6();
	//Test7();

	cout << "[unmount library]" << endl;
	library->Unmount();

	//deactivate runtime
	cout << "[deactivate runtime]" << endl;
	runtime->Deactivate();

	//_CrtDumpMemoryLeaks();
	system("pause");
	return 0;
}