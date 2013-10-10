using System.Collections.Generic;
using System.Xml.Linq;

namespace PIE.Models
{
    /// <summary>
    /// 数据计算应用中，用于描述读入数据到变量的映射关系
    /// </summary>
    public class DataVar
    {
        private string _Name;
        private string _File;
        private bool _AsBand = true;
        private List<int> _SpectralSubset = null;
        private int[] _SpatialSubset = null;

        /// <summary>
        /// 获取或设置变量名称
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
                if (string.IsNullOrWhiteSpace(value)) _Name = null;
                _Name = value;
            }
        }

        /// <summary>
        /// 获取或设置变量数据源
        /// </summary>
        public string File
        {
            get 
            {
                if (_File == null) return "";
                return _File; 
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _File = null;
                _File = value;
            }
        }

        /// <summary>
        /// 获取或设置变量按照波段还是波谱数据执行映射
        /// </summary>
        public bool AsBand
        {
            get { return _AsBand; }
            set { _AsBand = value; }
        }

        /// <summary>
        /// 获取或设置光谱子集
        /// </summary>
        public List<int> SpectralSubset
        {
            get { return _SpectralSubset; }
            set { _SpectralSubset = value; }
        }

        /// <summary>
        /// <para>获取或设置空间子集</para>
        /// <para>一共有四个分量：X1,X2,Y1,Y2</para>
        /// </summary>
        public int[] SpatialSubset
        {
            get { return _SpatialSubset; }
            set { _SpatialSubset = value; }
        }

        /// <summary>
        /// 空间子集宽度
        /// </summary>
        public int SpatWidth
        {
            get { return _SpatialSubset[1] - _SpatialSubset[0] + 1; }
        }

        /// <summary>
        /// 空间子集高度
        /// </summary>
        public int SpatHeight
        {
            get { return _SpatialSubset[3] - _SpatialSubset[2] + 1; }
        }

        public XElement ToXml()
        {
            XElement xe = new XElement("datavar",
                new XElement("name",Name),
                new XElement("file",new XCData(File)),
                new XElement("asband",_AsBand ? "true" : "false"),
                new XElement("specs"),
                new XElement("spats"));
            if (_SpectralSubset != null)
            {
                string specs = string.Join(",", _SpectralSubset);
                xe.Element("specs").SetValue(specs);
            }
            if (_SpatialSubset != null)
            {
                string spats = string.Join(",", _SpatialSubset);
                xe.Element("spats").SetValue(spats);
            }
            return xe;
        }
    }
}
