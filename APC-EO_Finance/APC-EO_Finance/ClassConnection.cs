using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace APC_EO_Finance
{
    public class ClassConnection
    {
        private ClassConnection()
        {
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }
        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static ClassConnection _instance = null;
        public static ClassConnection Instance()
        {
            if (_instance == null)
                _instance = new ClassConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                    return false;
                string connstring = string.Format("Server=207.148.125.191; database=u918776074_btwo; UID=btwo; password=asdf1234", databaseName);
                //string connstring = string.Format("Server=localhost; database=u918776074_btwo; UID=root; password=", databaseName);

                connection = new MySqlConnection(connstring);
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Internet Error", "Error");
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Error");
                }
            }

            return true;
        }
        public bool Connecting()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Internet Error dalam melempar data, Mohon diulangi", "Error");
                return false;
            }
        }
        public void Close()
        {
            connection.Close();
        }
    }
}
