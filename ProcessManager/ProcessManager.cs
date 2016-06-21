using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ProcessManager
{

    public class Proc
    {
        enum ShowWindowConstants : int {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11,
        };

        //instance variables
        private string name;
        private Process procptr;

        public Proc(string appProcessName)
        {
            name = appProcessName;
            procptr = getProc();
        }

        private void update()
        {
            procptr = getProc();
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private Process getProc()
        {
            Process[] RunningProcesses = Process.GetProcessesByName(name);
            if (RunningProcesses.Length == 1)
            {
                Debug.Print("found %s", name);
                return RunningProcesses[0];
            }
            else return null;
        }

        public bool isRunning()
        {
            update();
            if (procptr == null) return false;
            else return true;
        }

        public bool activate()
        {
            update();
            if (procptr == null) return false;
            ShowWindowAsync(procptr.MainWindowHandle, (int)ShowWindowConstants.SW_SHOWMINIMIZED);
            ShowWindowAsync(procptr.MainWindowHandle, (int)ShowWindowConstants.SW_RESTORE);
            return true;
        }
    }
}
