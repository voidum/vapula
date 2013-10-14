using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TCM.API;

namespace TCM.Model.Designer
{
    public class GroupListView : ListView
    {
        public GroupListView()
        {
            if (Environment.OSVersion.Version.Major < 6)
                throw new Exception("不支持此早期版本操作系统");
        }

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
                User32.SendMessage(Handle, User32.LVM_SETGROUPINFO, id, ref group);
                lvg.ListView.Refresh();
            }
        }

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
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == User32.WM_LBUTTONUP)
                base.DefWndProc(ref m);
            base.WndProc(ref m);
        }
    }

    public enum ListViewGroupMask
    {
        None = 0x00000,
        Header = 0x00001,
        Footer = 0x00002,
        State = 0x00004,
        Align = 0x00008,
        GroupId = 0x00010,
        SubTitle = 0x00100,
        Task = 0x00200,
        DescriptionTop = 0x00400,
        DescriptionBottom = 0x00800,
        TitleImage = 0x01000,
        ExtendedImage = 0x02000,
        Items = 0x04000,
        Subset = 0x08000,
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
}
