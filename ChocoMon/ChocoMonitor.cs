using ProcessManager;
using System.Timers;
using System.Diagnostics;
using System.Windows.Forms;

namespace ChocoMon
{
    class ChocoMonitor
    {

        private static string chocoexe = "choco";
        private Proc proc = new Proc(chocoexe);
        private bool chocoStatusActivated = false;
        private bool chocoStatusClosed = false;
        ChocoStatus chocoStatus = null;
        public bool chocoRunning = false;

        public ChocoMonitor()
        {
        }

        public void start()
        {
            chocoMonitorLoop();
        }

        private void chocoMonitorLoop()
        {
            Debug.Print("creating timer");
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000; //In milliseconds here
            t.AutoReset = true; //Stops it from repeating
            t.Elapsed += new ElapsedEventHandler(chocoCheck);
            t.Start();
        }

        private void chocoCheck(object sender, ElapsedEventArgs e)
        {
            Debug.Print("running check for " + chocoexe);
            if (proc.isRunning())
            {
                Debug.Print(chocoexe + " found");
                if (!chocoStatusActivated)
                {
                    chocoStatusActivated = true;
                    showChocoStatus("Installing Updates", 99);
                }
            }
            else if (chocoStatusActivated)
            {
                chocoStatusActivated = false;
                showChocoStatus("Updates Complete", 100);
            }
        } 

        public void showChocoStatus(string statusMessage, int progress) //http://stackoverflow.com/questions/16752960/activate-another-form
        {
            Debug.Print("showing chocoStatus");
            if (chocoStatus == null || chocoStatus.IsDisposed)
            {
                chocoStatus = new ChocoStatus();
                chocoStatus.SetStatus(statusMessage,progress);
                chocoStatus.FormClosed += new FormClosedEventHandler(ChocoStatusFormClosedHandler);
                chocoStatus.ShowDialog();
                //chocoStatus.BringToFront();
            }
            else if (chocoStatusClosed)
            {
                chocoStatusClosed = false;
                chocoStatus.SetStatus(statusMessage, progress);
                chocoStatus.ShowDialog();
            }
            {
                chocoStatus.SetStatus(statusMessage, progress);
                //chocoStatus.Activate();
                //chocoStatus.BringToFront();
            }
        }

        private void ChocoStatusFormClosedHandler(object sender, FormClosedEventArgs e)
        {
            Debug.Print("chocoStatus Closed.");
            chocoStatusClosed = true;
        }
    }
}
