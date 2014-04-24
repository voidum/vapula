#include "vf_bridge_c.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_method.h"
#include "vf_invoker.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_dataset.h"
#include "vf_error.h"
#include "vf_pipe.h"

using namespace vapula;


//Base

pcstr vfeGetVersion()
{
	return vapula::GetVersion();
}

void vfeDeleteObject(raw obj)
{
	Clear(obj);
}


//Error

int vfeWhatError(raw err)
{
	Error* obj = (Error*)err;
	return obj->What();
}

void vfeThrowError(int what)
{
	Error::Throw(what);
}


//Driver

int vfeGetDriverCount()
{
	DriverHub* obj = DriverHub::GetInstance();
	return obj->GetCount();
}

int vfeLinkDriver(pcstr path)
{
	DriverHub* obj = DriverHub::GetInstance();
	return obj->Link(path) ? 1 : 0;
}

void vfeKickDriver(pcstr id)
{
	DriverHub* obj = DriverHub::GetInstance();
	obj->Kick(id);
}

void vfeKickAllDrivers()
{
	DriverHub* obj = DriverHub::GetInstance();
	obj->KickAll();
}


//Library

raw vfeLoadLibrary(pcstr path)
{
	return Library::Load(path);
}

raw vfeLoadLibraryW(pcwstr path)
{
	pcstr path8 = str::ToStr(path);
	raw lib = Library::Load(path8);
	delete path8;
	return lib;
}

pcstr vfeGetRuntime(raw lib)
{
	Library* obj = (Library*)lib;
	return obj->GetDriver()->GetRuntimeId();
}

pcstr vfeGetLibraryId(raw lib)
{
	Library* obj = (Library*)lib;
	return obj->GetLibraryId();
}

pcstr vfeGetProcessSym(raw lib, pcstr id)
{
	Library* obj = (Library*)lib;
	return obj->GetMethod(id)->GetProcessSym();
}

pcstr vfeGetRollbackSym(raw lib, pcstr id)
{
	Library* obj = (Library*)lib;
	return obj->GetMethod(id)->GetRollbackSym();
}

int vfeMountLibrary(raw lib)
{
	Library* obj = (Library*)lib;
	return obj->Mount() ? TRUE : FALSE;
}

void vfeUnmountLibrary(raw lib)
{
	Library* obj = (Library*)lib;
	obj->Unmount();
}


//Invoker

raw vfeCreateInvoker(raw lib, pcstr id)
{
	Library* obj = (Library*)lib;
	return obj->CreateInvoker(id);
}

int vfeStartInvoker(raw inv)
{
	Invoker* obj = (Invoker*)inv;
	return obj->Start() ? TRUE : FALSE;
}

void vfeStopInvoker(raw inv, uint32 wait)
{
	Invoker* obj = (Invoker*)inv;
	obj->Stop(wait);
}

void vfePauseInvoker(raw inv, uint32 wait)
{
	Invoker* obj = (Invoker*)inv;
	obj->Pause(wait);
}

void vfeResumeInvoker(raw inv)
{
	Invoker* obj = (Invoker*)inv;
	obj->Resume();
}

int vfeRestartInvoker(raw inv, uint32 wait)
{
	Invoker* obj = (Invoker*)inv;
	return obj->Restart(wait) ? TRUE : FALSE;
}


//Stack

raw vfeGetStack(raw inv)
{
	Invoker* obj = (Invoker*)inv;
	return obj->GetStack();
}

raw vfeGetCurrentStack()
{
	Stack* stack = Stack::GetInstance();
	return stack;
}

pcstr vfeGetMethodId(raw stk)
{
	Stack* obj = (Stack*)stk;
	return obj->GetMethodId();
}

raw vfeGetContext(raw stk)
{
	Stack* obj = (Stack*)stk;
	return obj->GetContext();
}

raw vfeGetDataset(raw stk)
{
	Stack* obj = (Stack*)stk;
	return obj->GetDataset();
}

int vfeIsProtected(raw stk)
{
	Stack* obj = (Stack*)stk;
	return obj->IsProtected() ? TRUE : FALSE;
}

raw vfeGetError(raw stk)
{
	Stack* obj = (Stack*)stk;
	return obj->GetError();
}


