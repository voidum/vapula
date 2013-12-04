using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace TCM.xHost.CLR
{
    public class Loader
    {
        /// <summary>
        /// 加载.NET用户控件
        /// </summary>
        public static Control LoadCLR(string file, string clsid)
        {
            Assembly asb = Assembly.LoadFrom(file);
            Type[] types = asb.GetTypes();
            object instance = null;
            Type type = null;
            foreach (Type t in types)
                if (t.Name == clsid) type = t;
            instance = Activator.CreateInstance(type, true);
            Control ctrl = (Control)instance;
            return ctrl;
        }

        /// <summary>
        /// 加载ActiveX用户控件
        /// </summary>
        public Control LoadOCX(string file)
        {
            Assembly asb = Assembly.LoadFrom(file);
            Type[] types = asb.GetTypes();
            object instance = null;
            Type type = null;
            foreach (Type t in types)
                if (t.BaseType.Name == "AxHost") type = t;
            instance = Activator.CreateInstance(type, false);
            ((ISupportInitialize)instance).BeginInit();
            Control ctrl = (Control)instance;
            ((ISupportInitialize)instance).EndInit();
            return ctrl;
        }
    }
}
