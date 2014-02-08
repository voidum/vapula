#include "worker_pipe.h"
#include "vf_task.h"
#include "vf_invoker.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_envelope.h"
#include "vf_xml.h"
#include "vf_setting.h"

using std::ostringstream;

Worker_Pipe::Worker_Pipe()
{
	_Pipe = new Pipe();
}

Worker_Pipe::~Worker_Pipe()
{
	Clear(_Pipe);
}

bool Worker_Pipe::RunStageA()
{
	Task* task = dynamic_cast<Task*>(_Task);
	XML* xml_cfg = XML::Parse(task->GetCtrlSetting());
	std::locale::global(std::locale(""));
	pcstr pid = XML::ValStr(XML::XElem(xml_cfg, "pid"));
	if(!_Pipe->Connect(pid))
		return false;
	_Pipe->Write("A");
	//TODO: wait for permission
	return true;
}

bool Worker_Pipe::RunStageB()
{
	Setting* setting = Setting::GetInstance();
	Task* task = dynamic_cast<Task*>(_Task);
	Invoker* inv = task->GetInvoker();

	_Pipe->Write("B");
	//TODO: wait for permission

	int freq_monitor = setting->IsRealTimeMonitor() ? 5 : 50;
	inv->Start();
	Stack* stack = inv->GetStack();
	Context* ctx = stack->GetContext();
	while(ctx->GetCurrentState() != VF_STATE_IDLE)
	{
		ostringstream oss;
		oss<<ctx->GetCurrentState()<<",";
		oss<<ctx->GetProgress();
		_Pipe->Write(oss.str().c_str());
		pcstr data = _Pipe->Read();
		int ctrl = atoi(data);
		switch(ctrl)
		{
		case VF_CTRL_CANCEL:
			inv->Stop(30000);
			break;
		case VF_CTRL_PAUSE:
			inv->Pause();
		case VF_CTRL_RESUME:
			inv->Resume();
			break;
		}
		Sleep(freq_monitor);
	}
	ostringstream oss;
	oss<<VF_STATE_IDLE<<","<<100.0f;
	_Pipe->Write(oss.str().c_str());
	return true;
}

bool Worker_Pipe::RunStageC()
{
	Task* task = dynamic_cast<Task*>(_Task);
	Invoker* inv = task->GetInvoker();
	Stack* stack = inv->GetStack();
	Envelope* env = stack->GetEnvelope();

	string resp = "C<response><params>";
	for(int i=0;i<env->GetTotal();i++) 
	{
		if(env->GetMode(i) != VF_PM_IN)
		{
			resp += "<param id=\"";
			resp += str::Value(i);
			resp += "\">";
			resp += env->CastReadValue(i);
			resp += "</param>";
		}
	}
	resp += "</params><debug>";
	resp += "<exception>";
	resp += "</exception>";
	resp += "</debug></response>";

	//if(_Pipe->GetDataVol() >= xml.size())
	//{
	pcstr str = str::Copy(resp.c_str());
	_Pipe->Write(str);
	delete str;
	//}
	return false;
}