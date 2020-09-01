using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;
namespace AMONIC_Airlines
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            InitializeComponent();
        }
        
        
        private  Main_admin _window;

        private User_Window _window1;

       



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
 
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Bd.Login = Uname.Text;

           
            string query = "SELECT RoleId FROM users WHERE Email= @login AND Password= @password "; //запрос 
            string[] mass = new string[] { "login", Bd.Login, "password", Pword.Password };
            DataTable table = Bd.Database(query, mass);
            string role="";

            try
            {
               role = table.Rows[0][0].ToString();
            }
            catch
            {
                MessageBox.Show("Заполните все строки");
            }

            if (table.Rows.Count > 0)
            {

                MessageBox.Show("Вход выполнен");

                if (role == "1")//проверка полученного значения
                {
                   
                    _window = new Main_admin();
                    _window.Show();
                    //открыть форму админестратора
                }
                else
                {
                    _window1 = new User_Window();
                    _window1.Show();
                    // открыть форму пользователя 
                }
                this.Hide();
            }


            else
                MessageBox.Show("Ошибка Входа");
        }
    }
}
