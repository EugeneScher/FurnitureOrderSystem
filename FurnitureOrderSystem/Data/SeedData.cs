using Microsoft.AspNetCore.Identity;
using FurnitureOrderSystem.Models.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FurnitureOrderSystem.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roles = { "Admin", "Manager", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@furniture.com",
                FirstName = "Admin",
                LastName = "System"
            };

            if (await userManager.FindByNameAsync(admin.UserName) == null)
            {
                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}