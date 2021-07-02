using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing; //Point

namespace ConsoleApp1
{

    public class MouseHookHelper
    {

        #region 根據控制代碼尋找窗體併發送訊息

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //引數1:指的是類名。引數2，指的是視窗的標題名。兩者至少要知道1個
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);



        #endregion

        #region 獲取窗體位置
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左座標
            public int Top;                             //最上座標
            public int Right;                           //最右座標
            public int Bottom;                        //最下座標
        }
        #endregion

        #region 設定窗體顯示形式

        public enum nCmdShow : uint
        {
            SW_NONE,//初始值
            SW_FORCEMINIMIZE,//：在WindowNT5.0中最小化視窗，即使擁有視窗的執行緒被掛起也會最小化。在從其他執行緒最小化視窗時才使用這個引數。
            SW_MIOE,//：隱藏視窗並激活其他視窗。
            SW_MAXIMIZE,//：最大化指定的視窗。
            SW_MINIMIZE,//：最小化指定的視窗並且啟用在Z序中的下一個頂層視窗。
            SW_RESTORE,//：啟用並顯示視窗。如果視窗最小化或最大化，則系統將視窗恢復到原來的尺寸和位置。在恢復最小化視窗時，應用程式應該指定這個標誌。
            SW_SHOW,//：在視窗原來的位置以原來的尺寸啟用和顯示視窗。
            SW_SHOWDEFAULT,//：依據在STARTUPINFO結構中指定的SW_FLAG標誌設定顯示狀態，STARTUPINFO 結構是由啟動應用程式的程式傳遞給CreateProcess函式的。
            SW_SHOWMAXIMIZED,//：啟用視窗並將其最大化。
            SW_SHOWMINIMIZED,//：啟用視窗並將其最小化。
            SW_SHOWMINNOACTIVATE,//：視窗最小化，啟用視窗仍然維持啟用狀態。
            SW_SHOWNA,//：以視窗原來的狀態顯示視窗。啟用視窗仍然維持啟用狀態。
            SW_SHOWNOACTIVATE,//：以視窗最近一次的大小和狀態顯示視窗。啟用視窗仍然維持啟用狀態。
            SW_SHOWNOMAL,//：啟用並顯示一個視窗。如果視窗被最小化或最大化，系統將其恢復到原來的尺寸和大小。應用程式在第一次顯示視窗的時候應該指定此標誌。
        }

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        #region 控制滑鼠移動

        //移動滑鼠 
        public const int MOUSEEVENTF_MOVE = 0x0001;
        //模擬滑鼠左鍵按下 
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模擬滑鼠左鍵擡起 
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模擬滑鼠右鍵按下 
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模擬滑鼠右鍵擡起 
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模擬滑鼠中鍵按下 
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模擬滑鼠中鍵擡起 
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //標示是否採用絕對座標 
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        #endregion

        #region 獲取座標鉤子

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        //安裝鉤子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //解除安裝鉤子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        //呼叫下一個鉤子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);


        /// <summary>   
        /// 獲取滑鼠的座標   
        /// </summary>   
        /// <param name="lpPoint">傳址引數，座標point型別</param>   
        /// <returns>獲取成功返回真</returns>   
        [DllImport("User32")]
        public extern static bool GetCursorPos(ref Point lpPoint);


        #endregion

    }
}
