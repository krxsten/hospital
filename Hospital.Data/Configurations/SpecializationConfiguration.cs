using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasData(new Specialization { ID = new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), SpecializationName = "Infectious Disease", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("7a67c94b-50fb-4043-83f1-afdade20b451"), SpecializationName = "Cardiology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("c484edec-d525-438a-92c4-ad80a9a41878"), SpecializationName = "Endocrinology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"), SpecializationName = "Pulmonology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), SpecializationName = "Neurology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("e43cf086-24ad-4a75-b47e-549b8d8e467c"), SpecializationName = "Traumatology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("e82cc806-165f-4554-91f8-c9d9ae4909e5"), SpecializationName = "Allergy & Immunology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), SpecializationName = "Rheumatology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("4ee4ce69-da26-4f66-85cf-30623200cbf4"), SpecializationName = "Orthopedics", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("1a298771-7773-4c3b-828c-fff8dcedf0e9"), SpecializationName = "Endocrinology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Specialization { ID = new Guid("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"), SpecializationName = "Respiratory", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" });
        }
        public List<Specialization> CreateSpecializations()
        {
            List<Specialization> specializations = new List<Specialization>()
            {
               
            };
            return specializations;
        }
    }
}
