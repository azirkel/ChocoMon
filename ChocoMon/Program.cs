using System;
using System.Windows.Forms;
using ProcessManager;
using System.IO;
// for Path
using System.Diagnostics;
using System.Runtime.InteropServices;
// for DllImportAttribute

namespace ChocoMon
{

    static class Program
    {
        /// <summary>
        /// Framework for restricting app to a single instance and for running as a tray app.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Debug.WriteLine("Hello!");
            Console.WriteLine("Hello!");
            Proc existingInstance = new Proc(Path.GetFileNameWithoutExtension(Application.ExecutablePath));
            //why is this reverse logic?
            if (existingInstance.isRunning()) // just me, so run!
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CustomApplicationContext());
            }
            else // switch to the first instance and exit
            {
                existingInstance.activate();
            }
        }
    }
}
