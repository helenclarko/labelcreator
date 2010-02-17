using System;
using Microsoft.Win32;    
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms; 

namespace DVDScribe
{
    static public class libRegistry
    {
        private static RegistryKey pBaseRegistryKey = Registry.CurrentUser;        
        private static string pSubKey = @"SOFTWARE\" + Application.ProductName;

        public static string ReadValue(string Key)
        {
            string result = "";
            RegistryKey baseKey = pBaseRegistryKey;
            RegistryKey sub = baseKey.OpenSubKey(pSubKey);
            if (sub != null)
            {
                try
                {
                    result = (string)sub.GetValue(Key.ToUpper());
                }
                catch { }                
            }
            baseKey.Close();
            return result;
        }

        public static void WriteValue(string Key, string Value)
        {
            try
            {
                RegistryKey baseKey = pBaseRegistryKey;
                RegistryKey sub = baseKey.CreateSubKey(pSubKey);
                sub.SetValue(Key.ToUpper(), Value);
                baseKey.Close();
            }
            catch (Exception e) { }
        }

    }
}
