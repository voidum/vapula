#include "vf_bridge_c.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_method.h"
#include "vf_invoker.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_envelope.h"
#include "vf_error.h"
#include "vf_pipe.h"

using namespace vapula;


//Base

pcstr vfeGetVersion()
{
	return vapula::GetVersion();
}

void vfeDeleteObject(object obj)
{
	Clear(obj);
}


//Error

int vfeWhatError(object err)
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

object vfeLoadLibrary(pcstr path)
{
	return Library::Load(path);
}

object vfeLoadLibraryW(pcwstr path)
{
	pcstr path8 = str::ToStr(path);
	object lib = Library::Load(path8);
	delete path8;
	return lib;
}

pcstr vfeGetRuntime(object lib)
{
	Library* obj = (Library*)lib;
	return obj->GetDriver()->GetRuntimeId();
}

pcstr vfeGetLibraryId(object lib)
{
	Library* obj = (Library*)lib;
	return obj->GetLibraryId();
}

pcstr vfeGetProcessSym(object lib, pcstr id)
{
	Library* obj = (Library*)lib;
	return obj->GetMethod(id)->GetProcessSym();
}

pcstr vfeGetRollbackSym(object lib, pcstr id)
{
	Library* obj = (Library*)lib;
	return obj->GetMethod(id)->GetRollbackSym();
}

int vfeMountLibrary(object lib)
{
	Library* obj = (Library*)lib;
	return obj->Mount() ? TRUE : FALSE;
}

void vfeUnmountLibrary(object lib)
{
	Library* obj = (Library*)lib;
	obj->Unmount();
}


//Invoker

object vfeCreateInvoker(object lib, pcstr id)
{
	Library* obj = (Library*)lib;
	return obj->CreateInvoker(id);
}

int vfeStartInvoker(object inv)
{
	Invoker* obj = (Invoker*)inv;
	return obj->Start() ? TRUE : FALSE;
}

void vfeStopInvoker(object inv, uint32 wait)
{
	Invoker* obj = (Invoker*)inv;
	obj->Stop(wait);
}

void vfePauseInvoker(object inv, uint32 wait)
{
	Invoker* obj = (Invoker*)inv;
	obj->Pause(wait);
}

void vfeResumeInvoker(object inv)
{
	Invoker* obj = (Invoker*)inv;
	obj->Resume();
}

int vfeRestartInvoker(object inv, uint32 wait)
{
	Invoker* obj = (Invoker*)inv;
	return obj->Restart(wait) ? TRUE : FALSE;
}


//Stack

object vfeGetStack(object inv)
{
	Invoker* obj = (Invoker*)inv;
	return obj->GetStack();
}

object vfeGetCurrentStack()
{
	Stack* stack = Stack::GetInstance();
	return stack;
}

pcstr vfeGetMethodId(object stk)
{
	Stack* obj = (Stack*)stk;
	return obj->GetMethodId();
}

object vfeGetContext(object stk)
{
	Stack* obj = (Stack*)stk;
	return obj->GetContext();
}

object vfeGetEnvelope(object stk)
{
	Stack* obj = (Stack*)stk;
	return obj->GetEnvelope();
}

int vfeStackIsProtected(object stk)
{
	Stack* obj = (Stack*)stk;
	return obj->IsProtected() ? TRUE : FALSE;
}

object vfeGetError(object stk)
{
	Stack* obj = (Stack*)stk;
	return obj->GetError();
}


//Context

int8 vfeGetCurrentState(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetCurrentState();
}

int8 vfeGetLastState(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetLastState();
}

int8 vfeGetReturnCode(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetReturnCode();
}

int8 vfeGetCtrlCode(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetCtrlCode();
}

float vfeGetProgress(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetProgress();
}

pcstr vfeGetKeyFrame(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetKeyFrame();
}

void vfeSetReturnCode(object ctx, int8 ret)
{
	Context* obj = (Context*)ctx;
	obj->SetReturnCode(ret);
}

void vfeSetProgress(object ctx, float prog)
{
	Context* obj = (Context*)ctx;
	obj->SetProgress(prog);
}

void vfeSetKeyFrame(object ctx, pcstr frame)
{
	Context* obj = (Context*)ctx;
	obj->SetKeyFrame(frame);
}

void vfeSwitchHold(object ctx)
{
	Context* obj = (Context*)ctx;
	obj->SwitchHold();
}

void vfeSwitchBusy(object ctx)
{
	Context* obj = (Context*)ctx;
	obj->SwitchBusy();
}


//Envelope

object vfeParseEnvelope(pcstr xml)
{
	return Envelope::Parse(xml);
}

