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

int vfeLinkDriver(str id)
{
	DriverHub* obj = DriverHub::GetInstance();
	return obj->Link(id) ? 1 : 0;
}

int vfeKickDriver(str id)
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

object vfeLoadLibraryW(strw path)
{
	return Library::Load(path);
}

object vfeLoadLibraryA(str path)
{
	cstrw path_w = MbToWc(path);
	object ret = Library::Load(path_w);
	delete path_w;
	return ret;
}

cstr vfeGetLibraryRuntime(object lib)
{
	Library* obj = (Library*)lib;
	return obj->GetRuntimeId();
}

cstrw vfeGetLibraryId(object lib)
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

object vfeLoadEnvelopeW(strw path, int fid)
{
	return Envelope::Load(path, fid);
}

object vfeLoadEnvelopeA(str path, int fid)
{
	cstrw path_w = MbToWc(path);
	object ret = Envelope::Load(path_w, fid);
	delete path_w;
	return ret;
}

object vfeParseEnvelopeW(strw xml)
{
	cstr str = WcToMb(xml);
	object ret = Envelope::Parse(str);
	delete str;
	return ret;
}

object vfeParseEnvelopeA(str xml)
{
	return Envelope::Parse(xml);
}

void vfeWriteEnvelopeW(object env, int id, strw value)
{
	Envelope* obj = (Envelope*)env;
	obj->CastWriteW(id, value);
}

void vfeWriteEnvelopeA(object env, int id, str value)
{
	Envelope* obj = (Envelope*)env;
	obj->CastWriteA(id, value);
}

cstrw vfeReadEnvelopeW(object env, int id)
{
	Envelope* obj = (Envelope*)env;
	return obj->CastReadW(id);
}

cstr vfeReadEnvelopeA(object env, int id)
{
	Envelope* obj = (Envelope*)env;
	return obj->CastReadA(id);
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

cstr vfeListenPipe(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	if(!obj->Listen()) return null;
	return obj->GetPipeId();
}

int vfeConnectPipe(object pipe, cstr id)
{
	Pipe* obj = (Pipe*)pipe;
	return (obj->Connect(id) ? TRUE : FALSE);
}

void vfeClosePipe(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	obj->Close();
}

void vfeWritePipe(object pipe, cstrw value)
{
	Pipe* obj = (Pipe*)pipe;
	obj->Write(value);
}

cstrw vfeReadPipe(object pipe)
{
	Pipe* obj = (Pipe*)pipe;
	return obj->Read();
}