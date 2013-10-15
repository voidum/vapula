using System;

namespace TCM.Helper
{
    public class IOHelper
    {
        /// <summary>
        /// 获取当前路径
        /// </summary>
        public static string AppDir
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        /// <summary>
        /// <para>合并路径</para>
        /// <para>第一部分必须不是文件的路径</para>
        /// <para>第二部分可以是相对路径，包括"..\"</para>
        /// </summary>
        public static string MergePath(string part1, string part2)
        {
            if (string.IsNullOrWhiteSpace(part1) || string.IsNullOrWhiteSpace(part2))
                return part1 + part2;
            int tmplen = part1.Length - 1;
            string tmp1 = (part1[tmplen] == '\\' ? part1.Substring(0, tmplen) : part1);
            string tmp2 = part2;
            while (tmp2.Length > 3 && tmp2.Substring(0, 3) == "..\\")
            {
                tmp1 = tmp1.Substring(0, tmp1.LastIndexOf("\\"));
                tmp2 = tmp2.Substring(3);
            }
            string result = tmp1 + "\\" + tmp2;
            return result;
        }

        /// <summary>
        /// 获取全路径
        /// </summary>
        public static string GetFullDir(string src,bool isfile = false) 
        {
            if (string.IsNullOrWhiteSpace(src)) return src;
            string result = "";
            if (isfile)
            {
                if (src.IndexOf('\\') < 0) result = "\\";
                else result = src.Substring(0, src.LastIndexOf('\\') + 1);
            }
            else
                result = (src[src.Length - 1] == '\\' ? src : src + '\\');
            return result;
        }

        /// <summary>
        /// <para>获取文件名</para>
        /// <para>文件名不包含路径和扩展名</para>
        /// </summary>
        public static string GetFileNameNoExt(string src)
        {
            string file = GetFileName(src);
            return file.Substring(0, file.LastIndexOf('.'));
        }

        /// <summary>
        /// <para>获取文件名</para>
        /// <para>文件名不包含路径</para>
        /// </summary>
        public static string GetFileName(string src)
        {
            return src.Substring(src.LastIndexOf('\\') + 1);
        }
    }
}
