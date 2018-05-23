using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace SPRINT
{
    /// <summary>
    /// Логика взаимодействия для ConnectToBDWindow.xaml
    /// </summary>
    public partial class ConnectToBDWindow : Window
    {
        string _connString;
        public ConnectToBDWindow(string connectionString)
        {
            InitializeComponent();
            _connString = connectionString;
        }

        private void ApplyNewServerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServerStringTB.Text == "")
            {
                MessageBox.Show("Имя сервера не может быть пустым");
            }
            else
            {
                string connString = _connString;
                int startPos = connString.IndexOf('=');
                int endPos = connString.IndexOf("Initial Catalog");
                connString = connString.Remove(startPos + 1, endPos - startPos - 2);
                connString = connString.Insert(startPos + 1, ServerStringTB.Text);
                startPos = connString.IndexOf("Initial Catalog");
                endPos = connString.IndexOf("Integrated Security");
                
                string dbName = connString.Substring(startPos, endPos - startPos);
                connString = connString.Remove(startPos, endPos - startPos);            
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = connString;
                string query = "SELECT name from sys.databases";

                List<string> db_names = new List<string>();

                SqlCommand command = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    using (IDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            db_names.Add(dr[0].ToString());
                        }
                    }
                    conn.Close();
                    bool dbcontains = false;
                    foreach (string name in db_names)
                    {
                        if (dbName.Contains(name))
                        {
                            dbcontains = true;
                            break;
                        }
                    }
                    if (dbcontains == false)
                    {
                        query = File.ReadAllText("ModelSprintDB.edmx.sql");
                        IEnumerable<string> commandStrings = Regex.Split(query, @"^\s*GO\s*$",
                           RegexOptions.Multiline | RegexOptions.IgnoreCase);

                        conn.Open();
                        foreach (string commandString in commandStrings)
                        {
                            if (commandString.Trim() != "")
                            {
                                using (var commands = new SqlCommand(commandString, conn))
                                {
                                    commands.ExecuteNonQuery();
                                }
                            }
                        }
                        conn.Close();
                    }

                    connString = connString.Insert(startPos, dbName);
                    MainWindow main = this.Owner as MainWindow;
                    main.connString = connString;
                    this.DialogResult = true;
                    Close();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Неверное имя сервера");                   
                }                              
            }
        }
    }
}
