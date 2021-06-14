using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace IPGeo
{
    class Program
    {
        static void Main(string[] args)
        {
            var IP = "46.224.227.20";
            var url = $"https://ipwhois.app/json/{IP}?objects=country,city,region,country_phone,timezone,currency_code,currency";
            

            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return;
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                Console.WriteLine(result);
                var weatherForecast = JsonConvert.DeserializeObject<Root>(result);
                Console.WriteLine(weatherForecast.city);
            }

        }
    }
}