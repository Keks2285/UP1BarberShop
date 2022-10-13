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

namespace BarberShop
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private static RestClient client = new RestClient("http://192.168.1.49:80/BarberApi/");
        //Dictionary<string, string> user = new Dictionary<string, string>();
        public Authorization()
        {
            InitializeComponent();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {

            var req = new RestRequest("/authorization", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("email", Email.Text);
            req.AddParameter("password", Password.Password);
            var res = client.Post(req);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);

            MessageBox.Show(Convert.ToString(data.message));

            //  HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
            //      "http://192.168.1.49:80/BarberApi/");
            //  HttpWebResponse response = (HttpWebResponse)request.GetResponse();  
            //   Stream responseStream = response.GetResponseStream();
            //   StreamReader sr = new StreamReader(responseStream);
            //   string responseBody = sr.ReadToEnd();
            //  response.Close();
            // MessageBox.Show(responseBody);

        }
    }
}
