using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasOne(d => d.User)
                   .WithOne(u => u.Doctor)
                   .HasForeignKey<Doctor>(d => d.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Doctor { ID = new Guid("08ccdf4b-02ad-464f-9ef2-fb73ceee1826"), UserId = new Guid("072eae42-46ab-4919-aae5-073aef56c00d"), SpecializationId = new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), ShiftId = new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" },
                new Doctor { ID = new Guid("e1ceefa2-e56b-4395-9049-c689bea9417f"), UserId = new Guid("7c425879-d37a-48a6-91d9-2345120a3f6a"), SpecializationId = new Guid("7a67c94b-50fb-4043-83f1-afdade20b451"), ShiftId = new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" },

                new Doctor { ID = new Guid("186296e2-7114-4291-aa3b-897b96c75c21"), UserId = new Guid("3d86822f-0eba-44ce-8484-27addbfe7357"), SpecializationId = new Guid("c484edec-d525-438a-92c4-ad80a9a41878"), ShiftId = new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" },

                new Doctor { ID = new Guid("2a4e1d97-8411-4cf8-9da2-af9452f16eca"), UserId = new Guid("7dca2bf8-df73-4dbf-a602-52e147eafe1e"), SpecializationId = new Guid("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"), ShiftId = new Guid("aaaaaaf9-5059-478c-8df4-db3fd4342b14"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" },
                new Doctor { ID = new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"), UserId = new Guid("23b350b4-0dd6-43fc-b5dc-818faf2b74e6"), SpecializationId = new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), ShiftId = new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" },

                new Doctor { ID = new Guid("8e296807-75cf-45dd-bdfc-179495465c09"), UserId = new Guid("d9ccb374-6b17-4e66-9c11-79412a9e1e93"), SpecializationId = new Guid("e43cf086-24ad-4a75-b47e-549b8d8e467c"), ShiftId = new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" },

                new Doctor { ID = new Guid("26189d95-7ca7-40f7-9384-8454cfb99247"), UserId = new Guid("30f2b4ed-e0e3-4443-8595-4dc6e26b3338"), SpecializationId = new Guid("e82cc806-165f-4554-91f8-c9d9ae4909e5"), ShiftId = new Guid("aaaaaaf9-5059-478c-8df4-db3fd4342b14"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" },
                new Doctor { ID = new Guid("6d3dacc1-3b7a-4e43-8caa-5b82a6f4a21f"), UserId = new Guid("51daaed0-67e7-4c4a-b254-2745af5365df"), SpecializationId = new Guid("1a298771-7773-4c3b-828c-fff8dcedf0e9"), ShiftId = new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" },
                new Doctor { ID = new Guid("0f6d5fde-bc75-4df5-8886-090806665b82"), UserId = new Guid("cbdfa704-0f6d-431f-8ede-dd952adacfc9"), SpecializationId = new Guid("4ee4ce69-da26-4f66-85cf-30623200cbf4"), ShiftId = new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" },
                new Doctor { ID = new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"), UserId = new Guid("f6662c6a-414b-4b5c-ae1b-7b31103dd464"), SpecializationId = new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), ShiftId = new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", CloudinaryID = "logo_ro7b24" }
            );
        }

        public List<Doctor> CreateDoctors() => new List<Doctor>();
    }
}