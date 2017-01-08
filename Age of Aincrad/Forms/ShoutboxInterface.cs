using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Text.RegularExpressions;
using System.Timers;

using XenData;
using XenManagers;
using System.Data.SQLite;

namespace XenForms
{
    partial class ShoutboxInterface : MetroFramework.Forms.MetroForm
    {
        private ConnectionManager forumConnector;
        private DatabaseManager databaseManager;
        private DebugForm debugForm;

        private string messageColour = "#000000";

        private static DataTable queryData;

        private int lastMessageId = 0;

        public ShoutboxInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Simply setups the connector and uses it to fetch messages
        /// </summary>
        public void Configure(ConnectionManager connector, DebugForm debug, DatabaseManager database)
        {
            forumConnector = connector;
            debugForm = debug;
            databaseManager = database;

            debugForm.Log(forumConnector.VerifyLogin() ? "Connected." : "Disconnected.");

            queryData = databaseManager.Select("SELECT * FROM UserSettings WHERE 1 LIMIT 1;", null);
            if(queryData.Rows.Count > 0)
            {
                messageColour = queryData.Rows[0]["colour"].ToString();

                debugForm.Log("Set message colour to: " + messageColour);
            }

            lbMessages.Items.Add("Loading...");
            lbUsers.Items.Add("Loading...");
        }

