using BarberShop.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
using System.Windows.Shapes;

namespace BarberShop.DbAdmin
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        string generatedName;
        public AdminWindow()
        {
            InitializeComponent();
            if (!Directory.Exists(Helper.backupsDir))
                Directory.CreateDirectory(Helper.backupsDir);
            var directories = Directory.GetDirectories(Helper.backupsDir);
            foreach (var dir in directories)
            {
                BackupsList.Items.Add(new DirectoryInfo(dir).Name);
            }
            
        }

        private void BackupsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            //Удаление элементов ссписка
            //if(BackupsList.SelectedValue!=null)
            //MessageBox.Show(BackupsList.SelectedValue.ToString());
            //BackupsList.Items.Remove(BackupsList.SelectedItem);
        }

        private async void CreatePoint_Click(object sender, RoutedEventArgs e)
        {
             generatedName = DateTime.Now.ToString().Replace(":", ".");
            

            BackupsList.Items.Add(new DirectoryInfo(generatedName).Name);
            await Task.Run(() =>
            {
               // List<EmployeModel> _employers = new List<EmployeModel>();
                var req = new RestRequest("/getEmployers", Method.Get);
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var res = Helper.client.Get(req);
                List<EmployeModel> dataEmployers = JsonConvert.DeserializeObject<List<EmployeModel>>(res.Content);

                var reqclients = new RestRequest("/getClients", Method.Get);
                reqclients.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resclients = Helper.client.Get(reqclients);
                List<Client> dataClients = JsonConvert.DeserializeObject<List<Client>>(resclients.Content);

                var reqPosts = new RestRequest("/getPosts", Method.Get);
                reqPosts.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resPosts = Helper.client.Get(reqPosts);
                List<PostEmploye> dataPosts = JsonConvert.DeserializeObject<List<PostEmploye>>(resPosts.Content);

                var reqService = new RestRequest("/getServices", Method.Get);
                reqService.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resService = Helper.client.Get(reqService);
                List<Service> dataService = JsonConvert.DeserializeObject<List<Service>>(resService.Content);

                var reqRecord = new RestRequest("/getRecords", Method.Get);
                reqService.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resRecord = Helper.client.Get(reqRecord);
                List<Record> dataRecord = JsonConvert.DeserializeObject<List<Record>>(resRecord.Content);

                var reqStock = new RestRequest("/getStocks", Method.Get);
                reqStock.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resStock = Helper.client.Get(reqStock);
                List<Stock> dataStock = JsonConvert.DeserializeObject<List<Stock>>(resStock.Content);

                var reqProvider = new RestRequest("/getProviders", Method.Get);
                reqProvider.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resProvider = Helper.client.Get(reqProvider);
                List<Provider> dataProvider = JsonConvert.DeserializeObject<List<Provider>>(resProvider.Content);

                var reqSupply = new RestRequest("/getSupplies", Method.Get);
                reqSupply.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resSupply = Helper.client.Get(reqSupply);
                List<Supply> dataSupply = JsonConvert.DeserializeObject<List<Supply>>(resSupply.Content);

                var reqSickLeave = new RestRequest("/getSickLeaves", Method.Get);
                reqSickLeave.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resSickLeave = Helper.client.Get(reqSickLeave);
                List<SickLeave> dataSickLeave = JsonConvert.DeserializeObject<List<SickLeave>>(resSickLeave.Content);

                var reqVacation = new RestRequest("/getVacations", Method.Get);
                reqVacation.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resVacation = Helper.client.Get(reqVacation);
                List<Vacation> dataVacation = JsonConvert.DeserializeObject<List<Vacation>>(resVacation.Content);

                try
                {
                    Directory.CreateDirectory(Helper.backupsDir + generatedName);
                   
                    //перепиши на foreach а то выглядит убого, но пока работает не трогай
                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Employe.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {
                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataEmployers);
                        }

                    }
                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Post.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {
                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataPosts);
                        }

                    }
                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Client.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {

                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataClients);
                        }

                    }
                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Service.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {

                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataService);
                        }

                    }

                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName+ "\\Record.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {

                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataRecord);
                        }

                    }

                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Stock.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {

                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataStock);
                        }

                    }


                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Provider.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {

                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataProvider);
                        }

                    }

                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Supply.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {

                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataSupply);
                        }

                    }

                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\SickLeave.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {

                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataSickLeave);
                        }

                    }

                    using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Vacation.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {

                            HasHeaderRecord = false,
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(dataVacation);
                        }

                    }
                    MessageBox.Show("Данные о сотрудниках успешно экспортированы");
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так, возможно файл уже используется другим процессом");
                }


                



            });


              
        }

        private void DeletePoint_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private async void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {

                if (BackupsList.SelectedItem == null)
                {
                    MessageBox.Show("Не выбран элемент для удаления");
                    return;
                }

                
                    string path = Helper.backupsDir + BackupsList.SelectedItem.ToString();
                    MessageBoxButton button = MessageBoxButton.YesNoCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result;
                    result = MessageBox.Show("Вы уверены, что хотите удалить точку восстановления?", "Предупреждение", button, icon, MessageBoxResult.Yes);
                    try
                    {
                        await Task.Run(() =>
                        {
                            try {
                                Directory.Delete(path, true);
                            } catch { }
                            
                        });
                        if (result == MessageBoxResult.Yes)
                            BackupsList.Items.Remove(BackupsList.SelectedItem);
                    }
                    catch
                    {
                        MessageBox.Show("Не  удалось удалить бэкап возможно файл уже открыт другим приложенем");
                    }

                
               



            }
        }

        private void ImportPoint_Click(object sender, RoutedEventArgs e)
        {
            if (BackupsList.SelectedItem == null)
            {
                MessageBox.Show("Выберите точку сохранения");
                return;
            }
            var req = new RestRequest("/executeBackup", Method.Post);
            req.AddHeader("Content-Type", "multipart/form-data;");


            var files = Directory.GetDirectories(Helper.backupsDir+BackupsList.SelectedItem.ToString());
            // string
            // foreach (var file in files)
            // {
            //      req.AddFile("Employers", file);
            //  }

            req.AddFile("Employers", Helper.backupsDir + BackupsList.SelectedItem.ToString()+ "\\Employe.csv");
            req.AddFile("Reqords", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Record.csv");
            var res = Helper.client.Post(req);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
            



        }
    }
}
