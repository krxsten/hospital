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
                new Diagnose { ID = new Guid("91b25ace-9e01-4c25-b0ea-3c8bad060315"), Name = "Influenza", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764569/b992faff-00ff-4c16-9a01-e8aa73b286cc.png", PublicID="b992faff-00ff-4c16-9a01-e8aa73b286cc" },
                new Diagnose { ID = new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"), Name = "Hypertension", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764927/db29fdde-76a6-4e2d-974c-5bfd4d9bbd71.png", PublicID="v1775764927/db29fdde-76a6-4e2d-974c-5bfd4d9bbd71"  },
                new Diagnose { ID = new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c"), Name = "Diabetes", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764641/48ae0b79-490c-4fb9-84cd-9c688ac54aea.png", PublicID="48ae0b79-490c-4fb9-84cd-9c688ac54aea"  },
                new Diagnose { ID = new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"), Name = "Pneumonia", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764945/0f4c597f-8d2c-4a88-b923-fe49d824b5e6.png", PublicID="0f4c597f-8d2c-4a88-b923-fe49d824b5e6"  },
                new Diagnose { ID = new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0"), Name = "Migraine", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764975/328d764d-3a97-4933-8f28-a89c2f8805b7.png", PublicID="328d764d-3a97-4933-8f28-a89c2f8805b7" },
                new Diagnose { ID = new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"), Name = "Fracture", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764961/a72a5e9e-61ae-442b-8853-5bc76a336170.png", PublicID="a72a5e9e-61ae-442b-8853-5bc76a336170"  },
                new Diagnose { ID = new Guid("d793f73f-51a0-4ff0-b6fa-5ffd4d47cd15"), Name = "Asthma", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764810/51b2718c-af7a-41f3-96fb-aba23237bb9a.png", PublicID="51b2718c-af7a-41f3-96fb-aba23237bb9a" },
                new Diagnose { ID = new Guid("c97e4a52-4926-4268-8261-82739340e77b"), Name = "Raynauld's syndrome", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764908/9dfa36a2-4cc8-4319-acf1-1382c3766327.png", PublicID="9dfa36a2-4cc8-4319-acf1-1382c3766327"  },
                new Diagnose { ID = new Guid("885aea72-26c5-48b5-88cc-7128b7e81499"), Name = "Osteoporosis", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764880/1272e03f-5e20-4989-b638-ebe83f57bf86.png", PublicID="1272e03f-5e20-4989-b638-ebe83f57bf86" },
                new Diagnose { ID = new Guid("49a578fc-5f30-40b6-810f-3ca54b0e2a02"), Name = "Hashimoto's disease", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764602/71e1a51c-2d54-4ea6-8cc1-34b6716d2816.png", PublicID="71e1a51c-2d54-4ea6-8cc1-34b6716d2816"  },
                new Diagnose { ID = new Guid("2dfdf306-41d8-4aca-abd6-91ed7d4adc8a"), Name = "Bronchitis", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764674/82339fab-d0e6-48a4-ba49-5ce412cac822.png", PublicID="82339fab-d0e6-48a4-ba49-5ce412cac822z"  }
            };

            return diagnoses;
        }
    }
}
