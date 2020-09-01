
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
namespace AMONIC_Airlines
{
    class Bd 
    {//подключение к базе данных 
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;user id=root;persistsecurityinfo=True;database=session1_xx");


        public void openconection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void closeconection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public MySqlConnection getConnection()
        {
            return connection;
        }

        public static string Login;//переменная для хроненния логина

        public static DataTable Database(string query, string[] aray)
        {   DataTable table = new DataTable();
            try
            {
                Bd db = new Bd();
               
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(query, db.getConnection());
                for (int i = 0; i < aray.Length; i = i + 2)
                {
                    command.Parameters.AddWithValue(aray[i], aray[i + 1]);
                }
                adapter.SelectCommand = command;
                adapter.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return  table;
            }

        }

    }
}
