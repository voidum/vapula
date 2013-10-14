using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace TCM.API
{
    public class User32
    {
        public const int LVM_FIRST = 0x1000;
        public const int LVM_SETGROUPINFO = (LVM_FIRST + 147);
        public const int WM_LBUTTONUP = 0x0202;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct LVGROUP
        {
            public uint cbSize;
            public uint mask;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszHeader;
            public int cchHeader;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszFooter;
            public int cchFooter;
            public int iGroupId;
            public uint stateMask;
            public uint state;
            public uint uAlign;
            public IntPtr pszSubtitle;
            public uint cchSubtitle;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszTask;
            public uint cchTask;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszDescriptionTop;
            public uint cchDescriptionTop;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszDescriptionBottom;
            public uint cchDescriptionBottom;
            public int iTitleImage;
            public int iExtendedImage;
            public int iFirstItem;
            public uint cItems;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszSubsetTitle;
            public uint cchSubsetTitle;
        }

        [DllImport("user32.dll", EntryPoint="SendMessage")]
        public static extern int SendMessage(IntPtr window, int message, int wParam, ref LVGROUP lParam);
    }
}
