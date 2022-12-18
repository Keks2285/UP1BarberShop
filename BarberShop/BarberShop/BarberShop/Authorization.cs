using System;
using System.Collections.Generic;
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
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using BarberShop.EmployeMAnager;
using BarberShop.Stocker;

namespace BarberShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        bool result = false;
        string FirstName = "";
        string LastName = "";
        string Role = "";
        int Id = 0;
        
        //Dictionary<string, string> user = new Dictionary<string, string>();
        public Authorization()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Обработка кнопки перехода на окно восстановления пароля
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void RecoverBtn_Click(object sender, RoutedEventArgs e)
        {
            Window w = new Recover();
            this.Hide();
            w.Show();
        }
        /// <summary>
        /// Обработка кнопки перехода на окно регистрации
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RegistrationWindow();
            this.Hide();
            w.Show();
        }



        /// <summary>
        /// Обработка кнопки авторизации
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private async void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTb.Text;
            string password = PasswordPb.Password;
            await Task.Run(() => auth(email, password));
            switch (Role)
            {
                case "1":
                    {
                        Window W = new EmployeManager(FirstName, LastName);
                        W.Show();
                        this.Hide();
                        break;
                    }
                case "3":
                    {
                        Window W = new StockerWindow(FirstName, LastName);
                        W.Show();
                        this.Hide();
                        break;
                    }
                case "2":
                    {
                        Window W = new Buhgalter.BuhWindow(FirstName, LastName, Id);
                        W.Show();
                        this.Hide();
                        break;
                    }
                case "4":
                    {
                        Window W = new DbAdmin.AdminWindow();
                        W.Show();
                        this.Hide();
                        break;
                    }
                case "Client":
                    {
                        Window W = new ClientPages.ClientWindow(FirstName, LastName, Id);
                        W.Show();
                        this.Hide();
                        break;
                    }
            }


            //  HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
            //      "http://192.168.1.49:80/BarberApi/");
            //  HttpWebResponse response = (HttpWebResponse)request.GetResponse();  
            //   Stream responseStream = response.GetResponseStream();
            //   StreamReader sr = new StreamReader(responseStream);
            //   string responseBody = sr.ReadToEnd();
            //  response.Close();
            // MessageBox.Show(responseBody);

        }
        /// <summary>
        /// Метод отправляющий запрос к АПИ
        /// </summary>
        /// <param name="_email"></param>
        /// <param name="_password"></param>
        public void auth(string _email, string _password)
        {
            if (_email.Length == 0 || _password.Length == 0)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            try
            {
                var req = new RestRequest("/authorization", Method.Post);
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                req.AddParameter("email", _email);
                req.AddParameter("password", _password);
                var res = Helper.client.Post(req);
                dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);

                if (!data.status.Value)
                {
                    var reqClient = new RestRequest("/authorizationClient", Method.Post);
                    reqClient.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    reqClient.AddParameter("email", _email);
                    reqClient.AddParameter("password", _password);
                    var resClient = Helper.client.Post(reqClient);
                    dynamic dataClient = JsonConvert.DeserializeObject<dynamic>(resClient.Content);

                    if (!dataClient.status.Value)
                        MessageBox.Show("Такого пользователя нет, зарегетрируйтесь");
                    Role = "Client";
                    FirstName = Convert.ToString(dataClient.firstname);
                    LastName = Convert.ToString(dataClient.lastname);
                    Id = Convert.ToInt32(dataClient.id);
                    result = false; return;
                }
                FirstName = Convert.ToString(data.firstname);
                LastName = Convert.ToString(data.lastname);
                Role = Convert.ToString(data.post_id);
                Id = Convert.ToInt32(data.employer_id);
                //MessageBox.Show(FirstName + LastName);
                result = true;

            }
            catch (Exception e)
            {
                //  MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// событеие изменение размеров окна
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            EmailTb.FontSize = AuthWindow.Height / 52;
            PasswordPb.FontSize = AuthWindow.Height / 52;
        }

    }
}
