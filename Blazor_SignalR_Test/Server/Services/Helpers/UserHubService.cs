using Blazor_SignalR_Test.Client.Services.IServices;
using Blazor_SignalR_Test.Server.Data;
using Blazor_SignalR_Test.Server.Hubs;
using Blazor_SignalR_Test.Server.Services.Interfaces;
using Blazor_SignalR_Test.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Services.Helpers
{
    public class UserHubService : IUserHubService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IHubContext<UserHub> _hub;

        public UserHubService(UserManager<AppUser> userManager, DataContext context, IHubContext<UserHub> hubContext)
        {
            _userManager = userManager;
            _context = context;
            _hub = hubContext;
        }

        public async Task<List<AppUser>> GetAllAppUsersAsync()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task UpdateUserState(string UserId,UserStatus status)
        {
            AppUser x = await _userManager.FindByIdAsync(UserId);
            x.Status = status;
            await _userManager.UpdateAsync(x);
        }
        public async Task GetUserStatus()
        {
            List<AppUser> appUsers = await _context.AppUsers.ToListAsync();
            await _hub.Clients.All.SendAsync("receiveUserStatus", appUsers);
        }
    }
}
