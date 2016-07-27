/*
 MIT License

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

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
