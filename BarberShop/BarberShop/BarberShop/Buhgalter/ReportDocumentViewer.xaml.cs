using BarberShop.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Words.NET;

namespace BarberShop.Buhgalter
{
    /// <summary>
    /// Логика взаимодействия для SupplyDocumentViewer.xaml
    /// </summary>
    public partial class ReportDocumentViewer : Page
    {
        //DocX Taxdocument ;
        public ReportDocumentViewer(TaxReport report)
        {
            InitializeComponent();
            FlowDocument document = new FlowDocument();

            Bold bold = new Bold();
            Run header = new Run("Налоговый отчет");
            bold.Inlines.Add(header);
            //  Taxdocument.InsertParagraph("Налоговый отчет").FontSize(20).Bold().Alignment=Xceed.Document.NET.Alignment.center;
            Paragraph first = new Paragraph();
            first.FontSize = 20;
            first.Inlines.Add(bold);
            first.TextAlignment = TextAlignment.Center;
            Paragraph second = new Paragraph();
            Run data = new Run("Отчет подготовил:" + report.employer.FirstName + "." + report.employer.FirstName[0] + "." + report.employer.MiddleName[0]);
            // Taxdocument.InsertParagraph("Отчет подготовил:" + report.employer.FirstName + "." + report.employer.FirstName[0] + "." + report.employer.MiddleName[0]).FontSize(15).Alignment = Xceed.Document.NET.Alignment.center;
            second.FontSize = 15;
            second.Inlines.Add(data);
            second.TextAlignment = TextAlignment.Center;
            Paragraph thrid = new Paragraph();
            thrid.Inlines.Add(new Run("Дата подготовки отчета: " + report.Date_Report));
            //  Taxdocument.InsertParagraph("Дата подготовки отчета: " + report.Date_Report).FontSize(15).Bold().Alignment = Xceed.Document.NET.Alignment.left;
            thrid.Inlines.Add(new LineBreak());
            thrid.Inlines.Add(new Run("За период от:" + report.Date_Begin + " до:" + report.Date_End));
            //  Taxdocument.InsertParagraph("За период от:" + report.Date_Begin + " до:" + report.Date_End).FontSize(15).Alignment = Xceed.Document.NET.Alignment.left;
            thrid.FontSize = 15;
            //thrid.TextAlignment = TextAlignment.Left;

            Paragraph fourth = new Paragraph();
            fourth.FontSize = 15;
            fourth.Inlines.Add(new Run("Общее количество родаж за этот период:" + report.Value_Sells));
            // Taxdocument.InsertParagraph("Общее количество родаж за этот период:" + report.Value_Sells).FontSize(15).Alignment = Xceed.Document.NET.Alignment.left;
            fourth.Inlines.Add(new LineBreak());
            fourth.Inlines.Add(new Run("Количество налогов при НДС 13%:" + report.Value_Tax));
            //  Taxdocument.InsertParagraph("Количество налогов при НДС 13%:" + report.Value_Tax).FontSize(15).Alignment = Xceed.Document.NET.Alignment.left;
            fourth.Inlines.Add(new LineBreak());
            fourth.Inlines.Add(new Run("Подпись руководителя:___________________"));
            //  Taxdocument.InsertParagraph("Подпись руководителя:___________________").FontSize(15).Alignment = Xceed.Document.NET.Alignment.left;
            SupplyDoc.Blocks.Add(first);
            SupplyDoc.Blocks.Add(second);
            SupplyDoc.Blocks.Add(new Paragraph());
            SupplyDoc.Blocks.Add(thrid);
            SupplyDoc.Blocks.Add(fourth);
            // SupplyDoc.Blocks.Add(fourth);


        }
        /// <summary>
        /// событеие нажатия кнопки печати документа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(
                       ((IDocumentPaginatorSource)richTextBox.Document).DocumentPaginator,
                       "A Flow Document");
            }
            
        }
        /// <summary>
        /// событеие нажатия кнопки сохранения документа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.Filter =
                "RTF-файл (*.rtf)|*.rtf";

            if (save.ShowDialog() == true)
            {
                // Создание контейнера TextRange для всего документа
                TextRange documentTextRange = new TextRange(
                    richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                // Если такой файл существует, он перезаписывается, 
                using (FileStream fs = File.Create(save.FileName))
                {
                    if (System.IO.Path.GetExtension(save.FileName).ToLower() == ".rtf")
                    {
                        documentTextRange.Save(fs, DataFormats.Rtf);
                    }
                }
            }
        }
    }
}
