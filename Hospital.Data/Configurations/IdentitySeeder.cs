using Hospital.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public static async Task AssignRolesAsync(UserManager<User> userManager)
        {
            var doctorUserIds = new List<Guid>
    {
        Guid.Parse("072eae42-46ab-4919-aae5-073aef56c00d"),
        Guid.Parse("7c425879-d37a-48a6-91d9-2345120a3f6a"),
        Guid.Parse("3d86822f-0eba-44ce-8484-27addbfe7357"),
        Guid.Parse("7dca2bf8-df73-4dbf-a602-52e147eafe1e"),
        Guid.Parse("23b350b4-0dd6-43fc-b5dc-818faf2b74e6"),
        Guid.Parse("d9ccb374-6b17-4e66-9c11-79412a9e1e93"),
        Guid.Parse("30f2b4ed-e0e3-4443-8595-4dc6e26b3338"),
        Guid.Parse("51daaed0-67e7-4c4a-b254-2745af5365df"),
        Guid.Parse("cbdfa704-0f6d-431f-8ede-dd952adacfc9"),
        Guid.Parse("f6662c6a-414b-4b5c-ae1b-7b31103dd464")
    };

            var nurseUserIds = new List<Guid>
    {
        Guid.Parse("354fa92a-6b54-4d12-b90c-9926dc906462"),
        Guid.Parse("02e72b22-0abd-4ce4-80d1-30b8c13f952b"),
        Guid.Parse("741d970d-f405-4bd1-94b2-eec2c3fb33e2"),
        Guid.Parse("355ad73e-6b7d-4ade-846d-7cab0da06629")
    };

            var patientUserIds = new List<Guid>
    {
        Guid.Parse("a7e0d718-a822-48db-b8ff-82cff6dbd5c7"),
        Guid.Parse("04347895-6b6e-4608-be4c-5f428b759669"),
        Guid.Parse("96747275-9c90-449e-a91c-eb6863183a27"),
        Guid.Parse("865c2545-7806-4857-a621-f035e520a596"),
        Guid.Parse("c5982307-ef67-4b65-b438-8f9e1e3a240b")
    };

            foreach (var id in doctorUserIds)
            {
                var user = await userManager.FindByIdAsync(id.ToString());
                if (user != null && !await userManager.IsInRoleAsync(user, "Doctor"))
                {
                    var result = await userManager.AddToRoleAsync(user, "Doctor");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));
                    }
                }
            }

            foreach (var id in nurseUserIds)
            {
                var user = await userManager.FindByIdAsync(id.ToString());
                if (user != null && !await userManager.IsInRoleAsync(user, "Nurse"))
                {
                    var result = await userManager.AddToRoleAsync(user, "Nurse");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));
                    }
                }
            }

            foreach (var id in patientUserIds)
            {
                var user = await userManager.FindByIdAsync(id.ToString());
                if (user != null && !await userManager.IsInRoleAsync(user, "Patient"))
                {
                    var result = await userManager.AddToRoleAsync(user, "Patient");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));
                    }
                }
            }
        }

    }

}
