using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
namespace DVDScribe
{
    class libLS
    {
        public class DllNotFound : Exception
        {
            public DllNotFound() : base(){}
            public DllNotFound(string message) : base(message) { }
        }

        public class DriveNotFound : Exception
        {
            public DriveNotFound() : base() { }
            public DriveNotFound(string message) : base(message) { }
        }

        private static IntPtr iLightScribeDll = IntPtr.Zero;

        private delegate int PrintPreview(IntPtr args);        
        private delegate bool haveLSDrive();

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        private extern static IntPtr GetProcAddress(IntPtr dllPointer, string functionName);

        private static IntPtr LightScribeDll
        {
            get
            {
                if (iLightScribeDll == IntPtr.Zero)
                {
                    string dllPath = GetDllLocation();

                    if (dllPath == string.Empty)
                    {
                        dllPath = "C:\\Program Files\\Common Files\\LightScribe\\LSPrintLauncher.dll";
                    }//return IntPtr.Zero;
                    try
                    {                        
                        iLightScribeDll = LoadWin32Library(dllPath);
                    }
                    catch (ApplicationException)
                    {
                        return IntPtr.Zero;
                    }
                }
                return iLightScribeDll;
            }
        }

        private static System.IntPtr LoadWin32Library(string libPath)
        {
            System.IntPtr moduleHandle = LoadLibraryEx(libPath, IntPtr.Zero, 0);
            if (moduleHandle == IntPtr.Zero)
                throw new ApplicationException("Failed to load library " + libPath);
            return moduleHandle;
        }

        private static string GetDllLocation()
        {
            RegistryKey key = Registry.LocalMachine;
            key = key.OpenSubKey(@"Software\LightScribe");
            if (key == null) throw new DllNotFound("LightScribe Libraries not found");

            return key.GetValue("LsPrintLauncher", string.Empty) as string;
        }

        public static bool LightScribeSoftwareInstalled()
        {
            if (GetDllLocation() != "")
                return true;
            else
            {
                //throw new DllNotFound("LightScribe Libraries not found");                
                return true;
            }                
        }

        public static bool HasLightScribeDrive()
        {
            IntPtr pProc = GetProcAddress(LightScribeDll, "haveLSDrive");
            try
            {
                haveLSDrive HaveDrive = (haveLSDrive)Marshal.GetDelegateForFunctionPointer(pProc, typeof(haveLSDrive));
                return HaveDrive(); 
            }
            catch
            {
                return false;
            }                       
        }

        public static int DoPrintPreview(string AFileName)
        {
            string arguments = string.Format("--filename \"{0}\"  --deleteImageFile 1", AFileName);
            IntPtr args = Marshal.StringToHGlobalUni(arguments);
            try
            {
                IntPtr pProc = GetProcAddress(LightScribeDll, "launchPrintOptions");
                PrintPreview OnPrintPreview = (PrintPreview)Marshal.GetDelegateForFunctionPointer(pProc, typeof(PrintPreview));

                return OnPrintPreview(args);
            }
            finally
            {
                Marshal.FreeHGlobal(args);
            }
            return -1;
        }
    }
}
