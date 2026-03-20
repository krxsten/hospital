using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.HasData(
                new Medication
                {
                    ID = new Guid("95344cea-8f8e-413e-8f2f-769749bee51b"),
                    Name = "Paracetamol",
                    Description = "Pain relief",
                    SideEffects = "Nausea",
                    DiagnoseID = new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0") // Migraine
                },
                new Medication
                {
                    ID = new Guid("754771d0-8581-4a0b-aa8e-fe312d90e5a5"),
                    Name = "Ibuprofen",
                    Description = "Anti inflammatory",
                    SideEffects = "Stomach pain",
                    DiagnoseID = new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e") // Fracture
                },
                new Medication
                {
                    ID = new Guid("7cf144e7-7d29-4991-948d-ffb592c85488"),
                    Name = "Amoxicillin",
                    Description = "Antibiotic",
                    SideEffects = "Allergy",
                    DiagnoseID = new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216") // Pneumonia
                },
                new Medication
                {
                    ID = new Guid("3857c780-691f-4ef1-ae47-853911e00a97"),
                    Name = "Aspirin",
                    Description = "Blood thinner",
                    SideEffects = "Bleeding",
                    DiagnoseID = new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f") // Hypertension
                },
                new Medication
                {
                    ID = new Guid("04e1b6ac-4511-4cb2-91ad-ff8d8bf095c5"),
                    Name = "Metformin",
                    Description = "Diabetes medication",
                    SideEffects = "Fatigue",
                    DiagnoseID = new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c") // Diabetes
                },
                new Medication
                {
                    ID = new Guid("a73e5de4-cbe9-4e87-87a7-39230af38a1e"),
                    Name = "Lisinopril",
                    Description = "Blood pressure",
                    SideEffects = "Cough",
                    DiagnoseID = new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f") // Hypertension
                },
                new Medication
                {
                    ID = new Guid("ea31422a-7b5b-42f1-a476-b9aaab041732"),
                    Name = "Omeprazole",
                    Description = "Acid reflux",
                    SideEffects = "Headache",
                    DiagnoseID = new Guid("c97e4a52-4926-4268-8261-82739340e77b") // Raynauld's syndrome
                }
            );
        }

        public List<Medication> CreateMedications()
        {
            return new List<Medication>();
        }
    }
}