#include "stdafx.h"
#include "tcm_bridge_c.h"
#include "tcm_driver.h"
#include "tcm_library.h"
#include "tcm_executor.h"
#include "tcm_pipe.h"

void tcmDeleteObject(LPVOID object)
{
	delete object;
}

int tcmGetDriverCount()
{
	tcm::DriverHub* obj = tcm::DriverHub::GetInstance();
	return obj->GetCount();
}

BOOL tcmLinkDriver(LPCSTR id)
{
	tcm::DriverHub* obj = tcm::DriverHub::GetInstance();
	return obj->Link(id) ? TRUE : FALSE;
}

BOOL tcmKickDriver(LPCSTR id)
{
	tcm::DriverHub* obj = tcm::DriverHub::GetInstance();
	return obj->Kick(id) ? TRUE : FALSE;
}

void tcmKickAllDrivers()
{
	tcm::DriverHub* obj = tcm::DriverHub::GetInstance();
	obj->KickAll();
}

int tcmGetLibraryCount()
{
	return tcm::Library::GetCount();
}

LPVOID tcmLoadLibraryW(LPCWSTR path)
{
	return tcm::Library::Load(path);
}

LPVOID tcmLoadLibraryA(LPCSTR path)
{
	PCWSTR path_w = tcm::MbToWc(path);
	LPVOID ret = tcm::Library::Load(path_w);
	delete path_w;
	return ret;
}

LPCSTR tcmGetLibraryRuntime(LPVOID lib)
{
	tcm::Library* obj = (tcm::Library*)lib;
	return obj->GetRuntimeId();
}

LPCWSTR tcmGetLibraryId(LPVOID lib)
{
	tcm::Library* obj = (tcm::Library*)lib;
	return obj->GetLibraryId();
}

BOOL tcmMountLibrary(LPVOID lib)
{
	tcm::Library* obj = (tcm::Library*)lib;
	return obj->Mount() ? TRUE : FALSE;
}

void tcmUnmountLibrary(LPVOID lib)
{
	tcm::Library* obj = (tcm::Library*)lib;
	obj->Unmount();
}

LPVOID tcmCreateExecutor(LPVOID lib, int fid)
{
	tcm::Library* obj = (tcm::Library*)lib;
	return obj->CreateExecutor(fid);
}

int tcmGetFunctionId(LPVOID exec)
{
	tcm::Executor* obj = (tcm::Executor*)exec;
	return obj->GetFunctionId();
}

BOOL tcmStartExecutor(LPVOID exec)
{
	tcm::Executor* obj = (tcm::Executor*)exec;
	return obj->Start() ? TRUE : FALSE;
}

void tcmStopExecutor(LPVOID exec, UINT wait)
{
	tcm::Executor* obj = (tcm::Executor*)exec;
	obj->Stop(wait);
}

void tcmPauseExecutor(LPVOID exec, UINT wait)
{
	tcm::Executor* obj = (tcm::Executor*)exec;
	obj->Pause(wait);
}

void tcmResumeExecutor(LPVOID exec)
{
	tcm::Executor* obj = (tcm::Executor*)exec;
	obj->Resume();
}

void tcmRestartExecutor(LPVOID exec, UINT wait)
{
	tcm::Executor* obj = (tcm::Executor*)exec;
	obj->Restart(wait);
}

LPVOID tcmGetContextToken(LPVOID exec)
{
	tcm::Executor* obj = (tcm::Executor*)exec;
	return obj->GetContextToken();
}

LPVOID tcmStampContext(LPVOID ctx)
{
	tcm::Context* obj = (tcm::Context*)ctx;
	return tcm::Token::Stamp(obj);
}

LPVOID tcmGetContext(LPVOID exec)
{
	tcm::Executor* obj = (tcm::Executor*)exec;
	return obj->GetContext();
}

LPVOID tcmCreateContext()
{
	return new tcm::Context();
}

int tcmGetCtrlCode(LPVOID ctx)
{
	tcm::Context* obj = (tcm::Context*)ctx;
	return obj->GetCtrlCode();
}

void tcmSetCtrlCode(LPVOID ctx, int ctrl_code, LPVOID token)
{
	tcm::Context* obj1 = (tcm::Context*)ctx;
	tcm::Token* obj2 = (tcm::Token*)token;
	obj1->SetCtrlCode(obj2, ctrl_code);
}

float tcmGetProgress(LPVOID ctx)
{
	tcm::Context* obj = (tcm::Context*)ctx;
	return obj->GetProgress();
}

void tcmSetProgress(LPVOID ctx, float prog)
{
	tcm::Context* obj = (tcm::Context*)ctx;
	obj->SetProgress(prog);
}

