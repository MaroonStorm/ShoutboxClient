using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using XenData;
using XenManagers;

namespace ShoutboxClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private DebugWindow debugWindow = new DebugWindow();
        private DatabaseManager databaseManager = new DatabaseManager();
        private ConnectionManager forumConnector = new ConnectionManager("age-of-aincrd.com", "/forum", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36");

        private static DataTable queryData;

        public LoginWindow()
        {
            debugWindow.Show();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            databaseManager.Connect("Master.db");

            queryData = databaseManager.Select("SELECT * FROM UserSettings LIMIT 1", null);

            if (queryData.Rows.Count > 0)
            {
                if (queryData.Rows[0]["username"].ToString() != "")
                {
                    tUsername.Text = queryData.Rows[0]["username"].ToString();
                    cbSaveUsername.IsChecked = true;
                }
                if (queryData.Rows[0]["password"].ToString() != "")
                {
                    tPassword.Password = queryData.Rows[0]["password"].ToString();
                    cbSavePassword.IsChecked = true;
                }
            }

            debugWindow.Log("Ready!");
        }

        private async void bLogin_Click(object sender, RoutedEventArgs e)
        {
            bLogin.Content = "Logging in...";
            bLogin.IsEnabled = false;

            if (!forumConnector.GetUserData().IsLoggedIn() && tUsername.Text != "" && tPassword.Password != "")
            {
                try
                {
                    LoginState state = await forumConnector.Login(tUsername.Text, tPassword.Password, tTwoFactorAuth.Text);
                    switch(state)
                    {
                        case LoginState.STATE_SUCCESS:
                            debugWindow.Log("Connected as: " + forumConnector.GetUserData().GetUsername());
                            debugWindow.Log("Verifying login state...");

                            if (await forumConnector.VerifyLogin())
                            {
                                debugWindow.Log("Logged in.");

                                databaseManager.Update("UPDATE UserSettings SET username = @userName WHERE id = 1", new SQLiteParameter[] { new SQLiteParameter("userName", (cbSaveUsername.IsChecked.GetValueOrDefault() ? tUsername.Text : "")) });
                                databaseManager.Update("UPDATE UserSettings SET password = @userPass WHERE id = 1", new SQLiteParameter[] { new SQLiteParameter("userPass", (cbSavePassword.IsChecked.GetValueOrDefault() ? tPassword.Password : "")) });

                                ShoutboxWindow sbWindow = new ShoutboxWindow();
                                sbWindow.Configure(forumConnector, debugWindow, databaseManager);
                                sbWindow.Show();

                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Could not login, unable to verify login state.");
                                debugWindow.Log("Could not login.");
                            }
                            break;
                        case LoginState.STATE_FAILED:
                            MessageBox.Show("Could not login, your details are incorrect or there is no connection to the internet.");
                            debugWindow.Log("Could not login.");
                            break;
                        case LoginState.STATE_2FA:
                            MessageBox.Show("Two factor authentication required.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occured, check debug.log for more information.");
                    debugWindow.Log(ex.Message);
                }
            }
            else
            {
                debugWindow.Log("You are already logged in or fields are empty.");
            }

            bLogin.Content = "Login";
            bLogin.IsEnabled = true;
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
