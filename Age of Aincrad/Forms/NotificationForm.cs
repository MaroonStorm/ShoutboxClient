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
    partial class XenNotification : Form
    {
        Timer animationTimer;

        int startPosX, startPosY;

        public XenNotification(string t, string i, string c)
        {
            InitializeComponent();

            TopMost = true;
            ShowInTaskbar = false;

            animationTimer = new Timer();
            animationTimer.Interval = 5;
            animationTimer.Tick += animationTimer_Tick;

            lTitleText.Text = t;
            lNotificationText.Text = i;
            lNotificationContent.Text = c;
        }

        protected override void OnLoad(EventArgs e)
        {
            startPosX = Screen.PrimaryScreen.WorkingArea.Width;
            startPosY = Screen.PrimaryScreen.WorkingArea.Height - Height - 32;

            SetDesktopLocation(startPosX, startPosY);
            base.OnLoad(e);

            animationTimer.Start();
            closeTimer.Start();
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        void animationTimer_Tick(object sender, EventArgs e)
        {
            startPosX -= 20;

            if (startPosX < Screen.PrimaryScreen.WorkingArea.Width - Width - 32)
            {
                animationTimer.Stop();
            }
            else
            {
                SetDesktopLocation(startPosX, startPosY);
            }
        }
    }
}
