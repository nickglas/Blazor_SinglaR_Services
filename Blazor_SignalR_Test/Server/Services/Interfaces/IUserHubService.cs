using Blazor_SignalR_Test.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Services.Interfaces
{
    public interface IUserHubService
    {
        Task<List<AppUser>> GetAllAppUsersAsync();
        Task UpdateUserState(string Id, UserStatus status);
        Task GetUserStatus();
    }
}