        /// <summary>
        /// Don't use this to send messages, use forumConnector.SendMessage() to send a message manually
        /// This is used to wrap other statements and calls into one call
        /// </summary>
        private void SendMessage(KeyEventArgs e)
        {
            if (tbMessage.Text != "" && tbMessage.Text.Length < 400)
            {
                if(tbMessage.Text.StartsWith("#"))
                {
                    if(tbMessage.Text.StartsWith("#alert add "))
                    {
                        if(tbMessage.Text.Split(' ')[2] != "")
                        {
                            string final = "";
                            string[] phrase = tbMessage.Text.Split(' ');
                            phrase[0] = "";
                            phrase[1] = "";

                            foreach (string item in phrase)
                            {
                                if (item != "")
                                {
                                    final = final + item + " ";
                                }
                            }
                            final = final.Trim();

                            if (databaseManager.Select("SELECT * FROM NotifyKeywords WHERE value = @keyWord", new SQLiteParameter[] { new SQLiteParameter("keyWord", final) }).Rows.Count > 0)
                            {
                                debugForm.Log("Keyword [" + final + "] already exists in database.");
                            }
                            else
                            {
                                debugForm.Log("Keyword [" + final + "] has been added.");
                                databaseManager.Insert("INSERT INTO NotifyKeywords(value) VALUES(@keyWord)", new SQLiteParameter[] { new SQLiteParameter("keyWord", final) });
                            }
                        }
                        else
                        {
                            debugForm.Log("Keyword cannot be blank.");
                        }
                    }
                    else if (tbMessage.Text.StartsWith("#alert del"))
                    {
                        if (tbMessage.Text.Split(' ')[2] != "")
                        {
                            string final = "";
                            string[] phrase = tbMessage.Text.Split(' ');
                            phrase[0] = "";
                            phrase[1] = "";

                            foreach (string item in phrase)
                            {
                                if(item != "")
                                {
                                    final = final + item + " ";
                                }
                            }

                            if (databaseManager.Select("SELECT * FROM NotifyKeywords WHERE value = @keyWord", new SQLiteParameter[] { new SQLiteParameter("keyWord", final) }).Rows.Count > 0)
                            {
                                debugForm.Log("Keyword [" + final + "] has been deleted.");
                                databaseManager.Insert("DELETE FROM NotifyKeywords WHERE value = @keyWord", new SQLiteParameter[] { new SQLiteParameter("keyWord", final) });
                            }
                            else
                            {
                                debugForm.Log("Keyword [" + final + "] does not exist in database.");
                            }
                        }
                        else
                        {
                            debugForm.Log("Keyword cannot be blank.");
                        }
                    }
                    else if (tbMessage.Text.StartsWith("#alert list"))
                    {
                        queryData = databaseManager.Select("SELECT * FROM NotifyKeywords WHERE 1", null);

                        if (queryData.Rows.Count > 0)
                        {
                            debugForm.Log("------------- ALERT KEYWORDS -------------");
                            foreach (DataRow r in queryData.Rows)
                            {
                                debugForm.Log(r["value"].ToString());
                            }
                            debugForm.Log("------------- ALERT KEYWORDS -------------");
                        }
                        else
                        {
                            debugForm.Log("No keywords in database.");
                        }
                    }
                    else if (tbMessage.Text.StartsWith("#member follow"))
                    {
                        if (tbMessage.Text.Split(' ')[2] != "")
                        {
                            string final = "";
                            string[] phrase = tbMessage.Text.Split(' ');
                            phrase[0] = "";
                            phrase[1] = "";

                            foreach (string item in phrase)
                            {
                                if (item != "")
                                {
                                    final = final + item + " ";
                                }
                            }
                            final = final.Trim();

                            if (databaseManager.Select("SELECT * FROM NotifyUsers WHERE value = @memberName", new SQLiteParameter[] { new SQLiteParameter("memberName", final) }).Rows.Count > 0)
                            {
                                debugForm.Log("Member [" + final + "] already exists in database.");
                            }
                            else
                            {
                                debugForm.Log("Member [" + final + "] has been added.");
                                databaseManager.Insert("INSERT INTO NotifyUsers(value) VALUES(@memberName)", new SQLiteParameter[] { new SQLiteParameter("memberName", final) });
                            }
                        }
                        else
                        {
                            debugForm.Log("Member cannot be blank.");
                        }
                    }
                    else if (tbMessage.Text.StartsWith("#member unfollow"))
                    {
                        if (tbMessage.Text.Split(' ')[2] != "")
                        {
                            string final = "";
                            string[] phrase = tbMessage.Text.Split(' ');
                            phrase[0] = "";
                            phrase[1] = "";

                            foreach (string item in phrase)
                            {
                                if (item != "")
                                {
                                    final = final + item + " ";
                                }
                            }
                            final = final.Trim();

                            if (databaseManager.Select("SELECT * FROM NotifyUsers WHERE value = @memberName", new SQLiteParameter[] { new SQLiteParameter("memberName", final) }).Rows.Count > 0)
                            {
                                debugForm.Log("Member [" + final + "] has been deleted.");
                                databaseManager.Insert("DELETE FROM NotifyUsers WHERE value = @memberName", new SQLiteParameter[] { new SQLiteParameter("memberName", final) });
                            }
                            else
                            {
                                debugForm.Log("Member [" + final + "] does not exist in database.");
                            }
                        }
                        else
                        {
                            debugForm.Log("Keyword cannot be blank.");
                        }
                    }
                    else
                    {
                        debugForm.Log("Unknown command.");
                    }
                }
                else
                {
                    debugForm.Log(forumConnector.SendMessage(tbMessage.Text, messageColour, false));
                    debugForm.Log("Message has been sent.");

                    GetMessages();
                }

                tbMessage.Text = "";
            }
            else
            {
                debugForm.Log("Message was not sent, empty message or too long.");
            }
        }

