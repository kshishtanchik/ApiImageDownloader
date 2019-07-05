using ImageDownloader.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web.Helpers;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

namespace ImageDownloader.Controllers
{
    class ImgInfo
    {
        public string Url { get; set; }
        public string Alt { get; set; }
    }


    public class ValuesController:ApiController 
    {
        string defaultPath = ConfigurationManager.AppSettings.Get("downloadPath");
        string targetUrl= "http://localhost:61030/";
        private string domainPattern = @"(^http)*\S*\.\w+[\/\\]{1}";
        JavaScriptSerializer serializer = new JavaScriptSerializer();
              
        /// <summary>
        /// Получить с заданного адреса нужное количество картинок в требуемом количестве потоков
        /// </summary>
        /// <param name="url"></param>
        /// <param name="imageCount"></param>
        /// <param name="threadCount"></param>
        /// <returns></returns>
        public async Task<string> GetImages([FromUri] string url,[FromUri] string imageCount, [FromUri]string threadCount)
        {
            ImageRequest imageRequest = new ImageRequest(url, imageCount, threadCount);
            if (!imageRequest.isValid)
                return serializer.Serialize(imageRequest);                

           if (string.IsNullOrWhiteSpace(defaultPath))
               defaultPath = Environment.CurrentDirectory;

            targetUrl = imageRequest.TargetUrl;
            
           
            var downloadPath= Path.Combine(defaultPath, imageRequest.Uri.DnsSafeHost.Trim('\\', '/', '<', '>', ':','*','?','|'));
            if (!Directory.Exists(downloadPath))
                Directory.CreateDirectory(downloadPath);

            string pageStringContent = await new HttpClient().GetStringAsync(targetUrl);
            var parser = new HtmlParser(pageStringContent);
            var tags = parser.GetTags("img").Take(imageRequest.GetImageCount);
            var pageImages = new List<ImgInfo>();
            ThreadPool.SetMaxThreads(imageRequest.GetThradCount, imageRequest.GetThradCount);
            foreach (var item in tags)
            {
                var downloadTarget = item.getAttributeContent("src");
                string domainPattern = @"(^http){0,1}(\S*\.{1}\w+\/)|(localhost:\d+)";
                if (!Regex.IsMatch(downloadTarget, domainPattern))
                    downloadTarget = imageRequest.getDomain + downloadTarget;
                pageImages.Add(
                            new ImgInfo
                            {
                                Alt = item.getAttributeContent("alt"),
                                Url = downloadTarget
                            });
                ThreadPool.QueueUserWorkItem(
                   new WaitCallback(state =>
                       new WebClient().DownloadFile(downloadTarget, downloadPath)));
            }
            
            return serializer.Serialize(pageImages);
        }
    }
} 
