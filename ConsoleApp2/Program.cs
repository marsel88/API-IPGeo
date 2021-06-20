using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Telegram.Bot;

namespace IPGeo
{
    class Program
    {
        static void Main(string[] args)
        {      
            TelegramBotClient bot = new TelegramBotClient("1837793493:AAGeFcJoiQ4cf3RosFgUru5eto4XYCd29Ro");
            bot.OnMessage += (s, arg) =>
            {
                Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Information about IP: ''{GetAnswer(arg.Message.Text)}''");
            };

            bot.StartReceiving();

            Console.ReadKey();

        }

        static string GetAnswer(string arg)
            {
            var IP = arg;   
            var url = $"https://ipwhois.app/json/{IP}?objects=country,city,region,currency";


               var request = WebRequest.Create(url);

               var response = request.GetResponse();
               var httpStatusCode = (response as HttpWebResponse).StatusCode;

                if (httpStatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine(httpStatusCode);
                    return "It`s don`t work";
                }

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
    Console.WriteLine(result);
                    var IpLocation = JsonConvert.DeserializeObject<Root>(result);
                return $"{IpLocation.country} + {IpLocation.city} + {IpLocation.region} + {IpLocation.currency}";
                }
            }
    }
}