using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasData(CreateUser());
        }
        public List<IdentityUser> CreateUser()
        {
            var users = new List<IdentityUser>();
            var hasher = new PasswordHasher<IdentityUser>();
            var user = new IdentityUser()
            {
                Id = "e7d8baba-f7b1-4ed0-9bbb-139dc13e878e",
                UserName = "gencheva",
                NormalizedUserName = "GENCHEVA",
                Email = "gencheva@gmail.com",
                NormalizedEmail = "GENCHEVA@GMAIL.COM"
            };
            user.PasswordHash = hasher.HashPassword(user, "gencheva1");
            users.Add(user);

            var user2 = new IdentityUser()
            {
                Id = "0f1c902e-5055-49a6-9076-e8f16b52e9f9",
                UserName = "ivanov",
                NormalizedUserName = "IVANOV",
                Email = "ivanov@gmail.com",
                NormalizedEmail = "IVANOV@GMAIL.COM"
            };
            user2.PasswordHash = hasher.HashPassword(user2, "ivanov1");
            users.Add(user2);

            var user3 = new IdentityUser()
            {
                Id = "fd0200ba-5d51-4141-9535-e43a93b2105e",
                UserName = "petrova",
                NormalizedUserName = "PETROVA",
                Email = "petrova@gmail.com",
                NormalizedEmail = "PETROVA@GMAIL.COM"
            };
            user3.PasswordHash = hasher.HashPassword(user3, "petrova1");
            users.Add(user3);
            
            var user4 = new IdentityUser()
            {
                Id = "c5a55743-2623-4415-8a04-44dd5d19477c",
                UserName = "georgiev",
                NormalizedUserName = "GEORGIEV",
                Email = "georgiev@gmail.com",
                NormalizedEmail = "GEORGIEV@GMAIL.COM"
            };
            user4.PasswordHash = hasher.HashPassword(user4, "georgiev1");
            users.Add(user4);

            var user5 = new IdentityUser()
            {
                Id = "27594785-6b38-46b4-b6d4-835ae37ea560",
                UserName = "stoyanova",
                NormalizedUserName = "STOYANOVA",
                Email = "stoyanova@gmail.com",
                NormalizedEmail = "STOYANOVA@GMAIL.COM"
            };
            user5.PasswordHash = hasher.HashPassword(user5, "stoyanova1");
            users.Add(user5);
            return users;
        }
    }
}
