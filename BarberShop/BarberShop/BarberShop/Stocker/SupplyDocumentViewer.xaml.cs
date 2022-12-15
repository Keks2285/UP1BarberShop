using BarberShop.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для SupplyDocumentViewer.xaml
    /// </summary>
    public partial class SupplyDocumentViewer : Page
    {
        public SupplyDocumentViewer(Supply  supply)
        {
            InitializeComponent();
            //FlowDocument document = new FlowDocument();
           
            Bold bold = new Bold();
            Run header = new Run("Договор о поставке материалов");
            bold.Inlines.Add(header);
           
            Paragraph first = new Paragraph();
            first.Inlines.Add(bold);
            first.TextAlignment = TextAlignment.Center;
            Paragraph second = new Paragraph();
            Run data = new Run("Поставщик: "+supply.selectedProvider.Name_Provider+" ИНН:"+ supply.selectedProvider.INN+" Адрес:"+ supply.selectedProvider.Adres);
            second.Inlines.Add(data);
            second.TextAlignment = TextAlignment.Center;
            Paragraph thrid = new Paragraph();
            thrid.Inlines.Add(new Run("Дата договора:" + supply.Date_Supply + "   На сумму:" + supply.Value));
            thrid.TextAlignment = TextAlignment.Left;

            Paragraph fourth = new Paragraph();
            fourth.Inlines.Add(new Run("Подпись потавщика:________                  Подпись Получателя:________"));

            SupplyDoc.Blocks.Add(first);
            SupplyDoc.Blocks.Add(second);
            SupplyDoc.Blocks.Add(new Paragraph());
            SupplyDoc.Blocks.Add(thrid);
            SupplyDoc.Blocks.Add(fourth);


        }
    }
}
