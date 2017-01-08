namespace XenForms
{
    partial class XenNotification
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
            this.closeTimer = new System.Windows.Forms.Timer(this.components);
            this.lTitleText = new System.Windows.Forms.Label();
            this.lNotificationText = new System.Windows.Forms.Label();
            this.lNotificationContent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeTimer
            // 
            this.closeTimer.Interval = 7000;
            this.closeTimer.Tick += new System.EventHandler(this.closeTimer_Tick);
            // 
            // lTitleText
            // 
            this.lTitleText.AutoSize = true;
            this.lTitleText.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lTitleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitleText.ForeColor = System.Drawing.Color.White;
            this.lTitleText.Location = new System.Drawing.Point(13, 9);
            this.lTitleText.Name = "lTitleText";
            this.lTitleText.Size = new System.Drawing.Size(147, 25);
            this.lTitleText.TabIndex = 0;
            this.lTitleText.Text = "_TITLE_TEXT";
            // 
            // lNotificationText
            // 
            this.lNotificationText.AutoSize = true;
            this.lNotificationText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lNotificationText.ForeColor = System.Drawing.Color.White;
            this.lNotificationText.Location = new System.Drawing.Point(26, 36);
            this.lNotificationText.Name = "lNotificationText";
            this.lNotificationText.Size = new System.Drawing.Size(150, 16);
            this.lNotificationText.TabIndex = 1;
            this.lNotificationText.Text = "_NOTIFICATION_TEXT";
            // 
            // lNotificationContent
            // 
            this.lNotificationContent.AutoSize = true;
            this.lNotificationContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lNotificationContent.ForeColor = System.Drawing.Color.White;
            this.lNotificationContent.Location = new System.Drawing.Point(26, 55);
            this.lNotificationContent.Name = "lNotificationContent";
            this.lNotificationContent.Size = new System.Drawing.Size(181, 16);
            this.lNotificationContent.TabIndex = 2;
            this.lNotificationContent.Text = "_NOTIFICATION_CONTENT";
            // 
            // XenNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(360, 80);
            this.Controls.Add(this.lNotificationContent);
            this.Controls.Add(this.lNotificationText);
            this.Controls.Add(this.lTitleText);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "XenNotification";
            this.Text = "Alert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer closeTimer;
        private System.Windows.Forms.Label lTitleText;
        private System.Windows.Forms.Label lNotificationText;
        private System.Windows.Forms.Label lNotificationContent;
    }
}