using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class DoctorsSeeder : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            {
                builder.HasData(
            new Doctor
            {
                UserId = Guid.Parse("072eae42-46ab-4919-aae5-073aef56c00d"),
                SpecializationId = Guid.Parse("8a42cdba-ff58-4129-aea8-ae4c3b32f353"),
                ShiftId = Guid.Parse("c1111111-1111-1111-1111-111111111111"),
                IsAccepted = true,
                Image = "doctor1.jpg"
            },
            new Doctor
            {
                UserId = Guid.Parse("7c425879-d37a-48a6-91d9-2345120a3f6a"),
                SpecializationId = Guid.Parse("7a67c94b-50fb-4043-83f1-afdade20b451"),
                ShiftId = Guid.Parse("c2222222-2222-2222-2222-222222222222"),
                IsAccepted = true,
                Image = "doctor2.jpg"
            },
            new Doctor
            {
                UserId = Guid.Parse("3d86822f-0eba-44ce-8484-27addbfe7357"),
                SpecializationId = Guid.Parse("c484edec-d525-438a-92c4-ad80a9a41878"),
                ShiftId = Guid.Parse("c3333333-3333-3333-3333-333333333333"),
                IsAccepted = true,
                Image = "doctor3.jpg"
            },
            new Doctor
            {
                UserId = Guid.Parse("7dca2bf8-df73-4dbf-a602-52e147eafe1e"),
                SpecializationId = Guid.Parse("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"),
                ShiftId = Guid.Parse("c4444444-4444-4444-4444-444444444444"),
                IsAccepted = true,
                Image = "doctor4.jpg"
            },
            new Doctor
            {
                UserId = Guid.Parse("23b350b4-0dd6-43fc-b5dc-818faf2b74e6"),
                SpecializationId = Guid.Parse("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"),
                ShiftId = Guid.Parse("c5555555-5555-5555-5555-555555555555"),
                IsAccepted = true,
                Image = "doctor5.jpg"
            },
            new Doctor
            {
                UserId = Guid.Parse("d9ccb374-6b17-4e66-9c11-79412a9e1e93"),
                SpecializationId = Guid.Parse("e43cf086-24ad-4a75-b47e-549b8d8e467c"),
                ShiftId = Guid.Parse("c6666666-6666-6666-6666-666666666666"),
                IsAccepted = true,
                Image = "doctor6.jpg"
            },
            new Doctor
            {
                UserId = Guid.Parse("30f2b4ed-e0e3-4443-8595-4dc6e26b3338"),
                SpecializationId = Guid.Parse("e82cc806-165f-4554-91f8-c9d9ae4909e5"),
                ShiftId = Guid.Parse("c7777777-7777-7777-7777-777777777777"),
                IsAccepted = true,
                Image = "doctor7.jpg"
            },
             new Doctor
             {
                 UserId = Guid.Parse("51daaed0-67e7-4c4a-b254-2745af5365df"),
                 SpecializationId = Guid.Parse("1a298771-7773-4c3b-828c-fff8dcedf0e9"),
                 ShiftId = Guid.Parse("4ee4ce69-da26-4f66-85cf-30623200cbf4"),
                 IsAccepted = true,
                 Image = "doctor8.jpg"
             },
              new Doctor
              {
                  UserId = Guid.Parse("cbdfa704-0f6d-431f-8ede-dd952adacfc9"),
                  SpecializationId = Guid.Parse("4ee4ce69-da26-4f66-85cf-30623200cbf4"),
                  ShiftId = Guid.Parse(""),
                  IsAccepted = true,
                  Image = "doctor9.jpg"
              },
               new Doctor
               {
                   UserId = Guid.Parse("f6662c6a-414b-4b5c-ae1b-7b31103dd464"),
                   SpecializationId = Guid.Parse("a5519f22-cefb-4771-a5e9-de7b40817df8"),
                   ShiftId = Guid.Parse(""),
                   IsAccepted = true,
                   Image = "doctor10.jpg"
               }

        );
            }
        }
    }
}


