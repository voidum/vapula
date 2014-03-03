#pragma once

#include "vf_base.h"

using namespace vapula;

extern "C"
{
	//Base
	VAPULA_API pcstr vfeGetVersion();
	VAPULA_API void vfeDeleteObject(object ptr);

	//Error
	VAPULA_API int vfeWhatError(object err);
	VAPULA_API void vfeThrowError(int what);

	//Driver
	VAPULA_API int vfeGetDriverCount();
	VAPULA_API int vfeLinkDriver(pcstr path);
	VAPULA_API void vfeKickDriver(pcstr id);
	VAPULA_API void vfeKickAllDrivers();

	//Library
	VAPULA_API object vfeLoadLibrary(pcstr path);
	VAPULA_API object vfeLoadLibraryW(pcwstr path);
	VAPULA_API pcstr vfeGetRuntime(object lib);
	VAPULA_API pcstr vfeGetLibraryId(object lib);
	VAPULA_API pcstr vfeGetProcessSym(object lib, pcstr id);
	VAPULA_API pcstr vfeGetRollbackSym(object lib, pcstr id);
	VAPULA_API int vfeMountLibrary(object lib);
	VAPULA_API void vfeUnmountLibrary(object lib);

	//Invoker
	VAPULA_API object vfeCreateInvoker(object lib, pcstr id);
	VAPULA_API int vfeStartInvoker(object inv);
	VAPULA_API void vfeStopInvoker(object inv, uint32 wait);
	VAPULA_API void vfePauseInvoker(object inv, uint32 wait);
	VAPULA_API void vfeResumeInvoker(object inv);
	VAPULA_API int vfeRestartInvoker(object inv, uint32 wait);

	//Stack
	VAPULA_API object vfeGetStack(object inv);
	VAPULA_API object vfeGetCurrentStack();
	VAPULA_API pcstr vfeGetMethodId(object stk);
	VAPULA_API object vfeGetContext(object stk);
	VAPULA_API object vfeGetEnvelope(object stk);
	VAPULA_API int vfeStackIsProtected(object stk);
	VAPULA_API object vfeGetError(object stk);
	
	//Context
	VAPULA_API int8 vfeGetCurrentState(object ctx);
	VAPULA_API int8 vfeGetLastState(object ctx);
	VAPULA_API int8 vfeGetReturnCode(object ctx);
	VAPULA_API int8 vfeGetCtrlCode(object ctx);
	VAPULA_API float vfeGetProgress(object ctx);
	VAPULA_API pcstr vfeGetKeyFrame(object ctx);
	VAPULA_API void vfeSetReturnCode(object ctx, int8 ret);
	VAPULA_API void vfeSetProgress(object ctx, float prog);
	VAPULA_API void vfeSetKeyFrame(object ctx, pcstr frame);
	VAPULA_API void vfeSwitchHold(object ctx);
	VAPULA_API void vfeSwitchBusy(object ctx);

	//Envelope
	VAPULA_API object vfeParseEnvelope(pcstr xml);
	VAPULA_API object vfeParseEnvelopeW(pcwstr xml);
	VAPULA_API void vfeZeroEnvelope(object env);
	VAPULA_API object vfeCopyEnvelope(object env);
	VAPULA_API object vfeGetVariable(object env, int id);

	//Variable
	VAPULA_API uint32 vfeGetVarLength(object var);
	VAPULA_API void vfeWriteVar(object var, object value, uint32 len);
	VAPULA_API object vfeReadVar(object var);
	VAPULA_API void vfeCastWriteVarAt(object var, pcstr value, uint32 at);
	VAPULA_API void vfeCastWriteVarAtW(object var, pcwstr value, uint32 at);
	VAPULA_API pcstr vfeCastReadVarAt(object var, uint32 at);
	VAPULA_API pcwstr vfeCastReadVarAtW(object var, uint32 at);
	VAPULA_API void vfeCastWriteVar(object var, pcstr value);
	VAPULA_API void vfeCastWriteVarW(object var, pcwstr value);
	VAPULA_API pcstr vfeCastReadVar(object var);
	VAPULA_API pcwstr vfeCastReadVarW(object var);
	VAPULA_API void vfeDeliverVar(object src, object dst);

	//Pipe
	VAPULA_API object vfeCreatePipe();
	VAPULA_API int vfePipeIsClose(object pipe);
	VAPULA_API int vfePipeHasNewData(object pipe);
	VAPULA_API pcstr vfeListenPipe(object pipe);
	VAPULA_API int vfeConnectPipe(object pipe, pcstr id);
	VAPULA_API void vfeClosePipe(object pipe);
	VAPULA_API void vfeWritePipe(object pipe, pcstr value);
	VAPULA_API void vfeWritePipeW(object pipe, pcwstr value);
	VAPULA_API pcstr vfeReadPipe(object pipe);
	VAPULA_API pcwstr vfeReadPipeW(object pipe);
}