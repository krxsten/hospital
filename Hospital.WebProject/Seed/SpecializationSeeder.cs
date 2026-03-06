using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class SpecializationSeeder : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasData(
                new Specialization { ID = Guid.Parse("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), SpecializationName = "Infectious Disease", Image = "infections.jpg" },
                new Specialization { ID = Guid.Parse("7a67c94b-50fb-4043-83f1-afdade20b451"), SpecializationName = "Cardiology", Image = "cardiology.jpg" },
                new Specialization { ID = Guid.Parse("c484edec-d525-438a-92c4-ad80a9a41878"), SpecializationName = "Endocrinology", Image = "endocrinology.jpg" },
                new Specialization { ID = Guid.Parse("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"), SpecializationName = "Pulmonology", Image = "pulmonology.jpg" },
                new Specialization { ID = Guid.Parse("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), SpecializationName = "Neurology", Image = "neurology.jpg" },
                new Specialization { ID = Guid.Parse("e43cf086-24ad-4a75-b47e-549b8d8e467c"), SpecializationName = "Traumatology", Image = "Traumatology.jpg" },
                new Specialization { ID = Guid.Parse("e82cc806-165f-4554-91f8-c9d9ae4909e5"), SpecializationName = "Allergy & Immunology", Image = "allergy.jpg" },
                new Specialization { ID = Guid.Parse("a5519f22-cefb-4771-a5e9-de7b40817df8"), SpecializationName = "Rheumatology", Image = "rheumatology.jpg" },
                new Specialization { ID = Guid.Parse("4ee4ce69-da26-4f66-85cf-30623200cbf4"), SpecializationName = "Orthopedics", Image = "orthopedics.jpg" },
                new Specialization { ID = Guid.Parse("1a298771-7773-4c3b-828c-fff8dcedf0e9"), SpecializationName = "Endocrinology", Image = "endocrinology.jpg" },
                new Specialization { ID = Guid.Parse("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"), SpecializationName = "Respiratory", Image = "respiratory.jpg" }
            );
        }
    }
}
