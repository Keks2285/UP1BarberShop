using BarberShop.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BarberShop.Buhgalter
{
    /// <summary>
    /// Логика взаимодействия для TaxReports.xaml
    /// </summary>
    public partial class TaxReports : Page
    {

        private BindingList<TaxReport> _reports = new BindingList<TaxReport>();
        int employer_id=0;
        public TaxReports(int employerId)
        {
            this.employer_id=employerId;    
            InitializeComponent();
        }

        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var reqReports = new RestRequest("/getTaxReports", Method.Get);
            reqReports.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resReports = Helper.client.Get(reqReports);
            List<TaxReport> dataReports = JsonConvert.DeserializeObject<List<TaxReport>>(resReports.Content);
            foreach(TaxReport tr in dataReports)
            {
                _reports.Add(tr);
            };
            ReportsDg.ItemsSource= _reports;


        }
    }
}
