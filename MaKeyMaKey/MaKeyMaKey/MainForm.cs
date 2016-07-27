using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeystrokEventGenerator;

namespace MaKeyMaKey
{
    public partial class MainForm : Form
    {

        Timer elapseTimeCounter = new Timer();

        int countedTime = 0;

        /// <summary>
        /// Keystroke message filter handler.
        /// </summary>
        private KeystrokMessageFilter keyStrokeMessageFilter = new KeystrokMessageFilter();
        
        public MainForm()
        {
            InitializeComponent();

            this.ConfigureEvents();

            this.elapseTimeCounter.Stop();
            this.elapseTimeCounter.Tick += ElapseTimeCounter_Tick;
            this.elapseTimeCounter.Interval = 1;

            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (this.lblTime.InvokeRequired)
            {
                this.lblTime.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.lblTime.Text = String.Format("Time: {0:F2}", this.countedTime / 60.0F);
                });
            }
            else
            {
                this.lblTime.Text = String.Format("Time: {0:F2}", this.countedTime / 60.0F);
            }
        }

        private void ElapseTimeCounter_Tick(object sender, EventArgs e)
        {
            this.countedTime++;
        }

        #region Keboard controls

        private void ConfigureEvents()
        {
            // Attach keyboard strokes events.
            keyStrokeMessageFilter.OnUp += KeyStrokeMessageFilter_OnUp;
            keyStrokeMessageFilter.OnDown += KeyStrokeMessageFilter_OnDown;
            keyStrokeMessageFilter.OnLeft += KeyStrokeMessageFilter_OnLeft;
            keyStrokeMessageFilter.OnRight += KeyStrokeMessageFilter_OnRight;

            // 
            Application.AddMessageFilter(keyStrokeMessageFilter);
        }

        #endregion

        private void KeyStrokeMessageFilter_OnRight(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void KeyStrokeMessageFilter_OnLeft(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void KeyStrokeMessageFilter_OnDown(object sender, EventArgs e)
        {
            this.elapseTimeCounter.Stop();
        }

        private void KeyStrokeMessageFilter_OnUp(object sender, EventArgs e)
        {
            this.countedTime = 0;
            this.elapseTimeCounter.Start();
        }
    }
}
