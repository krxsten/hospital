using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Configurations
{
    public class CheckupConfiguration : IEntityTypeConfiguration<Checkup>
    {
        public void Configure(EntityTypeBuilder<Checkup> builder)
        {

            builder.HasOne(c => c.Patient)
                      .WithMany(p => p.Checkups)
                      .HasForeignKey(c => c.PatientID)
                      .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Doctor)
                      .WithMany(d => d.Checkups)
                      .HasForeignKey(c => c.DoctorID)
                      .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(CreateCheckups());
        }
        public List<Checkup> CreateCheckups()
        {
            List<Checkup> checkups = new List<Checkup>()
            {
                new Checkup
                {
                    ID = new Guid("d44665d6-6b30-4542-b932-cbb7ee71efbe"),
                    Date = new DateOnly(2026, 04, 12),
                    Time = new TimeOnly(16, 30),
                    DoctorID = new Guid("08ccdf4b-02ad-464f-9ef2-fb73ceee1826"),
                    PatientID = new Guid()
                },

                new Checkup
                {
                    ID = new Guid("7f55e59e-5ab2-4e36-92bd-d66f3c453ab9"),
                    Date = new DateOnly(2026, 06, 01),
                    Time = new TimeOnly(16, 30),
                    DoctorID = new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"),
                    PatientID = new Guid()
                },

                new Checkup
                {
                    ID = new Guid("5dad99cf-b93a-4376-b772-86fd44246d7e"),
                    Date = new DateOnly(2026, 07, 19),
                    Time = new TimeOnly(08, 30),
                    DoctorID = new Guid("b03aa359-5059-478c-8df4-db3fd4342b14"),
                    PatientID = new Guid()
                },

                new Checkup
                {
                    ID = new Guid("a828ee04-c7dc-417c-9d8b-85114a92ce47"),
                    Date = new DateOnly(2026, 12, 06),
                    Time = new TimeOnly(10, 00),
                    DoctorID = new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"),
                    PatientID = new Guid()
                },
                new Checkup
                {
                    ID = new Guid("aff1363e-af23-4c5b-a88f-ba81e536333b"),
                    Date = new DateOnly(2026, 05, 23),
                    Time = new TimeOnly(15, 30),
                    DoctorID = new Guid("b972fe92-5c0f-420b-87f0-fb1da4868b41"),
                    PatientID = new Guid()
                }
            };
            return checkups;
        }
    }
}

