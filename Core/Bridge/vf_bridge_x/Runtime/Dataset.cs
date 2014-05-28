using System;
using System.Xml.Linq;

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
            IntPtr ptr = Bridge.ParseDataset(s);
            Dataset dataset = new Dataset(ptr);
            return dataset;
        }

        public Record this[int id]
        {
            get
            {
                IntPtr ptr = Bridge.GetRecord(Handle, id);
                Record record = new Record(ptr);
                return record;
            }
        }

	    public void Zero()
        {
            Bridge.ZeroDataset(Handle);
        }

        public Dataset Copy() 
        {
            IntPtr handle = Bridge.CopyDataset(Handle);
            Dataset dataset = new Dataset(handle);
            return dataset;
        }
    }
}
