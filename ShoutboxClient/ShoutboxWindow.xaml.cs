using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Web;

using HtmlAgilityPack;

using XenData;
using XenManagers;

namespace ShoutboxClient
{
    /// <summary>
    /// Interaction logic for ShoutboxWindow.xaml
    /// </summary>
    public partial class ShoutboxWindow : Window
    {
        private ConnectionManager forumConnector;
        private DatabaseManager databaseManager;
        private DebugWindow debugWindow;

        private string messageColour = "#000000";
        private static DataTable queryData;
        private int lastMessageId = 0;

        DispatcherTimer messagesTimer = new DispatcherTimer();

        public ShoutboxWindow()
        {
            messagesTimer.Tick += messagesTimer_Tick;
            messagesTimer.Interval = new TimeSpan(0, 0, 5);
            messagesTimer.Start();

            InitializeComponent();
        }

        public void Configure(ConnectionManager connector, DebugWindow debug, DatabaseManager database)
        {
            forumConnector = connector;
            debugWindow = debug;
            databaseManager = database;

            debugWindow.Log(forumConnector.VerifyLogin() ? "Connected." : "Disconnected.");

            queryData = databaseManager.Select("SELECT * FROM UserSettings WHERE 1 LIMIT 1;", null);
            if (queryData.Rows.Count > 0)
            {
                messageColour = queryData.Rows[0]["colour"].ToString();

                debugWindow.Log("Set message colour to: " + messageColour);
            }

            lbMessages.Items.Add("Loading...");
            lbUsers.Items.Add("Loading...");
        }

