#pragma once

#include "vf_base.h"

using namespace vapula;

extern "C"
{
	//Base
	VAPULA_API pcstr vfeGetVersion();

	VAPULA_API raw vfeNewData(uint32 size);
	VAPULA_API void vfeDeleteData(raw data);
	VAPULA_API raw vfeOffsetData(raw data, uint32 offset);
	VAPULA_API void vfeCopyData(raw dst, raw src, uint32 size);

	//Error
	VAPULA_API int vfeWhatError(raw error);
	VAPULA_API void vfeThrowError(int what);

	//Runtime
	VAPULA_API void vfeStartRuntime();
	VAPULA_API void vfeStopRuntime();
	VAPULA_API void vfeReachFrame(pcstr frame);

	//Aspect
	VAPULA_API raw vfeLoadAspect(pcstr path);
	VAPULA_API raw vfeLoadAspectW(pcwstr path);
	VAPULA_API int vfeCountAspect();
	VAPULA_API raw vfeFindAspect(pcstr id);
	VAPULA_API void vfeLinkAspect(raw aspect);
	VAPULA_API void vfeKickAspect(raw aspect);

	//Driver
	VAPULA_API raw vfeLoadDriver(pcstr path);
	VAPULA_API raw vfeLoadDriverW(pcwstr path);
	VAPULA_API int vfeCountDriver();
	VAPULA_API raw vfeFindDriver(pcstr id);
	VAPULA_API void vfeLinkDriver(raw driver);
	VAPULA_API void vfeKickDriver(raw driver);

	//Library
	VAPULA_API raw vfeLoadLibrary(pcstr path);
	VAPULA_API raw vfeLoadLibraryW(pcwstr path);
	VAPULA_API int vfeCountLibrary();
	VAPULA_API raw vfeFindLibrary(pcstr id);
	VAPULA_API void vfeLinkLibrary(raw library);
	VAPULA_API void vfeKickLibrary(raw library);
	VAPULA_API pcstr vfeGetRuntime(raw library);
	VAPULA_API pcstr vfeGetLibraryId(raw library);
	VAPULA_API pcstr vfeGetProcessSym(raw library, pcstr id);
	VAPULA_API pcstr vfeGetRollbackSym(raw library, pcstr id);
	VAPULA_API int vfeMountLibrary(raw library);
	VAPULA_API void vfeUnmountLibrary(raw library);
	VAPULA_API raw vfeCreateTask(raw library, pcstr id);

	//Task
	VAPULA_API void vfeStartTask(raw task);
	VAPULA_API void vfeStopTask(raw task, uint32 wait);
	VAPULA_API void vfePauseTask(raw task, uint32 wait);
	VAPULA_API void vfeResumeTask(raw task);
	VAPULA_API raw vfeGetTaskStack(raw task);

	//Stack
	VAPULA_API raw vfeGetCurrentStack();
	VAPULA_API pcstr vfeGetMethodId(raw stack);
	VAPULA_API raw vfeGetContext(raw stack);
	VAPULA_API raw vfeGetDataset(raw stack);
	VAPULA_API int vfeHasProtect(raw stack);
	VAPULA_API raw vfeGetError(raw stack);
	
	//Context
	VAPULA_API uint8 vfeGetCurrentState(raw context);
	VAPULA_API uint8 vfeGetLastState(raw context);
	VAPULA_API uint8 vfeGetReturnCode(raw context);
	VAPULA_API uint8 vfeGetControlCode(raw context);
	VAPULA_API float vfeGetProgress(raw context);
	VAPULA_API pcstr vfeGetKeyFrame(raw context);
	VAPULA_API void vfeSetReturnCode(raw context, uint8 code);
	VAPULA_API void vfeSetProgress(raw context, float progress);
	VAPULA_API void vfeSetKeyFrame(raw context, pcstr frame);
	VAPULA_API void vfeSwitchHold(raw context);
	VAPULA_API void vfeSwitchBusy(raw context);

	//Dataset
	VAPULA_API raw vfeParseDataset(pcstr xml);
	VAPULA_API raw vfeParseDatasetW(pcwstr xml);
	VAPULA_API void vfeZeroDataset(raw dataset);
	VAPULA_API raw vfeCopyDataset(raw dataset);
	VAPULA_API raw vfeGetRecord(raw dataset, int id);

	//Record
	VAPULA_API uint32 vfeGetRecordSize(raw record);
	VAPULA_API void vfeWriteRecord(raw record, raw data, uint32 size, bool copy);
	VAPULA_API raw vfeReadRecord(raw record, bool copy);
	VAPULA_API void vfeDeliverRecord(raw src, raw dst);

	//Pipe
	VAPULA_API raw vfeCreatePipe();
	VAPULA_API int vfePipeIsClose(raw pipe);
	VAPULA_API int vfePipeHasNewData(raw pipe);
	VAPULA_API pcstr vfeListenPipe(raw pipe);
	VAPULA_API int vfeConnectPipe(raw pipe, pcstr id);
	VAPULA_API void vfeClosePipe(raw pipe);
	VAPULA_API void vfeWritePipe(raw pipe, raw data, uint32 size);
	VAPULA_API raw vfeReadPipe(raw pipe);
}