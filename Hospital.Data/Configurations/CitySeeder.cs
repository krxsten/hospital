using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using Hospital.Data.Entities;

namespace Hospital.WebProject.Seed
{
    public static class CitySeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();

            var cities = new List<City>
            {
               new City { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Sofia" },
               new City { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Plovdiv" },
               new City { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Varna" },
               new City { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Burgas" },
               new City { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Ruse" },
               new City { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Stara Zagora" },
               new City { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Pleven" },
               new City { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Blagoevgrad" },
               new City { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Dobrich" },
               new City { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Gabrovo" },
               new City { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Haskovo" },
               new City { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Kardzhali" },
               new City { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "Kyustendil" },
               new City { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Lovech" },
               new City { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "Montana" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-111111111111"), Name = "Pazardzhik" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-222222222222"), Name = "Pernik" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-333333333333"), Name = "Razgrad" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-444444444444"), Name = "Shumen" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-555555555555"), Name = "Silistra" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-666666666666"), Name = "Sliven" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-777777777777"), Name = "Smolyan" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-888888888888"), Name = "Targovishte" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-999999999999"), Name = "Veliko Tarnovo" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-aaaaaaaaaaaa"), Name = "Vidin" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-bbbbbbbbbbbb"), Name = "Vratsa" },
               new City { Id = Guid.Parse("11112222-3333-4444-5555-cccccccccccc"), Name = "Yambol" }
            };
    
            foreach (var city in cities)
            {
                if (!await context.Cities.AnyAsync(c => c.Id == city.Id))
                {
                    await context.Cities.AddAsync(city);
                }
            }
            await context.SaveChangesAsync();
            Console.WriteLine("Seeded Cities.");
        }
    }
}
