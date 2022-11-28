using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Annotation = System.ComponentModel.DataAnnotations;
using System.Windows;

namespace BarberShop
{
   class Helper
    {
      public static string backupsDir= Directory.GetCurrentDirectory() + @"\backups\";
      public static RestClient client = new RestClient("http://192.168.1.49:8080/BarberApi/");

        public static bool ValidData(Object obj)
        {
            var results = new List<Annotation.ValidationResult>();
            var context = new ValidationContext(obj);
            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                string message = "";
                foreach (var error in results)
                {
                    message += error.ErrorMessage + '\n';
                }
                MessageBox.Show(message);
                return false;
            }
            return true;
        }
    }
}
