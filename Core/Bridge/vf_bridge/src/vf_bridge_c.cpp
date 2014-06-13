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

raw vfeNewData(uint32 size)
{
	raw data = new vapula::byte[size];
	return data;
}

void vfeDeleteRaw(raw data)
{
	Clear(data);
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

raw vfeLoadDriver(pcstr path)
{
	return Driver::Load(path);
}

raw vfeLoadDriverW(pcwstr path)
{
	pcstr cs8_path = str::ToStr(path);
	raw driver = Driver::Load(cs8_path);
	delete cs8_path;
	return driver;
}

raw vfeLoadLibrary(pcstr path)
{
	return Library::Load(path);
}

raw vfeLoadLibraryW(pcwstr path)
{
	pcstr cs8_path = str::ToStr(path);
	raw library = Library::Load(cs8_path);
	delete cs8_path;
	return library;
}

raw vfeLoadAspect(pcstr path)
{
	return Aspect::Load(path);
}

raw vfeLoadAspectW(pcwstr path)
{
	pcstr cs8_path = str::ToStr(path);
	raw aspect = Aspect::Load(cs8_path);
	delete cs8_path;
	return aspect;
}


//Library

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
	pcstr cs8_xml = str::ToStr(xml, _vf_cp_msg);
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
	pcstr cs8_data = str::ToStr(data, _vf_cp_msg);
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
	pcwstr cs16_data = str::ToStrW(data, _vf_cp_msg);
	delete data;
	return cs16_data;
}