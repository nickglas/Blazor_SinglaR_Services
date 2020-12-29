using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_SignalR_Test.Shared
{
    public class Api
    {
        public string Api_Title { get; set; }
        public string Api_Description { get; set; }
        public string Api_Url { get; set; }
        public DateTime Last_Checked { get; set; }
        public Api_Status Status { get; set; }

        public static List<Api> GetBaseApi()
        {
            List<Api> api_list = new List<Api>()
            {
                new Api()
                {
                    Api_Title = "Coingecko api",
                    Api_Description = "Coingecko provides all crypto market information",
                    Api_Url = "https://api.coingecko.com/api/v3/ping",
                    Last_Checked = DateTime.Now,
                    Status = Api_Status.Fetching
                }

            };

            return api_list;
        }

    }
}
