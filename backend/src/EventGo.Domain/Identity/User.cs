using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGo.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace EventGo.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Title Title { get; set; }
        public string Description { get; set; }
        public Function Function { get; set; }
        public string ProfileImage { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}