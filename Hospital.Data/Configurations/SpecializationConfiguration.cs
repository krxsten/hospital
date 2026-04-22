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
            builder.HasData(new Specialization { ID = new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), SpecializationName = "Infectious Disease", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111137_nbbhc6.png", PublicID = "Screenshot_2026-04-02_111137_nbbhc6" },
                new Specialization { ID = new Guid("7a67c94b-50fb-4043-83f1-afdade20b451"), SpecializationName = "Cardiology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111310_zgxd8c.png", PublicID = "Screenshot_2026-04-02_111310_zgxd8c" },
                new Specialization { ID = new Guid("c484edec-d525-438a-92c4-ad80a9a41878"), SpecializationName = "Endocrinology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111403_uhhj71.png", PublicID = "Screenshot_2026-04-02_111403_uhhj71" },
                new Specialization { ID = new Guid("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"), SpecializationName = "Pulmonology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111428_ukzxth.png", PublicID = "Screenshot_2026-04-02_111428_ukzxth" },
                new Specialization { ID = new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), SpecializationName = "Neurology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111521_asrj53.png", PublicID = "Screenshot_2026-04-02_111521_asrj53" },
                new Specialization { ID = new Guid("e43cf086-24ad-4a75-b47e-549b8d8e467c"), SpecializationName = "Traumatology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111550_rletvt.png", PublicID = "Screenshot_2026-04-02_111550_rletvt" },
                new Specialization { ID = new Guid("e82cc806-165f-4554-91f8-c9d9ae4909e5"), SpecializationName = "Allergy & Immunology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111610_awmauh.png", PublicID = "Screenshot_2026-04-02_111610_awmauh" },
                new Specialization { ID = new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), SpecializationName = "Rheumatology", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1776836982/Screenshot_2026-04-02_111632_yhnkf5.png", PublicID = "Screenshot_2026-04-02_111632_yhnkf5" },
                new Specialization { ID = new Guid("4ee4ce69-da26-4f66-85cf-30623200cbf4"), SpecializationName = "Orthopedics", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1776837054/Screenshot_2026-04-02_111739_kgdwdt.png" , PublicID = "Screenshot_2026-04-02_111739_kgdwdt" },
                new Specialization { ID = new Guid("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"), SpecializationName = "Respiratory", ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117704/Screenshot_2026-04-02_111758_t9nbmc.png", PublicID = "Screenshot_2026-04-02_111758_t9nbmc" });
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
