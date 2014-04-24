#pragma once

#include "vf_base.h"

using namespace vapula;

extern "C"
{
	//Base
	VAPULA_API pcstr vfeGetVersion();
	VAPULA_API void vfeDeleteObject(raw ptr);
	VAPULA_API void vfeWriteAt(raw data, int8 type, uint32 at, pcstr value);
	VAPULA_API pcstr vfeReadAt(raw data, int8 type, uint32 at);
	VAPULA_API raw vfeToData(pcstr data);
	VAPULA_API raw vfeToDataW(pcwstr data);
	VAPULA_API pcstr vfeToString(raw data);
	VAPULA_API pcwstr vfeToStringW(raw data);

	//Error
	VAPULA_API int vfeWhatError(raw err);
	VAPULA_API void vfeThrowError(int what);

	//Driver
	VAPULA_API int vfeGetDriverCount();
	VAPULA_API int vfeLinkDriver(pcstr path);
	VAPULA_API void vfeKickDriver(pcstr id);
	VAPULA_API void vfeKickAllDrivers();

	//Library
	VAPULA_API raw vfeLoadLibrary(pcstr path);
	VAPULA_API raw vfeLoadLibraryW(pcwstr path);
	VAPULA_API pcstr vfeGetRuntime(raw lib);
	VAPULA_API pcstr vfeGetLibraryId(raw lib);
	VAPULA_API pcstr vfeGetProcessSym(raw lib, pcstr id);
	VAPULA_API pcstr vfeGetRollbackSym(raw lib, pcstr id);
	VAPULA_API int vfeMountLibrary(raw lib);
	VAPULA_API void vfeUnmountLibrary(raw lib);

	//Invoker
	VAPULA_API raw vfeCreateInvoker(raw lib, pcstr id);
	VAPULA_API int vfeStartInvoker(raw inv);
	VAPULA_API void vfeStopInvoker(raw inv, uint32 wait);
	VAPULA_API void vfePauseInvoker(raw inv, uint32 wait);
	VAPULA_API void vfeResumeInvoker(raw inv);
	VAPULA_API int vfeRestartInvoker(raw inv, uint32 wait);

	//Stack
	VAPULA_API raw vfeGetStack(raw inv);
	VAPULA_API raw vfeGetCurrentStack();
	VAPULA_API pcstr vfeGetMethodId(raw stk);
	VAPULA_API raw vfeGetContext(raw stk);
	VAPULA_API raw vfeGetDataset(raw stk);
	VAPULA_API int vfeIsProtected(raw stk);
	VAPULA_API raw vfeGetError(raw stk);
	
	//Context
	VAPULA_API int8 vfeGetCurrentState(raw ctx);
	VAPULA_API int8 vfeGetLastState(raw ctx);
	VAPULA_API int8 vfeGetReturnCode(raw ctx);
	VAPULA_API int8 vfeGetCtrlCode(raw ctx);
	VAPULA_API float vfeGetProgress(raw ctx);
	VAPULA_API pcstr vfeGetKeyFrame(raw ctx);
	VAPULA_API void vfeSetReturnCode(raw ctx, int8 ret);
	VAPULA_API void vfeSetProgress(raw ctx, float prog);
	VAPULA_API void vfeSetKeyFrame(raw ctx, pcstr frame);
	VAPULA_API void vfeSwitchHold(raw ctx);
	VAPULA_API void vfeSwitchBusy(raw ctx);

	//Dataset
	VAPULA_API raw vfeParseDataset(pcstr xml);
	VAPULA_API raw vfeParseDatasetW(pcwstr xml);
	VAPULA_API void vfeZeroDataset(raw ds);
	VAPULA_API raw vfeCopyDataset(raw ds);
	VAPULA_API raw vfeGetRecord(raw ds, int id);

	//Record
	VAPULA_API uint32 vfeGetRecordSize(raw rec);
	VAPULA_API void vfeWriteRecord(raw rec, raw data, uint32 size);
	VAPULA_API raw vfeReadRecord(raw rec);
	VAPULA_API void vfeDeliverRecord(raw src, raw dst);

	//Pipe
	VAPULA_API raw vfeCreatePipe();
	VAPULA_API int vfePipeIsClose(raw pipe);
	VAPULA_API int vfePipeHasNewData(raw pipe);
	VAPULA_API pcstr vfeListenPipe(raw pipe);
	VAPULA_API int vfeConnectPipe(raw pipe, pcstr id);
	VAPULA_API void vfeClosePipe(raw pipe);
	VAPULA_API void vfeWritePipe(raw pipe, pcstr value);
	VAPULA_API void vfeWritePipeW(raw pipe, pcwstr value);
	VAPULA_API pcstr vfeReadPipe(raw pipe);
	VAPULA_API pcwstr vfeReadPipeW(raw pipe);
}