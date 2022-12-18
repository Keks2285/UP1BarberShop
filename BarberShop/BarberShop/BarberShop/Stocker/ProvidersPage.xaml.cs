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
        ///  список складов
        /// </summary>

        private BindingList<Stock> _stocks = new BindingList<Stock>();
        /// <summary>
        /// список поставщиков
        /// </summary>
        private BindingList<Provider> _providers = new BindingList<Provider>();
        /// <summary>
        /// выбранный поставщик
        /// </summary>
        private Provider _selectedProvider;
        /// <summary>
        /// выбранный склад
        /// </summary>
        private Stock _selectedStock;
        public ProvidersPage()
        {
            InitializeComponent();
            _stocks.ListChanged += _stocks_ListChanged;
            _providers.ListChanged += _providers_ListChanged;
        }
        /// <summary>
        /// событеие изменеия списка поставщиков
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void _providers_ListChanged(object? sender, ListChangedEventArgs e)
        {
            
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                    var req = new RestRequest("/updateProvider", Method.Post);
                    req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    req.AddParameter("id", _selectedProvider.ID_Provider);
                    req.AddParameter("address", _selectedProvider.Adres);
                    req.AddParameter("name", _selectedProvider.Name_Provider);
                    req.AddParameter("inn", _selectedProvider.INN);
                    var res = Helper.client.Post(req);
                
            }
        }
        /// <summary>
        /// событеие изменеия списка складов
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void _stocks_ListChanged(object? sender, ListChangedEventArgs e)
        {
            var searchStockByAdress = _stocks.FirstOrDefault(item => item.Adres == AdresStockTb.Text);
            if (searchStockByAdress != null)
            {
                MessageBox.Show("Склад уже существует");
                 return;
            }

            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                var req = new RestRequest("/updateStock", Method.Post);
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                req.AddParameter("id", _selectedStock.ID_Stock);
                req.AddParameter("address", _selectedStock.Adres);
                var res = Helper.client.Post(req);
            }
        }
        /// <summary>
        /// событеие загрузки страницы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _stocks.Clear();
            _providers.Clear();
            Provider.AllINN.Clear();
            Provider.allNames.Clear();
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
        /// <summary>
        /// событеие нажатия кнопки добавления поставщика
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void ProviderCreateBtn_Click(object sender, RoutedEventArgs e)
        {

            if (AdresProviderTb.Text.Length < 4)
            {
                MessageBox.Show("Адрес слишком короткий");
                return;
            }
            if (NameTb.Text.Length < 2)
            {
                MessageBox.Show("Название слишком короткое");
                return;
            }
            if (InnTb.Text.Contains("_")){
                MessageBox.Show("Некорректный ИНН");
                return;
            }
            if (Provider.AllINN.Contains(InnTb.Text)){
                MessageBox.Show("ИНН уже используется");
                return;
            }
            if (Provider.allNames.Contains(NameTb.Text))
            {
                MessageBox.Show("Такой поставщик уже есть");
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
        /// <summary>
        /// Метод послыающий запрос к апи на создание
        /// </summary>
        /// <param name="parametres"> словарь параметров</param>
        /// <param name="request"> строка-метод который должна выполнить апи</param>
        /// <returns></returns>
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
        /// <summary>
        /// событеие нажатия кнопки добавления склада
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void CreatStockBtn_Click(object sender, RoutedEventArgs e)
        {

            if (AdresStockTb.Text.Length < 4)
            {
                MessageBox.Show("Адрес склада слишком короткий");
                return;
            }

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
        /// <summary>
        /// событие возникащее до нажатияна клаывишу
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
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
        /// <summary>
        /// событеие выбора поставщика
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void ProvidersDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedProvider = (Provider)ProvidersDg.SelectedItem;
        }
        /// <summary>
        /// событие возникащее до нажатияна клаывишу
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
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
        /// <summary>
        /// событеие выбора склада
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void StockDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedStock = (Stock)StockDg.SelectedItem;
        }

    }
}
