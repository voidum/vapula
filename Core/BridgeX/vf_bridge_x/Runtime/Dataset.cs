using System;
using System.Xml.Linq;
using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// dataset
    /// </summary>
    public class Dataset
    {
        private IntPtr _Handle = IntPtr.Zero;

        /// <summary>
        /// get handle
        /// </summary>
        public IntPtr Handle
        {
            get { return _Handle; }
        }

        public Dataset(IntPtr handle)
        {
            _Handle = handle;
        }

        /// <summary>
        /// parse dataset from xml
        /// </summary>
        public static Dataset Parse(XElement xml)
        {
            string s = xml.ToString();
            IntPtr hds = Bridge.ParseDataset(s);
            Dataset ds = new Dataset(hds);
            return ds;
        }

        public Record this[int id]
        {
            get
            {
                IntPtr ptr = Bridge.GetRecord(Handle, id);
                return null;
            }
        }

	    public void Zero()
        {
            Bridge.ZeroDataset(Handle);
        }

        public Dataset Copy() 
        {
            IntPtr hds = Bridge.CopyDataset(Handle);
            Dataset ds = new Dataset(hds);
            return ds;
        }
    }
}