        private void SendMessage(KeyEventArgs e)
        {
            if (tbMessage.Text != "" && tbMessage.Text.Length < 400)
            {
                if (tbMessage.Text.StartsWith("#"))
                {
                    if (tbMessage.Text.StartsWith("#alert add "))
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

                            if (databaseManager.Select("SELECT * FROM NotifyKeywords WHERE value = @keyWord", new SQLiteParameter[] { new SQLiteParameter("keyWord", final) }).Rows.Count > 0)
                            {
                                debugWindow.Log("Keyword [" + final + "] already exists in database.");
                            }
                            else
                            {
                                debugWindow.Log("Keyword [" + final + "] has been added.");
                                databaseManager.Insert("INSERT INTO NotifyKeywords(value) VALUES(@keyWord)", new SQLiteParameter[] { new SQLiteParameter("keyWord", final) });
                            }
                        }
                        else
                        {
                            debugWindow.Log("Keyword cannot be blank.");
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
                                if (item != "")
                                {
                                    final = final + item + " ";
                                }
                            }

                            if (databaseManager.Select("SELECT * FROM NotifyKeywords WHERE value = @keyWord", new SQLiteParameter[] { new SQLiteParameter("keyWord", final) }).Rows.Count > 0)
                            {
                                debugWindow.Log("Keyword [" + final + "] has been deleted.");
                                databaseManager.Insert("DELETE FROM NotifyKeywords WHERE value = @keyWord", new SQLiteParameter[] { new SQLiteParameter("keyWord", final) });
                            }
                            else
                            {
                                debugWindow.Log("Keyword [" + final + "] does not exist in database.");
                            }
                        }
                        else
                        {
                            debugWindow.Log("Keyword cannot be blank.");
                        }
                    }
                    else if (tbMessage.Text.StartsWith("#alert list"))
                    {
                        queryData = databaseManager.Select("SELECT * FROM NotifyKeywords WHERE 1", null);

                        if (queryData.Rows.Count > 0)
                        {
                            debugWindow.Log("------------- ALERT KEYWORDS -------------");
                            foreach (DataRow r in queryData.Rows)
                            {
                                debugWindow.Log(r["value"].ToString());
                            }
                            debugWindow.Log("------------- ALERT KEYWORDS -------------");
                        }
                        else
                        {
                            debugWindow.Log("No keywords in database.");
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
                                debugWindow.Log("Member [" + final + "] already exists in database.");
                            }
                            else
                            {
                                debugWindow.Log("Member [" + final + "] has been added.");
                                databaseManager.Insert("INSERT INTO NotifyUsers(value) VALUES(@memberName)", new SQLiteParameter[] { new SQLiteParameter("memberName", final) });
                            }
                        }
                        else
                        {
                            debugWindow.Log("Member cannot be blank.");
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
                                debugWindow.Log("Member [" + final + "] has been deleted.");
                                databaseManager.Insert("DELETE FROM NotifyUsers WHERE value = @memberName", new SQLiteParameter[] { new SQLiteParameter("memberName", final) });
                            }
                            else
                            {
                                debugWindow.Log("Member [" + final + "] does not exist in database.");
                            }
                        }
                        else
                        {
                            debugWindow.Log("Keyword cannot be blank.");
                        }
                    }
                    else
                    {
                        debugWindow.Log("Unknown command.");
                    }
                }
                else
                {
                    debugWindow.Log(forumConnector.SendMessage(tbMessage.Text, messageColour, false));
                    debugWindow.Log("Message has been sent.");

                    GetMessages();
                }

                tbMessage.Text = "";
            }
            else
            {
                debugWindow.Log("Message was not sent, empty message or too long.");
            }
        }

        private void GetMessages()
        {
            foreach (FormattedMessage m in forumConnector.GetShoutboxData())
            {
                string mText = Regex.Replace(m.GetMessage(), "<.*?>", String.Empty);
                mText = HttpUtility.HtmlDecode(mText);

                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(m.GetTime()).ToLocalTime();

                if (lastMessageId < forumConnector.GetLastMessageID())
                {
                    debugWindow.Log("Checking message for mentioned...");
                    queryData = databaseManager.Select("SELECT * FROM NotifyKeywords WHERE 1", null);

                    if (queryData.Rows.Count > 0)
                    {
                        foreach (DataRow r in queryData.Rows)
                        {
                            if (mText.ToLower().Contains(r["value"].ToString().ToLower()))
                            {
                                // TO-DO: SEND TOAST NOTIFICATIONS
                                //XenNotification loadNotification = new XenNotification("Shoutbox", m.GetName(), mText);
                                //loadNotification.Show();

                                lbNotifications.Items.Add(m.GetName() + " has mentioned a word you want alerts for:");
                                lbNotifications.Items.Add("    Message " + (m.IsWhisper() ? "(PM)" : "") + ": " + mText);
                                lbNotifications.Items.Add("    Date: " + dtDateTime.ToShortTimeString());

                                break;
                            }
                        }
                    }
                }

                lastMessageId = m.GetID();
                debugWindow.Log("Added message: " + lastMessageId.ToString());
                lbMessages.Items.Add("[" + dtDateTime.ToShortTimeString() + "] " + m.GetName() + ": " + mText);
            }

            debugWindow.Log("Done, last message ID is " + lastMessageId.ToString());
            lastMessageId = forumConnector.GetLastMessageID();

            // TO-DO: FIX THIS THING WHICH SCROLLS CHAT TO BOTTOM ON NEW MESSAGE
            //int visibleItems = lbMessages.ClientSize.Height / lbMessages.ItemHeight;
            //lbMessages.TopIndex = Math.Max(lbMessages.Items.Count - visibleItems + 1, 0);
        }

        private void GetMembers()
        {
            lbFollowed.Items.Clear();
            lbUsers.Items.Clear();
            lbUsers.Items.Add("Total: " + forumConnector.GetUsers().Count);

            foreach (string u in forumConnector.GetUsers())
            {
                lbUsers.Items.Add(u.Trim());

                if (databaseManager.Select("SELECT * FROM NotifyUsers WHERE value = @memberName", new SQLiteParameter[] { new SQLiteParameter("@memberName", u.Trim()) }).Rows.Count > 0)
                {
                    lbFollowed.Items.Add(u.Trim());
                }
            }
        }

        private void messagesTimer_Tick(object sender, EventArgs e)
        {
            debugWindow.Log("[Timer] Updating messages...");
            GetMessages();

            debugWindow.Log("[Timer] Updating members list...");
            GetMembers();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage(e);

                e.Handled = true;
            }
        }

        private void bSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void bExit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
