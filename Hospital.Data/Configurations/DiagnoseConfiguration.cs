using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class DiagnoseConfiguration : IEntityTypeConfiguration<Diagnose>
    {
        public void Configure(EntityTypeBuilder<Diagnose> builder)
        {
            builder.HasData(CreateDiagnoses());
        }
        public IEnumerable<Diagnose> CreateDiagnoses()
        {
            List<Diagnose> diagnoses = new List<Diagnose>()
            {
                new Diagnose { ID = new Guid("91b25ace-9e01-4c25-b0ea-3c8bad060315"), Name = "Influenza", Image = "flu.jpg" },
                new Diagnose { ID = new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"), Name = "Hypertension", Image = "hypertension.jpg" },
                new Diagnose { ID = new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c"), Name = "Diabetes", Image = "diabetes.jpg" },
                new Diagnose { ID = new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"), Name = "Pneumonia", Image = "pneumonia.jpg" },
                new Diagnose { ID = new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0"), Name = "Migraine", Image = "migraine.jpg" },
                new Diagnose { ID = new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"), Name = "Fracture", Image = "fracture.jpg" },
                new Diagnose { ID = new Guid("d793f73f-51a0-4ff0-b6fa-5ffd4d47cd15"), Name = "Asthma", Image = "asthma.jpg" },
                new Diagnose { ID = new Guid("c97e4a52-4926-4268-8261-82739340e77b"), Name = "Raynauld's syndrome", Image = "raynauld.jpg" },
                new Diagnose { ID = new Guid("885aea72-26c5-48b5-88cc-7128b7e81499"), Name = "Osteoporosis", Image = "asthma.jpg" },
                new Diagnose { ID = new Guid("49a578fc-5f30-40b6-810f-3ca54b0e2a02"), Name = "Hashimoto's disease", Image = "hashimoto.jpg" },
                new Diagnose { ID = new Guid("2dfdf306-41d8-4aca-abd6-91ed7d4adc8a"), Name = "Bronchitis", Image = "bronchitis.jpg" }
            };

            return diagnoses;
        }
    }
}
