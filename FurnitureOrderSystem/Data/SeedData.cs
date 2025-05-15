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
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            // Инициализация ролей
            string[] roles = { "Admin", "Manager", "User" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Создание администратора
            var admin = new User
            {
                UserName = "admin",
                Email = "admin@furniture.com",
                FirstName = "Admin",
                LastName = "System",
                EmailConfirmed = true // Рекомендуется подтвердить email для seed-пользователя
            };

            if (await userManager.FindByNameAsync(admin.UserName) == null)
            {
                var result = await userManager.CreateAsync(admin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
