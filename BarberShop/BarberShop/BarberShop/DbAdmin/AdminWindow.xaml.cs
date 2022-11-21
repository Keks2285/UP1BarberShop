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

                dynamic[] data = new dynamic[14];

                var req = new RestRequest("/getEmployers", Method.Get);
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var res = Helper.client.Get(req);
                //string a = res.Content;
                data[0] = JsonConvert.DeserializeObject<List<EmployeModel>>(res.Content);
                
                var reqMaterial = new RestRequest("/getMaterials", Method.Get);
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resMaterial = Helper.client.Get(reqMaterial);
                //string a = res.Content;
                data[1] = JsonConvert.DeserializeObject<List<Material>>(resMaterial.Content);

                var reqclients = new RestRequest("/getClients", Method.Get);
                reqclients.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resclients = Helper.client.Get(reqclients);
                data[2] = JsonConvert.DeserializeObject<List<Client>>(resclients.Content);

                var reqIncomes = new RestRequest("/getIncomes", Method.Get);
                reqIncomes.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resIncomes = Helper.client.Get(reqIncomes);
                data[3]  = JsonConvert.DeserializeObject<List<Income>>(resIncomes.Content);

                var reqConsumptions = new RestRequest("/getConsumptions", Method.Get);
                reqConsumptions.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resConsumptions = Helper.client.Get(reqConsumptions);
                string h = resConsumptions.Content;
                data[4] = JsonConvert.DeserializeObject<List<Consumption>>(resConsumptions.Content);

                var reqTaxReports = new RestRequest("/getTaxReports", Method.Get);
                reqTaxReports.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resTaxReports = Helper.client.Get(reqTaxReports);
                data[5] = JsonConvert.DeserializeObject<List<TaxReport>>(resTaxReports.Content);

                var reqPosts = new RestRequest("/getPosts", Method.Get);
                reqPosts.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resPosts = Helper.client.Get(reqPosts);
                data[6] = JsonConvert.DeserializeObject<List<PostEmploye>>(resPosts.Content);

                var reqService = new RestRequest("/getServices", Method.Get);
                reqService.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resService = Helper.client.Get(reqService);
                data[7] = JsonConvert.DeserializeObject<List<Service>>(resService.Content);

                var reqRecord = new RestRequest("/getRecords", Method.Get);
                reqRecord.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resRecord = Helper.client.Get(reqRecord);
                data[8] = JsonConvert.DeserializeObject<List<Record>>(resRecord.Content);

                var reqStock = new RestRequest("/getStocks", Method.Get);
                reqStock.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resStock = Helper.client.Get(reqStock);
                data[9] = JsonConvert.DeserializeObject<List<Stock>>(resStock.Content);

                var reqProvider = new RestRequest("/getProviders", Method.Get);
                reqProvider.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resProvider = Helper.client.Get(reqProvider);
                data[10] = JsonConvert.DeserializeObject<List<Provider>>(resProvider.Content);

                var reqSupply = new RestRequest("/getSupplies", Method.Get);
                reqSupply.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resSupply = Helper.client.Get(reqSupply);
                data[11] = JsonConvert.DeserializeObject<List<Supply>>(resSupply.Content);

                var reqSickLeave = new RestRequest("/getSickLeaves", Method.Get);
                reqSickLeave.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resSickLeave = Helper.client.Get(reqSickLeave);
                data[12]= JsonConvert.DeserializeObject<List<SickLeave>>(resSickLeave.Content);

                var reqVacation = new RestRequest("/getVacations", Method.Get);
                reqVacation.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resVacation = Helper.client.Get(reqVacation);
                data[13] = JsonConvert.DeserializeObject<List<Vacation>>(resVacation.Content);



                string[] filesNames = { 
                    "Employe.csv",
                    "Material.csv",
                    "Client.csv",
                    "Income.csv",
                    "Consumption.csv",
                    "TaxReport.csv",
                    "Post.csv",
                    "Service.csv",
                    "Record.csv",
                    "Stock.csv",
                    "Provider.csv",
                    "Supply.csv",
                    "SickLeave.csv",
                    "Vacation.csv"

                };
                try
                {
                    Directory.CreateDirectory(Helper.backupsDir + generatedName);
                    int counter = 0;                    foreach (var datafile in data)
                    {
                        using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\"+ filesNames[counter], false, Encoding.GetEncoding("utf-8")))
                        {
                            var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                            {
                                HasHeaderRecord = false,
                                Delimiter = ";",
                            };
                            using (var csv = new CsvWriter(writer, csvConfig))
                            {
                                csv.WriteField(1);
                                csv.NextRecord();
                                csv.WriteRecords(datafile);
                            }

                        }
                        counter++;
                    }

                    //перепиши на foreach а то выглядит убого, но пока доделываешь не трогай
                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Employe.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {
                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataEmployers);
                    //    }

                    //}


                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Income.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {
                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataIncomes);
                    //    }

                    //}

                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Consumption.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {
                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataConsumptions);
                    //    }

                    //}

                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Material.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {
                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataMaterial);
                    //    }

                    //}


                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\TaxReport.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {
                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataTaxReports);
                    //    }

                    //}


                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Post.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {
                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataPosts);
                    //    }

                    //}
                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Client.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {

                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataClients);
                    //    }

                    //}
                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Service.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {

                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataService);
                    //    }

                    //}

                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName+ "\\Record.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {

                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataRecord);
                    //    }

                    //}

                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Stock.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {

                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataStock);
                    //    }

                    //}


                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Provider.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {

                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataProvider);
                    //    }

                    //}

                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Supply.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {

                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataSupply);
                    //    }

                    //}

                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\SickLeave.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {

                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataSickLeave);
                    //    }

                    //}

                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Vacation.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {

                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteField(1);
                    //        csv.NextRecord();
                    //        csv.WriteRecords(dataVacation);
                    //    }

                    //}

                    //using (var writer = new StreamWriter(Helper.backupsDir + generatedName + "\\Income.csv", false, Encoding.GetEncoding("utf-8")))
                    //{
                    //    var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                    //    {

                    //        HasHeaderRecord = false,
                    //        Delimiter = ";",
                    //    };
                    //    using (var csv = new CsvWriter(writer, csvConfig))
                    //    {
                    //        csv.WriteRecords(dataVacation);
                    //    }

                    //}
                    MessageBox.Show("Бэкап успешно сохранен");
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

            //use foreach
            req.AddFile("Employers", Helper.backupsDir + BackupsList.SelectedItem.ToString()+ "\\Employe.csv");
            req.AddFile("Records", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Record.csv");
            req.AddFile("Stocks", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Stock.csv");
            req.AddFile("Posts", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Post.csv");
            req.AddFile("Stocks", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Stock.csv");
            req.AddFile("Incomes", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Income.csv");
            req.AddFile("Clients", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Client.csv");
            req.AddFile("Providers", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Provider.csv");
            req.AddFile("SickLeaves", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\SickLeave.csv");
            req.AddFile("Consumptions", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Consumption.csv");
            req.AddFile("TaxReports", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\TaxReport.csv");
            req.AddFile("Vacations", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Vacation.csv");
            req.AddFile("Supplies", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Supply.csv");
            req.AddFile("Services", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Service.csv");
            req.AddFile("Materials", Helper.backupsDir + BackupsList.SelectedItem.ToString() + "\\Material.csv");

            var res = Helper.client.Post(req);
            string a = res.Content;
            
            
            
            dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
            



        }
    }
}
