using System;
using System.Runtime.InteropServices;

namespace Vapula
{
    public class Bridge
    {
        #region Base
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetVersion",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetVersion();

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeNewData",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NewData(UInt32 size);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeDeleteData",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteData(IntPtr data);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeOffsetData",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OffsetData(IntPtr data, UInt32 offset);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeCopyData",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CopyData(IntPtr dst, IntPtr src, UInt32 size);
        #endregion

        #region Error
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeWhatError",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 WhatError(IntPtr error);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeThrowError",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ThrowError(Int32 what);
        #endregion

        #region Runtime
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeStartRuntime",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void StartRuntime();

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeStopRuntime",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void StopRuntime();

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeReachFrame",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReachFrame(string frame);
        #endregion

        #region Aspect
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeLoadAspectW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr LoadAspect(string path);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeCountAspect",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 CountAspect();

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeFindAspect",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr FindAspect(string id);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeLinkAspect",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void LinkAspect(IntPtr aspect);
	
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeKickAspect",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void KickAspect(IntPtr aspect);
        #endregion

        #region Driver
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeLoadDriverW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr LoadDriver(string path);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeCountDriver",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 CountDriver();

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeFindDriver",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr FindDriver(string id);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeLinkDriver",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void LinkDriver(IntPtr driver);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeKickDriver",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void KickDriver(IntPtr driver);
        #endregion

        #region Library
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeLoadLibraryW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr LoadLibrary(string path);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeCountLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 CountLibrary();

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeFindLibrary",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr FindLibrary(string id);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeLinkLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void LinkLibrary(IntPtr library);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeKickLibrary",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void KickLibrary(IntPtr library);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetRuntime",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRuntime(IntPtr library);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetLibraryId",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetLibraryId(IntPtr library);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetProcessSym",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetProcessSym(IntPtr library, string id);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetRollbackSym",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRollbackSym(IntPtr library, string id);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeMountLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MountLibrary(IntPtr library);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeUnmountLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnmountLibrary(IntPtr library);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeCreateTask",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateTask(IntPtr library, string id);
        #endregion

        #region Task
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeStartTask",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void StartTask(IntPtr task);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeStopTask",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void StopTask(IntPtr task, UInt32 wait);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfePauseTask",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void PauseTask(IntPtr task, UInt32 wait);
        
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeResumeTask",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResumeTask(IntPtr task);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetTaskStack",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetTaskStack(IntPtr task);
        #endregion

        #region Stack
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetCurrentStack",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetCurrentStack();

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetMethodId",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetMethodId(IntPtr stack);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetContext",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetContext(IntPtr stack);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetDataset",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetDataset(IntPtr stack);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeHasProtect",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool HasProtect(IntPtr stack);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetError",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetError(object stack);
        #endregion

        #region Context
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetCurrentState",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Byte GetCurrentState(IntPtr context);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetLastState",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Byte GetLastState(IntPtr context);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetReturnCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Byte GetReturnCode(IntPtr context);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetControlCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern Byte GetControlCode(IntPtr context);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetProgress",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetProgress(IntPtr context);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetKeyFrame",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetKeyFrame(IntPtr context);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeSetReturnCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetReturnCode(IntPtr context, Byte code);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeSetProgress",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetProgress(IntPtr context, float progress);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeSetKeyFrame",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetKeyFrame(IntPtr context, string frame);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeSwitchHold",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SwitchHold(IntPtr context);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeSwitchBusy",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SwitchBusy(IntPtr context);
        #endregion

        #region Dataset
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeParseDatasetW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ParseDataset(string xml);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeZeroDataset",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ZeroDataset(IntPtr dataset);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeCopyDataset",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CopyDataset(IntPtr dataset);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetRecord",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRecord(IntPtr dataset, Int32 id);
        #endregion

        #region Record
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeGetRecordSize",
            CallingConvention = CallingConvention.Cdecl)]
	    public static extern UInt32 GetRecordSize(IntPtr record);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeWriteRecord",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteRecord(IntPtr record, IntPtr data, UInt32 size, bool copy);
        
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeReadRecord",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ReadRecord(IntPtr record, bool copy);
        
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeDeliverRecord",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeliverRecord(IntPtr src, IntPtr dst);
        #endregion

        #region Pipe
        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeCreatePipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreatePipe();

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfePipeIsClose",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PipeIsClose(IntPtr pipe);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfePipeHasNewData",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PipeHasNewData(IntPtr pipe);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeListenPipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ListenPipe(IntPtr pipe);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeConnectPipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ConnectPipe(IntPtr pipe, string pid);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeClosePipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClosePipe(IntPtr pipe);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeWritePipeW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void WritePipe(IntPtr pipe, string data);

        [DllImport("vf_bridge_c.dll", EntryPoint = "vfeReadPipeW",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ReadPipe(IntPtr pipe);
        #endregion
    }
}
