using BarberShop.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace BarberShop.Stocker
{
    /// <summary>
    /// Логика взаимодействия для MaterialsPage.xaml
    /// </summary>
    public partial class SuppliesPage : Page
    {
        private static readonly Regex onlyNumbers = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private BindingList<Supply> _supplies = new BindingList<Supply>();
        public SuppliesPage()
        {
            InitializeComponent();
            _supplies.ListChanged += _supplies_ListChanged;
        }

        private void _supplies_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                var req = new RestRequest("/updateSupply", Method.Post);
                Supply supply = (Supply)SuppliesDg.SelectedItem;
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                req.AddParameter("date", supply.Date_Supply);
                req.AddParameter("value", supply.Value);
                req.AddParameter("provier_id", supply.Provider_ID);
                req.AddParameter("id", supply.ID_Supply);
                var res = Helper.client.Post(req);
                dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
                MessageBox.Show("Данные изменены");
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Provider.allNames.Clear();
            Provider.AllINN.Clear();
            if (Supply.Providers.Count > 0) Supply.Providers.Clear();

            var reqProviders = new RestRequest("/getProviders", Method.Get);
            reqProviders.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resProviders = Helper.client.Get(reqProviders);
            List<Provider> dataProviders = JsonConvert.DeserializeObject<List<Provider>>(resProviders.Content);

            foreach( Provider provider in dataProviders)
                Supply.Providers.Add(provider);
            ProvidersDg.ItemsSource = Supply.Providers;

            var reqSupply = new RestRequest("/getSupplies", Method.Get);
            reqSupply.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resSupply = Helper.client.Get(reqSupply);
            List<Supply> datasupply = JsonConvert.DeserializeObject<List<Supply>>(resSupply.Content);

            foreach (Supply supply in datasupply)
            {
                supply.selectedProvider = Supply.Providers[supply.Provider_ID-1];
                _supplies.Add(supply);
            }
            SuppliesDg.ItemsSource= _supplies;
        }

        private void DocumentViewBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SupplyDocumentViewer(SuppliesDg.SelectedItem as Supply));
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !onlyNumbers.IsMatch(text);
        }

        private void SupplyAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DateSupply.Text == "")
            {
                MessageBox.Show("Введите дату поставки");
                return;
            }
            if ( Convert.ToInt32(Price.Text) < 0)
            {
                MessageBox.Show("Сумма должжна быть больше 0");
                return ;
            }
            if (ProvidersDg.SelectedItem == null)
            {
                MessageBox.Show("Выберите поставщика");
                return;
            }
            var req = new RestRequest("/createSupply", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("date_supply", DateSupply.SelectedDate!.Value.ToString("yyyy.MM.dd"));
            req.AddParameter("value", Price.Text);
            req.AddParameter("provider_id", (ProvidersDg.SelectedItem as Provider).ID_Provider);
            var res = Helper.client.Post(req);
            dynamic reqContent = JsonConvert.DeserializeObject<dynamic>(res.Content);
            _supplies.Add(new Supply
            {
                ID_Supply= reqContent.id,
                Date_Supply = DateSupply.SelectedDate!.Value.ToString("yyyy-MM-dd"),
                Value = Convert.ToInt32(Price.Text),
                selectedProvider = (ProvidersDg.SelectedItem as Provider),
                Provider_ID = (ProvidersDg.SelectedItem as Provider).ID_Provider
            });
        }

        private void SuppliesDg_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Delete)
            {
                var reqDeleteSupply = new RestRequest("/removeSupply", Method.Post);
                reqDeleteSupply.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                reqDeleteSupply.AddParameter("id_supply", (SuppliesDg.SelectedItem as Supply).ID_Supply);
                var resDeleteSupply = Helper.client.Post(reqDeleteSupply);
                string a = resDeleteSupply.Content;

            }
        }
    }
}
