using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_SignalR_Test.Shared
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string roleName) : base(roleName)
        {

        }

        public AppRole(string roleName, string description, DateTime creationDate) : base(roleName)
        {
            this.Description = description;
            this.CreationDate = creationDate;
        }
        public string Description { get; set; }
        public DateTime CreationDate = DateTime.Now;

        public static List<AppRole> GetDefaultRoles()
        {
            return new List<AppRole>()
            {
                new AppRole()
                {
                    Name = "Admin",
                    CreationDate = DateTime.Now
                },
                new AppRole()
                {
                    Name = "User",
                    CreationDate = DateTime.Now
                }
            };
        }
    }
}
