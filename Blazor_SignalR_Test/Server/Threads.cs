using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server
{
    public class Threads
    {
        private HubConnection _hubConnection;
        private NavigationManager _navmanager;

        public Threads(NavigationManager navigationManager)
        {
            _navmanager = navigationManager;
        }
        public void Init()
        {
            _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navmanager.ToAbsoluteUri("/coinhub"))
            .Build();
            
        }
    }
}
