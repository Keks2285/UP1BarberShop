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

namespace BarberShop.Stocker
{
    /// <summary>
    /// Логика взаимодействия для SuppliesPage.xaml
    /// </summary>
    public partial class ProvidersPage : Page
    {


        /// <summary>
        /// надо доделать валидацию и изменение
        /// </summary>

        private BindingList<Stock> _stocks = new BindingList<Stock>();
        private BindingList<Provider> _providers = new BindingList<Provider>();
        private Provider _selectedProvider;
        private Stock _selectedStock;
        public ProvidersPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var reqStock = new RestRequest("/getStocks", Method.Get);
            reqStock.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resStock = Helper.client.Get(reqStock);
            List<Stock> dataStock = JsonConvert.DeserializeObject<List<Stock>>(resStock.Content);

            foreach (var stock in dataStock)
            {
                _stocks.Add(stock);
            }

            StockDg.ItemsSource = _stocks;


            var reqProviders = new RestRequest("/getProviders", Method.Get);
            reqProviders.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resProviders = Helper.client.Get(reqProviders);
            List<Provider> dataProviders = JsonConvert.DeserializeObject<List<Provider>>(resProviders.Content);

            foreach (var provider in dataProviders)
            {
                _providers.Add(provider);
            }
            ProvidersDg.ItemsSource = _providers;

        }

        private void ProviderCreateBtn_Click(object sender, RoutedEventArgs e)
        {

            if (InnTb.Text.Contains("_")){
                MessageBox.Show("некорректный ИНН");
                return;
            }
            if (Provider.AllInn.Contains(InnTb.Text)){
                MessageBox.Show("ИНН уже используется");
                return;
            }


            var stockParam = new Dictionary<string, string>()
            {
                ["adres"] = AdresProviderTb.Text,
                ["name_provider"]=NameTb.Text,
                ["inn"]=InnTb.Text
            };
            dynamic reqResult = createRequest(stockParam, "/createProvider");
            _providers.Add(new Provider
            {
                Adres = AdresProviderTb.Text,
                ID_Provider = reqResult.id,
                Name_Provider = NameTb.Text,
                INN = InnTb.Text
            });
        }

       
        private dynamic createRequest( Dictionary<string, string> parametres, string request)
        {
            var req = new RestRequest(request, Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            foreach( var parametr in parametres)
                req.AddParameter(parametr.Key,parametr.Value);
            var res = Helper.client.Post(req);
           return JsonConvert.DeserializeObject<dynamic>(res.Content);
           // MessageBox.Show("Должность создана");
        }

        private void CreatStockBtn_Click(object sender, RoutedEventArgs e)
        {

            var stockParam = new Dictionary<string, string>()
            {
                ["adres"]=AdresStockTb.Text
            };
            dynamic reqResult = createRequest(stockParam, "/createStock");
            _stocks.Add(new Stock
            {
                Adres = AdresStockTb.Text,
                ID_Stock  = reqResult.id
            });
        }

        private void ProvidersDg_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите удалить поставщика, это приведет к удалению\n всех связанных с ним данных.\n Продолжить?", "Предупреждение", button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    var reqDeleteEmployer = new RestRequest("/removeProvider", Method.Post);
                    reqDeleteEmployer.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    reqDeleteEmployer.AddParameter("id_provider", (ProvidersDg.SelectedItem as Provider).ID_Provider);
                    var resDeleteEmployer = Helper.client.Post(reqDeleteEmployer);
                    _providers.Remove(_selectedProvider);
                }
                else
                {
                    // _employers.AddNew();
                    _providers.Add(_selectedProvider);
                    return;
                }
            }
        }

        private void ProvidersDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedProvider = (Provider)ProvidersDg.SelectedItem;
        }

        private void StockDg_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Delete)
            {
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите удалить склад, это приведет к удалению\n всех связанных с ним данных.\n Продолжить?", "Предупреждение", button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    var reqDeleteEmployer = new RestRequest("/removeStock", Method.Post);
                    reqDeleteEmployer.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    reqDeleteEmployer.AddParameter("id_stock", (StockDg.SelectedItem as Stock).ID_Stock);
                    var resDeleteEmployer = Helper.client.Post(reqDeleteEmployer);
                    _stocks.Remove(_selectedStock);
                }
                else
                {
                    // _employers.AddNew();
                    _stocks.Add(_selectedStock);
                    return;
                }
            }


        }

        private void StockDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedStock = (Stock)StockDg.SelectedItem;
        }
    }
}
