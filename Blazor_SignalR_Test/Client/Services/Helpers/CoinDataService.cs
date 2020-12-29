using Blazor_SignalR_Test.Client.Services.IServices;
using Blazor_SignalR_Test.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Blazor_SignalR_Test.Shared.Api;

namespace Blazor_SignalR_Test.Client.Services.Helpers
{
    public class CoinDataService : ICoinDataService
    {
        private HubConnection _hubConnection;
        private NavigationManager _navigationManager;

        public event Action StatussesOnChange;

        public CoinDataService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public string ConnectionId { get; set; } = string.Empty;
        public IList<Api> Statusses { get; set; } = new List<Api>();


        public async Task Init()
        {
            _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri("/coinhub"))
            .Build();

            _hubConnection.On<List<Api>>("ReceiveApiStatus", (Status) =>
            {
                Statusses.Clear();
                foreach (var s in Status)
                {
                    Statusses.Add(s);
                }
                StatusChanged();
            });
            
            await _hubConnection.StartAsync();
            ConnectionId = _hubConnection.ConnectionId;
        }
        public async Task GetApiStatusses()
        {
            await _hubConnection.SendAsync("CheckApi");
        }
        public void StatusChanged()
        {
            StatussesOnChange.Invoke();
        }

        public async Task StartThread()
        {
            await _hubConnection.SendAsync("StartApiThreadCaller");
        }
    }
}
