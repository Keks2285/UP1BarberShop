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

namespace BarberShop.EmployeManagerPages
{
    /// <summary>
    /// Логика взаимодействия для VacationsPage.xaml
    /// </summary>
    public partial class VacationsPage : Page
    {

        private BindingList<EmployeModel> _employers = new BindingList<EmployeModel>();
        private BindingList<Vacation> _vacations = new BindingList<Vacation>();
        private BindingList<SickLeave> _sickleaves = new BindingList<SickLeave>();

        public VacationsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var req = new RestRequest("/getEmployers", Method.Get);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var res = Helper.client.Get(req);
            EmployeModel.AllEmail.Clear();
            EmployeModel.AllINN.Clear();
            List<EmployeModel> data = JsonConvert.DeserializeObject<List<EmployeModel>>(res.Content);
            
            foreach (EmployeModel employer in data)
            {
                
                _employers.Add(employer);
            }
            EmployersDg.ItemsSource= _employers;

            var reqSick = new RestRequest("/getSickLeaves", Method.Get);
            reqSick.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resSick = Helper.client.Get(reqSick);
            List<SickLeave> dataSick = JsonConvert.DeserializeObject<List<SickLeave>>(resSick.Content);
            foreach (SickLeave sick in dataSick)
            {
                _sickleaves.Add(sick);
            }
            SickLeavesDg.ItemsSource = _sickleaves;


            var reqVac = new RestRequest("/getVacations", Method.Get);
            reqVac.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resVac = Helper.client.Get(reqVac);
            List<Vacation> dataVac = JsonConvert.DeserializeObject<List<Vacation>>(resVac.Content);
            foreach (Vacation Vac in dataVac)
            {
                _vacations.Add(Vac);
            }
            VacationsDg.ItemsSource = _vacations;

        }
    }
}
