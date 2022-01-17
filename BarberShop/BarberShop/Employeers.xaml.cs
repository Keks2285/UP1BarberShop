using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace BarberShop
{
    /// <summary>
    /// Логика взаимодействия для Employeers.xaml
    /// </summary>
    public partial class Employeers : Window
    {
        public static SqlConnection connect = new SqlConnection(Connect.connectionString);
        string F = "";
        string I = "";
        string O = "";
        string EMAIL = "";
        string LOGIN = "";
        string PHONE = "";
        string SERIA = "";
        string NOMER = "";
        string POSTS = "";
        int RANG = 0;
        public Employeers(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang)
        {
            InitializeComponent();
            F = f;
            I = i;
            O = o;
            EMAIL = email;
            LOGIN = login;
            PHONE = phone;
            SERIA = seria;
            NOMER = nomer;
            POSTS = posts;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand commandcb1 = new SqlCommand("SELECT ID_Status, Name_Status as 'Название'  from Status", connect);
            DataTable datatblcb1 = new DataTable();
            datatblcb1.Load(commandcb1.ExecuteReader());
            cb1.ItemsSource = datatblcb1.DefaultView;
            cb1.DisplayMemberPath = "Название";
            cb1.SelectedValuePath = "ID_Status";        
            SqlCommand command = new SqlCommand("SELECT ID_Employee, First_Name as 'Фамилия',Employee_Number as 'Телефон', Name_Employee as 'Имя', Middle_Name as 'Отчество', Seria_Pasporta as 'Серия паспорта', Nomer_Pasporta as 'Номер паспорта', Password_Employee as 'Пароль',Employee_Email as 'Почта', Login_Employee as 'Логин', Rang as 'Уровень доступа', Name_Status as 'Статус' from Employee join Status on FK_ID_Status=ID_Status", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            // datatbl.Columns.AddRange();
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;
            connect.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            try
            {
                connect.Open();
                DataRowView row = (DataRowView)dg.SelectedItem;
                SqlCommand Del = new SqlCommand("Employee_Delete", connect);
                Del.CommandType = CommandType.StoredProcedure;
                Del.Parameters.AddWithValue("ID_Employee", (int)row["ID_Employee"]);
                Del.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Нельзя удалить этого сотрудника"); }
            finally { connect.Close(); Window_Loaded(sender, e);
                FirstNamE.Text = "";
                NamE.Text = "";
                OtcH.Text = "";
                SeriaPasporta.Text = "";
                NomerPasporta.Text = "";
                LogiN.Text = "";
                PassworD.Text = "";
                Level.Text = "";
                EmaiL.Text = "";
                PhonE.Text = "";
                cb1.SelectedValue = null;
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNamE.Text == "" || NamE.Text == "" || OtcH.Text == "" || cb1.SelectedValue == null || PhonE.Text == "" || SeriaPasporta.Text == "" || NomerPasporta.Text == "") { MessageBox.Show("Все поля должны быть заполненными"); return; }
            try
            {
                connect.Open();
                SqlCommand add = new SqlCommand("Employee_Update", connect);
                DataRowView row = (DataRowView)dg.SelectedItem;
                add.CommandType = CommandType.StoredProcedure;
                add.Parameters.AddWithValue("@ID_Employee", (int)row["ID_Employee"]);
                add.Parameters.AddWithValue("@First_Name", FirstNamE.Text);
                add.Parameters.AddWithValue("@Name_Employee", NamE.Text);
                add.Parameters.AddWithValue("@Middle_Name", OtcH.Text);
                add.Parameters.AddWithValue("@Seria_Pasporta", SeriaPasporta.Text);
                add.Parameters.AddWithValue("@Nomer_Pasporta", NomerPasporta.Text);
                add.Parameters.AddWithValue("@Login_Employee", LogiN.Text);
                add.Parameters.AddWithValue("@Password_Employee", PassworD.Text);
                add.Parameters.AddWithValue("@Rang", Level.Text);
                add.Parameters.AddWithValue("@Employee_Email", EmaiL.Text);
                add.Parameters.AddWithValue("@Employee_Number", PhonE.Text);
                add.Parameters.AddWithValue("@FK_ID_Status", cb1.SelectedValue);
                add.ExecuteNonQuery();
           }
            catch { MessageBox.Show("Введены некорректные данные или сотрудник с таким данными уже существует"); }
            finally
            {
                connect.Close();
                Window_Loaded(sender, e);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNamE.Text == "" || NamE.Text == "" || OtcH.Text == "" || cb1.SelectedValue == null || PhonE.Text == "" || SeriaPasporta.Text == ""|| NomerPasporta.Text=="") { MessageBox.Show("Все поля должны быть заполненными"); return; }
            try
            {
                connect.Open();
                SqlCommand add = new SqlCommand("Employee_Insert", connect);
                add.CommandType = CommandType.StoredProcedure;
                add.Parameters.AddWithValue("@First_Name",   FirstNamE.Text);
                add.Parameters.AddWithValue("@Name_Employee", NamE.Text);
                add.Parameters.AddWithValue("@Middle_Name", OtcH.Text);
                add.Parameters.AddWithValue("@Seria_Pasporta", SeriaPasporta.Text);
                add.Parameters.AddWithValue("@Nomer_Pasporta", NomerPasporta.Text);
                add.Parameters.AddWithValue("@Login_Employee", LogiN.Text);
                add.Parameters.AddWithValue("@Password_Employee", PassworD.Text);
                add.Parameters.AddWithValue("@Rang", Level.Text);
                add.Parameters.AddWithValue("@Employee_Email", EmaiL.Text);
                add.Parameters.AddWithValue("@Employee_Number", PhonE.Text);
                add.Parameters.AddWithValue("@FK_ID_Status", cb1.SelectedValue);
            add.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Введены некорректные данные или сотрудник с таким данными уже существует"); }
            finally
            {
                connect.Close();
                Window_Loaded(sender, e);
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new KadrOtdel(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG);
            this.Hide();
            haircuts.Show();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg.SelectedItem;
            FirstNamE.Text = row["Фамилия"].ToString();
            NamE.Text = row["Имя"].ToString();
            OtcH.Text = row["Отчество"].ToString();
            LogiN.Text = row["Логин"].ToString();
            NomerPasporta.Text = row["Номер паспорта"].ToString();
            SeriaPasporta.Text = row["Серия паспорта"].ToString();
            EmaiL.Text = row["Почта"].ToString();
            PassworD.Text = row["Пароль"].ToString();
            PhonE.Text = row["Телефон"].ToString();
            Level.Text = row["Уровень доступа"].ToString();
        }
    }
}
