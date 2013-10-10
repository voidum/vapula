using System;
using System.Windows.Forms;
using System.Xml.Linq;
using TCM.API;
using TCM.Models;
using TCM.Toolkit;

namespace BandMathUI
{
    public class AppData
    {
        #region 窗体
        public static PIE.Controls.DlgProgress FormProgress = null;
        #endregion

        #region 依赖
        public static string TcmPath = Application.StartupPath + "\\";

        private static IntPtr _TcmModHandle = IntPtr.Zero;
        public static IntPtr TcmModHandle
        {
            get
            {
                if (_TcmModHandle == IntPtr.Zero)
                {
                    string path = TcmPath + "BandMath.dll";
                    _TcmModHandle = Kernel32.LoadLibary(path);
                    if (_TcmModHandle == IntPtr.Zero)
                    {
                        int err = Kernel32.GetLastError();
                        TCM.Global.Logger.WriteLog(LogType.Error, "Win32 ERROR: " + err.ToString());
                    }
                }
                return _TcmModHandle;
            }
        }

        private static Function _TcmFuncDesc = null;
        public static Function TcmFuncDesc
        {
            get
            {
                if (_TcmFuncDesc == null)
                {
                    XDocument xdoc = XDocument.Load(TcmPath + "BandMath.tcm.xml");
                    XElement xe = xdoc.Element("root").Element("component").Element("functions").Element("function");
                    _TcmFuncDesc = Function.Parse(xe);
                }
                return _TcmFuncDesc;
            }
        }
        #endregion
    }
}
