using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XenForms
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Simply logs a string to the "console"
        /// </summary>
        public void Log(string m)
        {
            lbDebug.Items.Add(m);

            int visibleItems = lbDebug.ClientSize.Height / lbDebug.ItemHeight;
            lbDebug.TopIndex = Math.Max(lbDebug.Items.Count - visibleItems + 1, 0);
        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