int tcmGetState(LPVOID ctx)
{
	tcm::Context* obj = (tcm::Context*)ctx;
	return obj->GetState();
}

void tcmSetState(LPVOID ctx, int state, LPVOID token)
{
	tcm::Context* obj1 = (tcm::Context*)ctx;
	tcm::Token* obj2 = (tcm::Token*)token;
	return obj1->SetState(obj2, state);
}

int tcmGetReturnCode(LPVOID ctx)
{
	tcm::Context* obj = (tcm::Context*)ctx;
	return obj->GetReturnCode();
}

void tcmSetReturnCode(LPVOID ctx, int return_code, LPVOID token)
{
	tcm::Context* obj1 = (tcm::Context*)ctx;
	tcm::Token* obj2 = (tcm::Token*)token;
	obj1->SetReturnCode(obj2, return_code);
}

void tcmReplyCtrlCode(LPVOID ctx)
{
	tcm::Context* obj = (tcm::Context*)ctx;
	obj->ReplyCtrlCode();
}

LPVOID tcmGetEnvelope(LPVOID exec)
{
	tcm::Executor* obj = (tcm::Executor*)exec;
	return obj->GetEnvelope();
}

LPVOID tcmLoadEnvelopeW(LPCWSTR path, int fid)
{
	return tcm::Envelope::Load(path, fid);
}

LPVOID tcmLoadEnvelopeA(LPCSTR path, int fid)
{
	PCWSTR path_w = tcm::MbToWc(path);
	LPVOID ret = tcm::Envelope::Load(path_w, fid);
	delete path_w;
	return ret;
}

LPVOID tcmParseEnvelopeW(LPCWSTR xml)
{
	PCSTR str = tcm::WcToMb(xml);
	LPVOID ret = tcm::Envelope::Parse(str);
	delete str;
	return ret;
}

LPVOID tcmParseEnvelopeA(LPCSTR xml)
{
	return tcm::Envelope::Parse(xml);
}

void tcmWriteEnvelopeW(LPVOID env, int id, LPCWSTR value)
{
	tcm::Envelope* obj = (tcm::Envelope*)env;
	obj->CastWriteW(id, value);
}

void tcmWriteEnvelopeA(LPVOID env, int id, LPCSTR value)
{
	tcm::Envelope* obj = (tcm::Envelope*)env;
	obj->CastWriteA(id, value);
}

LPCWSTR tcmReadEnvelopeW(LPVOID env, int id)
{
	tcm::Envelope* obj = (tcm::Envelope*)env;
	return obj->CastReadW(id);
}

LPCSTR tcmReadEnvelopeA(LPVOID env, int id)
{
	tcm::Envelope* obj = (tcm::Envelope*)env;
	return obj->CastReadA(id);
}

void tcmDeliverEnvelope(LPVOID src_env, LPVOID dst_env, int from, int to)
{
	tcm::Envelope* src = (tcm::Envelope*)src_env;
	tcm::Envelope* dst = (tcm::Envelope*)dst_env;
	src->Deliver(dst, from, to);
}

void tcmCastDeliverEnvelope(LPVOID src_env, LPVOID dst_env, int from, int to)
{
	tcm::Envelope* src = (tcm::Envelope*)src_env;
	tcm::Envelope* dst = (tcm::Envelope*)dst_env;
	src->CastDeliver(dst, from, to);
}

//Design the following

LPVOID tcmCreatePipe()
{
	tcm::Pipe* pipe = new tcm::Pipe();
	return pipe;
}

LPCWSTR tcmListenPipe(LPVOID pipe)
{
	tcm::Pipe* obj = (tcm::Pipe*)pipe;
	if(!obj->Listen()) return NULL;
	return obj->GetPipeId();
}

BOOL tcmGetPipeFlag(LPVOID pipe, int action)
{
	tcm::Pipe* obj = (tcm::Pipe*)pipe;
	return obj->GetFlag(action) ? TRUE : FALSE;
}

void tcmSetPipeFlag(LPVOID pipe, int action, BOOL value)
{
	tcm::Pipe* obj = (tcm::Pipe*)pipe;
	obj->SetFlag(action, value == TRUE);
}

void tcmWritePipe(LPVOID pipe, LPVOID value, UINT size)
{
	tcm::Pipe* obj = (tcm::Pipe*)pipe;
	obj->Write(value, size);
}

void tcmReadPipe(LPVOID pipe, LPVOID data)
{
	tcm::Pipe* obj = (tcm::Pipe*)pipe;
	obj->Read(data);
}