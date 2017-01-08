namespace XenForms
{
    partial class ShoutboxInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShoutboxInterface));
            this.lbMessages = new System.Windows.Forms.ListBox();
            this.tbMessage = new MetroFramework.Controls.MetroTextBox();
            this.bSend = new MetroFramework.Controls.MetroButton();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.messagesTimer = new System.Windows.Forms.Timer(this.components);
            this.cmOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openShoutboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNotificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.niTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.bOpenDebug = new MetroFramework.Controls.MetroButton();
            this.bExit = new MetroFramework.Controls.MetroButton();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.lbNotifications = new System.Windows.Forms.ListBox();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mlAllUsers = new MetroFramework.Controls.MetroLabel();
            this.lbFollowed = new System.Windows.Forms.ListBox();
            this.cmOptions.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMessages
            // 
            this.lbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessages.FormattingEnabled = true;
            this.lbMessages.ItemHeight = 20;
            this.lbMessages.Location = new System.Drawing.Point(3, 3);
            this.lbMessages.Name = "lbMessages";
            this.lbMessages.Size = new System.Drawing.Size(1180, 504);
            this.lbMessages.TabIndex = 0;
            this.lbMessages.DoubleClick += new System.EventHandler(this.lbMessages_DoubleClick);
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.Location = new System.Drawing.Point(3, 520);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(1130, 26);
            this.tbMessage.TabIndex = 1;
            this.tbMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMessage_KeyDown);
            // 
            // bSend
            // 
            this.bSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSend.Location = new System.Drawing.Point(1139, 520);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(44, 26);
            this.bSend.TabIndex = 2;
            this.bSend.Text = ">";
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // lbUsers
            // 
            this.lbUsers.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.ItemHeight = 20;
            this.lbUsers.Location = new System.Drawing.Point(0, 0);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(447, 549);
            this.lbUsers.TabIndex = 3;
            this.lbUsers.DoubleClick += new System.EventHandler(this.lbUsers_DoubleClick);
            // 
            // messagesTimer
            // 
            this.messagesTimer.Enabled = true;
            this.messagesTimer.Interval = 5000;
            this.messagesTimer.Tick += new System.EventHandler(this.messagesTimer_Tick);
            // 
            // cmOptions
            // 
            this.cmOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openShoutboxToolStripMenuItem,
            this.openDebugToolStripMenuItem,
            this.openNotificationsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.cmOptions.Name = "cmOptions";
            this.cmOptions.Size = new System.Drawing.Size(157, 92);
            this.cmOptions.Text = "Menu";
            // 
            // openShoutboxToolStripMenuItem
            // 
            this.openShoutboxToolStripMenuItem.Name = "openShoutboxToolStripMenuItem";
            this.openShoutboxToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.openShoutboxToolStripMenuItem.Text = "Open Shoutbox";
            this.openShoutboxToolStripMenuItem.Click += new System.EventHandler(this.openShoutboxToolStripMenuItem_Click);
            // 
            // openDebugToolStripMenuItem
            // 
            this.openDebugToolStripMenuItem.Name = "openDebugToolStripMenuItem";
            this.openDebugToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.openDebugToolStripMenuItem.Text = "Open Debug";
            this.openDebugToolStripMenuItem.Click += new System.EventHandler(this.openDebugToolStripMenuItem_Click);
            // 
            // openNotificationsToolStripMenuItem
            // 
            this.openNotificationsToolStripMenuItem.Name = "openNotificationsToolStripMenuItem";
            this.openNotificationsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_2);
            // 
            // niTrayIcon
            // 
            this.niTrayIcon.BalloonTipText = "Listening for messages...";
            this.niTrayIcon.BalloonTipTitle = "Shoutbox";
            this.niTrayIcon.ContextMenuStrip = this.cmOptions;
            this.niTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("niTrayIcon.Icon")));
            this.niTrayIcon.Text = "Age of Aincrad - Shoutbox";
            this.niTrayIcon.Visible = true;
            // 
            // bOpenDebug
            // 
            this.bOpenDebug.Location = new System.Drawing.Point(3, 3);
            this.bOpenDebug.Name = "bOpenDebug";
            this.bOpenDebug.Size = new System.Drawing.Size(128, 64);
            this.bOpenDebug.TabIndex = 5;
            this.bOpenDebug.Text = "Debug";
            this.bOpenDebug.Click += new System.EventHandler(this.openDebugToolStripMenuItem_Click);
            // 
            // bExit
            // 
            this.bExit.Location = new System.Drawing.Point(137, 3);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(128, 64);
            this.bExit.TabIndex = 6;
            this.bExit.Text = "Exit";
            this.bExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_2);
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage4);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(20, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(1194, 588);
            this.metroTabControl1.TabIndex = 8;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.tbMessage);
            this.metroTabPage1.Controls.Add(this.bSend);
            this.metroTabPage1.Controls.Add(this.lbMessages);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(1186, 549);
            this.metroTabPage1.TabIndex = 2;
            this.metroTabPage1.Text = "Shoutbox Home";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.bExit);
            this.metroTabPage3.Controls.Add(this.bOpenDebug);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(1186, 549);
            this.metroTabPage3.TabIndex = 4;
            this.metroTabPage3.Text = "Settings";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.lbNotifications);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1186, 549);
            this.metroTabPage2.TabIndex = 3;
            this.metroTabPage2.Text = "Notifications Centre";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // lbNotifications
            // 
            this.lbNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNotifications.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNotifications.FormattingEnabled = true;
            this.lbNotifications.ItemHeight = 20;
            this.lbNotifications.Location = new System.Drawing.Point(3, 3);
            this.lbNotifications.Name = "lbNotifications";
            this.lbNotifications.Size = new System.Drawing.Size(1180, 527);
            this.lbNotifications.TabIndex = 2;
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.Controls.Add(this.metroLabel1);
            this.metroTabPage4.Controls.Add(this.mlAllUsers);
            this.metroTabPage4.Controls.Add(this.lbUsers);
            this.metroTabPage4.Controls.Add(this.lbFollowed);
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(1186, 549);
            this.metroTabPage4.TabIndex = 5;
            this.metroTabPage4.Text = "Online Users";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(617, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(122, 25);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Friends Online";
            // 
            // mlAllUsers
            // 
            this.mlAllUsers.AutoSize = true;
            this.mlAllUsers.Dock = System.Windows.Forms.DockStyle.Left;
            this.mlAllUsers.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlAllUsers.Location = new System.Drawing.Point(447, 0);
            this.mlAllUsers.Name = "mlAllUsers";
            this.mlAllUsers.Size = new System.Drawing.Size(139, 25);
            this.mlAllUsers.TabIndex = 4;
            this.mlAllUsers.Text = "Members Online";
            // 
            // lbFollowed
            // 
            this.lbFollowed.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbFollowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFollowed.FormattingEnabled = true;
            this.lbFollowed.ItemHeight = 20;
            this.lbFollowed.Location = new System.Drawing.Point(739, 0);
            this.lbFollowed.Name = "lbFollowed";
            this.lbFollowed.Size = new System.Drawing.Size(447, 549);
            this.lbFollowed.TabIndex = 0;
            // 
            // ShoutboxInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 668);
            this.Controls.Add(this.metroTabControl1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1234, 668);
            this.Name = "ShoutboxInterface";
            this.Text = "Age of Aincrad - Shoutbox Interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShoutboxInterface_FormClosing);
            this.Move += new System.EventHandler(this.ShoutboxInterface_Move);
            this.cmOptions.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage4.ResumeLayout(false);
            this.metroTabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbMessages;
        private MetroFramework.Controls.MetroTextBox tbMessage;
        private MetroFramework.Controls.MetroButton bSend;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.Timer messagesTimer;
        private System.Windows.Forms.ContextMenuStrip cmOptions;
        private System.Windows.Forms.ToolStripMenuItem openShoutboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon niTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem openNotificationsToolStripMenuItem;
        private MetroFramework.Controls.MetroButton bOpenDebug;
        private MetroFramework.Controls.MetroButton bExit;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;
        private System.Windows.Forms.ListBox lbFollowed;
        private MetroFramework.Controls.MetroLabel mlAllUsers;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.ListBox lbNotifications;
    }
}