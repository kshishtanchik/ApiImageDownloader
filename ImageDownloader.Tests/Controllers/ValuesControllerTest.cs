using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageDownloader;
using ImageDownloader.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ImageDownloader.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public async Task GetAsync()
        {
            // Упорядочение
            ValuesController controller = new ValuesController();

            // Действие
            var result = await controller.GetImages("http://localhost:61030/","2","3");

            // Утверждение
            Assert.IsNotNull(result);
            //Assert.AreEqual(95, result.Length);
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }
    }
}
