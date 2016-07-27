using System;
using System.Windows.Forms;

namespace KeystrokEventGenerator
{
    public class KeystrokMessageFilter : IMessageFilter
    {

        #region Events

        /// <summary>
        /// Calls when user touch the up and ground pads.
        /// </summary>
        public event EventHandler<EventArgs> OnUp;

        /// <summary>
        /// Calls when user touch the down and ground pads.
        /// </summary>
        public event EventHandler<EventArgs> OnDown;

        /// <summary>
        /// Calls when user touch the left and ground pads.
        /// </summary>
        public event EventHandler<EventArgs> OnLeft;

        /// <summary>
        /// Calls when user touch the right and ground pads.
        /// </summary>
        public event EventHandler<EventArgs> OnRight;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public KeystrokMessageFilter()
        {
        
        }

        #endregion

        #region Implementation of IMessageFilter

        /// <summary>
        /// Filter of the key strokes.
        /// </summary>
        /// <param name="m">Message argument.</param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if ((m.Msg == 256 /*0x0100*/))
            {
                switch (((int)m.WParam) | ((int)Control.ModifierKeys))
                {
                    case KeyCombinations.UP:
                        this.OnUp?.Invoke(this, new EventArgs());

                        break;

                    case KeyCombinations.DOWN:
                        this.OnDown?.Invoke(this, new EventArgs());

                        break;

                    case KeyCombinations.LEFT:
                        this.OnLeft?.Invoke(this, new EventArgs());

                        break;

                    case KeyCombinations.RIGHT:
                        this.OnRight?.Invoke(this, new EventArgs());

                        break;

                        //This does not work. It seems you can only check single character along with CTRL and ALT.
                        //case (int)(Keys.Control | Keys.Alt | Keys.K | Keys.P):
                        //    MessageBox.Show("You pressed ctrl + alt + k + p");
                        //    break;
                }
            }
            return false;
        }

        #endregion

    }
}
