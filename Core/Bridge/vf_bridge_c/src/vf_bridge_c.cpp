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

void vfeDeleteData(raw data)
{
	Clear(data);
}

raw vfeOffsetData(raw data, uint32 offset)
{
	return (raw)((uint32)data + offset);
}

void vfeCopyData(raw dst, raw src, uint32 size)
{
	memcpy(dst, src, size);
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

void vfeStartRuntime()
{
	Runtime* runtime = Runtime::Instance();
	runtime->Start();
}

void vfeStopRuntime()
{
	Runtime* runtime = Runtime::Instance();
	runtime->Stop();
}

void vfeReachFrame(pcstr frame)
{
	Runtime* runtime = Runtime::Instance();
	runtime->Reach(frame);
}


//Aspect

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

int vfeCountAspect()
{
	return Aspect::Count();
}

raw vfeFindAspect(pcstr id)
{
	return Aspect::Find(id);
}

void vfeLinkAspect(raw aspect)
{
	Aspect* object = (Aspect*)aspect;
	object->LinkHub();
}

void vfeKickAspect(raw aspect)
{
	Aspect* object = (Aspect*)aspect;
	object->KickHub();
}


//Driver

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

int vfeCountDriver()
{
	return Driver::Count();
}

raw vfeFindDriver(pcstr id)
{
	return Driver::Find(id);
}

void vfeLinkDriver(raw driver)
{
	Driver* object = (Driver*)driver;
	object->LinkHub();
}

void vfeKickDriver(raw driver)
{
	Driver* object = (Driver*)driver;
	object->KickHub();
}


//Library

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

int vfeCountLibrary()
{
	return Library::Count();
}

raw vfeFindLibrary(pcstr id)
{
	return Library::Find(id);
}

void vfeLinkLibrary(raw library)
{
	Library* object = (Library*)library;
	object->LinkHub();
}

void vfeKickLibrary(raw library)
{
	Library* object = (Library*)library;
	object->KickHub();
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

void vfeWriteRecord(raw record, raw data, uint32 size, bool copy)
{
	Record* object = (Record*)record;
	object->Write(data, size, copy);
}

raw vfeReadRecord(raw record, bool copy)
{
	Record* object = (Record*)record;
	return object->Read(copy);
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

void vfeWritePipe(raw pipe, raw data, uint32 size)
{
	Pipe* object = (Pipe*)pipe;
	object->Write(data, size);
}

raw vfeReadPipe(raw pipe)
{
	Pipe* object = (Pipe*)pipe;
	return object->Read();
}