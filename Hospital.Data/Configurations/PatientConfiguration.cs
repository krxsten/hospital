using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder
              .HasOne(p => p.User)
              .WithOne(u => u.Patient)
              .HasForeignKey<Patient>(p => p.UserId)
              .OnDelete(DeleteBehavior.Restrict);


            builder
                 .HasOne(p => p.Doctor)
                 .WithMany(d => d.Patients)
                 .HasForeignKey(p => p.DoctorId)
                 .OnDelete(DeleteBehavior.Restrict);

            

            builder
                .HasIndex(x => x.UCN)
                .IsUnique();
            builder
                .HasIndex(x => x.PhoneNumber)
                .IsUnique();
            builder.ToTable(t => t.HasCheckConstraint(
     "CK_DateOfBirth_Valid",
     "[DateOfBirth] >= '1920-01-01' AND [DateOfBirth] <= CAST(GETDATE() AS date)"
 ));
            builder.ToTable(t => t.HasCheckConstraint(
    "CK_HospitalizationDate_MinYear",
    "[HospitalizationDate] >= '1985-01-01'"
));

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_DischargeDate_MinYear",
                "[DischargeDate] >= '1985-01-01'"
            ));
            builder
                .HasOne(p => p.Room)
                .WithMany(r => r.ListOfPatients)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(CreatePatients());
        }
        public List<Patient> CreatePatients()
        {
            List<Patient> patients = new List<Patient>()
            {
                new Patient
            {
                ID = new Guid("9783d8b3-014f-477a-b951-6ff87057b44f"),
                UserId = new Guid("a7e0d718-a822-48db-b8ff-82cff6dbd5c7"),
                BirthCity = "Kazanlak",
                DateOfBirth = new DateOnly(2007, 10, 16),
                DischargeDate = new DateOnly(2026, 06, 17),
                DischargeTime = new TimeOnly(12, 30),
                DoctorId = new Guid("6d3dacc1-3b7a-4e43-8caa-5b82a6f4a21f"),
                HospitalizationDate = new DateOnly(2026, 05, 12),
                HospitalizationTime = new TimeOnly(10, 30),
                PhoneNumber = "0878104032",
                UCN = "0750167656",
                RoomId = new Guid("e748053a-f20a-4f80-a314-fbd9c2ac4e8a")
            },
                 new Patient
                 {
                     ID = new Guid("eac603b7-4022-4fb1-8c04-28db2f0f2162"),
                     UserId = new Guid("04347895-6b6e-4608-be4c-5f428b759669"),
                     BirthCity = "Stara Zagora",
                     DateOfBirth = new DateOnly(1992, 11, 23),
                     HospitalizationDate = new DateOnly(2026, 03, 01),
                     HospitalizationTime = new TimeOnly(14, 45),
                     DoctorId = new Guid("0f6d5fde-bc75-4df5-8886-090806665b82"),
                     DischargeDate = new DateOnly(2026, 03, 15),
                     DischargeTime = new TimeOnly(11, 00),
                     PhoneNumber = "0882123456",
                     UCN = "9251237890",
                     RoomId = new Guid("0de4b3b8-318e-42aa-b896-04cd14749d17")
                 },
                  new Patient
                  {
                      ID = new Guid("06999216-e4e9-4455-856d-5246259b2684"),
                      UserId = new Guid("96747275-9c90-449e-a91c-eb6863183a27"),
                      BirthCity = "Plovdiv",
                      DoctorId = new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"),
                      RoomId = new Guid("53546e3d-53ee-4e2c-861a-3cf5a3584893"),
                      DateOfBirth = new DateOnly(1994, 03, 15),
                      HospitalizationDate = new DateOnly(2026, 07, 05),
                      HospitalizationTime = new TimeOnly(09, 00),
                      DischargeDate = new DateOnly(2026, 07, 20),
                      DischargeTime = new TimeOnly(11, 30),
                      PhoneNumber = "0877123456",
                      UCN = "9413153443"
                  },
                   new Patient
                   {
                       ID = new Guid("718919c3-760f-4a6a-8abf-b1cd1b459d11"),
                       UserId = new Guid("865c2545-7806-4857-a621-f035e520a596"),
                       BirthCity = "Yambol",
                       DateOfBirth = new DateOnly(1995, 08, 12),
                       HospitalizationDate = new DateOnly(2026, 04, 18),
                       HospitalizationTime = new TimeOnly(11, 00),
                       DischargeDate = new DateOnly(2026, 04, 24),
                       DischargeTime = new TimeOnly(15, 30),
                       PhoneNumber = "0891122334",
                       UCN = "9548127883",
                       DoctorId = new Guid("2a4e1d97-8411-4cf8-9da2-af9452f16eca"),
                       RoomId = new Guid("85242158-d3c2-4542-bc27-88caf4a131c6")
                   },
                    new Patient
                    {
                        ID = new Guid("ba24d819-24f4-4e94-bb71-77765076d46c"),
                        UserId = new Guid("c5982307-ef67-4b65-b438-8f9e1e3a240b"),
                        BirthCity = "Sofia",
                        DateOfBirth = new DateOnly(2002, 09, 15),
                        HospitalizationDate = new DateOnly(2026, 06, 01),
                        HospitalizationTime = new TimeOnly(09, 45),
                        DischargeDate = new DateOnly(2026, 06, 15),
                        DischargeTime = new TimeOnly(12, 30),
                        PhoneNumber = "0881239876",
                        UCN = "0249152349",
                        DoctorId = new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"),
                        RoomId = new Guid("c219c792-8a69-4850-ac40-a36f5f752786")
                    }
            };
            return patients;
        }
    }
}
