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
    /// Логика взаимодействия для MaterialsPage.xaml
    /// </summary>
    public partial class MaterialsPage : Page
        {/// <summary>
         ///  список складов
         /// </summary>
        private BindingList<Stock> _stocks = new BindingList<Stock>();
        /// <summary>
        /// список поставок
        /// </summary>
        private BindingList<Supply> _supplies = new BindingList<Supply>();
        /// <summary>
        /// список поставщиков
        /// </summary>
        private BindingList<Provider> _providers = new BindingList<Provider>();
        /// <summary>
        /// список материалов
        /// </summary>
        private BindingList<Material> _materials = new BindingList<Material>();
        public MaterialsPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// событеие нажатия кнопки создания материала
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void MaterialCreateBtn_Click(object sender, RoutedEventArgs e)
        {
            var req = new RestRequest("/createMaterial", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("name_material", NameTb.Text);
            req.AddParameter("stock_id", (StockDg.SelectedItem as Stock).ID_Stock);
            req.AddParameter("supply_id", (SuppliesDg.SelectedItem as Supply).ID_Supply);
            var res = Helper.client.Post(req);
        }
        /// <summary>
        /// событеие загрузки сраницы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _stocks.Clear();
            _providers.Clear();
            Provider.AllINN.Clear();
            Provider.allNames.Clear();
            Material.Stocks.Clear();
            
            var reqStock = new RestRequest("/getStocks", Method.Get);
            reqStock.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resStock = Helper.client.Get(reqStock);
            List<Stock> dataStock = JsonConvert.DeserializeObject<List<Stock>>(resStock.Content);

            foreach (var stock in dataStock)
            {
                Material.Stocks.Add(stock);
                _stocks.Add(stock);
            }
            var reqProviders = new RestRequest("/getProviders", Method.Get);
            reqProviders.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resProviders = Helper.client.Get(reqProviders);
            List<Provider> dataProviders = JsonConvert.DeserializeObject<List<Provider>>(resProviders.Content);

            foreach (var provider in dataProviders)
            {
                _providers.Add(provider);
            }


            var reqMaterials= new RestRequest("/getMaterials", Method.Get);
            reqMaterials.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resMaterials = Helper.client.Get(reqMaterials);
            List<Material> dataMaterials = JsonConvert.DeserializeObject<List<Material>>(resMaterials.Content);

            foreach (Material material in dataMaterials)
            {
                material.stock= _stocks.FirstOrDefault(item => item.ID_Stock == material.Stock_ID);
                material.supply = _supplies.FirstOrDefault(item => item.ID_Supply == material.Supply_ID);
                _materials.Add(material);
            }


            var reqSupplies = new RestRequest("/getSupplies", Method.Get);
            reqSupplies.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resSupplies = Helper.client.Get(reqSupplies);
            List<Supply> dataSupplies = JsonConvert.DeserializeObject<List<Supply>>(resSupplies.Content);

            foreach (Supply supply in dataSupplies)
            {
                supply.selectedProvider = _providers.FirstOrDefault(item => item.ID_Provider == supply.Provider_ID);
                _supplies.Add(supply);
            }


            SuppliesDg.ItemsSource = _supplies;
            MaterialsDg.ItemsSource= _materials;
           // ProvidersDg.ItemsSource = _providers;
            StockDg.ItemsSource = _stocks;
        }
        /// <summary>
        /// событеие возникающее до нажатия кнопки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void MaterialsDg_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var reqDeleteSickLeave = new RestRequest("/removeMaterial", Method.Post);
                reqDeleteSickLeave.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                reqDeleteSickLeave.AddParameter("id_material", (MaterialsDg.SelectedItem as Material).ID_Material);
                var resDeleteSickLeave = Helper.client.Post(reqDeleteSickLeave);
                string a = resDeleteSickLeave.Content;

            }
        }
    }
}
