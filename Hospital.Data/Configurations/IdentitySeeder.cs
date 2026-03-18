using Hospital.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hospital.WebProject.Seed
{
    public class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = { "Admin", "Doctor", "Nurse", "Patient" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }
        public static async Task SeedAdminAsync(UserManager<User> userManager)
        {
            var adminUsername = "admin";
            var adminEmail = "admin@admin.com";

            var user = await userManager.FindByEmailAsync(adminUsername);

            if (user == null)
            {
                user = new User
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    FirstName = adminUsername,
                    LastName = adminUsername,
                };

                await userManager.CreateAsync(user, "Admin1234");
            }

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
