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
    /// Логика взаимодействия для Zapis.xaml
    /// </summary>
    public partial class Zapis : Window
    {
        public static SqlConnection connect = new SqlConnection(Connect.connectionString);
        string F = "";
        string I = "";
        string O = "";
        string EMAIL = "";
        string LOGIN = "";
        string PASSWORD = "";
        int ID = 0;
        public Zapis(string login, string password, string name, string Lastname, string otch, int id)
        {
            InitializeComponent();
            connect.Open();
            SqlCommand commandcb1 = new SqlCommand("select Name_Serwice as 'Название', Price_Serwice 'Цена', Name_Instrument as 'Инструмент', Name_Haircut as 'Прическа' ,concat(First_Name,' ',Name_Employee,' ', Middle_Name) as 'Сотрудник' from Serwice join Employee on ID_Employee=FK_ID_Employee join Haircut on ID_Haircut=FK_ID_Haircut join Instrument on ID_Instrument = FK_ID_Instrument", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(commandcb1.ExecuteReader());
            dg.ItemsSource = datatbl.DefaultView;
            connect.Close();
            F = Lastname;
            I = name;
            O = otch;
            PASSWORD = password;
            LOGIN = login;
            ID = id;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window barber = new UserWindow1(LOGIN, PASSWORD, I, F, O, ID);
            this.Hide();
            barber.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Date.Text != "" && Service.SelectedValue != null)
            {

                
                //if ()
                try
                {
                    DateTime date = DateTime.Parse(Date.Text);
                    if (DateTime.Compare(date, DateTime.Today) <= 0) { MessageBox.Show("Введены некорректные данные"); return; }
                    connect.Open();
                    SqlCommand add = new SqlCommand("Zapis_Insert", connect);
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@Data_Zapis", Date.Text);
                    add.Parameters.AddWithValue("@FK_ID_Serwice", Service.SelectedValue);
                    add.Parameters.AddWithValue("@FK_ID_User", ID);
                    add.ExecuteNonQuery();
                    connect.Close();
                    Window_Loaded(sender, e);
                }
                catch { MessageBox.Show("Введены некорректные данные"); }
                finally
                {
                    connect.Close();
                    Window_Loaded(sender, e);
                }
            }
        }

                private void Delete_Click(object sender, RoutedEventArgs e)
                {
            if (dg.SelectedItem == null) return;
            try
            {
                connect.Open();
                DataRowView row = (DataRowView)dg.SelectedItem;
                SqlCommand Del = new SqlCommand("Zapis_Delete", connect);
                Del.CommandType = CommandType.StoredProcedure;
                Del.Parameters.AddWithValue("ID_Zapis", row["ID_Zapis"]);
                Del.ExecuteNonQuery();
                connect.Close();
            }
                  catch { } finally { Window_Loaded(sender, e);}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
     
            SqlCommand comand = new SqlCommand("select ID_Zapis, Data_Zapis as 'Дата', Name_Serwice as 'Название' from Zapis join Serwice on FK_ID_Serwice = ID_Serwice", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(comand.ExecuteReader());
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;
            
            SqlCommand commandcb1 = new SqlCommand("select ID_Employee,  ID_Serwice, Name_Serwice as 'Название', concat(First_Name,' ',Name_Employee,' ', Middle_Name) as 'Сотрудник' from Serwice join Employee on ID_Employee=FK_ID_Employee join Haircut on ID_Haircut=FK_ID_Haircut join Instrument on ID_Instrument = FK_ID_Instrument", connect);
            DataTable datatblHaircut = new DataTable();
            datatblHaircut.Load(commandcb1.ExecuteReader());

            Service.ItemsSource = datatblHaircut.DefaultView;
            Service.DisplayMemberPath = "Название";
            Service.SelectedValuePath = "ID_Serwice";
            connect.Close();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
