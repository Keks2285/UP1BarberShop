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
                sick.employer = _employers.FirstOrDefault(item => item.ID_Employee == sick.Employe_ID);
                sick.FIO = sick.employer.FirstName + '.' + sick.employer.LastName[0] + '.' + sick.employer?.MiddleName?[0];
                _sickleaves.Add(sick);
            }
            SickLeavesDg.ItemsSource = _sickleaves;


            var reqVac = new RestRequest("/getVacations", Method.Get);
            reqVac.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resVac = Helper.client.Get(reqVac);
            List<Vacation> dataVac = JsonConvert.DeserializeObject<List<Vacation>>(resVac.Content);
            foreach (Vacation Vac in dataVac)
            {
                Vac.employer = _employers.FirstOrDefault(item=>item.ID_Employee==Vac.Employe_ID);
                Vac.FIO = Vac.employer.FirstName + '.' + Vac.employer.LastName[0] + '.' + Vac.employer?.MiddleName?[0];
                _vacations.Add(Vac);
            }
            VacationsDg.ItemsSource = _vacations;

        }
        
        private void CreatVacation_Click(object sender, RoutedEventArgs e)
        {
            if (!validateData()) return;


            var reqVac = new RestRequest("/createVacation", Method.Post);
            reqVac.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            reqVac.AddParameter("date_begin", DateBegin.SelectedDate!.Value.ToString("yyyy.MM.dd"));
            reqVac.AddParameter("date_end", DateEnd.SelectedDate!.Value.ToString("yyyy.MM.dd"));
            reqVac.AddParameter("employe_id", (EmployersDg.SelectedItem as EmployeModel).ID_Employee);
            var resVac = Helper.client.Post(reqVac);
            dynamic reqdata = JsonConvert.DeserializeObject<dynamic>(resVac.Content);
            _vacations.Add(new Vacation
            {
                Employe_ID = (EmployersDg.SelectedItem as EmployeModel).ID_Employee,
                employer = (EmployersDg.SelectedItem as EmployeModel),  
                Date_Begin = DateBegin.SelectedDate!.Value.ToString("dd-MM-yyyy"),
                Date_End = DateEnd.SelectedDate!.Value.ToString("dd-MM-yyyy"),
                ID_Vacation = reqdata.id
            }
            );
        }

        private void CreateSickLeave_Click(object sender, RoutedEventArgs e)
        {
            if (!validateData()) return;
            var reqVac = new RestRequest("/createSickLeave", Method.Post);
            reqVac.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            reqVac.AddParameter("date_begin", DateBegin.SelectedDate!.Value.ToString("yyyy.MM.dd"));
            reqVac.AddParameter("date_end", DateEnd.SelectedDate!.Value.ToString("yyyy.MM.dd"));
            reqVac.AddParameter("employe_id", (EmployersDg.SelectedItem as EmployeModel).ID_Employee);
            var resVac = Helper.client.Post(reqVac);
            dynamic reqdata = JsonConvert.DeserializeObject<dynamic>(resVac.Content);
            _vacations.Add(new Vacation
            {
                Date_Begin = DateBegin.SelectedDate!.Value.ToString("dd-MM-yyyy"),
                Date_End = DateEnd.SelectedDate!.Value.ToString("dd-MM-yyyy"),
                ID_Vacation = reqdata!.id
            }
            );
        }



        private bool validateData()
        {
            if (DateBegin.Text == "" || DateEnd.Text == "")
            {
                MessageBox.Show("Заполните все даты");
                return false;
            }
            if (EmployersDg.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return false;
            }
            TimeSpan a = (TimeSpan)( DateEnd.SelectedDate - DateBegin.SelectedDate);
            if (a.TotalDays < 1)
            {
                MessageBox.Show("Дата окончания не может быть раньше или совпадать с датой начала");
                return false;

            }
            return true;
        }

        private void VacationsDg_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var reqDeleteVacation = new RestRequest("/removeVacation", Method.Post);
                reqDeleteVacation.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                reqDeleteVacation.AddParameter("id", (VacationsDg.SelectedItem as Vacation).ID_Vacation);
                var resDeleteVacation = Helper.client.Post(reqDeleteVacation);
                string a = resDeleteVacation.Content;

            }
        }

        private void SickLeavesDg_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var reqDeleteSickLeave = new RestRequest("/removeSickLeave", Method.Post);
                reqDeleteSickLeave.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                reqDeleteSickLeave.AddParameter("id", (SickLeavesDg.SelectedItem as SickLeave).ID_SickLeave);
                var resDeleteSickLeave = Helper.client.Post(reqDeleteSickLeave);
                string a = resDeleteSickLeave.Content;

            }
        }
    }
}







