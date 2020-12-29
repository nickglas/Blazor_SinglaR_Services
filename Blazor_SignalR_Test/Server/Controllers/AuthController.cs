using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_SignalR_Test.Server.Hubs;
using Blazor_SignalR_Test.Server.Services.Interfaces;
using Blazor_SignalR_Test.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Blazor_SignalR_Test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserHubService userHubService;
        private readonly IHubContext<UserHub> _hub;

        public AuthController(IUserHubService userHubService, IHubContext<UserHub> hubContext)
        {
            this.userHubService = userHubService;
            _hub = hubContext;
        }


        [HttpPost("setoffline")]
        public async Task<IActionResult> SetOffline(string id)
        {
            Console.WriteLine("Called new function");
            await userHubService.UpdateUserState(id, Shared.UserStatus.Offline);
            await userHubService.GetUserStatus();
            return Ok();
        }
    }
}
