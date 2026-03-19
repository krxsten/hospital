using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Configurations
{
    public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.HasData(CreateNurses());
        }
        public List<Nurse> CreateNurses()
        {
            List<Nurse> nurses = new List<Nurse>()
            {
                new Nurse() 
                {
                    ID = Guid.Parse("698d0579-913c-42af-8a45-924cd9f740bb"),
                    UserId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    SpecializationId = Guid.Parse("8a42cdba-ff58-4129-aea8-ae4c3b32f353"),
                    ShiftId = Guid.Parse("0288c0db-23d0-4cef-b74c-ef997285b18c"),
                    IsAccepted = true,
                    Image = "nurse1.png"
                },
                 new Nurse()
                {
                    ID = Guid.Parse("e5f97752-f18b-4b36-8c47-4d238cb0e01f"),
                    UserId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    SpecializationId = Guid.Parse("a5519f22-cefb-4771-a5e9-de7b40817df8"),
                    ShiftId = Guid.Parse("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"),
                    IsAccepted = true,
                    Image = "nurse2.png"
                },
                  new Nurse()
                {
                    ID = Guid.Parse("e6a3850b-7a1e-465c-83fd-57b1134c68d2"),
                    UserId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    SpecializationId = Guid.Parse("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"),
                    ShiftId = Guid.Parse("b972fe92-5c0f-420b-87f0-fb1da4868b41"),
                    IsAccepted = true,
                    Image = "nurse3.png"
                },
                   new Nurse()
                {
                    ID = Guid.Parse("9fb6048e-03ae-407f-a83e-51b6c5399b41"),
                    UserId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    SpecializationId = Guid.Parse("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"),
                    ShiftId = Guid.Parse("2cd29802-44c5-4559-8cc3-225984ae748f"),
                    IsAccepted = true,
                    Image = "nurse4.png"
                },

            };
            return nurses;
        }
    }
}