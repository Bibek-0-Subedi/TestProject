using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestProject.Models;
using Nancy.Json;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration config;
        public HomeController(ILogger<HomeController> logger, IConfiguration _config)
        {
            _logger = logger;
            config = _config;
        }

        public IActionResult Index()
        {
            var baseUrl = config["BaseUrl"];

            try
            {
                var client = new RestClient(new Uri(string.Concat(baseUrl, "WeatherForecast")));
                var request = new RestRequest(Method.GET);
                client.Timeout = -1;
                var jsonResult = new WeatherModel();
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var resultData = response.Content;
                    JavaScriptSerializer jSerializer = new JavaScriptSerializer();
                    jsonResult = jSerializer.Deserialize<WeatherModel>(resultData);
                }
                return View(jsonResult);
            }
            catch (Exception)
            {
                return View();
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
