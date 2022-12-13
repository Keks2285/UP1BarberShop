using BarberShop.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
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

namespace BarberShop.EmployeManagerPages
{
    /// <summary>
    /// Логика взаимодействия для EmployerCreate.xaml
    /// </summary>
    public partial class EmployerCreate : Page
    {
        public EmployerCreate()
        {
            InitializeComponent();
        }

        private void CreateEmployer_Click(object sender, RoutedEventArgs e)
        {
            if (!validateEmplouer()) return;

            try { 
                var req = new RestRequest("/createEmploye", Method.Post);
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                req.AddParameter("firstname", FirstNameTb.Text);
                req.AddParameter("lastname", LastNameTb.Text);
                req.AddParameter("middlename", MiddleNameTb.Text);
                req.AddParameter("email", EmailTb.Text);
                req.AddParameter("password", PasswordTb.Text);
                req.AddParameter("inn", InnTb.Text);
                req.AddParameter("post_id", (PostCb.SelectedItem as PostEmploye).Id);
                req.AddParameter("status_id", 1);
                var res = Helper.client.Post(req);
                dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
            }
            catch
            {

            }
        }


        private bool validateEmplouer()
        {

            if (PostCb.SelectedItem == null)
            {
                MessageBox.Show("Выберите должность");
                return false;
            }

            if (!Helper.CheckEmail(EmailTb.Text))
            {
                MessageBox.Show("Некорректная почта");
                return false;
            };
            if (EmployeModel.AllEmail.Contains(EmailTb.Text))
            {
                MessageBox.Show("Данная почта уже занята");
                return false;
            };
            if (InnTb.Text.Contains("_"))
            {
                MessageBox.Show("Некорректный ИНН");
                return false;
            }
            if (EmployeModel.AllINN.Contains(InnTb.Text))
            {
                MessageBox.Show("Инн уже используется");
                return false;
            };
            if (!Helper.CheckFIO(FirstNameTb.Text)|| FirstNameTb.Text.Length<2)
            {
                MessageBox.Show("Неккоректная фамилия");
                return false;
            };

            if (!Helper.CheckFIO(LastNameTb.Text) || LastNameTb.Text.Length < 2)
            {
                MessageBox.Show("Неккоректное имя");
                return false;
            };

            if (!Helper.CheckFIO(MiddleNameTb.Text) && MiddleNameTb.Text!="")
            {
                MessageBox.Show("Неккоректное отчество");
                return false;
            };

            return true;
        }




        //  MessageBox.Show(InnTb.Text); работает
        // if(InnTb.Text.Contains("_")) MessageBox.Show("fdikgjifdjgi");

    }
}
