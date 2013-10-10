using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DecisionTreeUI.Models;
using PIE.Controls;
using TCM.API;
using TCM.I18N;
using TCM.Model;
using TCM.Toolkit;

namespace DecisionTreeUI
{
    public class AppData
    {
        #region 窗体
        private static FrmMain _FormMain;
        public static FrmMain FormMain 
        {
            get 
            {
                if (_FormMain == null) _FormMain = new FrmMain();
                return _FormMain;
            }
        }

        private static FrmClass _FormClass;
        public static FrmClass FormClass
        {
            get
            {
                if (_FormClass == null) _FormClass = new FrmClass();
                return _FormClass;
            }
        }

        private static FrmJudge _FormJudge;
        public static FrmJudge FormJudge
        {
            get
            {
                if (_FormJudge == null) _FormJudge = new FrmJudge();
                return _FormJudge;
            }
        }

        private static FrmCfgData _FormCfgData;
        public static FrmCfgData FormCfgData
        {
            get
            {
                if (_FormCfgData == null) _FormCfgData = new FrmCfgData();
                return _FormCfgData;
            }
        }

        private static DlgProgress _FormProgress;
        public static DlgProgress FormProgress
        {
            get { return _FormProgress; }
            set { _FormProgress = value; }
        }
        #endregion

        #region 数据
        private static Translator _LangPack = new Translator();
        public static Translator LangPack
        {
            get { return _LangPack; }
        }

        private static List<NodeBase> _Nodes = new List<NodeBase>();
        public static List<NodeBase> Nodes 
        {
            get { return _Nodes; }
        }
        public static List<int> NodeIds
        {
            get 
            {
                List<int> ids = new List<int>();
                foreach (var node in _Nodes)
                    ids.Add(node.Id);
                return ids;
            }
        }

        public static List<NodeBase> NodesJudge 
        {
            get
            {
                List<NodeBase> nodes = new List<NodeBase>();
                foreach (var node in _Nodes)
                    if(node.Type == NodeType.Judge) nodes.Add(node);
                return nodes;
            }
        }

        public static List<int> NodeIdsJudge
        {
            get
            {
                List<int> ids = new List<int>();
                foreach (var node in _Nodes)
                    if(node.Type == NodeType.Judge) ids.Add(node.InheritId);
                return ids;
            }
        }
        public static List<int> NodeIdsClass
        {
            get
            {
                List<int> ids = new List<int>();
                foreach (var node in _Nodes)
                    if (node.Type == NodeType.Class) ids.Add(node.InheritId);
                return ids;
            }
        }
        public static List<Color> NodeColors 
        {
            get 
            {
                List<Color> cs = new List<Color>();
                foreach (var node in _Nodes)
                    if (node.Type == NodeType.Class) cs.Add((node as NodeClass).ClassColor);
                return cs;
            }
        }

        private static List<Mapping> _Mappings = new List<Mapping>();
        public static List<Mapping> Mappings 
        {
            get { return _Mappings; }
        }

        public static List<string> VarNames
        {
            get
            {
                List<string> strs = new List<string>();
                foreach(Mapping mapping in _Mappings)
                    strs.Add(mapping.Name);
                return strs;
            }
        }

        private static string _OutDir;
        public static string OutDir
        {
            get { return _OutDir; }
            set { _OutDir = value; }
        }

        private static string _OutName;
        public static string OutName
        {
            get { return _OutName; }
            set { _OutName = value; }
        }

        private static int _OutFmt;
        public static int OutFmt
        {
            get { return _OutFmt; }
            set { _OutFmt = value; }
        }

        public static void InitData()
        {
            AppData.Nodes.Clear();
            AppData.Mappings.Clear();
            AppData.OutDir = "";
            AppData.OutFmt = 0;
            AppData.OutName = "";
        }
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
                    string path = TcmPath + "DecisionTree.dll";
                    _TcmModHandle = Kernel32.LoadLibary(path);
                    if (_TcmModHandle == IntPtr.Zero)
                    {
                        int err = Kernel32.GetLastError();
                        TCM.Global.Logger.WriteLog(LogType.Error, "Win32 ERROR: " + err.ToString());
                    }
                }
                return _TcmModHandle ;
            }
        }

        private static Function _TcmFuncDesc = null;
        public static Function TcmFuncDesc 
        {
            get
            {
                if (_TcmFuncDesc == null)
                {
                    XDocument xdoc = XDocument.Load(TcmPath + "DecisionTree.tcm.xml");
                    XElement xe = xdoc.Element("root").Element("component").Element("functions").Element("function");
                    _TcmFuncDesc = Function.Parse(xe);
                }
                return _TcmFuncDesc;
            }
        }
        #endregion
    }
}
