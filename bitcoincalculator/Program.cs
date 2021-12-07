using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace bitcoincalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            BitcoinRate currentbitcoin = getrates();
            Console.WriteLine($"current rate: {currentbitcoin.bpi.EUR.code} {currentbitcoin.bpi.EUR.rate_float}");
            Console.WriteLine("calculate in: EUR/USD/GBP");
            string userchoice = Console.ReadLine();
            Console.WriteLine("enter the amount of bitcoins");
            float usercoins = float.Parse(Console.ReadLine());
            float currentrate = 0;

            if(userchoice == "EUR")
            {
                currentrate = currentbitcoin.bpi.EUR.rate_float;
            }
            else if (userchoice == "USD")
            {
                currentrate = currentbitcoin.bpi.USD.rate_float;
            }

            else if (userchoice == "GBP")
            {
                currentrate = currentbitcoin.bpi.GBP.rate_float;
            }
            float result = currentrate * usercoins;
            Console.WriteLine($"your bitcoins are worth {userchoice} {result}");

            

        }
        public static BitcoinRate getrates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webresponse = request.GetResponse();
            var webstream = webresponse.GetResponseStream();

            BitcoinRate bitcoindata;

            using (var responsereader = new StreamReader(webstream))
            {
                var response = responsereader.ReadToEnd();
                bitcoindata = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }

            return bitcoindata;

        }
    }
}
