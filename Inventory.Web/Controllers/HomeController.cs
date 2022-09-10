using Inventory.CommonViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Web.Controllers
{
    public class HomeController : Controller
    {
        public  ActionResult  Index()
        {
            //string projectURL = ConfigurationManager.AppSettings["ProjectURL"];
            //string apiURL = projectURL + "GetAllInventoryRecords";

            //HttpClient client = new HttpClient();
            ////client.BaseAddress = new Uri("");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage responseMessage = await client.GetAsync(apiURL);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

            //    var allInventoryRecords = JsonConvert.DeserializeObject<List<tblInventory>>(responseData);

            //    return View(allInventoryRecords);
            //}

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}