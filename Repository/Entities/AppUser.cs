using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class AppUser : IdentityUser
    {
        //many - many
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
