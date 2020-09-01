
using System;
using System.Data;
using System.Windows;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace AMONIC_Airlines
{
    /// <summary>
    /// Логика взаимодействия для Main_admin.xaml
    /// </summary>
    public partial class Main_admin : Window
    {
       
        public Main_admin()
        {
            InitializeComponent();
        }
        private MainWindow _window;
        private Add_User _window2;
        private Change_role _window3;
        private void update(string query)
        {
            //выполненние запроса и заполненние таблици
            try
            {
                string[] mass = new string[] { "login", Bd.Login, "offis", offices.Text };

                Users.ItemsSource = Bd.Database(query, mass).DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        string Updatequery = "SELECT DISTINCT FirstName,LastName,   ((YEAR(CURRENT_DATE) - YEAR(Birthdate)) -(DATE_FORMAT(CURRENT_DATE, '%m%d') < DATE_FORMAT(Birthdate, '%m%d')))" +
            " AS age, roles.Title as Role,Email,offices.Title as office,Active FROM users , roles , session1_xx.offices where session1_xx.roles.ID =session1_xx.users.RoleID" +
            " AND session1_xx.offices.ID=session1_xx.users.officeID ";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            update(Updatequery);
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // закрытие окна и возвращение на главную форму
            this.Close();
            _window = new MainWindow();
            _window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // открытие формы добавления пользователя
            this.Close();
            _window2 = new Add_User();
            _window2.Show();
        }
        
        private void Offices_DropDownClosed(object sender, System.EventArgs e)
        {   if (offices.SelectedIndex == 0)
                update(Updatequery);
            else
                update(Updatequery + " AND session1_xx.offices.Title =@offis");

        }

        private void Users_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {//изменение цвета строк
            try
            {
                if (Convert.ToBoolean (((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[6].ToString()) == true)
                {
                    e.Row.Background = new SolidColorBrush(Colors.Green);
                }
                else if (Convert.ToBoolean(((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[6].ToString()) == false)
                {
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    e.Row.Background = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }
            catch 
            {
                          
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {//окрывает форму изменения роли

            this.Close();
            _window3 = new Change_role();
            _window3.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {//блокировка пользователя

           //запрос на обновление 
           string query = "UPDATE users SET active =@active WHERE FirstName=@Id "; 
            int active;
            try
            {
                //выбор имени из выделенной строки
                DataRowView dataRow = (DataRowView)Users.SelectedItem;
                string cellValue = dataRow.Row.ItemArray[0].ToString();

                //определение статуса
                if (Convert.ToBoolean(dataRow.Row.ItemArray[6]) == false)
                    active = 1;
                else
                    active = 0;
                //выполнение запроса 
                string[] mass = new string[] { "active", active.ToString(), "ID", cellValue };
                Bd.Database(query, mass);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //обновление таблицы
            update(Updatequery);
        }

    
    }
}
