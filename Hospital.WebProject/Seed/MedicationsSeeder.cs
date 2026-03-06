using Hospital.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class MedicationsSeeder : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.HasData(
                new Medication { ID = Guid.NewGuid(), Name = "Paracetamol", Description = "Pain relief", SideEffects = "Nausea" },
                new Medication { ID = Guid.NewGuid(), Name = "Ibuprofen", Description = "Anti inflammatory", SideEffects = "Stomach pain" },
                new Medication { ID = Guid.NewGuid(), Name = "Amoxicillin", Description = "Antibiotic", SideEffects = "Allergy" },
                new Medication { ID = Guid.NewGuid(), Name = "Aspirin", Description = "Blood thinner", SideEffects = "Bleeding" },
                new Medication { ID = Guid.NewGuid(), Name = "Metformin", Description = "Diabetes medication", SideEffects = "Fatigue" },
                new Medication { ID = Guid.NewGuid(), Name = "Lisinopril", Description = "Blood pressure", SideEffects = "Cough" },
                new Medication { ID = Guid.NewGuid(), Name = "Omeprazole", Description = "Acid reflux", SideEffects = "Headache" }
            );
        }

    }
}
