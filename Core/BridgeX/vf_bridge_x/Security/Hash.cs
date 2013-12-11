using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace TCM.Security
{
    public class Hash
    {
        public static string GetHardwareId()
        {
            Func<string, string, Func<ManagementObject, bool>, string> func_wmi =
                new Func<string, string, Func<ManagementObject, bool>, string>(
                    (a, b, c) =>
                    {
                        ManagementClass mc = new ManagementClass(a);
                        ManagementObjectCollection moc = mc.GetInstances();
                        StringBuilder sbd = new StringBuilder();
                        foreach (ManagementObject mo in moc)
                            if (c == null || c(mo))
                                sbd.Append(mo.Properties[b].Value.ToString());
                        mc.Dispose();
                        return sbd.ToString();
                    });
            StringBuilder sbd_r = new StringBuilder();
            sbd_r.Append(func_wmi("Win32_Processor", "ProcessorId", null));
            sbd_r.Append(func_wmi("Win32_BaseBoard", "SerialNumber", null));
            sbd_r.Append(func_wmi("Win32_BIOS", "SerialNumber", null));
            sbd_r.Append(func_wmi("Win32_NetworkAdapterConfiguration", "MacAddress",
                new Func<ManagementObject, bool>(
                    (o) =>
                    {
                        bool ret = (bool)(o["IPEnabled"]);
                        return ret;
                    })));
            return GetSHA1(sbd_r.ToString());
        }

        public static string GetMD5(string src, bool isfile = false)
        {
            MD5 hashmc = new MD5Cng();
            byte[] hash;
            if (isfile)
            {
                FileStream fs = File.OpenRead(src);
                hash = hashmc.ComputeHash(fs);
                fs.Close();
            }
            else
            {
                byte[] bytes = Encoding.Unicode.GetBytes(src);
                hash = hashmc.ComputeHash(bytes);
            }
            hashmc.Dispose();
            StringBuilder sbd = new StringBuilder();
            foreach (byte b in hash) sbd.Append(b.ToString("x2"));
            string result = sbd.ToString();
            return result;
        }

        public static string GetSHA1(string src, bool isfile = false)
        {
            SHA1Cng hashmc = new SHA1Cng();
            byte[] hash;
            if (isfile)
            {
                FileStream fs = File.OpenRead(src);
                hash = hashmc.ComputeHash(fs);
                fs.Close();
            }
            else
            {
                byte[] bytes = Encoding.Unicode.GetBytes(src);
                hash = hashmc.ComputeHash(bytes);
            }
            hashmc.Dispose();
            StringBuilder sbd = new StringBuilder();
            foreach (byte b in hash) sbd.Append(b.ToString("x2"));
            string result = sbd.ToString();
            return result;
        }
    }
}
