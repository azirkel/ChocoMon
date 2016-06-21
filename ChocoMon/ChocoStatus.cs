using System;
using System.Windows.Forms;

namespace ChocoMon
{
    public partial class ChocoStatus : Form
    {
        // This delegate enables asynchronous calls from another thread
        delegate void SetStatusCallback(string statusMessage, int progress);

        public ChocoStatus()
        {
            InitializeComponent();
        }

        private void ChocoStatus_Load(object sender, EventArgs e)
        {

        }

        public void SetStatus(string statusMessage, int progress)
        {
            if (labelStatus.InvokeRequired)
            {
                SetStatusCallback d = new SetStatusCallback(SetStatus);
                Invoke(d, new object[] { statusMessage, progress });
            }
            else
            {
                labelStatus.Text = statusMessage;
                progressBar1.Value = progress;
                ShowMe();
            }
        }

        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
