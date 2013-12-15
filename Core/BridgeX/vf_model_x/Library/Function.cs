﻿using System.Collections.Generic;
using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// Vapula模型的功能描述
    /// </summary>
    public class Function
    {
        #region 字段
        private int _Id;
        private string _Name;
        private string _Description;
        private Library _Library;
        private Tag _Tag = new Tag();
        private List<Parameter> _Params = new List<Parameter>();
        #endregion

        #region 构造
        public Function() { }
        #endregion

        #region 索引器
        /// <summary>
        /// 根据指定标识获取参数
        /// </summary>
        public Parameter this[int id]
        {
            get
            {
                foreach (var param in _Params)
                    if (param.Id == id)
                        return param;
                return null;
            }
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 由XML解析功能描述
        /// </summary>
        public static Function Parse(XElement xml)
        {
            Function func = new Function();
            func.Id = int.Parse(xml.Attribute("id").Value);
            func.Description = xml.Element("description").Value;
            func.Name = xml.Element("name").Value;
            var xml_params = xml.Element("params").Elements("param");
            foreach (var xml_param in xml_params)
            {
                var param = Parameter.Parse(xml_param);
                param.Function = func;
                func._Params.Add(param);
            }
            return func;
        }

        /// <summary>
        /// 将功能描述序列化为XML元素
        /// </summary>
        public XElement ToXML()
        {
            XElement xml = new XElement("function",
                new XElement("description", Description),
                new XElement("name", Name),
                new XElement("params"),
                new XAttribute("id", Id));
            foreach (var param in _Params)
            {
                var xml_param = param.ToXML();
                xml.Element("params").Add(xml_param);
            }
            return xml;
        }
        #endregion

        #region 集合
        public void Clear()
        {
            _Params.Clear();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取功能的标识
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 获取或设置功能的名称
        /// </summary>
        public string Name
        {
            get
            {
                if (_Name == null) return "";
                return _Name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Name = null;
                else _Name = value;
            }
        }

        /// <summary>
        /// 获取或设置功能的描述
        /// </summary>
        public string Description
        {
            get
            {
                if (_Description == null) return "";
                return _Description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Description = null;
                else _Description = value;
            }
        }

        /// <summary>
        /// 获取功能所在的组件
        /// </summary>
        public Library Library
        {
            get { return _Library; }
            set { _Library = value; }
        }

        /// <summary>
        /// 获取或设置附加数据
        /// </summary>
        public Tag Tag
        {
            get { return _Tag; }
        }

        /// <summary>
        /// 获取功能的参数集合
        /// </summary>
        public List<Parameter> Parameters
        {
            get { return _Params; }
        }
        #endregion
    }
}
