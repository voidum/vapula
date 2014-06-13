using System;
using System.Runtime.InteropServices;

namespace Vapula
{
    public class Bridge
    {
        #region Base
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetVersion",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetVersion();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeNewData",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NewData(UInt32 size);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeDeleteRaw",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteRaw(IntPtr obj);
        #endregion

        #region Error
        [DllImport("vf_bridge.dll", EntryPoint = "vfeWhatError",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 WhatError(IntPtr error);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeThrowError",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ThrowError(Int32 what);
        #endregion

        #region Runtime
        [DllImport("vf_bridge.dll", EntryPoint = "vfeActivateRuntime",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ActivateRuntime();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeDeactivateRuntime",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeactivateRuntime();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeCountObjects",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 CountObjects(Byte type);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSelectObject",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SelectObject(Byte type, string id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeLinkObject",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void LinkObject(IntPtr target);
	
        [DllImport("vf_bridge.dll", EntryPoint = "vfeKickObject",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void KickObject(Byte type, string id);
	
        [DllImport("vf_bridge.dll", EntryPoint = "vfeKickAllObjects",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void KickAllObjects(Byte type);
	
        [DllImport("vf_bridge.dll", EntryPoint = "vfeReachFrame",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReachFrame(string frame);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeLoadDriverW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr LoadDriver(string path);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeLoadLibraryW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr LoadLibrary(string path);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeLoadAspectW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr LoadAspect(string path);
        #endregion

        #region Library
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetRuntime",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRuntime(IntPtr library);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetLibraryId",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetLibraryId(IntPtr library);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetProcessSym",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetProcessSym(IntPtr library, string id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetRollbackSym",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRollbackSym(IntPtr library, string id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeMountLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MountLibrary(IntPtr library);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeUnmountLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnmountLibrary(IntPtr library);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeCreateTask",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateTask(IntPtr library, string id);
        #endregion

        #region Task
        [DllImport("vf_bridge.dll", EntryPoint = "vfeStartTask",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void StartTask(IntPtr task);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeStopTask",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void StopTask(IntPtr task, UInt32 wait);

        [DllImport("vf_bridge.dll", EntryPoint = "vfePauseTask",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void PauseTask(IntPtr task, UInt32 wait);
        
        [DllImport("vf_bridge.dll", EntryPoint = "vfeResumeTask",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResumeTask(IntPtr task);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetTaskStack",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetTaskStack(IntPtr task);
        #endregion

        #region Stack
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetCurrentStack",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetCurrentStack();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetMethodId",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetMethodId(IntPtr stack);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetContext",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetContext(IntPtr stack);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetDataset",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetDataset(IntPtr stack);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeHasProtect",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool HasProtect(IntPtr stack);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetError",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetError(object stack);
        #endregion

        #region Context
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetCurrentState",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Byte GetCurrentState(IntPtr context);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetLastState",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Byte GetLastState(IntPtr context);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetReturnCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Byte GetReturnCode(IntPtr context);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetControlCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Byte GetControlCode(IntPtr context);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetProgress",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetProgress(IntPtr context);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetKeyFrame",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetKeyFrame(IntPtr context);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetReturnCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetReturnCode(IntPtr context, Byte code);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetProgress",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetProgress(IntPtr context, float progress);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetKeyFrame",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetKeyFrame(IntPtr context, string frame);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSwitchHold",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SwitchHold(IntPtr context);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSwitchBusy",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SwitchBusy(IntPtr context);
        #endregion

        #region Dataset
        [DllImport("vf_bridge.dll", EntryPoint = "vfeParseDatasetW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ParseDataset(string xml);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeZeroDataset",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ZeroDataset(IntPtr dataset);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeCopyDataset",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CopyDataset(IntPtr dataset);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetRecord",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRecord(IntPtr dataset, Int32 id);
        #endregion

        #region Record
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetRecordSize",
            CallingConvention = CallingConvention.Cdecl)]
	    public static extern uint GetRecordSize(IntPtr record);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeWriteRecord",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteRecord(IntPtr record, IntPtr data, UInt32 size);
        
        [DllImport("vf_bridge.dll", EntryPoint = "vfeReadRecord",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ReadRecord(IntPtr record);
        
        [DllImport("vf_bridge.dll", EntryPoint = "vfeDeliverRecord",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeliverRecord(IntPtr src, IntPtr dst);
        #endregion

        #region Pipe
        [DllImport("vf_bridge.dll", EntryPoint = "vfeCreatePipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreatePipe();

        [DllImport("vf_bridge.dll", EntryPoint = "vfePipeIsClose",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PipeIsClose(IntPtr pipe);

        [DllImport("vf_bridge.dll", EntryPoint = "vfePipeHasNewData",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PipeHasNewData(IntPtr pipe);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeListenPipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ListenPipe(IntPtr pipe);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeConnectPipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ConnectPipe(IntPtr pipe, string pid);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeClosePipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClosePipe(IntPtr pipe);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeWritePipeW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void WritePipe(IntPtr pipe, string data);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeReadPipeW",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ReadPipe(IntPtr pipe);
        #endregion
    }
}
