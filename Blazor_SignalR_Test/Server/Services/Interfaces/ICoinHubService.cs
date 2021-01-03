using Blazor_SignalR_Test.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Services.Interfaces
{
    public interface ICoinHubService
    {
        Task AddCoinAsync(Coin data);

        Task RemoveCoinAsync(int id);

        Task GetAllCoinsAsync();

        Task RemoveAllCoinsAsync();

    }
}
