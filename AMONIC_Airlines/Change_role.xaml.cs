using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace AMONIC_Airlines
{
    /// <summary>
    /// Логика взаимодействия для Change_role.xaml
    /// </summary>
    public partial class Change_role : Window
    {
        int a =1 ; // переменная для RoleID
        private Main_admin _window;
        public Change_role()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//Выполнение запроса и вывод ошибки
            string query = "UPDATE users SET RoleID=@role WHERE Email=@email AND FirstName=@fname AND LastName=@lname AND OfficeID=@office; ";

                string[] mass = new string[] { "role", a.ToString(), "email", Email.Text, "fname", Fname.Text, "lname", Lname.Text,"office", offices.Tag.ToString() };
                Bd.Database(query, mass);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {//закрыть форму и открыть форму админестратора
            this.Close();
            _window = new Main_admin();
            _window.Show();
        }
        private void HandleCheck(object sender, RoutedEventArgs e)
        {//параметр чекбокса
            RadioButton rb = sender as RadioButton;
            if (rb.Name == "CheckAdmin")
            {
                a = 1;
            }
            else
            {
                a = 2;
            }
          
            
        }
    }
}
