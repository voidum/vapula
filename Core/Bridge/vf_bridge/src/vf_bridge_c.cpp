#include "vf_bridge_c.h"
#include "vf_error.h"
#include "vf_runtime.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_method.h"
#include "vf_aspect.h"
#include "vf_task.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_dataset.h"
#include "vf_record.h"
#include "vf_pipe.h"

//Base

pcstr vfeGetVersion()
{
	Runtime* runtime = Runtime::Instance();
	return runtime->GetVersion();
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

void vfeDeleteRaw(raw data)
{
	Clear(data);
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

int vfeWhatError(raw error)
{
	Error* object = (Error*)error;
	return object->What();
}

void vfeThrowError(int what)
{
	Error::Throw(what);
}


//Runtime

void vfeActivateRuntime()
{
	Runtime* runtime = Runtime::Instance();
	runtime->Activate();
}

void vfeDeactivateRuntime()
{
	Runtime* runtime = Runtime::Instance();
	runtime->Deactivate();
}

int vfeCountObjects(uint8 type)
{
	Runtime* runtime = Runtime::Instance();
	return runtime->CountObjects(type);
}

raw vfeSelectObject(uint8 type, pcstr id)
{
	Runtime* runtime = Runtime::Instance();
	return runtime->SelectObject(type, id);
}

void vfeLinkObject(raw target)
{
	Runtime* runtime = Runtime::Instance();
	return runtime->LinkObject((Core*)target);
}

void vfeKickObject(uint8 type, pcstr id)
{
	Runtime* runtime = Runtime::Instance();
	return runtime->KickObject(type, id);
}

void vfeKickAllObjects(uint8 type)
{
	Runtime* runtime = Runtime::Instance();
	return runtime->KickAllObjects(type);
}

void vfeReachFrame(pcstr frame)
{
	Runtime* runtime = Runtime::Instance();
	runtime->Reach(frame);
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

pcstr vfeGetRuntime(raw library)
{
	Library* object = (Library*)library;
	return object->GetDriver()->GetRuntimeId();
}

pcstr vfeGetLibraryId(raw library)
{
	Library* object = (Library*)library;
	return object->GetLibraryId();
}

pcstr vfeGetProcessSym(raw library, pcstr id)
{
	Library* object = (Library*)library;
	return object->GetMethod(id)->GetProcessSym();
}

pcstr vfeGetRollbackSym(raw library, pcstr id)
{
	Library* object = (Library*)library;
	return object->GetMethod(id)->GetRollbackSym();
}

int vfeMountLibrary(raw library)
{
	Library* object = (Library*)library;
	return object->Mount() ? TRUE : FALSE;
}

void vfeUnmountLibrary(raw library)
{
	Library* object = (Library*)library;
	object->Unmount();
}

raw vfeCreateTask(raw library, pcstr id)
{
	Library* object = (Library*)library;
	return object->CreateTask(id);
}


//Task

void vfeStartTask(raw task)
{
	Task* object = (Task*)task;
	object->Start();
}

void vfeStopTask(raw task, uint32 wait)
{
	Task* object = (Task*)task;
	object->Stop(wait);
}

void vfePauseTask(raw task, uint32 wait)
{
	Task* object = (Task*)task;
	object->Pause(wait);
}

void vfeResumeTask(raw task)
{
	Task* object = (Task*)task;
	object->Resume();
}

raw vfeGetTaskStack(raw task)
{
	Task* object = (Task*)task;
	return object->GetStack();
}


//Stack

raw vfeGetCurrentStack()
{
	Stack* stack = Stack::Instance();
	return stack;
}

pcstr vfeGetMethodId(raw stack)
{
	Stack* object = (Stack*)stack;
	return object->GetMethodId();
}

raw vfeGetContext(raw stack)
{
	Stack* object = (Stack*)stack;
	return object->GetContext();
}

raw vfeGetDataset(raw stack)
{
	Stack* object = (Stack*)stack;
	return object->GetDataset();
}

int vfeHasProtect(raw stack)
{
	Stack* object = (Stack*)stack;
	return object->HasProtect() ? TRUE : FALSE;
}

raw vfeGetError(raw stack)
{
	Stack* object = (Stack*)stack;
	return object->GetError();
}


//Context

uint8 vfeGetCurrentState(raw context)
{
	Context* object = (Context*)context;
	return object->GetCurrentState();
}

uint8 vfeGetLastState(raw context)
{
	Context* object = (Context*)context;
	return object->GetLastState();
}

uint8 vfeGetReturnCode(raw context)
{
	Context* object = (Context*)context;
	return object->GetReturnCode();
}

uint8 vfeGetControlCode(raw context)
{
	Context* object = (Context*)context;
	return object->GetControlCode();
}

float vfeGetProgress(raw context)
{
	Context* object = (Context*)context;
	return object->GetProgress();
}

pcstr vfeGetKeyFrame(raw context)
{
	Context* object = (Context*)context;
	return object->GetKeyFrame();
}

void vfeSetReturnCode(raw context, uint8 code)
{
	Context* object = (Context*)context;
	object->SetReturnCode(code);
}

void vfeSetProgress(raw context, float progress)
{
	Context* object = (Context*)context;
	object->SetProgress(progress);
}

void vfeSetKeyFrame(raw context, pcstr frame)
{
	Context* object = (Context*)context;
	object->SetKeyFrame(frame);
}

void vfeSwitchHold(raw context)
{
	Context* object = (Context*)context;
	object->SwitchHold();
}

void vfeSwitchBusy(raw context)
{
	Context* object = (Context*)context;
	object->SwitchBusy();
}


//Dataset

raw vfeParseDataset(pcstr xml)
{
	return Dataset::Parse(xml);
}

raw vfeParseDatasetW(pcwstr xml)
{
	pcstr cs8_xml = str::ToStr(xml, _vf_msg_cp);
	raw dataset = Dataset::Parse(cs8_xml);
	delete cs8_xml;
	return dataset;
}

void vfeZeroDataset(raw dataset)
{
	Dataset* object = (Dataset*)dataset;
	object->Zero();
}

raw vfeCopyDataset(raw dataset)
{
	Dataset* object = (Dataset*)dataset;
	return object->Copy();
}

raw vfeGetRecord(raw dataset, int id)
{
	Dataset* object = (Dataset*)dataset;
	return (*object)[id];
}


//Record

uint32 vfeGetRecordSize(raw record)
{
	Record* object = (Record*)record;
	return object->GetSize();
}

void vfeWriteRecord(raw record, raw data, uint32 size)
{
	Record* object = (Record*)record;
	object->Write(data, size);
}

raw vfeReadRecord(raw record)
{
	Record* object = (Record*)record;
	return object->Read();
}

void vfeDeliverRecord(raw src, raw dst)
{
	Record* object = (Record*)src;
	object->Deliver((Record*)dst);
}


//Pipe

raw vfeCreatePipe()
{
	Pipe* pipe = new Pipe();
	return pipe;
}

int vfePipeIsClose(raw pipe)
{
	Pipe* object = (Pipe*)pipe;
	return object->IsClose() ? TRUE : FALSE;
}

int vfePipeHasNewData(raw pipe)
{
	Pipe* object = (Pipe*)pipe;
	return object->HasNewData() ? TRUE : FALSE;
}

pcstr vfeListenPipe(raw pipe)
{
	Pipe* object = (Pipe*)pipe;
	if (!object->Listen())
		return null;
	return object->GetPipeId();
}

int vfeConnectPipe(raw pipe, pcstr id)
{
	Pipe* object = (Pipe*)pipe;
	return (object->Connect(id) ? TRUE : FALSE);
}

void vfeClosePipe(raw pipe)
{
	Pipe* object = (Pipe*)pipe;
	object->Close();
}

void vfeWritePipe(raw pipe, pcstr data)
{
	Pipe* object = (Pipe*)pipe;
	object->Write(data);
}

void vfeWritePipeW(raw pipe, pcwstr data)
{
	Pipe* object = (Pipe*)pipe;
	pcstr cs8_data = str::ToStr(data, _vf_msg_cp);
	object->Write(cs8_data);
	delete cs8_data;
}

pcstr vfeReadPipe(raw pipe)
{
	Pipe* obj = (Pipe*)pipe;
	return obj->Read();
}

pcwstr vfeReadPipeW(raw pipe)
{
	Pipe* obj =(Pipe*)pipe;
	pcstr data = obj->Read();
	pcwstr cs16_data = str::ToStrW(data, _vf_msg_cp);
	delete data;
	return cs16_data;
}