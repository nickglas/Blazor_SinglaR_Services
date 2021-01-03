using Blazor_SignalR_Test.Client.Services.IServices;
using Blazor_SignalR_Test.Server.Services.Interfaces;
using Blazor_SignalR_Test.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Hubs
{
    public class CoinHub : Hub
    {
        private readonly ICoinHubService coinHub;

        public CoinHub(ICoinHubService coinHub)
        {
            this.coinHub = coinHub;
        }
        public async Task CheckApi()
        {
            List<Api> BaseData = Api.GetBaseApi();

            await Clients.All.SendAsync("ReceiveApiStatus", BaseData);

            List<Api> UpdatedData = CheckApiStatus(BaseData);

            await Clients.All.SendAsync("ReceiveApiStatus", UpdatedData);

        }

        public async Task AddCoinAsync(Coin data)
        {

        }
        public async Task RemoveCoinAsync(int id)
        {

        }
        public async Task GetAllCoinsAsync()
        {

        }
        public async Task RemoveAllCoinsAsync()
        {

        }


        private List<Api> CheckApiStatus(List<Api>Api_to_check)
        {
            List<Api> UpdatedList = new List<Api>();
            using (var client = new HttpClient())
            {
                foreach (var api in Api_to_check)
                {
                    client.BaseAddress = new Uri(api.Api_Url);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                    if (response.IsSuccessStatusCode)
                    {
                        api.Status = Api_Status.Online;
                        UpdatedList.Add(api);
                    }
                    else
                    {
                        api.Status = Api_Status.Offline;
                        UpdatedList.Add(api);
                    }
                }
            }
            return UpdatedList;
        }
        public async Task StartApiThreadCaller()
        {
            while (true)
            {
                await CheckApi();
                Thread.Sleep(5000);
            }
        }
       
    }
}

