using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Symbol {
    public static class Win32 {
        /// <summary>
        /// 获得当前前台窗体句柄。
        /// </summary>
        /// <returns>返回当前前台窗体的句柄。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 根据窗体的类名和窗口的名称获得目标窗体。
        /// </summary>
        /// <param name="lpClassName">窗口的类名</param>
        /// <param name="lpWindowName">窗口的标题</param>
        /// <returns>返回窗体的句柄。</returns>
        /// <remarks>两个参数可以只知道其中一个。</remarks>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 查找子窗体的方法
        /// </summary>
        /// <param name="hwndParent">父窗口句柄，如果hwndParent为Null，则函数以桌面窗口为父窗口，查找桌面窗口的所有子窗口；如果hwndParent是HWND_MESSAGE,函数仅查找所有消息窗口。</param>
        /// <param name="hwndChildAfter">子窗口句柄。查找从在Z序中的下一个子窗口开始。子窗口必须为hwndPareRt窗口的直接子窗口而非后代窗口。如果HwndChildAfter为NULL，查找从hwndParent的第一个子窗口开始。如果hwndParent 和 hwndChildAfter同时为NULL，则函数查找所有的顶层窗口及消息窗口。</param>
        /// <param name="lpszClass">指向一个指定了类名的空结束字符串，或一个标识类名字符串的成员的指针。如果该参数为一个成员，则它必须为前次调用theGlobaIAddAtom函数产生的全局成员。该成员为16位，必须位于lpClassName的低16位，高位必须为0。</param>
        /// <param name="lpszWindow">指向一个指定了窗口名（窗口标题）的空结束字符串。如果该参数为 NULL，则为所有窗口全匹配。返回值：如果函数成功，返回值为具有指定类名和窗口名的窗口句柄。如果函数失败，返回值为NULL。总之，这个函数查找子窗口，从排在给定的子窗口后面的下一个子窗口开始。在查找时不区分大小写。</param>
        /// <returns>返回找到的句柄。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        private static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        /// <summary>
        /// 获取窗口文本内容
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>返回窗口内容。</returns>
        public static string GetWindowText(int hWnd) {
            StringBuilder s = new StringBuilder(512);
            int i = GetWindowText(hWnd, s, s.Capacity);
            return s.ToString();
        }
        /// <summary>
        /// 获取窗口文本内容
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>返回窗口内容。</returns>
        public static string GetWindowText(IntPtr hWnd) {
            StringBuilder s = new StringBuilder(512);
            int i = GetWindowText(hWnd, s, s.Capacity);
            return s.ToString();
        }
        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr hwnd, string lPstring);
        [DllImport("user32.dll")]
        public static extern bool SetWindowText(int hwnd, string lPstring);

        //定义回调函数的委托
        public delegate bool CallBack(int hWnd, int lParm);
        //定义回调函数的委托
        public delegate bool CallBackPtr(IntPtr hWnd, int lParm);

        [DllImport("user32.dll")]
        public static extern int EnumThreadWindows(int dwThreadId, CallBack lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumThreadWindows(int dwThreadId, CallBackPtr lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumWindows(CallBack lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumWindows(CallBackPtr lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(int hWndParent, CallBack lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBackPtr lpfn, int lParam);

        /// <summary>
        /// 获取与指定进程Id相关的顶级窗口。
        /// </summary>
        /// <param name="windowTitle">窗口标题</param>
        /// <param name="processId">进程Id</param>
        /// <returns>返回窗口句柄，未找到时返回IntPtr.Zero。</returns>
        public static IntPtr FindWindowByProcessId(string windowTitle, int processId) {
            IntPtr ptr = IntPtr.Zero;
            Win32.EnumWindows((IntPtr w, int l) => {
                if (string.Equals(GetWindowText(w), windowTitle, StringComparison.OrdinalIgnoreCase) && Win32.GetWindowProcessId((IntPtr)w) == processId) {
                    ptr = (IntPtr)w;
                }
                return true;
            }, 0);
            return ptr;
        }
        public static IntPtr GetProcessMainWindowHandle(int processId) {
            using (System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById(processId)) {
                return process.MainWindowHandle;
            }
        }

        /// <summary>
        /// 获取指定窗口的线程Id和进程Id
        /// </summary>
        /// <param name="hWnd">窗口的句柄</param>
        /// <param name="processId">输出所在进程的Id</param>
        /// <returns>返回所在线程Id</returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);
        /// <summary>
        /// 获取指定窗口的线程Id
        /// </summary>
        /// <param name="hWnd">窗口的句柄</param>
        /// <returns>返回所在线程Id</returns>
        public static int GetWindowThreadId(IntPtr hWnd) {
            int processId;
            return GetWindowThreadProcessId(hWnd, out processId);
        }
        /// <summary>
        /// 获取指定窗口的进程Id
        /// </summary>
        /// <param name="hWnd">窗口的句柄</param>
        /// <returns>返回所在进程Id</returns>
        public static int GetWindowProcessId(IntPtr hWnd) {
            int processId;
            GetWindowThreadProcessId(hWnd, out processId);
            return processId;
        }
        /// <summary>
        /// 将目标窗口设置为前台窗口。
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// 设置为活动窗口。
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        /// <summary>
        /// 对窗体的显示大小及状态的操作
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="nCmdShow">操作命令。</param>
        /// <returns>如果窗口之前可见，则返回值为非零。如果窗口之前被隐藏，则返回值为零。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ShowWindow(IntPtr hwnd, ShowWindowCommands nCmdShow);
        /// <summary>
        /// 窗体显示状态集
        /// </summary>
        /// <remarks>Windows CE：nCmdShow参数不支持下列值：SW_MAXIMINZE；SW_MINIMIZE；SW_RESTORE；SW_SHOWDEFAULT；SW_SHOWMAXIMIZED；SW_SHOWMINIMIZED；SW_SHOWMININOACTIVATE。</remarks>
        public enum ShowWindowCommands : int {
            /// <summary>
            /// 隐藏窗口并激活其他窗口。
            /// </summary>
            Hide = 0,
            /// <summary>
            /// 激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。应用程序在第一次显示窗口的时候应该指定此标志。
            /// </summary>
            ShowNormal = 1,
            /// <summary>
            /// 激活窗口并将其最小化。
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// 最大化指定的窗口。
            /// </summary>
            Maximize = 3,
            /// <summary>
            /// 激活窗口并将其最大化。
            /// </summary>
            ShowMaximized = 3,
            /// <summary>
            /// 以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// 在窗口原来的位置以原来的尺寸激活和显示窗口。
            /// </summary>
            Show = 5,
            /// <summary>
            /// 最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// 窗口最小化，激活窗口仍然维持激活状态。
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// 以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
            /// </summary>
            ShowNa = 8,
            /// <summary>
            /// 激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志。
            /// </summary>
            Restore = 9,
            /// <summary>
            /// 依据在STARTUPINFO结构中指定的SW_FLAG标志设定显示状态，STARTUPINFO 结构是由启动应用程序的程序传递给CreateProcess函数的。
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            /// 在WindowNT5.0中最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程最小化窗口时才使用这个参数。
            /// </summary>
            ShowForceMinimize = 11,
        }
        /// <summary>
        /// 关闭指定的窗口。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int CloseWindow(IntPtr hwnd);
        /// <summary>
        /// 判断窗口是否可见。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <returns>返回是否可见。</returns>
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);
        /// <summary>
        /// 关闭指定的窗口。
        /// </summary>
        /// <param name="hProcess">进程句柄。</param>
        /// <param name="uExitCode">退出代码</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int TerminateProcess(IntPtr hProcess, int uExitCode);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect {
            /// <summary>
            /// 最左坐标
            /// </summary>
            public int Left;
            /// <summary>
            /// 最上坐标
            /// </summary>
            public int Top;
            /// <summary>
            /// 最右坐标
            /// </summary>
            public int Right;
            /// <summary>
            /// 最下坐标
            /// </summary>
            public int Bottom;
            public int Width { get { return Right - Left; } }
            public int Height { get { return Bottom - Top; } }

            public override string ToString() {
                return string.Format("({0},{1})+{2}x{3}=({4},{5})", Left, Top, Width, Height, Right, Bottom);
            }
        }
        /// <summary>
        /// 获取窗体大小及位置
        /// </summary>
        /// <param name="hWnd">窗体句柄</param>
        /// <param name="lpRect"></param>
        /// <returns>返回是否成功。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        ///// <summary>
        ///// 触发
        ///// </summary>
        ///// <param name="flags"></param>
        ///// <param name="dx"></param>
        ///// <param name="dy"></param>
        ///// <param name="buttons"></param>
        ///// <param name="extraInfo"></param>
        //[DllImport("user32.dll", EntryPoint = "mouse_event")]
        //public static extern void MouseEvent(MouseEventFlag flags, int dx, int dy, int buttons, int extraInfo);
        //public static void MouseWheel() {
        //    MouseEvent(MouseEventFlag.Wheel, 0, 0, -100, 0);
        //}
        //[Flags]
        //public enum MouseEventFlag : uint {
        //    Move = 0x0001,
        //    LeftDown = 0x0002,
        //    LeftUp = 0x0004,
        //    RightDown = 0x0008,
        //    RightUp = 0x0010,
        //    MiddleDown = 0x0020,
        //    MiddleUp = 0x0040,
        //    XDown = 0x0080,
        //    XUp = 0x0100,
        //    Wheel = 0x0800,
        //    VirtualDesk = 0x4000,
        //    Absolute = 0x8000
        //}

        public const int WM_CLICK = 0x00F5;
        public const int WM_KEYDOWN = 0X100;
        public const int WM_KEYUP = 0X101;
        public const int WM_SYSCHAR = 0X106;
        public const int WM_SYSKEYUP = 0X105;
        public const int WM_SYSKEYDOWN = 0X104;
        public const int WM_CHAR = 0X102;

        /// <summary>
        /// 向指定句柄发送消息。
        /// </summary>
        /// <param name="hWnd">要操作的对象句柄。</param>
        /// <param name="Msg">消息</param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SendMessage_Mouse(IntPtr hWnd, int wMsg, IntPtr wParam, string lParam);
        /// <summary>
        /// 向指定句柄发送消息。
        /// </summary>
        /// <param name="hWnd">要操作的对象句柄。</param>
        /// <param name="Msg">消息</param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SendMessage_Mouse(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 发送鼠标点击消息。
        /// </summary>
        /// <param name="hWnd"></param>
        public static void MouseClickMessage(IntPtr hWnd) {
            SendMessage_Mouse(hWnd, WM_CLICK, IntPtr.Zero, "0");
        }
        /// <summary>
        /// 发送鼠标点击消息。
        /// </summary>
        /// <param name="hWnd"></param>
        public static void MouseClickMessage(IntPtr hWnd, int x, int y) {
            SendMessage_Mouse(hWnd, WM_CLICK, IntPtr.Zero, new IntPtr(x + (y << 16)));
        }
        private const int KEYEVENTF_KEYUP = 0x2;
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        public static void Keyboard_Down(VirtualKeys keys) {
            keybd_event((byte)keys, 0, 0, 0);
        }
        public static void Keyboard_Up(VirtualKeys keys) {
            keybd_event((byte)keys, 0, KEYEVENTF_KEYUP, 0);
        }
        /// <summary>
        /// 向指定句柄发送按键消息（ASCII）。
        /// </summary>
        /// <param name="hWnd">需要操作的句柄。</param>
        /// <param name="keyword">需要输入的文本。</param>
        public static void InputKeyword(IntPtr hWnd, string keyword) {
            if (string.IsNullOrEmpty(keyword))
                return;
            InputKeyword(hWnd, System.Text.Encoding.ASCII.GetBytes(keyword));
        }
        /// <summary>
        /// 向指定句柄发送按键消息。
        /// </summary>
        /// <param name="hWnd">需要操作的句柄。</param>
        /// <param name="keyword">需要输入的文本（已转码）。</param>
        public static void InputKeyword(IntPtr hWnd, byte[] keywords) {
            if (keywords == null || keywords.Length == 0)
                return;
            for (int i = 0; i < keywords.Length; i++) {
                SendMessage(hWnd, WM_CHAR, keywords[i], 0);
            }
        }
        /// <summary>
        /// 模拟鼠标事件
        /// </summary>
        /// <param name="dwFlags">标志位集，指定点击按钮和鼠标动作的多种情况。此参数里的各位可以是下列值的任何合理组合</param>
        /// <param name="dx">指定鼠标沿x轴的绝对位置或者从上次鼠标事件产生以来移动的数量，依赖于MOUSEEVENTF_ABSOLUTE的设置。给出的绝对数据作为鼠标的实际X坐标；给出的相对数据作为移动的mickeys数。一个mickey表示鼠标移动的数量，表明鼠标已经移动。</param>
        /// <param name="dy">指定鼠标沿y轴的绝对位置或者从上次鼠标事件产生以来移动的数量，依赖于MOUSEEVENTF_ABSOLUTE的设置。给出的绝对数据作为鼠标的实际y坐标，给出的相对数据作为移动的mickeys数。</param>
        /// <param name="cButtons">如果dwFlags为MOUSEEVENTF_WHEEL，则dwData指定鼠标轮移动的数量。正值表明鼠标轮向前转动，即远离用户的方向；负值表明鼠标轮向后转动，即朝向用户。一个轮击定义为WHEEL_DELTA，即120 如果dwFlagsS不是MOUSEEVENTF_WHEEL，则dWData应为零</param>
        /// <param name="dwExtraInfo">指定与鼠标事件相关的附加32位值。应用程序调用函数GetMessageExtraInfo来获得此附加信息。</param>
        /// <returns>无。</returns>
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int mouse_event(MouseEventFlags dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public static void MouseAbsoluteMove(int x, int y, System.Windows.Forms.Screen screen = null) {
            if (screen == null) {
                screen = System.Windows.Forms.Screen.PrimaryScreen;
            }
            mouse_event(Symbol.Win32.MouseEventFlags.Absolute | Symbol.Win32.MouseEventFlags.Move, x * 65536 / screen.Bounds.Width, y * 65536 / screen.Bounds.Height, 0, 0);
        }
        [DllImport("user32", EntryPoint = "WindowFromPoint")]
        public static extern IntPtr WindowFromPoint(int xPoint,int yPoint);  

        [Flags]
        public enum MouseEventFlags {
            /// <summary>
            /// 移动鼠标，表明发生移动。
            /// </summary>
            Move = 0x0001,
            /// <summary>
            /// 模拟鼠标左键按下
            /// </summary>
            LeftDown = 0x0002,
            /// <summary>
            /// 模拟鼠标左键抬起
            /// </summary>
            LeftUp = 0x0004,
            /// <summary>
            /// 模拟鼠标右键按下
            /// </summary>
            RightDown = 0x0008,
            /// <summary>
            /// 模拟鼠标右键抬起
            /// </summary>
            RightUp = 0x0010,
            /// <summary>
            /// 模拟鼠标中键按下
            /// </summary>
            MiddelDown = 0x0020,
            /// <summary>
            /// 模拟鼠标中键抬起
            /// </summary>
            MiddleUp = 0x0040,
            /// <summary>
            /// X键按下
            /// </summary>
            XDown = 0x0080,
            /// <summary>
            /// X键弹起
            /// </summary>
            XUp = 0x0100,
            /// <summary>
            /// 滚轮事件
            /// </summary>
            Wheel = 0x800,
            /// <summary>
            /// 未知
            /// </summary>
            VirtualDesk = 0x4000,
            /// <summary>
            /// 标示是否采用绝对坐标
            /// </summary>
            Absolute = 0x8000,
        }


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]string path,
            [MarshalAs(UnmanagedType.LPTStr)]StringBuilder shortPath,
            int shortPathLength);
        /// <summary>
        /// 获取DOS短文件路径
        /// </summary>
        /// <param name="path">完整的文件路径</param>
        /// <returns>返回短文件路径</returns>
        public static string GetShortPathName(string path) {
            StringBuilder shortPath = new StringBuilder(path == null ? 128 : path.Length * 2);
            int result = GetShortPathName(path, shortPath, shortPath.Capacity);
            return shortPath.ToString();
        }


        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)] //
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wparam, int lparam);
        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 获取Edit的内容。
        /// </summary>
        /// <param name="hWnd">Edit的句柄。</param>
        /// <returns>返回内容。</returns>
        public static string GetEditText(IntPtr hWnd) {
            // Get the size of the string required to hold the window title. 
            int size = (int)SendMessage(hWnd, WM_GETTEXTLENGTH, 0, 0);

            // If the return is 0, there is no title. 
            if (size > 0) {
                StringBuilder title = new StringBuilder(size + 1);

                SendMessage(hWnd, WM_GETTEXT, title.Capacity, title);
                return title.ToString();

            }
            return string.Empty;
        }
        [DllImport("user32", EntryPoint = "SetWindowLongA")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32", EntryPoint = "SetWindowLongA")]
        public static extern IntPtr SetWindowLong(IntPtr hwnd, int nIndex, WndCallback callback);
        //Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
        [DllImport("user32")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32")]
        public static extern int MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, int bRepaint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public delegate IntPtr WndCallback(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("shell32.dll", EntryPoint = "ExtractAssociatedIcon")]
        private static extern IntPtr ExtractAssociatedIconA(
            IntPtr hInst,
            [System.Runtime.InteropServices.MarshalAs(
                 System.Runtime.InteropServices.UnmanagedType.LPStr)] string lpIconPath,
            ref int lpiIcon);

        [System.Runtime.InteropServices.DllImport("shell32.dll", EntryPoint = "ExtractIcon")]
        private static extern IntPtr ExtractIconA(
            IntPtr hInst,
            [System.Runtime.InteropServices.MarshalAs(
                 System.Runtime.InteropServices.UnmanagedType.LPStr)] string lpszExeFileName,
            int nIconIndex);
        private static IntPtr hInst=IntPtr.Zero;

        public static System.Drawing.Icon ExtractIcon(string fileName, int index) {
            if (System.IO.File.Exists(fileName) || System.IO.Directory.Exists(fileName)) {
                System.IntPtr hIcon;

                // 文件所含图标的总数
                hIcon = ExtractIconA(hInst, fileName, -1);

                // 没取到的时候
                if (hIcon.Equals(IntPtr.Zero)) {
                    // 取得跟文件相关的图标
                    return ExtractAssociatedIcon(fileName);
                } else {
                    // 图标的总数
                    int numOfIcons = hIcon.ToInt32();

                    if (0 <= index && index < numOfIcons) {
                        hIcon = ExtractIconA(hInst, fileName, index);

                        if (!hIcon.Equals(IntPtr.Zero)) {
                            return System.Drawing.Icon.FromHandle(hIcon);
                        } else {
                            return null;
                        }
                    } else {
                        return null;
                    }
                }
            } else {
                return null;
            }
        }

        public static System.Drawing.Icon ExtractAssociatedIcon(string fileName) {
            if (System.IO.File.Exists(fileName) || System.IO.Directory.Exists(fileName)) {
                int i = 0;

                IntPtr hIcon = ExtractAssociatedIconA(hInst, fileName, ref i);

                if (!hIcon.Equals(IntPtr.Zero)) {
                    return System.Drawing.Icon.FromHandle(hIcon);
                } else {
                    return null;
                }
            } else {
                return null;
            }
        }
        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteDC(IntPtr hDC);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int Width, int Heigth);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd">Window to copy,Handle to the window that will be copied. </param>
        /// <param name="hdcBlt">HDC to print into,Handle to the device context. </param>
        /// <param name="nFlags">Optional flags,Specifies the drawing options. It can be one of the following values. </param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, UInt32 nFlags);

        /// <summary>
        /// 将窗口截图为图像对象
        /// </summary>
        /// <param name="hWnd">需要截图的窗口。</param>
        /// <returns>返回图像。</returns>
        public static System.Drawing.Bitmap PrintWindow(IntPtr hWnd) {
            IntPtr hscrdc = GetWindowDC(hWnd);

            Rect rect= new Rect();
            GetWindowRect(hWnd, ref rect);

            IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, rect.Width,rect.Height);
            IntPtr hmemdc = CreateCompatibleDC(hscrdc);

            SelectObject(hmemdc, hbitmap);
            PrintWindow(hWnd, hmemdc, 0);

            System.Drawing.Bitmap result = System.Drawing.Bitmap.FromHbitmap(hbitmap);

            DeleteDC(hscrdc);
            DeleteDC(hmemdc);

            return result;
        }
        [DllImport("Gdi32.dll")]
        public extern static int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        public static System.Drawing.Bitmap CopyFromScreen(int x, int y, int width = -1, int height = -1, System.Windows.Forms.Screen screen = null) {
            if (screen == null) {
                screen = System.Windows.Forms.Screen.PrimaryScreen;
            }
            int widthX = screen.Bounds.Width;
            int heightX = screen.Bounds.Height;
            if (width == -1)
                width = widthX - x;
            if (height == -1)
                height = heightX - y;
            System.Drawing.Bitmap result = new System.Drawing.Bitmap(width, height);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(result)) {
                g.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(width, height), System.Drawing.CopyPixelOperation.SourceCopy);
            }

            return result;

        }
        public static System.Drawing.Bitmap CopyFromScreeBitBlt(int x, int y, int width=-1,int height=-1, System.Windows.Forms.Screen screen = null) {
            if (screen == null) {
                screen = System.Windows.Forms.Screen.PrimaryScreen;
            }
            int widthX = screen.Bounds.Width;
            int heightX = screen.Bounds.Height;
            if (width == -1)
                width = widthX - x;
            if (height == -1)
                height = heightX - y;

            System.Drawing.Bitmap result = new System.Drawing.Bitmap(width, height);
            using (System.Drawing.Graphics gDest = System.Drawing.Graphics.FromImage(result)) {
                System.Drawing.Graphics gSrc = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
                IntPtr hSrcDC = gSrc.GetHdc();
                IntPtr hDC = gDest.GetHdc();
                int retval = BitBlt(hDC, 0, 0, width, height, hSrcDC, x, y, (int)(System.Drawing.CopyPixelOperation.SourceCopy | System.Drawing.CopyPixelOperation.CaptureBlt));
                gDest.ReleaseHdc();
                gSrc.ReleaseHdc();
            }
            return result;
        }


        //public static extern int GetClassName(IntPtr hWnd, out STRINGBUFFER ClassName, int nMaxCount);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder ClassName, int nMaxCount);
        public static string GetClassName(int hWnd) {
            return GetClassName((IntPtr)hWnd);
        }
        public static string GetClassName(IntPtr hWnd) {
            StringBuilder buffer = new StringBuilder(512);
            int i=GetClassName(hWnd, buffer, 512);
            if (i > 0)
                return buffer.ToString();
            return null;
        }
        #region STRINGBUFFER
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct STRINGBUFFER {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string szText;
        }
        #endregion
        /// <summary>
        /// 虚拟按键集 VK codes
        /// </summary>
        public enum VirtualKeys : int {

            #region mouse
            /// <summary>
            /// 鼠标移动
            /// </summary>
            MouseMove = 0x0001,
            /// <summary>
            /// 鼠标左键按下
            /// </summary>
            MouseLeftDown = 0x0002,
            /// <summary>
            /// 鼠标左键弹起
            /// </summary>
            MouseLeftUp = 0x0004,
            /// <summary>
            /// 鼠标右键按下
            /// </summary>
            MouseRightDown = 0x0008,
            /// <summary>
            /// 鼠标右键弹起
            /// </summary>
            MouseRightUp = 0x0010,
            /// <summary>
            /// 鼠标中键按下
            /// </summary>
            MouseMiddleDown = 0x0020,
            /// <summary>
            /// 鼠标中键弹起
            /// </summary>
            MouseMiddleUp = 0x0040,
            /// <summary>
            /// 鼠标滚轮
            /// </summary>
            MouseWheel = 0x0800,
            /// <summary>
            /// X键按下
            /// </summary>
            XDown = 0x0080,
            /// <summary>
            /// X键弹起
            /// </summary>
            XUp = 0x0100,
            //keyboard stuff
            /// <summary>
            /// 鼠标左按钮
            /// </summary>
            LButton = 1,
            /// <summary>
            /// 鼠标右按钮
            /// </summary>
            RButton = 2,
            /// <summary>
            /// 鼠标中按钮（三个按钮的鼠标）。
            /// </summary>
            MButton = 4,
            /// <summary>
            /// 
            /// </summary>
            XButton1 = 5,
            /// <summary>
            /// 
            /// </summary>
            XButton2 = 6,
            #endregion


            /// <summary>
            /// Cancel 键。
            /// </summary>
            Cancel = 3,
            /// <summary>
            /// Backspace 键。
            /// </summary>
            Back = 8,
            Backspace = 8,
            Tab = 9,
            Clear = 12,
            Return = 13,
            /// <summary>
            /// Shift 键
            /// </summary>
            ShiftKey = 16,
            /// <summary>
            /// Ctrl 键
            /// </summary>
            ControlKey = 17,
            /// <summary>
            /// Alt 键
            /// </summary>
            Menu = 18,
            Pause = 19,
            /// <summary>
            /// Caps Lock 键
            /// </summary>
            Capital = 20,
            CapsLock = 20,
            /// <summary>
            /// Esc 键
            /// </summary>
            Escape = 27,
            Esc = 27,
            Space = 32,


            #region position
            /// <summary>
            /// Page Up 键
            /// </summary>
            Prior = 33,
            PageUp = 33,
            /// <summary>
            /// Page Down 键
            /// </summary>
            Next = 34,
            PageDown = 34,
            End = 35,
            Home = 36,
            Left = 37,
            Up = 38,
            Right = 39,
            Down = 40,
            #endregion


            Select = 41,
            Print = 42,
            Execute = 43,
            /// <summary>
            /// Print Screen 键
            /// </summary>
            Snapshot = 44,
            PrintScreen = 44,
            Insert = 45,
            Delete = 46,
            Help = 47,

            #region 0-9
            Num0 = 48, //0
            Num1 = 49, //1
            Num2 = 50, //2
            Num3 = 51, //3
            Num4 = 52, //4
            Num5 = 53, //5
            Num6 = 54, //6
            Num7 = 55, //7
            Num8 = 56, //8
            Num9 = 57, //9
            #endregion

            #region A-Z
            A = 65, //A
            B = 66, //B
            C = 67, //C
            D = 68, //D
            E = 69, //E
            F = 70, //F
            G = 71, //G
            H = 72, //H
            I = 73, //I
            J = 74, //J
            K = 75, //K
            L = 76, //L
            M = 77, //M
            N = 78, //N
            O = 79, //O
            P = 80, //P
            Q = 81, //Q
            R = 82, //R
            S = 83, //S
            T = 84, //T
            U = 85, //U
            V = 86, //V
            W = 87, //W
            X = 88, //X
            Y = 89, //Y
            Z = 90, //Z
            #endregion

            #region NumPad 0-9
            /// <summary>
            /// 小键盘0
            /// </summary>
            NumPad0 = 96, //0
            /// <summary>
            /// 小键盘1
            /// </summary>
            NumPad1 = 97, //1
            /// <summary>
            /// 小键盘2
            /// </summary>
            NumPad2 = 98, //2
            /// <summary>
            /// 小键盘3
            /// </summary>
            NumPad3 = 99, //3
            /// <summary>
            /// 小键盘4
            /// </summary>
            NumPad4 = 100, //4
            /// <summary>
            /// 小键盘5
            /// </summary>
            NumPad5 = 101, //5
            /// <summary>
            /// 小键盘6
            /// </summary>
            NumPad6 = 102, //6
            /// <summary>
            /// 小键盘7
            /// </summary>
            NumPad7 = 103, //7
            /// <summary>
            /// 小键盘8
            /// </summary>
            NumPad8 = 104, //8
            /// <summary>
            /// 小键盘9
            /// </summary>
            NumPad9 = 105, //9
            #endregion

            /// <summary>
            /// *乘号键
            /// </summary>
            Multiply = 106,
            /// <summary>
            /// +加号键
            /// </summary>
            Add = 107,
            /// <summary>
            /// ;分隔符键
            /// </summary>
            Separator = 108,
            /// <summary>
            /// -减号键
            /// </summary>
            Subtract = 109,
            /// <summary>
            /// .句点键
            /// </summary>
            Decimal = 110,
            /// <summary>
            /// /除号键
            /// </summary>
            Divide = 111,

            #region F1-F12
            F1 = 112,
            F2 = 113,
            F3 = 114,
            F4 = 115,
            F5 = 116,
            F6 = 117,
            F7 = 118,
            F8 = 119,
            F9 = 120,
            F10 = 121,
            F11 = 122,
            F12 = 123,
            #endregion

            #region Lock
            /// <summary>
            /// 数字锁定
            /// </summary>
            NumLock = 144,
            /// <summary>
            /// Scroll Lock 键
            /// </summary>
            Scroll = 145,
            ScrollLock = 145,
            #endregion

            VirtualDesk = 0x4000,
            Absolute = 0x8000,

            /// <summary>
            /// 左 Windows 徽标键（Microsoft Natural Keyboard，人体工程学键盘）。
            /// </summary>
            LWin = 91,
            /// <summary>
            /// 右 Windows 徽标键（Microsoft Natural Keyboard，人体工程学键盘）。
            /// </summary>
            RWin = 92,
            /// <summary>
            /// 应用程序键（Microsoft Natural Keyboard，人体工程学键盘）。
            /// </summary>
            Apps = 93,
            /// <summary>
            /// 计算机睡眠键
            /// </summary>
            Sleep = 95,
            /// <summary>
            /// 左 Shift 键
            /// </summary>
            LShfitKey = 160,
            /// <summary>
            /// 右 Shift 键
            /// </summary>
            RShfitKey = 161,
            /// <summary>
            /// 左 Ctrl 键
            /// </summary>
            LControlKey = 162,
            /// <summary>
            /// 右 Ctrl 键
            /// </summary>
            RControlKey = 163,
            /// <summary>
            /// 左 Alt 键
            /// </summary>
            LMenu = 164,
            /// <summary>
            /// 右 Alt 键
            /// </summary>
            RMenu = 165,

            #region browser
            /// <summary>
            /// 浏览器后退键（Windows 2000 或更高版本）。 
            /// </summary>
            BrowserBack = 166,
            /// <summary>
            /// 浏览器前进键（Windows 2000 或更高版本）。 
            /// </summary>
            BrowserForward = 167,
            /// <summary>
            /// 浏览器刷新键（Windows 2000 或更高版本）。 
            /// </summary>
            BrowserRefresh = 168,
            /// <summary>
            /// 浏览器停止键（Windows 2000 或更高版本）。 
            /// </summary>
            BrowserStop = 169,
            /// <summary>
            /// 浏览器搜索键（Windows 2000 或更高版本）。 
            /// </summary>
            BrowserSearch = 170,
            /// <summary>
            /// 浏览器收藏夹键（Windows 2000 或更高版本）。 
            /// </summary>
            BrowserFavorites = 171,
            /// <summary>
            /// 浏览器主页键（Windows 2000 或更高版本）。 
            /// </summary>
            BrowserHome = 172,
            #endregion

            #region volume
            /// <summary>
            /// 静音键（Windows 2000 或更高版本）。 
            /// </summary>
            VolumeMute = 173,
            /// <summary>
            /// 减小音量键（Windows 2000 或更高版本）。 
            /// </summary>
            VolumeDown = 174,
            /// <summary>
            /// 增大音量键（Windows 2000 或更高版本）。 
            /// </summary>
            VolumeUp = 175,
            #endregion

            #region media player
            /// <summary>
            /// 媒体下一曲目键（Windows 2000 或更高版本）
            /// </summary>
            MediaNextTrack = 176,
            /// <summary>
            /// 媒体上一曲目键（Windows 2000 或更高版本）。 
            /// </summary>
            MediaPreviousTrack = 177,
            /// <summary>
            /// 媒体停止键（Windows 2000 或更高版本）。 
            /// </summary>
            MediaStop = 178,
            /// <summary>
            /// 媒体播放暂停键（Windows 2000 或更高版本）。 
            /// </summary>
            MediaPlayPause = 179,
            #endregion

            /// <summary>
            /// 启动邮件键（Windows 2000 或更高版本）。 
            /// </summary>
            LaunchMail = 180,
            /// <summary>
            /// 选择媒体键（Windows 2000 或更高版本）。 
            /// </summary>
            SelectMedia = 181,

            /// <summary>
            /// 启动应用程序一键（Windows 2000 或更高版本）。 
            /// </summary>
            LaunchApplication1 = 182,
            /// <summary>
            /// 启动应用程序二键（Windows 2000 或更高版本）。 
            /// </summary>
            LaunchApplication2 = 183,

            /// <summary>
            /// Play 键
            /// </summary>
            Play = 250,
            /// <summary>
            /// Zoom 键
            /// </summary>
            Zoom = 251,
            LineFeed = 10,

            /// <summary>
            /// 没有按任何键
            /// </summary>
            None = 0,
            /// <summary>
            /// 从键值提取修饰符的位屏蔽
            /// </summary>
            Modifiers = 1,
            /// <summary>
            /// 从键值提取键代码的位屏蔽
            /// </summary>
            KeyCode = 65535,
            /// <summary>
            /// Shift 修改键
            /// </summary>
            Shift = 65536,
            /// <summary>
            /// Ctrl 修改键
            /// </summary>
            Control = 131072,
            /// <summary>
            /// Alt 修改键
            /// </summary>
            Alt = 262144,
        }
    }
}
