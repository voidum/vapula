using System;
using System.Runtime.InteropServices;

namespace Vapula.API
{
    internal class Bridge
    {
        /// <summary>
        /// 转换P/Invoke返回的字符串
        /// </summary>
        public static string MarshalString(IntPtr ptr, bool unicode = true)
        {
            string ret =
                unicode ?
                    Marshal.PtrToStringUni(ptr) :
                    Marshal.PtrToStringAnsi(ptr);
            return ret;
        }

        #region Base
        [DllImport("vf_bridge.dll", EntryPoint = "vfeTestBridge",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void TestBridge();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeDeleteObject",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteObject(IntPtr obj);
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
        public static extern bool KickDriver(string id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeKickAllDrivers",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void KickAllDrivers();
        #endregion

        #region Library
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetLibraryCount",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLibraryCount();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeLoadLibraryW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr LoadLibrary(string path);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetLibraryRuntime",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetLibraryRuntime(IntPtr lib);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetLibraryId",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetLibraryId(IntPtr lib);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeMountLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MountLibrary(IntPtr lib);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeUnmountLibrary",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnmountLibrary(IntPtr lib);
        #endregion

        #region Invoker
        [DllImport("vf_bridge.dll", EntryPoint = "vfeCreateInvoker",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateInvoker(IntPtr lib, int fid);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetFunctionId",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetFunctionId(IntPtr lib);

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

        #region Context
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetContext",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetContext(IntPtr inv);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetCtrlCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetCtrlCode(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetCtrlCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetCtrlCode(IntPtr ctx, int ctrl_code);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeReplyCtrlCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReplyCtrlCode(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetProgress",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetProgress(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetProgress",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetProgress(IntPtr ctx, float prog);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetState",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetState(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetState",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetState(IntPtr ctx, int state);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetReturnCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetReturnCode(IntPtr ctx);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeSetReturnCode",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetReturnCode(IntPtr ctx, int return_code);
        #endregion

        #region Envelope
        [DllImport("vf_bridge.dll", EntryPoint = "vfeGetEnvelope",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetEnvelope(IntPtr inv);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeParseEnvelopeW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ParseEnvelope(string xml);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeWriteEnvelopeW",
            CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteEnvelope(IntPtr env, int id, string value);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeReadEnvelopeW",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ReadEnvelope(IntPtr env, int id);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeDeliverEnvelope",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeliverEnvelope(IntPtr src, IntPtr dst, int from, int to);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeCastDeliverEnvelope",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void CastDeliverEnvelope(IntPtr src, IntPtr dst, int from, int to);
        #endregion

        #region Pipe
        [DllImport("vf_bridge.dll", EntryPoint = "vfeCreatePipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreatePipe();

        [DllImport("vf_bridge.dll", EntryPoint = "vfeListenPipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ListenPipe(IntPtr pipe);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeConnectPipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ConnectPipe(IntPtr pipe, string pid);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeClosePipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClosePipe(IntPtr pipe);

        [DllImport("vf_bridge.dll", EntryPoint = "vfePipeIsClose",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PipeIsClose(IntPtr pipe);

        [DllImport("vf_bridge.dll", EntryPoint = "vfePipeHasNewData",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PipeHasNewData(IntPtr pipe);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeWritePipe",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void WritePipe(IntPtr pipe, string data);

        [DllImport("vf_bridge.dll", EntryPoint = "vfeReadPipe",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ReadPipe(IntPtr pipe);
        #endregion
    }
}
