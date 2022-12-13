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
        
        //Dictionary<string, string> user = new Dictionary<string, string>();
        public Authorization()
        {
            InitializeComponent();
        }



        private  void RecoverBtn_Click(object sender, RoutedEventArgs e)
        {
            Window w = new Recover();
            this.Hide();
            w.Show();
        }






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
                    MessageBox.Show("Такого пользователя нет, зарегетрируйтесь");
                    result = false; return;
                }
                FirstName = Convert.ToString(data.firstname);
                LastName = Convert.ToString(data.lastname);
                Role = Convert.ToString(data.post_id);
                //MessageBox.Show(FirstName + LastName);
                result = true;

            }
            catch (Exception e)
            {
                //  MessageBox.Show(e.Message);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            EmailTb.FontSize = AuthWindow.Height / 52;
            PasswordPb.FontSize = AuthWindow.Height / 52;
        }

    }
}
