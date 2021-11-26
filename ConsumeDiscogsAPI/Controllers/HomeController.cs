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

            // get the desired data from the JObject (ie. ignore the pagination data)
            JArray allWants = (JArray)wantlist["wants"];

            List<Record> records = new();
                
            // iterate over each record in the wantlist 
            foreach(var want in allWants)
            {               

                // Select out pertient data from wantlist: id, title, artist, labele & catno; genre, style 
                // And Map each to the Record class
                Record record = new()
                {
                    // [0] used legitimately, to grab the primary/most pertinent instance of that property for simplicity
                    DiscogsId = (int)want["id"],
                    Title =     (string)want["basic_information"]["title"],
                    Artist =    (string)want["basic_information"]["artists"][0]["name"], 
                    Label =     (string)want["basic_information"]["labels"][0]["name"],
                    CatNo =     (string)want["basic_information"]["labels"][0]["catno"],
                    Genre =     (string)want["basic_information"]["genres"][0],
                    Style =     (string)want["basic_information"]["styles"][0]
                };

                // Add to a Records list
                records.Add(record);
            } 
            // Output list of wantlist records to the view  
            return View(records);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
