using System;
using Microsoft.AspNetCore.Identity;

namespace manav.identityServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
