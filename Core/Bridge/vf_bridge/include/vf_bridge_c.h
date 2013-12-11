#pragma once

#include "vf_base.h"

using namespace vf;

extern "C"
{
	//Base
	TCM_BRIDGE_API void vfeTestBridge();
	TCM_BRIDGE_API void vfeDeleteObject(object ptr);

	//Driver
	TCM_BRIDGE_API int vfeGetDriverCount();
	TCM_BRIDGE_API int vfeLinkDriver(str id);
	TCM_BRIDGE_API int vfeKickDriver(str id);
	TCM_BRIDGE_API void vfeKickAllDrivers();

	//Library
	TCM_BRIDGE_API int vfeGetLibraryCount();
	TCM_BRIDGE_API object vfeLoadLibraryW(strw path);
	TCM_BRIDGE_API object vfeLoadLibraryA(str path);
	TCM_BRIDGE_API str vfeGetLibraryRuntime(object lib);
	TCM_BRIDGE_API strw vfeGetLibraryId(object lib);
	TCM_BRIDGE_API int vfeMountLibrary(object lib);
	TCM_BRIDGE_API void vfeUnmountLibrary(object lib);

	//Invoker
	TCM_BRIDGE_API object vfeCreateInvoker(object lib, int fid);
	TCM_BRIDGE_API int vfeGetFunctionId(object inv);
	TCM_BRIDGE_API int vfeStartInvoker(object inv);
	TCM_BRIDGE_API void vfeStopInvoker(object inv, uint32 wait);
	TCM_BRIDGE_API void vfePauseInvoker(object inv, uint32 wait);
	TCM_BRIDGE_API void vfeResumeInvoker(object inv);
	TCM_BRIDGE_API void vfeRestartInvoker(object inv, uint32 wait);
	
	//Token
	TCM_BRIDGE_API object vfeGetContextToken(object inv);
	TCM_BRIDGE_API object vfeStampContext(object ctx);

	//Context
	TCM_BRIDGE_API object vfeGetContext(object inv);
	TCM_BRIDGE_API object vfeCreateContext();
	TCM_BRIDGE_API int vfeGetCtrlCode(object ctx);
	TCM_BRIDGE_API void vfeSetCtrlCode(object ctx, int ctrl_code, object token);
	TCM_BRIDGE_API float vfeGetProgress(object ctx);
	TCM_BRIDGE_API void vfeSetProgress(object ctx, float prog);
	TCM_BRIDGE_API int	 vfeGetState(object ctx);
	TCM_BRIDGE_API void vfeSetState(object ctx, int state, object token);
	TCM_BRIDGE_API int vfeGetReturnCode(object ctx);
	TCM_BRIDGE_API void vfeSetReturnCode(object ctx, int return_code, object token);
	TCM_BRIDGE_API void vfeReplyCtrlCode(object ctx);

	//Envelope
	TCM_BRIDGE_API object vfeGetEnvelope(object inv);
	TCM_BRIDGE_API object vfeCreateEnvelope(object lib, int fid);
	TCM_BRIDGE_API object vfeParseEnvelopeW(strw xml);
	TCM_BRIDGE_API object vfeParseEnvelopeA(str xml);
	TCM_BRIDGE_API void vfeWriteEnvelopeW(object env, int id, strw value);
	TCM_BRIDGE_API void vfeWriteEnvelopeA(object env, int id, str value);
	TCM_BRIDGE_API strw vfeReadEnvelopeW(object env, int id);
	TCM_BRIDGE_API str vfeReadEnvelopeA(object env, int id);
	TCM_BRIDGE_API void vfeDeliverEnvelope(object src_env, object dst_env, int from, int to);
	TCM_BRIDGE_API void vfeCastDeliverEnvelope(object src_env, object dst_env, int from, int to);

	//Pipe
	TCM_BRIDGE_API object vfeCreatePipe();
	TCM_BRIDGE_API int vfePipeIsClose(object pipe);
	TCM_BRIDGE_API int vfePipeHasNewData(object pipe);
	TCM_BRIDGE_API int vfeConnectPipe(object pipe, str id);
	TCM_BRIDGE_API str vfeListenPipe(object pipe);
	TCM_BRIDGE_API void vfeClosePipe(object pipe);
	TCM_BRIDGE_API void vfeWritePipe(object pipe, strw value);
	TCM_BRIDGE_API strw vfeReadPipe(object pipe);
}