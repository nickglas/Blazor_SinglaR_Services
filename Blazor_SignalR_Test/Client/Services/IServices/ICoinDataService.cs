using Blazor_SignalR_Test.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Blazor_SignalR_Test.Shared.Api;

namespace Blazor_SignalR_Test.Client.Services.IServices
{
    public interface ICoinDataService
    {
        event Action StatussesOnChange;
        public Task Init();
        string ConnectionId { get; set; }
        IList<Api> Statusses { get; set; }
        Task GetApiStatusses();
        Task StartThread();
    }
}

