#include "stdafx.h"
#include "worker_pipe.h"

#include "tcm_executor.h"
#include "tcm_xml.h"
#include "tcm_config.h"

using namespace rapidxml;
using std::string;

Worker_PIPE::Worker_PIPE()
{
	_Pipe = new Pipe();
}

Worker_PIPE::~Worker_PIPE()
{
	if(_Pipe != NULL) delete _Pipe;
}

bool Worker_PIPE::RunStageA()
{
	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	xml_node<>* cfg = (xml_node<>*)xml::Parse(task->GetCtrlConfig());
	std::locale::global(std::locale(""));
	PCWSTR pid = xml::ValueW(cfg->first_node("pid"));
	if(!_Pipe->Connect(pid)) return false;

	char pipe_w = 'A';
	LPVOID pipe_r = NULL;

	_Pipe->Write(&pipe_w, 1);
	pipe_r = _Pipe->WaitRead(1000);
	if(pipe_r == NULL) return false;
	if(((int*)pipe_r)[0] == 0) return true;
	return false;
}

bool Worker_PIPE::RunStageB()
{
	Config* config = Config::GetInstance();
	Flag* flag = config->GetFlag();

	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	Executor* exec = task->GetExecutor();

	LPVOID pipe_w = new BYTE[9];
	*((char*)pipe_w) = 'B';
	LPVOID pipe_r = NULL;

	int freq_monitor = flag->Valid(TCM_CONFIG_RTMON) ? 5 : 50;
	exec->Start();
	Context* ctx = exec->GetContext();
	while(ctx->GetState() != TCM_STATE_IDLE)
	{
		*((int*)((ULONG)pipe_w+1)) = ctx->GetState();
		*((float*)((ULONG)pipe_w+5)) = ctx->GetProgress();
		_Pipe->Write(pipe_w, 9);
		pipe_r = _Pipe->WaitRead();
		int ctrl = *((int*)pipe_r);
		switch(ctrl)
		{
		case TCM_CTRL_CANCEL:
			exec->Stop(30000);
			break;
		case TCM_CTRL_PAUSE:
			exec->Pause();
		case TCM_CTRL_RESUME:
			exec->Resume();
			break;
		}
		Sleep(freq_monitor);
	}
	*((int*)((ULONG)pipe_w+1)) = TCM_STATE_IDLE;
	*((float*)((ULONG)pipe_w+5)) = 100.0f;
	_Pipe->Write(pipe_w, 9);
	bool ret = true;
	if(!_Pipe->BeenRead(10000))
	{
		//TODO: 重新考虑返回值，表达通信延迟
		ret = false;
	}
	delete pipe_r;
	delete pipe_w;
	return ret;
}

bool Worker_PIPE::RunStageC()
{
	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	Executor* exec = task->GetExecutor();
	Envelope* env = exec->GetEnvelope();

	string resp = "C<response><params>";
	for(int i=0;i<env->GetParamTotal();i++) 
	{
		if(!env->GetInState(i))
		{
			resp += "<param id=\"";
			resp += ValueToStrA(i);
			resp += "\">";
			resp += env->CastReadA(i);
			resp += "</param>";
		}
	}
	resp += "</params><debug>";
	resp += "<exception>";
	resp += "</exception>";
	resp += "</debug></response>";

	//if(_Pipe->GetDataVol() >= xml.size())
	//{
	PCSTR str = CopyStrA(resp.c_str());
	_Pipe->Write((LPVOID)str, resp.size()+1);
	delete str;
	//}
	return false;
}