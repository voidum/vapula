#include "vf_bridge_c.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_invoker.h"
#include "vf_pipe.h"

using namespace vapula;

void vfeTestBridge()
{
}

void vfeDeleteObject(object obj)
{
	delete obj;
}

int vfeGetDriverCount()
{
	DriverHub* obj = DriverHub::GetInstance();
	return obj->GetCount();
}

int vfeLinkDriver(cstr8 id)
{
	DriverHub* obj = DriverHub::GetInstance();
	return obj->Link(id) ? 1 : 0;
}

int vfeKickDriver(cstr8 id)
{
	DriverHub* obj = DriverHub::GetInstance();
	return obj->Kick(id) ? 1 : 0;
}

void vfeKickAllDrivers()
{
	DriverHub* obj = DriverHub::GetInstance();
	obj->KickAll();
}

int vfeGetLibraryCount()
{
	return Library::GetCount();
}

object vfeLoadLibraryW(cstr16 path)
{
	cstr8 path8 = str::ToCh8(path);
	object lib = Library::Load(path8);
	delete path8;
	return lib;
}

object vfeLoadLibrary(cstr8 path)
{
	return Library::Load(path);
}

cstr8 vfeGetLibraryRuntime(object lib)
{
	Library* obj = (Library*)lib;
	return obj->GetRuntimeId();
}

cstr8 vfeGetLibraryId(object lib)
{
	Library* obj = (Library*)lib;
	return obj->GetLibraryId();
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

object vfeCreateInvoker(object lib, int fid)
{
	Library* obj = (Library*)lib;
	return obj->CreateInvoker(fid);
}

int vfeGetFunctionId(object inv)
{
	Invoker* obj = (Invoker*)inv;
	return obj->GetFunctionId();
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

void vfeRestartInvoker(object inv, uint32 wait)
{
	Invoker* obj = (Invoker*)inv;
	obj->Restart(wait);
}

object vfeGetContext(object inv)
{
	Invoker* obj = (Invoker*)inv;
	return obj->GetContext();
}

object vfeCreateContext()
{
	return new Context();
}

int vfeGetCtrlCode(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetCtrlCode();
}

float vfeGetProgress(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetProgress();
}

int vfeGetState(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetState();
}

int vfeGetReturnCode(object ctx)
{
	Context* obj = (Context*)ctx;
	return obj->GetReturnCode();
}

void vfeSetProgress(object ctx, float prog)
{
	Context* obj = (Context*)ctx;
	obj->SetProgress(prog);
}

void vfeReplyCtrlCode(object ctx)
{
	Context* obj = (Context*)ctx;
	obj->ReplyCtrlCode();
}

object vfeGetEnvelope(object inv)
{
	Invoker* obj = (Invoker*)inv;
	return obj->GetEnvelope();
}

object vfeLoadEnvelopeW(cstr16 path, int fid)
{
	cstr8 s8 = str::ToCh8(path);
	object env = Envelope::Load(s8, fid);
	delete s8;
	return env;
}

object vfeLoadEnvelope(cstr8 path, int fid)
{
	return Envelope::Load(path, fid);
}

object vfeParseEnvelopeW(cstr16 xml)
{
	cstr8 s8 = str::ToCh8(xml);
	object env = Envelope::Parse(s8);
	delete s8;
	return env;
}

object vfeParseEnvelope(cstr8 xml)
{
	return Envelope::Parse(xml);
}

void vfeWriteEnvelopeW(object env, int id, cstr16 value)
{
	cstr8 s8 = str::ToCh8(value, _vf_default_encoding);
	Envelope* obj = (Envelope*)env;
	obj->CastWrite(id, s8);
	delete s8;
}

void vfeWriteEnvelope(object env, int id, cstr8 value)
{
	Envelope* obj = (Envelope*)env;
	obj->CastWrite(id, value);
}

cstr16 vfeReadEnvelopeW(object env, int id)
{
	Envelope* obj = (Envelope*)env;
	cstr8 s8 = obj->CastRead(id);
	cstr16 s16 = str::ToCh16(s8);
	delete s8;
	return s16;
}

cstr8 vfeReadEnvelope(object env, int id)
{
	Envelope* obj = (Envelope*)env;
	return obj->CastRead(id);
}

void vfeDeliverEnvelope(object src_env, object dst_env, int from, int to)
{
	Envelope* src = (Envelope*)src_env;
	Envelope* dst = (Envelope*)dst_env;
	src->Deliver(dst, from, to);
}

void vfeCastDeliverEnvelope(object src_env, object dst_env, int from, int to)
{
	Envelope* src = (Envelope*)src_env;
	Envelope* dst = (Envelope*)dst_env;
	src->CastDeliver(dst, from, to);
}

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

cstr8 vfeListenPipe(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	if(!obj->Listen()) return null;
	return obj->GetPipeId();
}

int vfeConnectPipe(object pipe, cstr8 id)
{
	Pipe* obj = (Pipe*)pipe;
	return (obj->Connect(id) ? TRUE : FALSE);
}

void vfeClosePipe(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	obj->Close();
}

void vfeWritePipe(object pipe, cstr8 value)
{
	Pipe* obj = (Pipe*)pipe;
	obj->Write(value);
}

cstr8 vfeReadPipe(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	return obj->Read();
}