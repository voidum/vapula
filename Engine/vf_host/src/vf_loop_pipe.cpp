#include "vf_loop_pipe.h"

/*
Worker_Pipe::Worker_Pipe()
{
	_Pipe = new Pipe();
}

Worker_Pipe::~Worker_Pipe()
{
	Clear(_Pipe);
}

bool Worker_Pipe::OnPrepare()
{
	XML* xml = XML::Parse(_Task->GetCtrlSetting());
	raw xdoc = xml->GetEntity();
	pcstr pid = XML::ValStr(XML::XElem(xdoc, "pipe"));
	if(!_Pipe->Connect(pid))
		return false;

	ostringstream oss;
	oss<<VFH_WORKER_PREPARE;
	_Pipe->Write(oss.str().c_str());

	while(!_Pipe->HasNewData());
	pcstr resp = _Pipe->Read();
	bool ret = strcmp(resp, "true") == 0;
	return ret;
}

bool Worker_Pipe::OnProcess()
{
	Setting* setting = Setting::GetInstance();
	int freq_monitor = setting->IsRealTimeMonitor() ? 5 : 50;

	Stack* stack = _Invoker->GetStack();
	Context* ctx = stack->GetContext();

	_Invoker->Start();
	while(ctx->GetCurrentState() != VF_STATE_IDLE)
	{
		ostringstream oss;
		oss<<VFH_WORKER_PROCESS<<",";
		oss<<ctx->GetCurrentState()<<",";
		oss<<ctx->GetProgress();
		_Pipe->Write(oss.str().c_str());
		
		pcstr data = _Pipe->Read();
		int ctrl = atoi(data);
		switch(ctrl)
		{
		case VF_CTRL_CANCEL:
			_Invoker->Stop(30000);
			break;
		case VF_CTRL_PAUSE:
			_Invoker->Pause();
		case VF_CTRL_RESUME:
			_Invoker->Resume();
			break;
		}
		Sleep(freq_monitor);
	}

	ostringstream oss;
	oss<<VF_STATE_IDLE<<","<<100.0f;
	_Pipe->Write(oss.str().c_str());
	return true;
}

bool Worker_Pipe::OnFinish()
{
	ostringstream oss;
	oss<<VFH_WORKER_FINISH;
	_Pipe->Write(oss.str().c_str());
	Worker::OnFinish();
	return false;
}
*/