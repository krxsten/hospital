using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Data.Configurations
{
    public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.HasOne(n => n.User)
                   .WithOne(u => u.Nurse)
                   .HasForeignKey<Nurse>(n => n.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Nurse { ID = new Guid("698d0579-913c-42af-8a45-924cd9f740bb"), UserId = new Guid("354fa92a-6b54-4d12-b90c-9926dc906462"), SpecializationId = new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), ShiftId = new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },
                new Nurse { ID = new Guid("e5f97752-f18b-4b36-8c47-4d238cb0e01f"), UserId = new Guid("02e72b22-0abd-4ce4-80d1-30b8c13f952b"), SpecializationId = new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), ShiftId = new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },

                new Nurse { ID = new Guid("e6a3850b-7a1e-465c-83fd-57b1134c68d2"), UserId = new Guid("741d970d-f405-4bd1-94b2-eec2c3fb33e2"), SpecializationId = new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), ShiftId = new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" },

                new Nurse { ID = new Guid("9fb6048e-03ae-407f-a83e-51b6c5399b41"), UserId = new Guid("355ad73e-6b7d-4ade-846d-7cab0da06629"), SpecializationId = new Guid("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"), ShiftId = new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), IsAccepted = true, ImageURL = "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", PublicID = "logo_ro7b24" }
            );
        }

        public List<Nurse> CreateNurses() => new List<Nurse>();
    }
}