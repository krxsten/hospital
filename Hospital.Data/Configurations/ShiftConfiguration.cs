using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.HasData(new Shift { ID = new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), Type = "Morning", StartTime = new TimeOnly(6, 0, 0), EndTime = new TimeOnly(14, 0, 0) },
                new Shift { ID = new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), Type = "Afternoon", StartTime = new TimeOnly(14, 0, 0), EndTime = new TimeOnly(22, 0, 0) },
                new Shift { ID = new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), Type = "Night", StartTime = new TimeOnly(22, 0, 0), EndTime = new TimeOnly(6, 0, 0) },
                new Shift { ID = new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), Type = "Emergency", StartTime = new TimeOnly(0, 0, 0), EndTime = new TimeOnly(8, 0, 0) },
                new Shift { ID = new Guid("aaaaaaf9-5059-478c-8df4-db3fd4342b14"), Type = "Weekend", StartTime = new TimeOnly(8, 0, 0), EndTime = new TimeOnly(16, 0, 0) });
        }
        public List<Shift> CreateShifts()
        {
            List<Shift> shifts = new List<Shift>()
            {
               
            };
            return shifts;
        }
    }
}
