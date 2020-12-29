using Blazor_SignalR_Test.Client.Services.IServices;
using Blazor_SignalR_Test.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Client.Services.Helpers
{
    public class UserService : IUserService
    {
        private HubConnection _hubConnection;
        private NavigationManager _navigationManager;

        public event Action StatussesOnChange;

        public UserService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public string ConnectionId { get; set; } = string.Empty;
        public List<AppUser> Users { get; set; } = new List<AppUser>();


        public async Task Init()
        {
            _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri("/userhub"))
            .Build();

            _hubConnection.On<List<AppUser>>("receiveUserStatus", (Status) =>
            {
                Console.WriteLine("Called locally");
                Users.Clear();
                foreach (var s in Status)
                {
                    Users.Add(s);
                }
                StatusChanged();
            });

            await _hubConnection.StartAsync();
            ConnectionId = _hubConnection.ConnectionId;
        }
        public void StatusChanged()
        {
            StatussesOnChange.Invoke();
        }

        public async Task GetAppUsers()
        {
            Console.WriteLine("sending");
            await _hubConnection.SendAsync("GetUserStatus");
        }
        public async Task SetAllOffline()
        {
            Console.WriteLine("sending");
            await _hubConnection.SendAsync("SetAllOffline");
        }
    }
}
