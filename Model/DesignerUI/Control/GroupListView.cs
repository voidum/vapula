using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Vapula.API;

namespace Vapula.Designer
{
    public enum ListViewGroupMask
    {
        None = 0,
        Header = 1,
        Footer = 2,
        State = 4,
        Align = 8,
        GroupId = 0x10,
        SubTitle = 0x100,
        Task = 0x200,
        DescriptionTop = 0x400,
        DescriptionBottom = 0x800,
        TitleImage = 0x1000,
        ExtendedImage = 0x2000,
        Items = 0x4000,
        Subset = 0x8000,
        SubsetItems = 0x10000
    }

    public enum ListViewGroupState
    {
        Normal = 0,
        Collapsed = 1,
        Hidden = 2,
        NoHeader = 4,
        Collapsible = 8,
        Focused = 16,
        Selected = 32,
        SubSeted = 64,
        SubSetLinkFocused = 128,
    }
    
    /// <summary>
    /// 增强组操作的ListView
    /// </summary>
    public class GroupListView : ListView
    {
        public GroupListView()
        {
            if (Environment.OSVersion.Version.Major < 6)
                throw new Exception("不支持此早期版本操作系统");
        }

        /// <summary>
        /// 获取群组内部标识
        /// </summary>
        private int GetGroupID(ListViewGroup lvg)
        {
            Type type = typeof(ListViewGroup);
            PropertyInfo pi
                = type.GetProperty("ID",
                BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi == null) return -1;
            object obj = pi.GetValue(lvg, null);
            if (obj == null) return -1;
            int? tmp = obj as int?;
            if (tmp.HasValue) return tmp.Value;
            else return -1;
        }

        /// <summary>
        /// 设置组状态
        /// </summary>
        public void SetGroupState(ListViewGroup lvg, ListViewGroupState state)
        {
            if (InvokeRequired)
                Invoke(
                    new Action<ListViewGroup, ListViewGroupState>(SetGroupState),
                    lvg, state);
            else
            {
                int id = GetGroupID(lvg);
                User32.LVGROUP group = new User32.LVGROUP();
                group.cbSize = (uint)Marshal.SizeOf(group);
                group.state = (uint)state;
                group.mask = (uint)ListViewGroupMask.State;
                group.iGroupId = id;
                User32.SendMessage(Handle, User32.LVM_SETGROUPINFO, id, ref group);
                Refresh();
            }
        }

        /// <summary>
        /// 设置组页脚
        /// </summary>
        public void SetGroupFooter(ListViewGroup lvg, string footer)
        {
            if (InvokeRequired)
                Invoke(
                    new Action<ListViewGroup, string>(SetGroupFooter),
                    lvg, footer);
            else
            {
                int id = GetGroupID(lvg);
                User32.LVGROUP group = new User32.LVGROUP();
                group.cbSize = (uint)Marshal.SizeOf(group);
                group.pszFooter = footer;
                group.mask = (uint)ListViewGroupMask.Footer;
                group.iGroupId = id;
                User32.SendMessage(Handle, User32.LVM_SETGROUPINFO, id, ref group);
                Refresh();
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == User32.WM_LBUTTONUP)
                base.DefWndProc(ref m);
            base.WndProc(ref m);
        }
    }
}
