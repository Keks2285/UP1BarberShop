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
        private BindingList<EmployeModel> _employers = new BindingList<EmployeModel>();
        int employer_id=0;
        public TaxReports(int employerId)
        {
            this.employer_id=employerId;    
            InitializeComponent();
        }

        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {

            var req = new RestRequest("/createTaxReport", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("date_report", DateTime.Now.ToString("yyyy-MM-dd"));
            req.AddParameter("date_begin", DateBegin.SelectedDate.Value.ToString("yyyy-MM-dd"));
            req.AddParameter("date_end", DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd"));
            req.AddParameter("value_sells", Convert.ToInt32(Price.Text));
            req.AddParameter("value_tax", Convert.ToInt32(Price.Text)*0.13);
            req.AddParameter("employe_id", employer_id);
            var res = Helper.client.Post(req);

            _reports.Add(new TaxReport
            {
                Date_Report = DateTime.Now.ToString("yyyy-MM-dd"),
                Date_Begin = DateBegin.SelectedDate.Value.ToString("yyyy-MM-dd"),
                Date_End = DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd"),
                Value_Sells = Convert.ToInt32(Price.Text),
                Value_Tax = Convert.ToInt32(Price.Text)*0.13,
                Employe_ID = employer_id,
                employer = _employers.FirstOrDefault(item => item.ID_Employee == employer_id)
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeModel.AllINN.Clear();
            EmployeModel.AllEmail.Clear();

            var reqEmployers = new RestRequest("/getEmployers", Method.Get);
            reqEmployers.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resEmployers = Helper.client.Get(reqEmployers);
            List<EmployeModel> dataEmployers = JsonConvert.DeserializeObject<List<EmployeModel>>(resEmployers.Content);
            foreach (EmployeModel employer in dataEmployers)
            {
                _employers.Add(employer);
            };

            var reqReports = new RestRequest("/getTaxReports", Method.Get);
            reqReports.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resReports = Helper.client.Get(reqReports);
            List<TaxReport> dataReports = JsonConvert.DeserializeObject<List<TaxReport>>(resReports.Content);
            foreach(TaxReport tr in dataReports)
            {
                tr.employer = _employers.FirstOrDefault(item => item.ID_Employee == employer_id);
                _reports.Add(tr);
            };
            ReportsDg.ItemsSource= _reports;


            
            ReportsDg.ItemsSource = _reports;





            ///////////////////// статистика доходов за текущий год
            var reqIncomes = new RestRequest("/getIncomes", Method.Get);
            reqIncomes.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resIncomes = Helper.client.Get(reqIncomes);
            List<Income> dataIncomes = JsonConvert.DeserializeObject<List<Income>>(resIncomes.Content);
            string[] nameMonths = {"Январь","Февраль", "Март",
                                    "Апрель", "Май", "Июнь",
                                    "Июль", "Август", "Сентябрь",
                                    "Октябрь", "Ноябрь", "Декабрь" };
            double[] months = new double[12];
            int counterMonth = 0;
            for (int i = 1; i < 13; i++)
            {
                var curentMonth = from income in dataIncomes
                                  where
                                  income.Date_Income.Month == i && income.Date_Income.Year == DateTime.Now.Year
                                  select income;
                foreach (Income x in curentMonth)
                {
                    months[counterMonth] += x.Value;
                };

                counterMonth++;

            }
            double[] positions = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            Statistic.Plot.AddBar(months, positions);
            Statistic.Plot.XTicks(positions, nameMonths);
            Statistic.Plot.SetAxisLimits(yMin: 0);
            Statistic.Plot.XAxis.Grid(false);
            Statistic.Plot.SaveFig("stats_histogram.png");
            Statistic.Refresh();
            ///////////////










        }

        private void ViewDoc_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportDocumentViewer(ReportsDg.SelectedItem as TaxReport));
        }
    }
}
