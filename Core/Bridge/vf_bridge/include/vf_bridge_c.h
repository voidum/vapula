#pragma once

#include "vf_base.h"

using namespace vapula;

extern "C"
{
	//Base
	VAPULA_API pcstr vfeGetVersion();

	VAPULA_API raw vfeNewData(uint8 type, uint32 count);
	VAPULA_API void vfeWriteAt(raw data, uint8 type, uint32 at, pcstr value);
	VAPULA_API pcstr vfeReadAt(raw data, uint8 type, uint32 at);
	VAPULA_API void vfeDeleteRaw(raw data);

	VAPULA_API raw vfeBase64ToRaw(pcstr data);
	VAPULA_API pcstr vfeRawToBase64(raw data, uint32 size);

	//Error
	VAPULA_API int vfeWhatError(raw error);
	VAPULA_API void vfeThrowError(int what);

	//Runtime
	VAPULA_API void vfeActivateRuntime();
	VAPULA_API void vfeDeactivateRuntime();
	VAPULA_API int vfeCountObjects(uint8 type);
	VAPULA_API void vfeLinkObject(uint8 type, raw target);
	VAPULA_API void vfeKickObject(uint8 type, pcstr id);
	VAPULA_API void vfeKickAllObjects(uint8 type);
	VAPULA_API void vfeReachFrame(pcstr frame);

	//Library
	VAPULA_API raw vfeLoadLibrary(pcstr path);
	VAPULA_API raw vfeLoadLibraryW(pcwstr path);
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
	VAPULA_API void vfeWriteRecord(raw record, raw data, uint32 size);
	VAPULA_API raw vfeReadRecord(raw record);
	VAPULA_API void vfeDeliverRecord(raw src, raw dst);

	//Pipe
	VAPULA_API raw vfeCreatePipe();
	VAPULA_API int vfePipeIsClose(raw pipe);
	VAPULA_API int vfePipeHasNewData(raw pipe);
	VAPULA_API pcstr vfeListenPipe(raw pipe);
	VAPULA_API int vfeConnectPipe(raw pipe, pcstr id);
	VAPULA_API void vfeClosePipe(raw pipe);
	VAPULA_API void vfeWritePipe(raw pipe, pcstr data);
	VAPULA_API void vfeWritePipeW(raw pipe, pcwstr data);
	VAPULA_API pcstr vfeReadPipe(raw pipe);
	VAPULA_API pcwstr vfeReadPipeW(raw pipe);
}