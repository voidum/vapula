#include "vf_bridge_c.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_weaver.h"
#include "vf_aspect.h"
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

raw vfeNewData(uint8 type, uint32 count)
{
	raw data = new vapula::byte[GetValueUnit(type) * count];
	return data;
}

void vfeWriteAt(raw data, uint8 type, uint32 at, pcstr value)
{
	if (data == null)
		return;
	switch (type)
	{
	case VF_VALUE_INT8: 
		((int8*)data)[at] = (int8)atoi(value); break;
	case VF_VALUE_INT16: 
		((int16*)data)[at] = (int16)atoi(value); break;
	case VF_VALUE_INT32:
		((int32*)data)[at] = atoi(value); break;
	case VF_VALUE_INT64:
		((int64*)data)[at] = atoll(value); break;
	case VF_VALUE_UINT8:
		((uint8*)data)[at] = (uint8)atoi(value); break;
	case VF_VALUE_UINT16:
		((uint16*)data)[at] = (uint16)atoi(value); break;
	case VF_VALUE_UINT32:
		((uint32*)data)[at] = (uint32)atoi(value); break;
	case VF_VALUE_UINT64:
		((uint64*)data)[at] = (uint64)atoll(value); break;
	case VF_VALUE_REAL32:
		((float*)data)[at] = (float)atof(value); break;
	case VF_VALUE_REAL64:
		((double*)data)[at] = atof(value); break;
	}
}

pcstr vfeReadAt(raw data, uint8 type, uint32 at)
{
	if (data == null)
		return null;
	switch (type)
	{
	case VF_VALUE_INT8:
		return str::Value(((int8*)data)[at]);
	case VF_VALUE_INT16:
		return str::Value(((int16*)data)[at]);
	case VF_VALUE_INT32:
		return str::Value(((int32*)data)[at]);
	case VF_VALUE_INT64:
		return str::Value(((int64*)data)[at]);
	case VF_VALUE_UINT8:
		return str::Value(((uint8*)data)[at]);
	case VF_VALUE_UINT16:
		return str::Value(((uint16*)data)[at]);
	case VF_VALUE_UINT32:
		return str::Value(((uint32*)data)[at]);
	case VF_VALUE_UINT64:
		return str::Value(((uint64*)data)[at]);
	case VF_VALUE_REAL32:
		return str::Value(((float*)data)[at]);
	case VF_VALUE_REAL64:
		return str::Value(((double*)data)[at]);
	default:
		return null;
	}
}

void vfeDeleteRaw(raw ptr)
{
	Clear(ptr);
}

raw vfeBase64ToRaw(pcstr data)
{
	raw out = Base64ToRaw(data);
	return out;
}

pcstr vfeRawToBase64(raw data, uint32 size)
{
	pcstr out = RawToBase64(data, size);
	return out;
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


//Weaver

int vfeLinkAspect(pcstr path)
{
	Weaver* weaver = Weaver::GetInstance();
	Aspect* aspect = Aspect::Load(path);
	if (aspect == null)
		return FALSE;
	weaver->Link(aspect);
	return TRUE;
}

int vfeLinkAspectW(pcwstr path)
{
	pcstr cs8_path = str::ToStr(path);
	Scoped autop((raw)cs8_path);
	return vfeLinkAspect(cs8_path);
}

void vfeReachFrame(pcstr frame)
{
	Weaver* weaver = Weaver::GetInstance();
	weaver->Reach(frame);
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

uint8 vfeGetCurrentState(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetCurrentState();
}

uint8 vfeGetLastState(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetLastState();
}

uint8 vfeGetReturnCode(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetReturnCode();
}

uint8 vfeGetControlCode(raw ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetControlCode();
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

void vfeSetReturnCode(raw ctx, uint8 ret)
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