//Context

int8 vfeGetCurrentState(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetCurrentState();
}

int8 vfeGetLastState(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetLastState();
}

int8 vfeGetReturnCode(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetReturnCode();
}

int8 vfeGetCtrlCode(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetCtrlCode();
}

float vfeGetProgress(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetProgress();
}

pcstr vfeGetKeyFrame(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetKeyFrame();
}

void vfeSetReturnCode(raw ctx, int8 ret)
{
	Context* obj = (Context*)ctx;
	obj->SetReturnCode(ret);
}

void vfeSetProgress(raw ctx, float prog)
{
	Context* obj = (Context*)ctx;
	obj->SetProgress(prog);
}

void vfeSetKeyFrame(raw ctx, pcstr frame)
{
	Context* obj = (Context*)ctx;
	obj->SetKeyFrame(frame);
}

void vfeSwitchHold(raw ctx)
{
	Context* obj = (Context*)ctx;
	obj->SwitchHold();
}

void vfeSwitchBusy(raw ctx)
{
	Context* obj = (Context*)ctx;
	obj->SwitchBusy();
}


//Dataset

raw vfeParseDataset(pcstr xml)
{
	return Dataset::Parse(xml);
}

raw vfeParseDatasetW(pcwstr xml)
{
	pcstr s8 = str::ToStr(xml, _vf_msg_cp);
	raw ds = Dataset::Parse(s8);
	delete s8;
	return ds;
}

void vfeZeroDataset(raw ds)
{
	Dataset* obj = (Dataset*)ds;
	obj->Zero();
}

raw vfeCopyDataset(raw ds)
{
	Dataset* obj = (Dataset*)ds;
	return obj->Copy();
}

raw vfeGetRecord(raw ds, int id)
{
	Dataset* obj = (Dataset*)ds;
	return (*obj)[id];
}


//Record

uint32 vfeGetRecordSize(raw rec)
{
	Record* obj = (Record*)rec;
	return obj->GetSize();
}

void vfeWriteRecord(raw rec, raw data, uint32 size)
{
	Record* obj = (Record*)rec;
	obj->Write(data, size);
}

raw vfeReadRecord(raw rec)
{
	Record* obj = (Record*)rec;
	return obj->Read();
}

void vfeDeliverRecord(raw src, raw dst)
{
	Record* obj = (Record*)src;
	obj->Deliver((Record*)dst);
}


//Pipe

raw vfeCreatePipe()
{
	Pipe* pipe = new Pipe();
	return pipe;
}

int vfePipeIsClose(raw pipe)
{
	Pipe* obj = (Pipe*)pipe;
	return obj->IsClose() ? TRUE : FALSE;
}

int vfePipeHasNewData(raw pipe)
{
	Pipe* obj = (Pipe*)pipe;
	return obj->HasNewData() ? TRUE : FALSE;
}

pcstr vfeListenPipe(raw pipe)
{
	Pipe* obj = (Pipe*)pipe;
	if(!obj->Listen()) 
		return null;
	return obj->GetPipeId();
}

int vfeConnectPipe(raw pipe, pcstr id)
{
	Pipe* obj = (Pipe*)pipe;
	return (obj->Connect(id) ? TRUE : FALSE);
}

void vfeClosePipe(raw pipe)
{
	Pipe* obj = (Pipe*)pipe;
	obj->Close();
}

void vfeWritePipe(raw pipe, pcstr value)
{
	Pipe* obj = (Pipe*)pipe;
	obj->Write(value);
}

void vfeWritePipeW(raw pipe, pcwstr value)
{
	Pipe* obj = (Pipe*)pipe;
	pcstr s8 = str::ToStr(value, _vf_msg_cp);
	obj->Write(s8);
	delete s8;
}

pcstr vfeReadPipe(raw pipe)
{
	Pipe* obj = (Pipe*)pipe;
	return obj->Read();
}

pcwstr vfeReadPipeW(raw pipe)
{
	Pipe* obj =(Pipe*)pipe;
	pcstr s8 = obj->Read();
	pcwstr s16 = str::ToStrW(s8, _vf_msg_cp);
	delete s8;
	return s16;
}