using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Configurations
{
    public class DoctorAndNurseConfiguration : IEntityTypeConfiguration<DoctorAndNurse>
    {
        public void Configure(EntityTypeBuilder<DoctorAndNurse> builder)
        {
            builder
                 .HasOne(x => x.Doctor)
                 .WithMany(d => d.DoctorNurses)
                 .HasForeignKey(x => x.DoctorID)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Nurse)
                .WithMany(n => n.DoctorNurses)
                .HasForeignKey(x => x.NurseID)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(x => new { x.DoctorID, x.NurseID })
                .IsUnique();
            builder.HasData(
                new DoctorAndNurse
                {
                    ID = new Guid("c5c2c0dd-c326-4f37-804c-e53354c825ed"),
                    DoctorID = new Guid("e1ceefa2-e56b-4395-9049-c689bea9417f"), // Correct (Doctor 2)
                    NurseID = new Guid("698d0579-913c-42af-8a45-924cd9f740bb")  // Correct (Nurse 1)
                },
    new DoctorAndNurse
    {
        ID = new Guid("5a6d7c84-48ec-4b3c-a2d6-a468f92f12dc"),
        DoctorID = new Guid("8e296807-75cf-45dd-bdfc-179495465c09"), // Correct (Doctor 6)
        NurseID = new Guid("e6a3850b-7a1e-465c-83fd-57b1134c68d2")  // Correct (Nurse 3)
    },
    new DoctorAndNurse
    {
        ID = new Guid("038d24e4-d697-4ab6-a04a-70989e133c70"),
        DoctorID = new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"), // FIXED: Was User ID, now Doctor 10 ID
        NurseID = new Guid("e5f97752-f18b-4b36-8c47-4d238cb0e01f")  // Correct (Nurse 2)
    }
                );
        }
        //public List<DoctorAndNurse> CreateDoctorsNurses()
        //{
        //    List<DoctorAndNurse> doctorAndNurses = new List<DoctorAndNurse>()
        //    {
               
        //    };
        //    return doctorAndNurses;
        //} 
    }
}

