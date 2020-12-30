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
        private readonly IUtilityService utilityService;

        public DataContext AppContext { get; }
        public UserManager<AppUser> UserManager { get; }
        public RoleManager<AppRole> RoleManager { get; }
        public SignInManager<AppUser> SignInManager { get; }

        public StartupService(DataContext appContext,
                              UserManager<AppUser> userManager,
                              RoleManager<AppRole> roleManager,
                              SignInManager<AppUser> signInManager,
                              IUtilityService utilityService)
        {
            AppContext = appContext;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            this.utilityService = utilityService;
        }

        public async Task InitializeSystemRoles()
        {

            List<AppRole> DefaultRoles = AppRole.GetDefaultRoles();
            Console.WriteLine($"Checking system roles... roles loaded: {DefaultRoles.Count}");

            foreach (var dr in DefaultRoles)
            {
                Console.Write("Loaded default role: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(dr.Name + "\n");
                Console.ResetColor();
            }

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
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"User {role.Name} already exists in the database");
                    Console.ResetColor();
                }
            }
        }
        public async Task InitializeSystemUsers()
        {
            List<AppUser> DefaultUsers = AppUser.GetDefaultUsers();
            Console.WriteLine($"Checking system users... roles loaded: {DefaultUsers.Count}");
            foreach (var dr in DefaultUsers)
            {
                Console.Write("Loaded default user: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(dr.UserName + "\n");
                Console.ResetColor();
            }
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
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"User {user.UserName} already exists in the database");
                    Console.ResetColor();
                }
            }

        }

        public async Task CreateDatabaseIfNotExist()
        {
            Console.Write("Does database already exists? ");
            if (!await AppContext.Database.EnsureCreatedAsync())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("YES!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("NO! Creating...");
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        public async Task CheckDBConnection()
        {
            Console.Write("Connected to database? ");
            if (await AppContext.Database.CanConnectAsync())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("YES!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("NO!");
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        public void PrintStartupText(string text, bool ExtraWhiteLine)
        {
            Console.WriteLine(text);
            if (ExtraWhiteLine)
            {
                Console.WriteLine();
            }
        }
        
    }
}
