using HC.Api.Identity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HC.Api.Identity
{
    
    public class IdentityDbContext : IdentityDbContext<ApiIdentityUser, ApiIdentityUserRole, string>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("identity");
            modelBuilder.Entity<IdentityUser>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("user_role_claims");
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();

        }
    }
}
