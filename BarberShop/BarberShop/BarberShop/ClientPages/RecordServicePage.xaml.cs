using BarberShop.Models;
using MailKit.Net.Smtp;
using MimeKit;
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

namespace BarberShop.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для RecordServicePage.xaml
    /// </summary>
    public partial class RecordServicePage : Page
    {
        DateTime recortDateTime = new DateTime();
        private BindingList<Service> _services = new BindingList<Service>();
        int clientId = 0;
        public RecordServicePage(int idClient)
        {
            clientId = idClient;
            InitializeComponent();
        }
        /// <summary>
        /// событеие нажатия кнопки созданимя записи на услугу
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void RecordBtn_Click(object sender, RoutedEventArgs e)
        {

            if (DateRecord.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату записи");
                return;
            }
            if (TimeRecord.SelectedTime == null)
            {
                MessageBox.Show("Выберите время записи");
                return;
            }
            if (TimeRecord.SelectedTime == null)
            {
                MessageBox.Show("Выберите время записи");
                return;
            }
            if (ServicesDg.SelectedItem == null)
            {
                MessageBox.Show("Выберите услугу");
                return;
            }

            recortDateTime = DateRecord.SelectedDate!.Value;
            recortDateTime.AddHours(TimeRecord.SelectedTime.Value.Hour);
            recortDateTime.AddHours(TimeRecord.SelectedTime.Value.Minute);
            var req = new RestRequest("/createRecord", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("date_record", recortDateTime.ToString("yyyy.MM.dd hh:mm"));
            req.AddParameter("client_id", clientId);
            req.AddParameter("service_id", (ServicesDg.SelectedItem as Service).ID_Service);
            var res = Helper.client.Post(req);
            SendMessge("ilion23082003@gmail.com");
        }
        /// <summary>
        /// событеие загрузки страницы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var reqServices = new RestRequest("/getServices", Method.Get);
            reqServices.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resService = Helper.client.Get(reqServices);
            List<Service> dataService= JsonConvert.DeserializeObject<List<Service>>(resService.Content);

            foreach (var service in dataService)
            {
              //  Material.Stocks.Add(stock);
                _services.Add(service);
            }
            ServicesDg.ItemsSource = _services;
            
        }

        /// <summary>
        /// метод отпраки письмаа на почту
        /// </summary>
        /// <param name="Email">почта на которую приходит письмо</param>
        private async void SendMessge(String Email)
        {
            // await Task.Run(() =>
            //  {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("BarberShop", "magizin451@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("Запись на прием", Email));
                emailMessage.Subject = "Восстановление";
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = $"Уважаемый клиент, благодраим вас за оформление записи на услугу: {(ServicesDg.SelectedItem as Service).Name_Service}" +"\n" +
                           $"Ждем вас на прием  {recortDateTime.ToString("yyyy.MM.dd hh:mm")}"
                };
                using (var SMTPclient = new SmtpClient())
                {
                    //SMTPclient.is
                    SMTPclient.Connect("smtp.gmail.com", 587);  //587 465 25

                    //SMTPclient.UseDefaultCredentials = false;
                    //SMTPclient.Cre
                    SMTPclient.Authenticate("magizin451@gmail.com", "sgxlffaunvcfgxnr"); //NewMyPass222 

                    SMTPclient.Send(emailMessage);
                    SMTPclient.Disconnect(true);
                    MessageBox.Show("Запись на услугу оформлена, письмо о записи отправлено на почту");
                }

            }
            catch
            {
                MessageBox.Show("Проблемы с сетью, попробуйте позже");
            }


            //   });

        }

    }
}
