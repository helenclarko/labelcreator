using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace DVDScribe
{
    class NativeCalls
    {            
        [DllImport("USER32.DLL", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref int lParam);
        [DllImport("user32")]
        public static extern int ReleaseCapture(IntPtr hwnd);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_DRAGMOVE = 0xF012;
        public const int SC_DRAGSIZE_N = 0xF003;
        public const int SC_DRAGSIZE_S = 0xF006;
        public const int SC_DRAGSIZE_E = 0xF002;
        public const int SC_DRAGSIZE_W = 0xF001;
        public const int SC_DRAGSIZE_NW = 0xF004;
        public const int SC_DRAGSIZE_NE = 0xF005;
        public const int SC_DRAGSIZE_SW = 0xF007;
        public const int SC_DRAGSIZE_SE = 0xF008;
    }
}
