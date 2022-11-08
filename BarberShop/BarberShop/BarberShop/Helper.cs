using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop
{
   class Helper
    {
      public static string backupsDir= Directory.GetCurrentDirectory() + @"\backups\";
      public static RestClient client = new RestClient("http://192.168.58.74:8080/BarberApi/");
    }
}
