using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_SignalR_Test.Shared
{
    public class Coin
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }

        public static List<Coin> GetDefaultCoins()
        {
            return new List<Coin>()
            {
                new Coin()
                {
                    symbol = "BTC",
                    name = "Bitcoin",
                    Description = "Bitcoin coin",
                    ProjectUrl = "https://bitcoin.org/nl/"
                }
            };
        }
    }
}
