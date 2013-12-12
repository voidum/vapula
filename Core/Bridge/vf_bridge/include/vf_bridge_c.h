#pragma once

#include "vf_base.h"

using namespace vapula;

extern "C"
{
	//Base
	VAPULA_API void vfeTestBridge();
	VAPULA_API void vfeDeleteObject(object ptr);

	//Driver
	VAPULA_API int vfeGetDriverCount();
	VAPULA_API int vfeLinkDriver(cstr id);
	VAPULA_API int vfeKickDriver(cstr id);
	VAPULA_API void vfeKickAllDrivers();

	//Library
	VAPULA_API int vfeGetLibraryCount();
	VAPULA_API object vfeLoadLibraryW(cstrw path);
	VAPULA_API object vfeLoadLibraryA(cstr path);
	VAPULA_API cstr vfeGetLibraryRuntime(object lib);
	VAPULA_API cstrw vfeGetLibraryId(object lib);
	VAPULA_API int vfeMountLibrary(object lib);
	VAPULA_API void vfeUnmountLibrary(object lib);

	//Invoker
	VAPULA_API object vfeCreateInvoker(object lib, int fid);
	VAPULA_API int vfeGetFunctionId(object inv);
	VAPULA_API int vfeStartInvoker(object inv);
	VAPULA_API void vfeStopInvoker(object inv, uint32 wait);
	VAPULA_API void vfePauseInvoker(object inv, uint32 wait);
	VAPULA_API void vfeResumeInvoker(object inv);
	VAPULA_API void vfeRestartInvoker(object inv, uint32 wait);
	
	//Context
	VAPULA_API object vfeGetContext(object inv);
	VAPULA_API object vfeCreateContext();
	VAPULA_API int vfeGetCtrlCode(object ctx);
	VAPULA_API int vfeGetState(object ctx);
	VAPULA_API int vfeGetReturnCode(object ctx);
	VAPULA_API float vfeGetProgress(object ctx);
	VAPULA_API void vfeSetProgress(object ctx, float prog);
	VAPULA_API void vfeReplyCtrlCode(object ctx);

	//Envelope
	VAPULA_API object vfeGetEnvelope(object inv);
	VAPULA_API object vfeCreateEnvelope(object lib, int fid);
	VAPULA_API object vfeParseEnvelopeW(cstrw xml);
	VAPULA_API object vfeParseEnvelopeA(cstr xml);
	VAPULA_API void vfeWriteEnvelopeW(object env, int id, cstrw value);
	VAPULA_API void vfeWriteEnvelopeA(object env, int id, cstr value);
	VAPULA_API cstrw vfeReadEnvelopeW(object env, int id);
	VAPULA_API cstr vfeReadEnvelopeA(object env, int id);
	VAPULA_API void vfeDeliverEnvelope(object src_env, object dst_env, int from, int to);
	VAPULA_API void vfeCastDeliverEnvelope(object src_env, object dst_env, int from, int to);

	//Pipe
	VAPULA_API object vfeCreatePipe();
	VAPULA_API int vfePipeIsClose(object pipe);
	VAPULA_API int vfePipeHasNewData(object pipe);
	VAPULA_API int vfeConnectPipe(object pipe, cstr id);
	VAPULA_API cstr vfeListenPipe(object pipe);
	VAPULA_API void vfeClosePipe(object pipe);
	VAPULA_API void vfeWritePipe(object pipe, cstrw value);
	VAPULA_API cstrw vfeReadPipe(object pipe);
}