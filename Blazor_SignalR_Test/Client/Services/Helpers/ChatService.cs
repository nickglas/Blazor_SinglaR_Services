using Blazor_SignalR_Test.Client.Services.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Client.Services.Helpers
{
    public class ChatService : IChatService
    {
        private HubConnection _hubConnection;
        private readonly NavigationManager _navigationManager;

        public event Action Reload_Messages_OnChange;

        public List<string> Messages { get; set; } = new List<string>();
        public string ConnectionId { get; set; } = string.Empty;

        public ChatService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        //checking if connected
        public bool IsConnected()
        {
            return _hubConnection.State == HubConnectionState.Connected;
        }

        
        //creating hub connection
        public async Task Init()
        {
            _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri("/chathub"))
            .Build();

            //subscribing to the receive event
            _hubConnection.On<string, string>("ReceiveMessage", (name, message) =>
            {
                var encodedMsg = $"{name}: {message}";
                Messages.Add(encodedMsg);
                MessagesChanged();
            });
            await _hubConnection.StartAsync();
            ConnectionId = _hubConnection.ConnectionId;
        }

        void MessagesChanged() 
        {
            Reload_Messages_OnChange.Invoke();
        } 


        //sending message
        public async Task SendMessage(string name, string message)
        {
            await _hubConnection.SendAsync("SendMessage", name, message);
        }

        public async ValueTask DisposeAsync()
        {
            await _hubConnection.DisposeAsync();
        }

        public HubConnectionState HubState()
        {
            return _hubConnection.State;
        }
    }
}
