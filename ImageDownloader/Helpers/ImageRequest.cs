using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ImageDownloader.Helpers
{
    /// <summary>
    /// Пааметры для скачивания 
    /// </summary>
    public class ImageRequest
    {
        string targetUrl;
        int imageCount,
            threadCount;

        

        private string domainPattern = @"(^http){0,1}(\S*\.{1}\w+\/)|(localhost:\d+)";
        public List<string> ErrorMessages { get; private set; }


        public ImageRequest(string targetUrl,
                            string imageCount,
                            string threadCount)
        {
            ErrorMessages = new List<string>();
            TargetUrl = targetUrl;
            SetImageCount = imageCount;
            SetThreadCount = threadCount;
        }

        public Uri Uri { get; private set; }

        public string TargetUrl
        {
            get => targetUrl;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorMessages.Add(string.Format("Не задан параметр - Целевой URL адресс. заданое значение {0}", value));
                    return;
                }

                Uri = new Uri(value,UriKind.RelativeOrAbsolute);
                var isvalid = Regex.IsMatch(value, domainPattern,RegexOptions.IgnoreCase);
                if (isvalid)
                    targetUrl = value;
                else
                    ErrorMessages.Add(string.Format("Параметр - Целевой URL адресс не соответствует формату. заданое значение {0}",value));
            }
        }
        private string SetImageCount
        {
            set
            {
                if (!int.TryParse(value, out int count))
                {
                    ErrorMessages.Add(string.Format(
                        "Введенное значение \"{0}\" (количство изображений) не соответствует формату",
                        value));
                    return;
                }
                imageCount = count;
            }
        }
        /// <summary>
        /// Количество изображений
        /// </summary>
        public int GetImageCount => imageCount;

        /// <summary>
        /// Количество потоков
        /// </summary>
        public int GetThradCount => threadCount;

        private string SetThreadCount
        {
            set
            {
                if (!int.TryParse(value, out int count))
                {
                    ErrorMessages.Add(string.Format(
                        "Введенное значение \"{0}\" (количство количество потоков) не соответствует формату",
                        value));
                    return;
                }
                threadCount = count;
            }
        }
        /// <summary>
        /// Корректные данные
        /// </summary>
        public bool isValid
        {
            get
            {
                if (ErrorMessages == null || ErrorMessages.Count == 0)
                    return true;
                return false;
            }
        }
        public string getDomain { get
            {
                return Regex.Match(targetUrl, domainPattern, RegexOptions.IgnoreCase).Value;
            }
        }

    }
}