using System;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace XenManagers
{
    public class DatabaseManager
    {
        private SQLiteConnection MasterDatabase = new SQLiteConnection();
        private string key = "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";

        public bool Connect(string database)
        {
            try
            {
                if (!File.Exists(database))
                {
                    SQLiteConnection.CreateFile(database);

                    try
                    {
                        this.MasterDatabase = new SQLiteConnection("Data Source=" + database + ";Version=3;");
                        this.MasterDatabase.Open();

                        MasterDatabase.ChangePassword(key);
                        this.MasterDatabase.Close();
                    }
                    catch (Exception ex)
                    {
                        this.HandleError(ex);
                        return false;
                    }
                }

                try
                {
                    this.MasterDatabase = new SQLiteConnection("Data Source=" + database + ";Password=" + key + ";Version=3;");
                    this.MasterDatabase.Open();
                }
                catch (Exception ex)
                {
                    this.HandleError(ex);
                    return false;
                }

                Execute("CREATE TABLE IF NOT EXISTS UserSettings(id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT, password TEXT, colour TEXT);");
                Execute("CREATE TABLE IF NOT EXISTS NotifyKeywords(id INTEGER PRIMARY KEY AUTOINCREMENT, value TEXT);");
                Execute("CREATE TABLE IF NOT EXISTS NotifyUsers(id INTEGER PRIMARY KEY AUTOINCREMENT, value TEXT);");
                Execute("CREATE TABLE IF NOT EXISTS SavedQuotes(id INTEGER PRIMARY KEY AUTOINCREMENT, value TEXT);");
                Execute("CREATE TABLE IF NOT EXISTS SavedNotifications(id INTEGER PRIMARY KEY AUTOINCREMENT, value TEXT);");

                if (Select("SELECT * FROM UserSettings WHERE 1 LIMIT 1;", null).Rows.Count == 0)
                {
                    Insert("INSERT INTO UserSettings(username, password, colour) VALUES(@userName, @userPass, @userColour);", new SQLiteParameter[] { new SQLiteParameter("userName", ""), new SQLiteParameter("userPass", ""), new SQLiteParameter("userColour", "#000000") });
                }

                return true;
            }
            catch (Exception e)
            {
                this.HandleError(e);
                return false;
            }
        }

        public void Execute(string sql)
        {
            try
            {
                SQLiteCommand SqlCommand = MasterDatabase.CreateCommand();
                SqlCommand.CommandType = CommandType.Text;
                SqlCommand.CommandText = sql;
                SQLiteDataReader reader = SqlCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                this.HandleError(e);
            }
        }

        public DataTable Select(string sql, SQLiteParameter[] sqlparams)
        {
            try
            {
                SQLiteCommand SqlCommand = MasterDatabase.CreateCommand();
                SqlCommand.CommandType = CommandType.Text;
                SqlCommand.CommandText = sql;
                if (sqlparams != null)
                {
                    SqlCommand.Parameters.AddRange(sqlparams);
                }
                SQLiteDataReader reader = SqlCommand.ExecuteReader();
                DataTable QueryResults = new DataTable();
                QueryResults.Load(reader);

                return QueryResults;
            }
            catch (Exception e)
            {
                this.HandleError(e);
                return null;
            }
        }

        public void Insert(string sql, SQLiteParameter[] sqlparams)
        {
            try
            {
                SQLiteCommand SqlCommand = MasterDatabase.CreateCommand();
                SqlCommand.CommandType = CommandType.Text;
                SqlCommand.CommandText = sql;
                SqlCommand.Parameters.AddRange(sqlparams);
                SQLiteDataReader reader = SqlCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                this.HandleError(e);
            }
        }

        public void Update(string sql, SQLiteParameter[] sqlparams)
        {
            try
            {
                SQLiteCommand SqlCommand = MasterDatabase.CreateCommand();
                SqlCommand.CommandType = CommandType.Text;
                SqlCommand.CommandText = sql;
                SqlCommand.Parameters.AddRange(sqlparams);
                SQLiteDataReader reader = SqlCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                this.HandleError(e);
            }
        }

        public void HandleError(Exception ex)
        {
            System.Windows.MessageBox.Show("An error occured:\n" + ex.GetBaseException());
            Environment.Exit(0);
        }
    }
}
