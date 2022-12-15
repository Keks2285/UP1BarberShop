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
using System.Text.RegularExpressions;

namespace BarberShop
{
   class Helper
    {



        public  enum DateComparisonResult
        {
            Earlier = -1,
            Later = 1,
            TheSame = 0
        };
        public static string backupsDir= Directory.GetCurrentDirectory() + @"\backups\";
        public static RestClient client = new RestClient("http://192.168.1.49:8080/BarberApi/");
        public static bool CheckFIO(string fio)
        {
            foreach (char a in fio)
            {
                if (!Regex.IsMatch(a.ToString(), @"[а-яА-Я]") && (int)a != 65279) 
                {
                    return false;
                }
            }
            return true;
        }
        public static bool INNcheck(string fio)
        {
            foreach (char a in fio)
            {
                if (!Regex.IsMatch(a.ToString(), @"[0-9]"))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CheckEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }
        public static bool CheckPostName(string postName)
        {
            foreach (char a in postName)
            {
                if (!Regex.IsMatch(a.ToString(), @"[а-яА-Я ]")&& a!=' ')
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CheckDate(string date)
        {
            DateTime dt;
            return DateTime.TryParse(date, out dt);
        }


    }
}
