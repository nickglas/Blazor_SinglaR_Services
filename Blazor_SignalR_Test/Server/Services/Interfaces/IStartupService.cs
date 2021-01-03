using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Services.Interfaces
{
    public interface IStartupService
    {
        void PrintStartupText(string text, bool ExtraWhiteLine);
        Task InitializeSystemUsers();
        Task InitializeSystemRoles();
        Task InitializeDefaultCoinsAsync();
        Task CreateDatabaseIfNotExist();
        Task CheckDBConnection();
    }
}