        /// <summary>
        /// This is what gets and displays the messages.
        /// Here is where you can add your own stuff like alert calls, etc
        /// </summary>
        private void GetMessages()
        {
            foreach (FormattedMessage m in forumConnector.GetShoutboxData())
            {
                string mText = Regex.Replace(m.GetMessage(), "<.*?>", String.Empty);
                mText = HttpUtility.HtmlDecode(mText);

                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(m.GetTime()).ToLocalTime();

                // So, this is where you can add checks for messages with values and such
                if (lastMessageId < forumConnector.GetLastMessageID())
                {
                    debugForm.Log("Checking message for mentioned...");
                    queryData = databaseManager.Select("SELECT * FROM NotifyKeywords WHERE 1", null);

                    if(queryData.Rows.Count > 0)
                    {
                        foreach (DataRow r in queryData.Rows)
                        {
                            if(mText.ToLower().Contains(r["value"].ToString().ToLower()))
                            {
                                XenNotification loadNotification = new XenNotification("Shoutbox", m.GetName(), mText);
                                loadNotification.Show();

                                lbNotifications.Items.Add(m.GetName() + " has mentioned a word you want alerts for:");
                                lbNotifications.Items.Add("    Message " + (m.IsWhisper() ? "(PM)" : "") + ": " + mText);
                                lbNotifications.Items.Add("    Date: " + dtDateTime.ToShortTimeString());

                                break;
                            }
                        }
                    }
                    //if(mText.ToLower().Contains("xenowarrior") || mText.ToLower().Contains("xeno") || mText.ToLower().Contains("xeno warrior") || mText.ToLower().Contains("ashley") || mText.ToLower().Contains("ashrey") || mText.ToLower().Contains("ash") || m.IsWhisper())
                }

                lastMessageId = m.GetID();
                debugForm.Log("Added message: " + lastMessageId.ToString());
                lbMessages.Items.Add("[" + dtDateTime.ToShortTimeString() + "] " + m.GetName() + ": " + mText);
            }

            debugForm.Log("Done, last message ID is " + lastMessageId.ToString());
            lastMessageId = forumConnector.GetLastMessageID();

            int visibleItems = lbMessages.ClientSize.Height / lbMessages.ItemHeight;
            lbMessages.TopIndex = Math.Max(lbMessages.Items.Count - visibleItems + 1, 0);
        }

        /// <summary>
        /// This just updates the member list
        /// You could also add a feature here to alert you when a member becomes active
        /// </summary>
        private void GetMembers()
        {
            lbFollowed.Items.Clear();
            lbUsers.Items.Clear();
            lbUsers.Items.Add("Total: " + forumConnector.GetUsers().Count);

            foreach (string u in forumConnector.GetUsers())
            {
                lbUsers.Items.Add(u.Trim());

                if(databaseManager.Select("SELECT * FROM NotifyUsers WHERE value = @memberName", new SQLiteParameter[] { new SQLiteParameter("@memberName", u.Trim()) }).Rows.Count > 0)
                {
                    lbFollowed.Items.Add(u.Trim());
                }
            }
        }

        /// <summary>
        /// Runs every X amount of time (set in the messageTimer properties on the form design view)
        /// </summary>
        private void messagesTimer_Tick(object sender, EventArgs e)
        {
            debugForm.Log("[Timer] Updating messages...");
            GetMessages();

            debugForm.Log("[Timer] Updating members list...");
            GetMembers();

            messagesTimer.Start();
        }

        /// <summary>
        /// Handle button press to send a message
        /// </summary>
        private void bSend_Click(object sender, EventArgs e)
        {
            SendMessage(null);
        }

        /// <summary>
        /// Handle the ENTER key press and stop it making a sound
        /// </summary>
        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendMessage(e);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// When this main form closes, also exit the application to close all other forms
        /// </summary>
        private void ShoutboxInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShoutboxInterface_Move(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void openShoutboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void openDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debugForm.Show();
            debugForm.WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbMessages_DoubleClick(object sender, EventArgs e)
        {
            Regex linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = linkParser.Matches(lbMessages.Items[lbMessages.SelectedIndex].ToString());

            if (matches.Count > 0)
            {
                XenNotification loadNotification = new XenNotification("Information", "Link opened.", matches[0].ToString());
                loadNotification.Show();

                System.Diagnostics.Process.Start(matches[0].ToString());
            }
            else
            {
                XenNotification loadNotification = new XenNotification("Information", "Text copied.", lbMessages.Items[lbMessages.SelectedIndex].ToString());
                loadNotification.Show();

                Clipboard.SetText(lbMessages.Items[lbMessages.SelectedIndex].ToString());
            }
        }

        private void lbUsers_DoubleClick(object sender, EventArgs e)
        {
            tbMessage.Text = "/pm " + lbUsers.Items[lbUsers.SelectedIndex].ToString() + " ";
            
            tbMessage.Focus();
        }
    }
}
