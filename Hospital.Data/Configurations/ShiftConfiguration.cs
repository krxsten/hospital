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
            builder.HasData(CreateShifts());
        }
        public List<Shift> CreateShifts()
        {
            List<Shift> shifts = new List<Shift>()
            {
                new Shift { ID = new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), Type = "Morning", StartTime = new TimeSpan(6, 0, 0), EndTime = new TimeSpan(14, 0, 0) },
                new Shift { ID = new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), Type = "Afternoon", StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(22, 0, 0) },
                new Shift { ID = new Guid("b972fe92-5c0f-420b-87f0-fb1da4868b41"), Type = "Night", StartTime = new TimeSpan(22, 0, 0), EndTime = new TimeSpan(6, 0, 0) },
                new Shift { ID = new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), Type = "Emergency", StartTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(8, 0, 0) },
                new Shift { ID = new Guid("b03aa359-5059-478c-8df4-db3fd4342b14"), Type = "Weekend", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(16, 0, 0) }
            };
            return shifts;
        }
    }
}
