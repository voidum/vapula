#pragma once

#include "tcm_base.h"

using namespace tcm;

extern "C"
{
	//Base
	TCM_BRIDGE_API void tcmTestBridge();
	TCM_BRIDGE_API void tcmDeleteObject(object ptr);

	//Driver
	TCM_BRIDGE_API int tcmGetDriverCount();
	TCM_BRIDGE_API int tcmLinkDriver(str id);
	TCM_BRIDGE_API int tcmKickDriver(str id);
	TCM_BRIDGE_API void tcmKickAllDrivers();

	//Library
	TCM_BRIDGE_API int tcmGetLibraryCount();
	TCM_BRIDGE_API object tcmLoadLibraryW(strw path);
	TCM_BRIDGE_API object tcmLoadLibraryA(str path);
	TCM_BRIDGE_API str tcmGetLibraryRuntime(object lib);
	TCM_BRIDGE_API strw tcmGetLibraryId(object lib);
	TCM_BRIDGE_API int tcmMountLibrary(object lib);
	TCM_BRIDGE_API void tcmUnmountLibrary(object lib);

	//Invoker
	TCM_BRIDGE_API object tcmCreateInvoker(object lib, int fid);
	TCM_BRIDGE_API int tcmGetFunctionId(object inv);
	TCM_BRIDGE_API int tcmStartInvoker(object inv);
	TCM_BRIDGE_API void tcmStopInvoker(object inv, uint32 wait);
	TCM_BRIDGE_API void tcmPauseInvoker(object inv, uint32 wait);
	TCM_BRIDGE_API void tcmResumeInvoker(object inv);
	TCM_BRIDGE_API void tcmRestartInvoker(object inv, uint32 wait);
	
	//Token
	TCM_BRIDGE_API object tcmGetContextToken(object inv);
	TCM_BRIDGE_API object tcmStampContext(object ctx);

	//Context
	TCM_BRIDGE_API object tcmGetContext(object inv);
	TCM_BRIDGE_API object tcmCreateContext();
	TCM_BRIDGE_API int tcmGetCtrlCode(object ctx);
	TCM_BRIDGE_API void tcmSetCtrlCode(object ctx, int ctrl_code, object token);
	TCM_BRIDGE_API float tcmGetProgress(object ctx);
	TCM_BRIDGE_API void tcmSetProgress(object ctx, float prog);
	TCM_BRIDGE_API int	 tcmGetState(object ctx);
	TCM_BRIDGE_API void tcmSetState(object ctx, int state, object token);
	TCM_BRIDGE_API int tcmGetReturnCode(object ctx);
	TCM_BRIDGE_API void tcmSetReturnCode(object ctx, int return_code, object token);
	TCM_BRIDGE_API void tcmReplyCtrlCode(object ctx);

	//Envelope
	TCM_BRIDGE_API object tcmGetEnvelope(object inv);
	TCM_BRIDGE_API object tcmCreateEnvelope(object lib, int fid);
	TCM_BRIDGE_API object tcmParseEnvelopeW(strw xml);
	TCM_BRIDGE_API object tcmParseEnvelopeA(str xml);
	TCM_BRIDGE_API void tcmWriteEnvelopeW(object env, int id, strw value);
	TCM_BRIDGE_API void tcmWriteEnvelopeA(object env, int id, str value);
	TCM_BRIDGE_API strw tcmReadEnvelopeW(object env, int id);
	TCM_BRIDGE_API str tcmReadEnvelopeA(object env, int id);
	TCM_BRIDGE_API void tcmDeliverEnvelope(object src_env, object dst_env, int from, int to);
	TCM_BRIDGE_API void tcmCastDeliverEnvelope(object src_env, object dst_env, int from, int to);

	//Pipe
	TCM_BRIDGE_API object tcmCreatePipe();
	TCM_BRIDGE_API str tcmListenPipe(object pipe);
	TCM_BRIDGE_API void tcmClosePipe(object pipe);
	TCM_BRIDGE_API void tcmWritePipe(object pipe, strw value);
	TCM_BRIDGE_API strw tcmReadPipe(object pipe);
}