using Hospital.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { Id = new Guid("e7d8baba-f7b1-4ed0-9bbb-139dc13e878e"), UserName = "gencheva", NormalizedUserName = "GENCHEVA", Email = "gencheva@gmail.com", NormalizedEmail = "GENCHEVA@GMAIL.COM", FirstName = "Admin", LastName = "User", PasswordHash = "HASH", SecurityStamp = "S1", ConcurrencyStamp = "C1" },
                // Doctors (IDs matched to your DoctorConfiguration)
                new User { Id = new Guid("072eae42-46ab-4919-aae5-073aef56c00d"), UserName = "doc1", NormalizedUserName = "DOC1", Email = "doc1@h.com", NormalizedEmail = "DOC1@H.COM", FirstName = "John", LastName = "Doe", PasswordHash = "HASH", SecurityStamp = "S2", ConcurrencyStamp = "C2" },
                new User { Id = new Guid("7c425879-d37a-48a6-91d9-2345120a3f6a"), UserName = "doc2", NormalizedUserName = "DOC2", Email = "doc2@h.com", NormalizedEmail = "DOC2@H.COM", FirstName = "Jane", LastName = "Smith", PasswordHash = "HASH", SecurityStamp = "S3", ConcurrencyStamp = "C3" },
                new User { Id = new Guid("3d86822f-0eba-44ce-8484-27addbfe7357"), UserName = "doc3", NormalizedUserName = "DOC3", Email = "doc3@h.com", NormalizedEmail = "DOC3@H.COM", FirstName = "Bob", LastName = "Brown", PasswordHash = "HASH", SecurityStamp = "S4", ConcurrencyStamp = "C4" },
                new User { Id = new Guid("7dca2bf8-df73-4dbf-a602-52e147eafe1e"), UserName = "doc4", NormalizedUserName = "DOC4", Email = "doc4@h.com", NormalizedEmail = "DOC4@H.COM", FirstName = "Alice", LastName = "White", PasswordHash = "HASH", SecurityStamp = "S5", ConcurrencyStamp = "C5" },
                new User { Id = new Guid("23b350b4-0dd6-43fc-b5dc-818faf2b74e6"), UserName = "doc5", NormalizedUserName = "DOC5", Email = "doc5@h.com", NormalizedEmail = "DOC5@H.COM", FirstName = "Charlie", LastName = "Green", PasswordHash = "HASH", SecurityStamp = "S6", ConcurrencyStamp = "C6" },
                new User { Id = new Guid("d9ccb374-6b17-4e66-9c11-79412a9e1e93"), UserName = "doc6", NormalizedUserName = "DOC6", Email = "doc6@h.com", NormalizedEmail = "DOC6@H.COM", FirstName = "Dave", LastName = "Black", PasswordHash = "HASH", SecurityStamp = "S7", ConcurrencyStamp = "C7" },
                new User { Id = new Guid("30f2b4ed-e0e3-4443-8595-4dc6e26b3338"), UserName = "doc7", NormalizedUserName = "DOC7", Email = "doc7@h.com", NormalizedEmail = "DOC7@H.COM", FirstName = "Eve", LastName = "Grey", PasswordHash = "HASH", SecurityStamp = "S8", ConcurrencyStamp = "C8" },
                new User { Id = new Guid("51daaed0-67e7-4c4a-b254-2745af5365df"), UserName = "doc8", NormalizedUserName = "DOC8", Email = "doc8@h.com", NormalizedEmail = "DOC8@H.COM", FirstName = "Frank", LastName = "Blue", PasswordHash = "HASH", SecurityStamp = "S9", ConcurrencyStamp = "C9" },
                new User { Id = new Guid("cbdfa704-0f6d-431f-8ede-dd952adacfc9"), UserName = "doc9", NormalizedUserName = "DOC9", Email = "doc9@h.com", NormalizedEmail = "DOC9@H.COM", FirstName = "Grace", LastName = "Red", PasswordHash = "HASH", SecurityStamp = "S10", ConcurrencyStamp = "C10" },
                new User { Id = new Guid("f6662c6a-414b-4b5c-ae1b-7b31103dd464"), UserName = "doc10", NormalizedUserName = "DOC10", Email = "doc10@h.com", NormalizedEmail = "DOC10@H.COM", FirstName = "Hank", LastName = "Yellow", PasswordHash = "HASH", SecurityStamp = "S11", ConcurrencyStamp = "C11" },
                // Nurses (New IDs for NurseConfiguration)
                new User { Id = new Guid("354fa92a-6b54-4d12-b90c-9926dc906462"), UserName = "nurse1", NormalizedUserName = "NURSE1", Email = "n1@h.com", NormalizedEmail = "N1@H.COM", FirstName = "Nurse", LastName = "One", PasswordHash = "HASH", SecurityStamp = "S12", ConcurrencyStamp = "C12" },
                new User { Id = new Guid("02e72b22-0abd-4ce4-80d1-30b8c13f952b"), UserName = "nurse2", NormalizedUserName = "NURSE2", Email = "n2@h.com", NormalizedEmail = "N2@H.COM", FirstName = "Nurse", LastName = "Two", PasswordHash = "HASH", SecurityStamp = "S13", ConcurrencyStamp = "C13" },
                new User { Id = new Guid("741d970d-f405-4bd1-94b2-eec2c3fb33e2"), UserName = "nurse3", NormalizedUserName = "NURSE3", Email = "n3@h.com", NormalizedEmail = "N3@H.COM", FirstName = "Nurse", LastName = "Three", PasswordHash = "HASH", SecurityStamp = "S14", ConcurrencyStamp = "C14" },
                new User { Id = new Guid("355ad73e-6b7d-4ade-846d-7cab0da06629"), UserName = "nurse4", NormalizedUserName = "NURSE4", Email = "n4@h.com", NormalizedEmail = "N4@H.COM", FirstName = "Nurse", LastName = "Four", PasswordHash = "HASH", SecurityStamp = "S15", ConcurrencyStamp = "C15" },
                new User { Id = new Guid("a7e0d718-a822-48db-b8ff-82cff6dbd5c7"), UserName = "patient1", NormalizedUserName = "PATIENT1", Email = "p1@h.com", NormalizedEmail = "P1@H.COM", FirstName = "Patient", LastName = "One", PasswordHash = "HASH", SecurityStamp = "S16", ConcurrencyStamp = "C16" },
                new User { Id = new Guid("04347895-6b6e-4608-be4c-5f428b759669"), UserName = "patient2", NormalizedUserName = "PATIENT2", Email = "p2@h.com", NormalizedEmail = "P2@H.COM", FirstName = "Patient", LastName = "Two", PasswordHash = "HASH", SecurityStamp = "S17", ConcurrencyStamp = "C17" },
                new User { Id = new Guid("96747275-9c90-449e-a91c-eb6863183a27"), UserName = "patient3", NormalizedUserName = "PATIENT3", Email = "p3@h.com", NormalizedEmail = "P3@H.COM", FirstName = "Patient", LastName = "Three", PasswordHash = "HASH", SecurityStamp = "S18", ConcurrencyStamp = "C18" },
                new User { Id = new Guid("865c2545-7806-4857-a621-f035e520a596"), UserName = "patient4", NormalizedUserName = "PATIENT4", Email = "p4@h.com", NormalizedEmail = "P4@H.COM", FirstName = "Patient", LastName = "Four", PasswordHash = "HASH", SecurityStamp = "S19", ConcurrencyStamp = "C19" },
                new User { Id = new Guid("c5982307-ef67-4b65-b438-8f9e1e3a240b"), UserName = "patient5", NormalizedUserName = "PATIENT5", Email = "p5@h.com", NormalizedEmail = "P5@H.COM", FirstName = "Patient", LastName = "Five", PasswordHash = "HASH", SecurityStamp = "S20", ConcurrencyStamp = "C20" }
            );
        }
    }
}