using Blazor_SignalR_Test.Server.Data;
using Blazor_SignalR_Test.Server.Services.Interfaces;
using Blazor_SignalR_Test.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Server.Services.Helpers
{
    public class StartupService : IStartupService
    {
        public DataContext AppContext { get; }
        public UserManager<AppUser> UserManager { get; }
        public RoleManager<AppRole> RoleManager { get; }
        public SignInManager<AppUser> SignInManager { get; }

        public StartupService(DataContext appContext,
                              UserManager<AppUser> userManager,
                              RoleManager<AppRole> roleManager,
                              SignInManager<AppUser> signInManager)
        {
            AppContext = appContext;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

        public async Task InitializeSystemRoles()
        {

            List<AppRole> DefaultRoles = AppRole.GetDefaultRoles();
            Console.WriteLine($"Checking system roles... roles loaded: {DefaultRoles.Count}");
            foreach (var role in DefaultRoles)
            {
                if (await RoleManager.FindByNameAsync(role.Name) == null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Creating new default role {role.Name}");
                    role.CreationDate = DateTime.Now;
                    await RoleManager.CreateAsync(role);
                    Console.ResetColor();
                }
            }
        }
        public async Task InitializeSystemUsers()
        {
            List<AppUser> DefaultUsers = AppUser.GetDefaultUsers();
            Console.WriteLine($"Checking system users... roles loaded: {DefaultUsers.Count}");

            foreach (var user in DefaultUsers)
            {
                if (await UserManager.FindByEmailAsync(user.Email) == null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Creating new default user {user.UserName}");
                    await UserManager.CreateAsync(user);
                    await UserManager.AddPasswordAsync(user, user.DesiredPassword);
                    await UserManager.AddToRoleAsync(user, "Admin");
                    Console.ResetColor();
                }
            }

        }

        public async Task CreateDatabaseIfNotExist()
        {
            await AppContext.Database.EnsureCreatedAsync();
        }

        public async Task CheckDBConnection()
        {
            if (await AppContext.Database.CanConnectAsync())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Succesfully connected to the database");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to connect to the database");
                Console.ResetColor();
            }
        }
    }
}
