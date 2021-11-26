using ConsumeDiscogsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsumeDiscogsAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string username = "chrishughandrew";
            JObject wantlist = new();
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri("https://api.discogs.com/");

                //discogs requires a User Agent string
                string userAgentString = "ConsumeDiscogsAPI/1.0"; //TODO: Discogs wants it in this format "ConsumeDiscogsAPI/1.0" +https://localhost:44309/"; but .NET wont parse it. Works for now.
                httpClient.DefaultRequestHeaders.Add("User-Agent", userAgentString);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync($"users/{username}/wants");

                if (!response.IsSuccessStatusCode)
                {
                    //give error view if fails to get data
                    return RedirectToAction(nameof(Error));
                }

                // get the content and convert to a LINQ queryable object
                string responseContent = await response.Content.ReadAsStringAsync();
                wantlist = JObject.Parse(responseContent);
            }

                // select out pertient data from wantlist: id, title, artist, label:name catno; genre, style 
                JArray allWants = (JArray)wantlist["wants"];
                

                // Map each to a Want Class

                // Add to Wants list


               
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
