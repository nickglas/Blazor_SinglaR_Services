using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_SignalR_Test.Shared
{
    public class Coin
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public bool favorite { get; set; }

        public static List<Coin> GetDefaultCoins()
        {
            return new List<Coin>()
            {
                new Coin()
                {
                    symbol = "BTC",
                    name = "Bitcoin"
                }
            };
        }
    }
}
