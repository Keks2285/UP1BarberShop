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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarberShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static SqlConnection connect = new SqlConnection(Connect.connectionString);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(Connect.connectionString))
            {
                connect.Open();
                SqlCommand c = new SqlCommand($@"select * from Users where Login_User='{Login.Text}' and Password_User='{Password.Password}'",connect);
                SqlDataReader reader = c.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                        //TB1.Text = reader["Login_User"] + " " + reader["Password_User"];
                        Window u1 = new UserWindow1(reader["Login_User"].ToString(), reader["Password_User"].ToString(),reader["Name_User"].ToString(), reader["LastName_User"].ToString(), reader["Otch_User"].ToString(), Convert.ToInt32(reader["ID_User"].ToString()));
                        this.Hide();
                        u1.Show();

                }
                else // если вошел сотрудник
                {
                    reader.Close();
                    string Posts = "";
                    int ID_Employee = 0;
                    //ID_Employee	First_Name	Name_Employee	Middle_Name	Employee_Number	Employee_Email	Seria_Pasporta	Nomer_Pasporta	Login_Employee	Password_Employee	FK_ID_Status	Rang
                    SqlCommand findIDEmploy = new SqlCommand($@"select * from Employee where Login_Employee='{Login.Text}' and Password_Employee='{Password.Password}'", connect);
                    SqlDataReader readerEmploy = findIDEmploy.ExecuteReader();
                    if (readerEmploy.HasRows) // если есть данный сотрудник 
                    {
                        readerEmploy.Read();
                        ID_Employee = Convert.ToInt32(readerEmploy["ID_Employee"].ToString());
                        string F = readerEmploy["First_Name"].ToString();
                        string I = readerEmploy["Name_Employee"].ToString();
                        string O = readerEmploy["Middle_Name"].ToString();
                        string EMAIL = readerEmploy["Employee_Email"].ToString();
                        string LOGIN = readerEmploy["Login_Employee"].ToString();
                        string PHONE = readerEmploy["Employee_Number"].ToString();
                        string SERIA = readerEmploy["Seria_Pasporta"].ToString();
                        string NOMER = readerEmploy["Nomer_Pasporta"].ToString();
                        int level = Convert.ToInt32(readerEmploy["Rang"].ToString());
                        readerEmploy.Close();
                        SqlCommand findCombination = new SqlCommand($@"select * from Combination where FK_ID_Employee='{ID_Employee}'", connect);
                        SqlDataReader readCombinations = findCombination.ExecuteReader();
                        if (readCombinations.HasRows)
                        {
                            List <int> posts_Employee  = new List<int>();
                            while (readCombinations.Read()) // построчно считываем данные
                            {
                                posts_Employee.Add(Convert.ToInt32(readCombinations["FK_ID_Post"].ToString()));
                            }
                            readCombinations.Close();
                            foreach (int post in posts_Employee)
                            {
                                SqlCommand findPosts = new SqlCommand($@"Select Name_Post from Post where ID_Post ={post}", connect);
                                SqlDataReader readNamepost = findPosts.ExecuteReader();
                                readNamepost.Read();
                                Posts += readNamepost["Name_Post"].ToString()+" ";
                                readNamepost.Close();
                            }
                        }
                        connect.Close();
                        switch (level)
                        {
                            case 1:
                                {
                                    // парикмахер
                                    Window barber = new BarberWindow1(LOGIN, SERIA, NOMER, EMAIL, Posts, F, I, O, PHONE, level, ID_Employee);
                                    this.Hide();
                                    barber.Show();
                                    break;
                                }
                            case 2:
                                {
                                    // кладмен
                                    Window slader = new SkladManager(LOGIN, SERIA, NOMER, EMAIL, Posts, F, I, O, PHONE, level, ID_Employee);
                                    this.Hide();
                                    slader.Show();
                                    break;
                                }
                            case 3:
                                {
                                    // отдел кадров
                                    Window slader = new KadrOtdel(LOGIN, SERIA, NOMER, EMAIL, Posts, F, I, O, PHONE, level, ID_Employee);
                                    this.Hide();
                                    slader.Show();
                                    break;
                                }
                            case 4:
                                {
                                    // отдел закупок
                                    Window slader = new Zakupmen(LOGIN, SERIA, NOMER, EMAIL, Posts, F, I, O, PHONE, level, ID_Employee);
                                    this.Hide();
                                    slader.Show();
                                    break;
                                }
                            case 5:
                                {
                                    // отдел закупок
                                    Window slader = new BuhWindow(LOGIN, SERIA, NOMER, EMAIL, Posts, F, I, O, PHONE, level, ID_Employee);
                                    this.Hide();
                                    slader.Show();
                                    break;
                                }
                            case 6:
                                {
                                    // отдел Admin
                                    Window slader = new Admin(LOGIN, SERIA, NOMER, EMAIL, Posts, F, I, O, PHONE, level, ID_Employee);
                                    this.Hide();
                                    slader.Show();
                                    break;
                                }
                        }
                    }
                      else 
                      {
                        MessageBox.Show("Такого пользователя не существует, сначала пройдите регистрацию");
                        
                     };

                }
                connect.Close();

            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Window r = new Reg();
            this.Hide();
            r.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
