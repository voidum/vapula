using System;
using System.Runtime.InteropServices;

namespace PIE.API
{
    internal class GDAL
    {
        public const int GA_ReadOnly = 0;
        public const int GA_Update = 1;

        [DllImport("gdal19.dll", EntryPoint = "GDALAllRegister",
            CallingConvention = CallingConvention.StdCall)]
        public static extern void AllRegister();

        [DllImport("gdal19.dll", EntryPoint = "GDALClose",
            CallingConvention = CallingConvention.StdCall)]
        public static extern void Close(IntPtr hdataset);

        [DllImport("gdal19.dll", EntryPoint = "GDALOpen",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr Open(string filename, int access);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetDatasetDriver",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetDatasetDriver(IntPtr hdataset);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetDriverShortName",
            CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string GetDriverShortName(IntPtr hdriver);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetDriverLongName",
            CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string GetDriverLongName(IntPtr hdriver);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetRasterXSize",
            CallingConvention = CallingConvention.StdCall)]
        public static extern int GetRasterXSize(IntPtr hdataset);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetRasterYSize",
            CallingConvention = CallingConvention.StdCall)]
        public static extern int GetRasterYSize(IntPtr hdataset);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetRasterCount",
            CallingConvention = CallingConvention.StdCall)]
        public static extern int GetRasterCount(IntPtr hdataset);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetRasterBand",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetRasterBand(IntPtr hdataset, int bandindex);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetRasterDataType",
            CallingConvention = CallingConvention.StdCall)]
        public static extern int GetRasterDataType(IntPtr hband);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetDataTypeName",
            CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string GetDataTypeName(int datatype);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetDataTypeSize",
            CallingConvention = CallingConvention.StdCall)]
        public static extern int GetDataTypeSize(int datatype);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetRasterBandXSize",
            CallingConvention = CallingConvention.StdCall)]
        public static extern int GetRasterBandXSize(IntPtr hband);

        [DllImport("gdal19.dll", EntryPoint = "GDALGetRasterBandYSize",
            CallingConvention = CallingConvention.StdCall)]
        public static extern int GetRasterBandYSize(IntPtr hband);
    }
}
