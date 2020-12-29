using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blazor_SignalR_Test.Server
{
    public class Program
    {
        static string HostLocalIp = string.Empty;
        static int HttpPort = 5003;
        static int HttpsPort = 5004;
        static string HttpUrl = string.Empty;
        static string HttpsUrl = string.Empty;

        public static void Main(string[] args)
        {
            HostLocalIp = GetLocalIpv4Address();
            HttpUrl = BuildUrl(HostLocalIp, HttpPort, ConnectionType.Http);
            HttpsUrl = BuildUrl(HostLocalIp, HttpsPort, ConnectionType.Https);
            PrintStartUpAdresses();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
                  webBuilder.UseUrls(
                      HttpUrl, HttpsUrl);
              });
        static public string GetLocalIpv4Address()
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList.Last().ToString();
            return myIP;
        }
        static void PrintStartUpAdresses()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[INITIALIZING HOST BUILDER]");
            Console.ResetColor();

            Console.Write("Startup local address: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(HostLocalIp + "\n");
            Console.ResetColor();

            Console.Write("HTTP Port: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(HttpPort + "\n");
            Console.ResetColor();

            Console.Write("HTTPS Port: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(HttpsPort + "\n\n");
            Console.ResetColor();

            Console.Write("Http url: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(HttpUrl + "\n");
            Console.ResetColor();

            Console.Write("Https url: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(HttpsUrl + "\n");
            Console.ResetColor();

            Console.WriteLine("\n");
        }
        static string BuildUrl(string ip, int port, ConnectionType type) 
        {
            string url = string.Empty;
            switch (type)
            {
                case ConnectionType.Http:
                    url = $"http://{ip}:{port}";
                    break;
                case ConnectionType.Https:
                    url = $"https://{ip}:{port}";
                    break;
                default:
                    break;
            }
            return url;
        }

        private enum ConnectionType
        {
            Http,
            Https
        }
    }
}
