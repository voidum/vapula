#include "stdafx.h"
#include "worker_pipe.h"

#include "tcm_invoker.h"
#include "tcm_xml.h"
#include "tcm_config.h"

using namespace rapidxml;
using std::string;
using std::ostringstream;

Worker_PIPE::Worker_PIPE()
{
	_Pipe = new Pipe();
}

Worker_PIPE::~Worker_PIPE()
{
	Clear(_Pipe);
}

bool Worker_PIPE::RunStageA()
{
	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	xml_node<>* cfg = (xml_node<>*)xml::Parse(task->GetCtrlConfig());
	std::locale::global(std::locale(""));
	str pid = xml::ValueA(cfg->first_node("pid"));
	if(!_Pipe->Connect(pid))
		return false;
	_Pipe->Write(L"A");
	//TODO: wait for permission
	return true;
}

bool Worker_PIPE::RunStageB()
{
	Config* config = Config::GetInstance();
	Flag* flag = config->GetFlag();

	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	Invoker* inv = task->GetInvoker();

	_Pipe->Write(L"B");
	//TODO: wait for permission

	int freq_monitor = flag->Valid(TCM_CONFIG_RTMON) ? 5 : 50;
	inv->Start();
	Context* ctx = inv->GetContext();
	while(ctx->GetState() != TCM_STATE_IDLE)
	{
		ostringstream oss;
		oss<<ctx->GetState()<<",";
		oss<<ctx->GetProgress();
		_Pipe->WriteA(oss.str().c_str());
		str data = _Pipe->ReadA();
		int ctrl = atoi(data);
		switch(ctrl)
		{
		case TCM_CTRL_CANCEL:
			inv->Stop(30000);
			break;
		case TCM_CTRL_PAUSE:
			inv->Pause();
		case TCM_CTRL_RESUME:
			inv->Resume();
			break;
		}
		Sleep(freq_monitor);
	}
	ostringstream oss;
	oss<<TCM_STATE_IDLE<<","<<100.0f;
	_Pipe->WriteA(oss.str().c_str());
	return true;
}

bool Worker_PIPE::RunStageC()
{
	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	Invoker* inv = task->GetInvoker();
	Envelope* env = inv->GetEnvelope();

	string resp = "C<response><params>";
	for(int i=0;i<env->GetTotal();i++) 
	{
		if(!env->GetInState(i))
		{
			resp += "<param id=\"";
			resp += ValueToStr(i);
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
	str str = CopyStrA(resp.c_str());
	_Pipe->WriteA(str);
	delete str;
	//}
	return false;
}