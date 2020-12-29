using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Client.Services.IServices
{
    public interface IChatService
    {
        event Action Reload_Messages_OnChange;

        List<string> Messages { get; set; }
        string ConnectionId { get; set; }
        public Task Init();
        public bool IsConnected();
        public HubConnectionState HubState();
        public Task SendMessage(string name, string message);
        public ValueTask DisposeAsync();

    }
}
