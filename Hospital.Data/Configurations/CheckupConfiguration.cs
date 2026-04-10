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
                DoctorID = new Guid("6d3dacc1-3b7a-4e43-8caa-5b82a6f4a21f"),
                PatientID = new Guid("9783d8b3-014f-477a-b951-6ff87057b44f")
            },

                new Checkup
                {
                    ID = new Guid("7f55e59e-5ab2-4e36-92bd-d66f3c453ab9"),
                    Date = new DateOnly(2026, 06, 01),
                    Time = new TimeOnly(16, 30),
                    DoctorID = new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"),
                    PatientID = new Guid("06999216-e4e9-4455-856d-5246259b2684")
                },

                new Checkup
                {
                    ID = new Guid("5dad99cf-b93a-4376-b772-86fd44246d7e"),
                    Date = new DateOnly(2026, 07, 19),
                    Time = new TimeOnly(08, 30),
                    DoctorID = new Guid("0f6d5fde-bc75-4df5-8886-090806665b82"),
                    PatientID = new Guid("eac603b7-4022-4fb1-8c04-28db2f0f2162")
                },

                new Checkup
                {
                    ID = new Guid("a828ee04-c7dc-417c-9d8b-85114a92ce47"),
                    Date = new DateOnly(2026, 12, 06),
                    Time = new TimeOnly(10, 00),
                    DoctorID = new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"),
                    PatientID = new Guid("ba24d819-24f4-4e94-bb71-77765076d46c")
                },
                new Checkup
                {
                    ID = new Guid("aff1363e-af23-4c5b-a88f-ba81e536333b"),
                    Date = new DateOnly(2026, 05, 23),
                    Time = new TimeOnly(15, 30),
                    DoctorID = new Guid("2a4e1d97-8411-4cf8-9da2-af9452f16eca"),
                    PatientID = new Guid("718919c3-760f-4a6a-8abf-b1cd1b459d11")
                }
            };
                
            return checkups;
        }
    }
}

