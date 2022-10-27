using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any()) 
            {
                var user = new AppUser
                {
                    DisplayName = "admin", 
                    Email = "admin@test.com",
                    UserName = "admin@test.com",
                    Address = new Address 
                    {
                        FirstName = "Admin",
                        LastName = "Doe",
                        Street = " 100 Main Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "10000"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}