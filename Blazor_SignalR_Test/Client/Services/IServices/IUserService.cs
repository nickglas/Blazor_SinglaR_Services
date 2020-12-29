using Blazor_SignalR_Test.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Client.Services.IServices
{
    public interface IUserService
    {
        public List<AppUser> Users { get; set; }
        public string ConnectionId { get; set; }

        public Task Init();
        public Task GetAppUsers();
        public Task SetAllOffline();
        event Action StatussesOnChange;
    }
}
