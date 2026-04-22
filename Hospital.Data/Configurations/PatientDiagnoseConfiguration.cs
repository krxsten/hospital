using Hospital.Data.Entities;
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
    public class PatientDiagnoseConfiguration : IEntityTypeConfiguration<PatientAndDiagnose>
    {
        public void Configure(EntityTypeBuilder<PatientAndDiagnose> builder)
        {
            builder
                .HasOne(x => x.Patient)
                .WithMany(p => p.PatientDiagnoses)
                .HasForeignKey(x => x.PatientId);

            builder
                .HasOne(x => x.Diagnose)
                .WithMany(d => d.ListOfPatientsAndDiagnoses)
                .HasForeignKey(x => x.DiagnoseId);

            builder
                .HasIndex(x => new { x.PatientId, x.DiagnoseId })
                .IsUnique();

            builder.HasData(
                new PatientAndDiagnose
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    PatientId = new Guid("9783d8b3-014f-477a-b951-6ff87057b44f"),
                    DiagnoseId = new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216")
                },
                new PatientAndDiagnose
                {
                    Id = new Guid("22222222-2222-2222-2222-222222222222"),
                    PatientId = new Guid("06999216-e4e9-4455-856d-5246259b2684"),
                    DiagnoseId = new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f")
                },
                new PatientAndDiagnose
                {
                    Id = new Guid("33333333-3333-3333-3333-333333333333"),
                    PatientId = new Guid("eac603b7-4022-4fb1-8c04-28db2f0f2162"),
                    DiagnoseId = new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e")
                }
            );
        }

        public List<PatientAndDiagnose> CreatePatientDiagnose()
        {
            List<PatientAndDiagnose> patientAndDiagnoses = new List<PatientAndDiagnose>()
            {
               
            };
            return patientAndDiagnoses;
        }
    }
}
