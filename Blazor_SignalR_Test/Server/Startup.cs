using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Blazor_SignalR_Test.Server.Hubs;
using System.Threading;
using Microsoft.AspNetCore.Components;
using Blazor_SignalR_Test.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Blazor_SignalR_Test.Server.Services.Interfaces;
using Blazor_SignalR_Test.Server.Services.Helpers;
using Blazor_SignalR_Test.Shared;
using System;
using Blazor_SignalR_Test.Client.Services.IServices;
using Microsoft.AspNetCore.SignalR;
using Blazor_SignalR_Test.Server.Controllers;

namespace Blazor_SignalR_Test.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSignalR();

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddScoped<IStartupService, StartupService>();
            services.AddScoped<IUserHubService, UserHubService>();
            services.AddScoped<IUtilityService, UtilityService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            DataContext _context,
            UserManager<AppUser> userManager,
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IStartupService startupService,
            IUtilityService utilityService,
            IHubContext<UserHub> userhub,
            IUserService userService
            )
        {
            //_context.Database.EnsureCreatedAsync().Wait();
            app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapHub<CoinHub>("/coinhub");
                endpoints.MapHub<UserHub>("/userhub");
            });


            try
            {
                utilityService.PrintText("[STARTUP LOGS]",false,ConsoleColor.Green);
                startupService.CheckDBConnection().Wait();
                startupService.CreateDatabaseIfNotExist().Wait();
                startupService.InitializeSystemRoles().Wait();
                startupService.InitializeSystemUsers().Wait();
                utilityService.PrintText("[END OF LOGS]",true,ConsoleColor.Green);
            }
            catch (Exception err)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"\n\nA error occured while initializing database items... Error: {err.Message}");
                Console.Write("Drop the database? (Y/N) : ");
                ConsoleKeyInfo Input = Console.ReadKey();
                if (Input.Key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    Console.WriteLine("Dropping database...");
                    _context.Database.EnsureDeletedAsync().Wait();
                    Console.WriteLine("Creating database...");
                    _context.Database.EnsureCreatedAsync().Wait();
                }
                else
                {
                    Console.WriteLine("Exiting application...");
                    Environment.Exit(-1);
                }
            }
            
            //Thread thread = new Thread(new ThreadStart(Test));
        }
        
    }
}
