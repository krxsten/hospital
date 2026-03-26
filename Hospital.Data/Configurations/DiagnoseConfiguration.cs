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
                new Diagnose { ID = new Guid("91b25ace-9e01-4c25-b0ea-3c8bad060315"), Name = "Influenza", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774522942/716959ed-ecf1-4c3e-a6ad-dfb7caca7863.png", PublicID="716959ed-ecf1-4c3e-a6ad-dfb7caca7863" },
                new Diagnose { ID = new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"), Name = "Hypertension", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523282/5ab0f5a2-2ef5-4769-b2a7-9e485afd8a21.png", PublicID="5ab0f5a2-2ef5-4769-b2a7-9e485afd8a21"  },
                new Diagnose { ID = new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c"), Name = "Diabetes", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774522900/79aadf40-1666-44d1-8a39-b72d08f433ac.png", PublicID="79aadf40-1666-44d1-8a39-b72d08f433ac"  },
                new Diagnose { ID = new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"), Name = "Pneumonia", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774522988/12f38fbe-0962-464f-89fc-26cf3598a478.png", PublicID="12f38fbe-0962-464f-89fc-26cf3598a478"  },
                new Diagnose { ID = new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0"), Name = "Migraine", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523421/31435cc9-7c7f-47ba-94c6-8e14a8790876.png", PublicID="31435cc9-7c7f-47ba-94c6-8e14a8790876" },
                new Diagnose { ID = new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"), Name = "Fracture", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523396/de9b8762-40d7-459f-9f11-aeb90f41f4a8.png", PublicID="de9b8762-40d7-459f-9f11-aeb90f41f4a8"  },
                new Diagnose { ID = new Guid("d793f73f-51a0-4ff0-b6fa-5ffd4d47cd15"), Name = "Asthma", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523070/38a63bdf-b3a2-4719-91d1-e874170d0387.png", PublicID="38a63bdf-b3a2-4719-91d1-e874170d0387" },
                new Diagnose { ID = new Guid("c97e4a52-4926-4268-8261-82739340e77b"), Name = "Raynauld's syndrome", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523162/1c6fae53-754a-48e0-92ba-309ceaed5972.png", PublicID="1c6fae53-754a-48e0-92ba-309ceaed5972"  },
                new Diagnose { ID = new Guid("885aea72-26c5-48b5-88cc-7128b7e81499"), Name = "Osteoporosis", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523117/88005d89-b665-4fac-b2a8-1325f75a2809.png", PublicID="88005d89-b665-4fac-b2a8-1325f75a2809" },
                new Diagnose { ID = new Guid("49a578fc-5f30-40b6-810f-3ca54b0e2a02"), Name = "Hashimoto's disease", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774522872/f83a45f2-58db-4045-99a9-8b5829b13f05.png", PublicID="f83a45f2-58db-4045-99a9-8b5829b13f05"  },
                new Diagnose { ID = new Guid("2dfdf306-41d8-4aca-abd6-91ed7d4adc8a"), Name = "Bronchitis", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523335/c4510bbe-5309-4952-9ccd-326308a2c64a.png", PublicID="c4510bbe-5309-4952-9ccd-326308a2c64a"  }
            };

            return diagnoses;
        }
    }
}
