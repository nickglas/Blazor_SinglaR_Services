using Blazor_SignalR_Test.Server.Data;
using Blazor_SignalR_Test.Server.Services.Interfaces;
using Blazor_SignalR_Test.Shared;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Hubs
{
    public class UserHub : Hub
    {
        private readonly DataContext context;
        private readonly IUserHubService userHubService;


        public UserHub(DataContext context, IUserHubService userHubService)
        {
            this.context = context;
            this.userHubService = userHubService;
        }

        public async Task GetUserStatus()
        {
            Console.WriteLine("Called");

            //call service
            await userHubService.GetUserStatus();
        }
        public async Task SetAllOffline()
        {
            Console.WriteLine("offline called");
            List<AppUser> appUsers = await context.AppUsers.ToListAsync();
            foreach (var item in appUsers)
            {
                item.Status = UserStatus.Offline;
            }
            await Clients.All.SendAsync("receiveUserStatus", appUsers);

        }
    }
}
