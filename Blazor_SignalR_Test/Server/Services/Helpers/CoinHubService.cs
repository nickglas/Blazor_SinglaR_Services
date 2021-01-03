using Blazor_SignalR_Test.Server.Data;
using Blazor_SignalR_Test.Server.Services.Interfaces;
using Blazor_SignalR_Test.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Services.Helpers
{
    public class CoinHubService : ICoinHubService
    {
        private readonly DataContext context;

        public CoinHubService(DataContext context)
        {
            this.context = context;
        }
        public async Task AddCoinAsync(Coin data)
        {
            await context.Coins.AddAsync(data);
        }

        public async Task GetAllCoinsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAllCoinsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCoinAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
