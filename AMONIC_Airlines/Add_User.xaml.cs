using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace AMONIC_Airlines
{
    /// <summary>
    /// Логика взаимодействия для Add_User.xaml
    /// </summary>
    public partial class Add_User : Window
    {
        public Add_User()
        {
            InitializeComponent();
        }
        private Main_admin _window;
        private void Button_Click(object sender, RoutedEventArgs e)
        {//отмена создания и переход обратно на форму админестратора
            this.Close();
            _window = new Main_admin();
            _window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {//выполненние запроса и проверка на ошибки
            string query = "INSERT INTO users (RoleID,Email,Password,FirstName,LastName,Birthdate,OfficeID,Active) VALUES(2,@Email,@pass,@Fname,@Lname,@date,@offis,1)";
           
        
            string[] mass = new string[] { "Email", Email.Text, "pass", Password.Text, "Fname", FName.Text, "Lname", LName.Text, "date", data.Text, "offis", offices.Tag.ToString()};
            Bd.Database(query, mass);

         
        }
    }
}
