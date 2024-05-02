using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Seeding
{
    public static class Seed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            // Seed roles if needed
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new Role("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new Role("User"));
            }

            // Seed default user
            var defaultUser = await userManager.FindByEmailAsync("broccolicodeman.shopoyisty@gmail.com");
            if (defaultUser == null)
            {
                defaultUser = new User
                {
                    UserName = "broccolicodeman",
                    Email = "broccolicodeman.shopoyisty@gmail.com"
                    // Add any additional properties here
                };
                var result = await userManager.CreateAsync(defaultUser, ",Hjrjksrjlvfy8"); // Change the password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, "Admin");
                }
            }
        }
    }
}
