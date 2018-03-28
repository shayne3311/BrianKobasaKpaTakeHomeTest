using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;

namespace KpaTakeHomeTest.Controllers
{
    public class WeatherProxyController : Controller
    {
        public ActionResult GetWeather(string Longitude, string Latitude)
        {
            var Key = "1f9a5eedd4211ec9f965223be81f2020";
            var ApiUrl = $"https://api.darksky.net/forecast/{Key}/{Longitude},{Latitude}";

            HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(ApiUrl);
            wr.Method = "GET";
            wr.ContentType = "application/json";

            // Get the response.
            var httpResponse = (HttpWebResponse)wr.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return Json(streamReader.ReadToEnd());
            }
        }
    }
}