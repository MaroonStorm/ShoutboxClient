using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using XenManagers;
using XenData;
using System.Data.SQLite;

namespace XenForms
{
    public partial class LoginForm : Form
    {
        private DebugForm debugForm = new DebugForm();
        private DatabaseManager databaseManager = new DatabaseManager();
        private ConnectionManager forumConnector = new ConnectionManager("age-of-aincrd.com", "/forum", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36");

        private static DataTable queryData;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            databaseManager.Connect("Master.db");

            queryData = databaseManager.Select("SELECT * FROM UserSettings LIMIT 1", null);

            if(queryData.Rows.Count > 0)
            {
                if (queryData.Rows[0]["username"].ToString() != "")
                {
                    tUsername.Text = queryData.Rows[0]["username"].ToString();
                    cbSaveUsername.Checked = true;
                }
                if (queryData.Rows[0]["password"].ToString() != "")
                {
                    tPassword.Text = queryData.Rows[0]["password"].ToString();
                    cbSavePassword.Checked = true;
                }
            }

            debugForm.Log("Ready!");
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            bLogin.Enabled = false;

            if(!forumConnector.GetUserData().IsLoggedIn())
            {
                if (tUsername.Text != "" && tPassword.Text != "")
                {
                    bLogin.Text = "Logging in...";

                    try
                    {
                        if (forumConnector.Login(tUsername.Text, tPassword.Text))
                        {
                            debugForm.Log("Connected as: " + forumConnector.GetUserData().GetUsername());
                            debugForm.Log("Verifying login state...");

                            if(forumConnector.VerifyLogin())
                            {
                                debugForm.Log("Logged in.");

                                databaseManager.Update("UPDATE UserSettings SET username = @userName WHERE id = 1", new SQLiteParameter[] { new SQLiteParameter("userName", (cbSaveUsername.Checked ? tUsername.Text : "")) });
                                databaseManager.Update("UPDATE UserSettings SET password = @userPass WHERE id = 1", new SQLiteParameter[] { new SQLiteParameter("userPass", (cbSavePassword.Checked ? tPassword.Text : "")) });

                                ShoutboxInterface sbInterface = new ShoutboxInterface();
                                sbInterface.Configure(forumConnector, debugForm, databaseManager);
                                sbInterface.Show();

                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Could not login, either you use Two Factor Auth or your details are incorrect.");
                                debugForm.Log("Could not login.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Could not login, either you use Two Factor Auth or your details are incorrect.");
                            debugForm.Log("Could not login.");
                        }
                    }
                    catch (Exception ex)
                    {
                        debugForm.Log(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Could not login, please fill in all fields.");
                    debugForm.Log("Please fill in all fields.");
                }
            }
            else
            {
                debugForm.Log("You are logged in.");
            }

            bLogin.Text = "Login";
            bLogin.Enabled = true;
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
