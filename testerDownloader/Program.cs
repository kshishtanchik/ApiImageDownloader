using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace testerDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultPath = ConfigurationManager.AppSettings.Get("apiUrl");
            string exit= "";
            while (exit!="нет")
            {
                Console.Clear();
                Console.WriteLine("Введите через запятую параметры (URL_адресс,Количество_изображений,Количество_потоков)");
                string inputstr = Console.ReadLine();
                var parametrs = inputstr.Split(',');
                if (string.IsNullOrEmpty(defaultPath))
                    throw new ArgumentNullException("В наатройках не задан apiUrl");
                string requestUrl = string.Format("{0}?url={1}&imageCount={2}&threadCount={3}", defaultPath,parametrs[0],parametrs[1],parametrs[2]);

                var client = new HttpClient().GetStringAsync(requestUrl).Result;
                Console.WriteLine(client);
                Console.WriteLine("Повторить? (нет - выход из программы)");
                exit = Console.ReadLine();
            }
        }
    }
}
