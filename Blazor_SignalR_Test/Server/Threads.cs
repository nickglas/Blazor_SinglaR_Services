using Blazor_SignalR_Test.Server.Hubs;
using Blazor_SignalR_Test.Server.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server
{
    public class Threads
    {
        private readonly IUserHubService userHubService;
        private readonly IHubContext<UserHub> _hub;

        public Threads(IUserHubService userHubService, IHubContext<UserHub> hubContext)
        {
            this.userHubService = userHubService;
            _hub = hubContext;
        }

        public void test()
        {
            while (true)
            {
                userHubService.GetUserStatus();
                Thread.Sleep(1000);
            }
        }
    }
}
