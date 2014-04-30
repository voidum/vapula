using System;
using System.Runtime.InteropServices;

namespace Vapula.API
{
    public class Bridge
    {
        /// <summary>
        /// convert IntPtr from PInvoke into string
        /// </summary>
        public static string ToString(IntPtr ptr, bool unicode = true)
        {
            string ret =
                unicode ?
                    Marshal.PtrToStringUni(ptr) :
                    Marshal.PtrToStringAnsi(ptr);
            return ret;
        }

        #region Base
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetVersion",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetVersion();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeNewData",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NewData(byte type, uint count);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeWriteAt",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteAt(IntPtr data, byte type, uint at, string value);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeReadAt",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ReadAt(IntPtr data, byte type, uint at);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeDeleteRaw",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteRaw(IntPtr obj);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeBase64ToRaw",
            CallingConvention = CallingConvention.Cdecl)]
	    public static extern IntPtr Base64ToRaw(string data);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeRawToBase64",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr RawToBase64(IntPtr data, uint size);
        #endregion

        #region Error
        [DllImport("vf_bridge.dll", EntryPoint = "vfeWhatError",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int WhatError(IntPtr err);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeThrowError",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ThrowError(int what);
        #endregion

        #region Driver
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetDriverCount",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetDriverCount();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeLinkDriver",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LinkDriver(string id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeKickDriver",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void KickDriver(string id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeKickAllDrivers",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void KickAllDrivers();
        #endregion

        #region Library
        [DllImport("vf_bridge.dll", EntryPoint = "vfeLoadLibraryW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr LoadLibrary(string path);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetRuntime",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRuntime(IntPtr lib);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetLibraryId",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetLibraryId(IntPtr lib);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetProcessSym",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetProcessSym(IntPtr lib, string id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetRollbackSym",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRollbackSym(IntPtr lib, string id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeMountLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MountLibrary(IntPtr lib);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeUnmountLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnmountLibrary(IntPtr lib);
        #endregion

        #region
        [DllImport("vf_bridge.dll", EntryPoint = "vfeLinkAspectW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LinkAspect(string path);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeReachFrame",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReachFrame(string frame);
        #endregion

        #region Invoker
        [DllImport("vf_bridge.dll", EntryPoint = "vfeCreateInvoker",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateInvoker(IntPtr lib, string id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeStartInvoker",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool StartInvoker(IntPtr inv);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeStopInvoker",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void StopInvoker(IntPtr inv, uint wait);

        [DllImport("vf_bridge.dll", EntryPoint = "vfePauseInvoker",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void PauseInvoker(IntPtr inv, uint wait);
        
        [DllImport("vf_bridge.dll", EntryPoint = "vfeResumeInvoker",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResumeInvoker(IntPtr inv);
        
        [DllImport("vf_bridge.dll", EntryPoint = "vfeRestartInvoker",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void RestartInvoker(IntPtr inv, uint wait);
        #endregion

        #region Stack
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetStack",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetStack(IntPtr inv);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetCurrentStack",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetCurrentStack();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetMethodId",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetMethodId(IntPtr stk);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetContext",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetContext(IntPtr stk);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetEnvelope",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetEnvelope(IntPtr stk);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeIsProtected",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsProtected(IntPtr stk);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetError",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetError(object stk);
        #endregion

        #region Context
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetCurrentState",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetCurrentState(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetLastState",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetLastState(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetReturnCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetReturnCode(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetControlCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetControlCode(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetProgress",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetProgress(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetKeyFrame",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetKeyFrame(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetReturnCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetReturnCode(IntPtr ctx, byte ret);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetProgress",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetProgress(IntPtr ctx, float prog);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetKeyFrame",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetKeyFrame(IntPtr ctx, string frame);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSwitchHold",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SwitchHold(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSwitchBusy",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SwitchBusy(IntPtr ctx);
        #endregion

        #region Dataset
        [DllImport("vf_bridge.dll", EntryPoint = "vfeParseDatasetW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ParseDataset(string xml);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeZeroDataset",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ZeroDataset(IntPtr ds);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeCopyDataset",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CopyDataset(IntPtr ds);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetRecord",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRecord(IntPtr ds, int id);
        #endregion

        #region Record
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetRecordSize",
            CallingConvention = CallingConvention.Cdecl)]
	    public static extern uint GetRecordSize(IntPtr rec);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeWriteRecord",
            CallingConvention = CallingConvention.Cdecl)]
	    public static extern void WriteRecord(IntPtr rec, IntPtr data, uint size);
        
        [DllImport("vf_bridge.dll", EntryPoint = "vfeReadRecord",
            CallingConvention = CallingConvention.Cdecl)]
	    public static extern IntPtr ReadRecord(IntPtr rec);
        
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
