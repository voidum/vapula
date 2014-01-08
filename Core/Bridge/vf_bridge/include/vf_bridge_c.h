#pragma once

#include "vf_base.h"

using namespace vapula;

extern "C"
{
	//Base
	VAPULA_API cstr8 vfeGetVersion();
	VAPULA_API void vfeDeleteObject(object ptr);

	//Driver
	VAPULA_API int vfeGetDriverCount();
	VAPULA_API int vfeLinkDriver(cstr8 path);
	VAPULA_API void vfeKickDriver(cstr8 id);
	VAPULA_API void vfeKickAllDrivers();

	//Library
	VAPULA_API int vfeGetLibraryCount();
	VAPULA_API object vfeLoadLibrary(cstr8 path);
	VAPULA_API object vfeLoadLibraryW(cstr16 path);
	VAPULA_API cstr8 vfeGetRuntime(object lib);
	VAPULA_API cstr8 vfeGetLibraryId(object lib);
	VAPULA_API cstr8 vfeGetEntrySym(object lib);
	VAPULA_API int vfeMountLibrary(object lib);
	VAPULA_API void vfeUnmountLibrary(object lib);

	//Invoker
	VAPULA_API object vfeCreateInvoker(object lib, int fid);
	VAPULA_API object vfeGetStack(object inv);
	VAPULA_API int vfeStartInvoker(object inv);
	VAPULA_API void vfeStopInvoker(object inv, uint32 wait);
	VAPULA_API void vfePauseInvoker(object inv, uint32 wait);
	VAPULA_API void vfeResumeInvoker(object inv);
	VAPULA_API void vfeRestartInvoker(object inv, uint32 wait);

	//Stack
	VAPULA_API object vfeGetCurrentStack();
	VAPULA_API int vfeGetFunctionId(object stack);
	VAPULA_API object vfeGetContext(object stack);
	VAPULA_API object vfeGetEnvelope(object stack);
	
	//Context
	VAPULA_API int vfeGetCtrlCode(object ctx);
	VAPULA_API int vfeGetState(object ctx);
	VAPULA_API int vfeGetReturnCode(object ctx);
	VAPULA_API float vfeGetProgress(object ctx);
	VAPULA_API void vfeSetProgress(object ctx, float prog);
	VAPULA_API void vfeReplyCtrlCode(object ctx);

	//Envelope
	VAPULA_API object vfeParseEnvelope(cstr8 xml);
	VAPULA_API object vfeParseEnvelopeW(cstr16 xml);
	VAPULA_API void vfeWriteEnvelopeValue(object env, int id, cstr8 value);
	VAPULA_API void vfeWriteEnvelopeValueW(object env, int id, cstr16 value);
	VAPULA_API cstr8 vfeReadEnvelopeValue(object env, int id);
	VAPULA_API cstr16 vfeReadEnvelopeValueW(object env, int id);
	VAPULA_API void vfeWriteEnvelopeObject(object env, int id, object value, uint32 length);
	VAPULA_API object vfeReadEnvelopeObject(object env, int id, uint32* length);
	VAPULA_API void vfeDeliverEnvelope(object src_env, object dst_env, int from, int to);
	VAPULA_API void vfeCastDeliverEnvelope(object src_env, object dst_env, int from, int to);

	//Pipe
	VAPULA_API object vfeCreatePipe();
	VAPULA_API int vfePipeIsClose(object pipe);
	VAPULA_API int vfePipeHasNewData(object pipe);
	VAPULA_API cstr8 vfeListenPipe(object pipe);
	VAPULA_API int vfeConnectPipe(object pipe, cstr8 id);
	VAPULA_API void vfeClosePipe(object pipe);
	VAPULA_API void vfeWritePipe(object pipe, cstr8 value);
	VAPULA_API void vfeWritePipeW(object pipe, cstr16 value);
	VAPULA_API cstr8 vfeReadPipe(object pipe);
	VAPULA_API cstr16 vfeReadPipeW(object pipe);
}