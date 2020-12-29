using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Services.Interfaces
{
    public interface IStartupService
    {
        Task InitializeSystemUsers();
        Task InitializeSystemRoles();
        Task CreateDatabaseIfNotExist();
        Task CheckDBConnection();
    }
}
