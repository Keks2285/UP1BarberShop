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
using System.Windows.Shapes;

namespace BarberShop
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Helper.CheckEmail(EmailTb.Text) || EmailTb.Text.Length<1) {
                MessageBox.Show("Неккоекртная почта");
                return;
            }
            if (!Helper.CheckPass(PasswordPb.Password) || PasswordPb.Password.Length<1)
            {
                MessageBox.Show("Пароль должен быть минимум 6 символов содержать спец символ цифру, большую и маленькую букву, и содержать только латинские буквы");
                return;
            }
            if (PasswordPb.Password!=RepeatPasswordPb.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            
            if (!Helper.CheckFIO(FirstaName.Text) || FirstaName.Text.Length < 1)
            {
                MessageBox.Show("Неккоекртная Фамилия, она может содеражать только кириллицу");
                return;
            }
            if (!Helper.CheckFIO(LastName.Text) || LastName.Text.Length < 1)
            {
                MessageBox.Show("Неккоекртное имя, оно может содеражать только кириллицу");
                return;
            }
            if (!Helper.CheckFIO(MiddleName.Text) && MiddleName.Text.Length > 0)
            {
                MessageBox.Show("Неккоекртное отчество, оно может содеражать только кириллицу");
                return;
            }
            if (Phone.Text.Contains("_"))
            {
                MessageBox.Show("Заполните номер телефона");
                return;
            }

            var reqVac = new RestRequest("/registrateClient", Method.Post);
            reqVac.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            reqVac.AddParameter("firstname", FirstaName.Text);
            reqVac.AddParameter("lastname", LastName.Text);
            reqVac.AddParameter("middlename", MiddleName.Text==""?" ": MiddleName.Text);
            reqVac.AddParameter("phone",Phone.Text);
            reqVac.AddParameter("email", EmailTb.Text);
            reqVac.AddParameter("password", PasswordPb.Password);
            var resVac = Helper.client.Post(reqVac);
            dynamic reqdata = JsonConvert.DeserializeObject<dynamic>(resVac.Content);

            if (reqdata.status.Value)
            {
                MessageBox.Show("Регистрация прошла успешно");
                Window w = new Authorization();
                this.Hide();
                w.Show();
            }
            else
            {
                MessageBox.Show("К сожалению не удалось вас зарегистрировать, такие данные уже есть в системе");
                return;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Window W = new Authorization();
            this.Hide();
            W.Show();
        }
    }
}
