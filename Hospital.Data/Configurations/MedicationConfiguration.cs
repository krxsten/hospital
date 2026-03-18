using Hospital.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.HasData(
                new Medication { ID = Guid.Parse("95344cea-8f8e-413e-8f2f-769749bee51b"), Name = "Paracetamol", Description = "Pain relief", SideEffects = "Nausea" },
                new Medication { ID = Guid.Parse("754771d0-8581-4a0b-aa8e-fe312d90e5a5"), Name = "Ibuprofen", Description = "Anti inflammatory", SideEffects = "Stomach pain" },
                new Medication { ID = Guid.Parse("7cf144e7-7d29-4991-948d-ffb592c85488"), Name = "Amoxicillin", Description = "Antibiotic", SideEffects = "Allergy" },
                new Medication { ID = Guid.Parse("3857c780-691f-4ef1-ae47-853911e00a97"), Name = "Aspirin", Description = "Blood thinner", SideEffects = "Bleeding" },
                new Medication { ID = Guid.Parse("04e1b6ac-4511-4cb2-91ad-ff8d8bf095c5"), Name = "Metformin", Description = "Diabetes medication", SideEffects = "Fatigue" },
                new Medication { ID = Guid.Parse("a73e5de4-cbe9-4e87-87a7-39230af38a1e"), Name = "Lisinopril", Description = "Blood pressure", SideEffects = "Cough" },
                new Medication { ID = Guid.Parse("ea31422a-7b5b-42f1-a476-b9aaab041732"), Name = "Omeprazole", Description = "Acid reflux", SideEffects = "Headache" }
            );
        }

    }
}
