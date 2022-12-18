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



       /// <summary>
       /// путь к папке с дампами
       /// </summary>
        public static string backupsDir= Directory.GetCurrentDirectory() + @"\backups\";
        /// <summary>
        /// объект с подключением к апи
        /// </summary>
        public static RestClient client = new RestClient("http://192.168.1.49:8080/BarberApi/");
        /// <summary>
        /// метод валидации ФИО
        /// </summary>
        /// <param name="fio"> строка для валидации</param>
        /// <returns></returns>
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
        /// <summary>
        /// Метод валидации ИНН
        /// </summary>
        /// <param name="inn"> валидируемая строка</param>
        /// <returns> возвращает результат валидации</returns>
        public static bool INNcheck(string inn)
        {
            foreach (char a in inn)
            {
                if (!Regex.IsMatch(a.ToString(), @"[0-9]"))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Метод валидации почты
        /// </summary>
        /// <param name="email">валидируемая строка</param>
        /// <returns>возвращает результат валидации</returns>
        public static bool CheckEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }
        /// <summary>
        /// Метод валидации названия должности
        /// </summary>
        /// <param name="postName">валидируемая строка</param>
        /// <returns>возвращает результат валидации</returns>
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
        /// <summary>
        /// Метод валидации нна соответствие дате
        /// </summary>
        /// <param name="date">валидируемая строка</param>
        /// <returns>возвращает результат валидации</returns>
        public static bool CheckDate(string date)
        {
            DateTime dt;
            return DateTime.TryParse(date, out dt);
        }
        /// <summary>
        /// Метод валидации пароля
        /// </summary>
        /// <param name="pass">валидируемая строка</param>
        /// <returns>возвращает результат валидации</returns>
        public static bool CheckPass(string pass)
        {
            bool check = false;
            foreach (char a in pass)
            {
                check = !Regex.IsMatch(a.ToString(), @"[а-яА-Я ]");
            }
            check = 
                    pass.Length >= 6
                    && pass.Any(char.IsLetter)
                    && pass.Any(char.IsDigit)
                    && pass.Any(char.IsPunctuation)
                    && pass.Any(char.IsLower)
                    && pass.Any(char.IsUpper);
            
            return check;
        }

    }
}