object vfeParseEnvelopeW(pcwstr xml)
{
	pcstr s8 = str::ToStr(xml, _vf_msg_cp);
	object env = Envelope::Parse(s8);
	delete s8;
	return env;
}

void vfeZeroEnvelope(object env)
{
	Envelope* obj = (Envelope*)env;
	obj->Zero();
}

object vfeCopyEnvelope(object env)
{
	Envelope* obj = (Envelope*)env;
	return obj->Copy();
}

void vfeDeliverEnvelope(object src, object dst, int from, int to)
{
	Envelope* src_env = (Envelope*)src;
	Envelope* dst_env = (Envelope*)dst;
	src_env->Deliver(dst_env, from, to);
}

void vfeCastDeliverEnvelope(object src, object dst, int from, int to)
{
	Envelope* src_env = (Envelope*)src;
	Envelope* dst_env = (Envelope*)dst;
	src_env->CastDeliver(dst_env, from, to);
}

uint32 vfeEnvGetLen(object env, int id)
{
	Envelope* obj = (Envelope*)env;
	uint32 len = obj->GetLength(id);
	return len;
}

void vfeEnvWriteVal(object env, int id, pcstr value)
{
	Envelope* obj = (Envelope*)env;
	obj->CastWrite(id, value);
}

void vfeEnvWriteValW(object env, int id, pcwstr value)
{
	pcstr s8 = str::ToStr(value, _vf_msg_cp);
	Envelope* obj = (Envelope*)env;
	obj->CastWrite(id, s8);
	delete s8;
}

void vfeEnvWriteObj(object env, int id, object value, uint32 len)
{
	Envelope* obj = (Envelope*)env;
	obj->WriteObject(id, value, len);
}

pcstr vfeReadEnvVal(object env, int id)
{
	Envelope* obj = (Envelope*)env;
	return obj->CastRead(id);
}

pcwstr vfeReadEnvValW(object env, int id)
{
	Envelope* obj = (Envelope*)env;
	pcstr s8 = obj->CastRead(id);
	pcwstr s16 = str::ToStrW(s8, _vf_msg_cp);
	delete s8;
	return s16;
}

object vfeReadEnvObj(object env, int id)
{
	Envelope* obj = (Envelope*)env;
	object data = obj->ReadObject(id);
	return data;
}

void vfeCreateArray(object env, int id, uint32 len)
{
	Envelope* obj = (Envelope*)env;
	obj->CreateArray(id, len);
}

void vfeWriteValAt(object env, int id, uint32 idx, pcstr value)
{
	Envelope* obj = (Envelope*)env;
	obj->CastWrite(id, value, idx);
}

void vfeWriteValAtW(object env, int id, uint32 idx, pcwstr value)
{
	Envelope* obj = (Envelope*)env;
	pcstr s8 = str::ToStr(value, _vf_msg_cp);
	obj->CastWrite(id, s8, idx);
	delete s8;
}

pcstr vfeReadValAt(object env, int id, uint32 idx)
{
	Envelope* obj = (Envelope*)env;
	return obj->CastRead(id, idx);
}

pcwstr vfeReadValAtW(object env, int id, uint32 idx)
{
	Envelope* obj = (Envelope*)env;
	pcstr s8 = obj->CastRead(id, idx);
	pcwstr s16 = str::ToStrW(s8, _vf_msg_cp);
	delete s8;
	return s16;
}


//Pipe

object vfeCreatePipe()
{
	Pipe* pipe = new Pipe();
	return pipe;
}

int vfePipeIsClose(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	return obj->IsClose() ? TRUE : FALSE;
}

int vfePipeHasNewData(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	return obj->HasNewData() ? TRUE : FALSE;
}

pcstr vfeListenPipe(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	if(!obj->Listen()) 
		return null;
	return obj->GetPipeId();
}

int vfeConnectPipe(object pipe, pcstr id)
{
	Pipe* obj = (Pipe*)pipe;
	return (obj->Connect(id) ? TRUE : FALSE);
}

void vfeClosePipe(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	obj->Close();
}

void vfeWritePipe(object pipe, pcstr value)
{
	Pipe* obj = (Pipe*)pipe;
	obj->Write(value);
}

void vfeWritePipeW(object pipe, pcwstr value)
{
	Pipe* obj = (Pipe*)pipe;
	pcstr s8 = str::ToStr(value, _vf_msg_cp);
	obj->Write(s8);
	delete s8;
}

pcstr vfeReadPipe(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	return obj->Read();
}

pcwstr vfeReadPipeW(object pipe)
{
	Pipe* obj =(Pipe*)pipe;
	pcstr s8 = obj->Read();
	pcwstr s16 = str::ToStrW(s8, _vf_msg_cp);
	delete s8;
	return s16;
}