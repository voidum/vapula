#pragma once

#include "tcm_base.h"

extern "C"
{
	//Base
	TCM_BRIDGE_API void tcmTestBridge();
	TCM_BRIDGE_API void tcmDeleteObject(LPVOID object);

	//Driver
	TCM_BRIDGE_API int tcmGetDriverCount();
	TCM_BRIDGE_API BOOL tcmLinkDriver(LPCSTR id);
	TCM_BRIDGE_API BOOL tcmKickDriver(LPCSTR id);
	TCM_BRIDGE_API void tcmKickAllDrivers();

	//Library
	TCM_BRIDGE_API int tcmGetLibraryCount();
	TCM_BRIDGE_API LPVOID tcmLoadLibraryW(LPCWSTR path);
	TCM_BRIDGE_API LPVOID tcmLoadLibraryA(LPCSTR path);
	TCM_BRIDGE_API LPCSTR tcmGetLibraryRuntime(LPVOID lib);
	TCM_BRIDGE_API LPCWSTR tcmGetLibraryId(LPVOID lib);
	TCM_BRIDGE_API BOOL tcmMountLibrary(LPVOID lib);
	TCM_BRIDGE_API void tcmUnmountLibrary(LPVOID lib);

	//Invoker
	TCM_BRIDGE_API LPVOID tcmCreateInvoker(LPVOID lib, int fid);
	TCM_BRIDGE_API int tcmGetFunctionId(LPVOID inv);
	TCM_BRIDGE_API BOOL tcmStartInvoker(LPVOID inv);
	TCM_BRIDGE_API void tcmStopInvoker(LPVOID inv, UINT wait);
	TCM_BRIDGE_API void tcmPauseInvoker(LPVOID inv, UINT wait);
	TCM_BRIDGE_API void tcmResumeInvoker(LPVOID inv);
	TCM_BRIDGE_API void tcmRestartInvoker(LPVOID inv, UINT wait);
	
	//Token
	TCM_BRIDGE_API LPVOID tcmGetContextToken(LPVOID inv);
	TCM_BRIDGE_API LPVOID tcmStampContext(LPVOID ctx);

	//Context
	TCM_BRIDGE_API LPVOID tcmGetContext(LPVOID inv);
	TCM_BRIDGE_API LPVOID tcmCreateContext();
	TCM_BRIDGE_API int tcmGetCtrlCode(LPVOID ctx);
	TCM_BRIDGE_API void tcmSetCtrlCode(LPVOID ctx, int ctrl_code, LPVOID token);
	TCM_BRIDGE_API float tcmGetProgress(LPVOID ctx);
	TCM_BRIDGE_API void tcmSetProgress(LPVOID ctx, float prog);
	TCM_BRIDGE_API int	 tcmGetState(LPVOID ctx);
	TCM_BRIDGE_API void tcmSetState(LPVOID ctx, int state, LPVOID token);
	TCM_BRIDGE_API int tcmGetReturnCode(LPVOID ctx);
	TCM_BRIDGE_API void tcmSetReturnCode(LPVOID ctx, int return_code, LPVOID token);
	TCM_BRIDGE_API void tcmReplyCtrlCode(LPVOID ctx);

	//Envelope
	TCM_BRIDGE_API LPVOID tcmGetEnvelope(LPVOID inv);
	TCM_BRIDGE_API LPVOID tcmCreateEnvelope(LPVOID lib, int fid);
	TCM_BRIDGE_API LPVOID tcmParseEnvelopeW(LPCWSTR xml);
	TCM_BRIDGE_API LPVOID tcmParseEnvelopeA(LPCSTR xml);
	TCM_BRIDGE_API void tcmWriteEnvelopeW(LPVOID env, int id, LPCWSTR value);
	TCM_BRIDGE_API void tcmWriteEnvelopeA(LPVOID env, int id, LPCSTR value);
	TCM_BRIDGE_API LPCWSTR tcmReadEnvelopeW(LPVOID env, int id);
	TCM_BRIDGE_API LPCSTR tcmReadEnvelopeA(LPVOID env, int id);
	TCM_BRIDGE_API void tcmDeliverEnvelope(LPVOID src_env, LPVOID dst_env, int from, int to);
	TCM_BRIDGE_API void tcmCastDeliverEnvelope(LPVOID src_env, LPVOID dst_env, int from, int to);

	//Pipe
	TCM_BRIDGE_API LPVOID tcmCreatePipe();
	TCM_BRIDGE_API LPCWSTR tcmListenPipe(LPVOID pipe);
	TCM_BRIDGE_API BOOL tcmGetPipeFlag(LPVOID pipe, int action);
	TCM_BRIDGE_API void tcmSetPipeFlag(LPVOID pipe, int action, BOOL value);
	TCM_BRIDGE_API void tcmWritePipe(LPVOID pipe, LPVOID value, UINT size);
	TCM_BRIDGE_API void tcmReadPipe(LPVOID pipe, LPVOID data);
}