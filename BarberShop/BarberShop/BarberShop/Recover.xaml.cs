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
using MimeKit;
using MailKit.Net.Smtp;
using System.Text.RegularExpressions;
using RestSharp;
using Newtonsoft.Json;

namespace BarberShop
{
    /// <summary>
    /// Логика взаимодействия для Recover.xaml
    /// </summary>
    public partial class Recover : Window { 
        Regex r = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,20}$");
        private static RestClient client = new RestClient("http://192.168.1.49:8080/BarberApi/");
        string code = "";
        public Recover()
        {
            InitializeComponent();
            Random rnd = new Random();
            for (int i = 0; i < 6; i++)
            {
                code += rnd.Next(0, 9);
            }
        }


        private async void SendMessge( String Email)
        {
            await Task.Run(() =>
            {
                try
                {
                    var emailMessage = new MimeMessage();

                    emailMessage.From.Add(new MailboxAddress("Администрация", "magizin451@gmail.com"));
                    emailMessage.To.Add(new MailboxAddress("Восстановление", Email));
                    emailMessage.Subject = "Восстановление";
                    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text ="Код для восстановления:"+code
                    };
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate("magizin451@gmail.com", "qjjlttxxzpjuytny");
                        client.Send(emailMessage);
                        client.Disconnect(true);
                        MessageBox.Show("Код отправлен");
                    }

                }
                catch
               {
                   MessageBox.Show("Проблемы с сетью, попробуйте позже");
                }


            });

        }

        private void SendCodeBtn_Click(object sender, RoutedEventArgs e)
        {
            // bool existUser = false;
            string userType;
            if(EmailTb.Text.Length<5)
            {
                MessageBox.Show("Некорректная или пустая почта");
                return;
            }
            SendMessge(EmailTb.Text);
          
           
        }

        private void Recovertn_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordPb.Password != RepeatPasswordPb.Password) {
                MessageBox.Show("Пароли не совпадают");return;
            }
            if (CodeTb.Text != code) { MessageBox.Show("Неверный код восстановления"); return; }


            string userType;   
           try{
                var reqEmploye = new RestRequest("/getEmployeByEmail", Method.Get);
                reqEmploye.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                reqEmploye.AddParameter("email", EmailTb.Text);
                var resRmploye = client.Get(reqEmploye);
                dynamic dataEmploye = JsonConvert.DeserializeObject<dynamic>(resRmploye.Content);

                if (!dataEmploye.status.Value)
                {
                    //MessageBox.Show("Такого пользователя нет, зарегетрируйтесь");
                    var reqClient = new RestRequest("/getClientByEmail", Method.Get);
                    reqClient.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    reqClient.AddParameter("email", EmailTb.Text);
                    var resClient = client.Get(reqEmploye);
                    dynamic dataClient = JsonConvert.DeserializeObject<dynamic>(resClient.Content);
                    if (!dataClient.status.Value)
                    {
                        MessageBox.Show("В системе нет пользователя с такой почтой, зарегестрируйтесь"); return;
                    }
                    else
                    {
                        userType = "Client";
                    }

                }
                else userType = "Employe";

                if (userType != null)
                {
                    var reqUpdatet = new RestRequest("/recoverPassword", Method.Post);
                    reqUpdatet.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    reqUpdatet.AddParameter("email", EmailTb.Text);
                    reqUpdatet.AddParameter("userType", userType);
                    reqUpdatet.AddParameter("newPassword", PasswordPb.Password);
                    var resUpdate = client.Post(reqUpdatet);
                    dynamic dataUpdate = JsonConvert.DeserializeObject<dynamic>(resUpdate.Content);

                    if (!dataUpdate.status.Value)
                    {
                        MessageBox.Show("Что-то пошло не так, пароль не обновлен"); return;
                    }
                    else{
                         MessageBox.Show("Пароль  изменен"); return;
                    }
                } 
           }catch{
                MessageBox.Show("Проблемы с сетью, Попробуйте позже");
           }
        }
    }
}
