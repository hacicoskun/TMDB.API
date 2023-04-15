using HC.Api.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Api.Identity.Identity
{

    public class ApiIdentityUser : IdentityUser, IIdentityUser
    {

    }
    public class ApiIdentityUserRole : IdentityRole
    {

    }
}
