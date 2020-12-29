using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Shared
{
    public class AppUser : IdentityUser
    {
        public AppUser() : base() { }


        [NotMapped]
        public string DesiredPassword = string.Empty;
        [NotMapped]
        public AppRole DesiredRole = new AppRole();

        public UserStatus Status { get; set; }

        public static List<AppUser> GetDefaultUsers()
        {
            return new List<AppUser>()
            {
                new AppUser
                {
                    Email = "nickglas@hotmail.nl",
                    UserName = "nickglas@hotmail.nl",
                    DesiredPassword = "Testen123"
                }
            };
        }
    }
}
