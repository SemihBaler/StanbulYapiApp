using ApplicationCore.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole
                    {
                        Id = "d575f12c-a0d3-4ead-b94c-c0ee62ef7652",
                        Name = "Admin",
                        NormalizedName = "ADMIN".ToUpper()
                    }
                );

            var hasher = new PasswordHasher<AppUser>();

            builder.Entity<AppUser>().HasData
                (
                    new AppUser
                    {
                        Id = "9588b738-b7c2-4dab-96f7-5ccedde5be23",
                        UserName = "test",
                        NormalizedUserName = "TEST",
                        Email = "test@test.com",
                        PasswordHash = hasher.HashPassword(null, "1234")
                    }
                );

            builder.Entity<IdentityUserRole<string>>().HasData
                (
                    new IdentityUserRole<string>
                    {
                        RoleId = "d575f12c-a0d3-4ead-b94c-c0ee62ef7652",
                        UserId = "9588b738-b7c2-4dab-96f7-5ccedde5be23"
                    }
                );

        }
    }
}
