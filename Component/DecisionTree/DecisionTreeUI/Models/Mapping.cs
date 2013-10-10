using System.Xml.Linq;

namespace DecisionTreeUI.Models
{
    public class Mapping
    {
        private string _Name;
        private string _File;
        private int _BandIndex = 1;

        public string Name 
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string File
        {
            get { return _File; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value)) _File = null;
                _File = value;
            }
        }

        public int BandIndex 
        {
            get { return _BandIndex; }
            set { _BandIndex = value; }
        }

        public XElement ToXml()
        {
            return null;
        }
    }
}
