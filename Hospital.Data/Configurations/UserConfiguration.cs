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

               new User
               {
                   Id = new Guid("e7d8baba-f7b1-4ed0-9bbb-139dc13e878e"),
                   UserName = "kristen_gencheva",
                   NormalizedUserName = "KRISTEN_GENCHEVA",
                   Email = "kristeng@gmail.com",
                   NormalizedEmail = "KRISTENG@GMAIL.COM",
                   FirstName = "Kristen",
                   LastName = "Gencheva",
                   PasswordHash = "kristen!",
                   SecurityStamp = "9699665e-7685-48b4-9f44-846c4f923e3e",
                   ConcurrencyStamp = "f259740e-7d8a-4c91-9556-54776103b41e"
               },
new User
{
    Id = new Guid("072eae42-46ab-4919-aae5-073aef56c00d"),
    UserName = "ivan_petrov",
    NormalizedUserName = "IVAN_PETROV",
    Email = "ivanp@gmail.com",
    NormalizedEmail = "IVANP@GMAIL.COM",
    FirstName = "Ivan",
    LastName = "Petrov",
    PasswordHash = "ivan!",
    SecurityStamp = "c4d1f2a3-6b7c-4d8e-9f0a-1b2c3d4e5f6g",
    ConcurrencyStamp = "a1b2c3d4-e5f6-4a5b-6c7d-8e9f0a1b2c3d"
},
new User
{
    Id = new Guid("7c425879-d37a-48a6-91d9-2345120a3f6a"),
    UserName = "georgi_dimitrov",
    NormalizedUserName = "GEORGI_DIMITROV",
    Email = "gergid@gmail.com",
    NormalizedEmail = "GEORGID@GMAIL.COM",
    FirstName = "Georgi",
    LastName = "Dimitrov",
    PasswordHash = "georgi!",
    SecurityStamp = "d5e6f7a8-b9c0-4d1e-2f3a-4b5c6d7e8f9a",
    ConcurrencyStamp = "b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"
},
new User
{
    Id = new Guid("3d86822f-0eba-44ce-8484-27addbfe7357"),
    UserName = "nikolai_ivanov",
    NormalizedUserName = "NIKOLAI_IVANOV",
    Email = "nikolaii@gmail.com",
    NormalizedEmail = "NIKOLAII@GMAIL.COM",
    FirstName = "Nikolai",
    LastName = "Ivanov",
    PasswordHash = "nikolai!",
    SecurityStamp = "e6f7a8b9-c0d1-2e3f-4a5b-6c7d8e9f0a1b",
    ConcurrencyStamp = "c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"
},
new User
{
    Id = new Guid("7dca2bf8-df73-4dbf-a602-52e147eafe1e"),
    UserName = "dimitur_stoyanov",
    NormalizedUserName = "DIMITUR_STOYANOV",
    Email = "dimiturs@gmail.com",
    NormalizedEmail = "DIMITURS@GMAIL.COM",
    FirstName = "Dimitur",
    LastName = "Stoyanov",
    PasswordHash = "dimitur!",
    SecurityStamp = "f7a8b9c0-d1e2-3f4a-5b6c-7d8e9f0a1b2c",
    ConcurrencyStamp = "d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"
},
new User
{
    Id = new Guid("23b350b4-0dd6-43fc-b5dc-818faf2b74e6"),
    UserName = "aleksandur_nikolov",
    NormalizedUserName = "ALEKSANDUR_NIKOLOV",
    Email = "aleksandurn@gmail.com",
    NormalizedEmail = "ALEKSANDURN@GMAIL.COM",
    FirstName = "Aleksandur",
    LastName = "Nikolov",
    PasswordHash = "aleksandur!",
    SecurityStamp = "a8b9c0d1-e2f3-4a5b-6c7d-8e9f0a1b2c3d",
    ConcurrencyStamp = "e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b"
},
new User
{
    Id = new Guid("d9ccb374-6b17-4e66-9c11-79412a9e1e93"),
    UserName = "yoana_ilieva",
    NormalizedUserName = "YOANA_ILIEVA",
    Email = "yoanai@gmail.com",
    NormalizedEmail = "YOANAI@GMAIL.COM",
    FirstName = "Yoana",
    LastName = "Ilieva",
    PasswordHash = "yoana!",
    SecurityStamp = "b9c0d1e2-f3a4-5b6c-7d8e-9f0a1b2c3d4e",
    ConcurrencyStamp = "f6a7b8c9-d0e1-2f3a-4b5c-6d7e8f9a0b1c"
},
new User
{
    Id = new Guid("30f2b4ed-e0e3-4443-8595-4dc6e26b3338"),
    UserName = "viktoriya_nikolova",
    NormalizedUserName = "VIKTORIYA_NIKOLOVA",
    Email = "viktoriyan@gmail.com",
    NormalizedEmail = "VIKTORIYAN@GMAIL.COM",
    FirstName = "Viktoriya",
    LastName = "Nikolova",
    PasswordHash = "viktoriya!",
    SecurityStamp = "c0d1e2f3-a4b5-6c7d-8e9f-0a1b2c3d4e5f",
    ConcurrencyStamp = "a7b8c9d0-e1f2-3a4b-5c6d-7e8f9a0b1c2d"
},
new User
{
    Id = new Guid("51daaed0-67e7-4c4a-b254-2745af5365df"),
    UserName = "anna_aleksandrova",
    NormalizedUserName = "ANNA_ALEKSANDROVA",
    Email = "annaa@gmail.com",
    NormalizedEmail = "ANNAA@GMAIL.COM",
    FirstName = "Anna",
    LastName = "Aleksandrova",
    PasswordHash = "anna!",
    SecurityStamp = "d1e2f3a4-b5c6-7d8e-9f0a-1b2c3d4e5f6a",
    ConcurrencyStamp = "b8c9d0e1-f2a3-4b5c-6d7e-8f9a0b1c2d3e"
},
new User
{
    Id = new Guid("cbdfa704-0f6d-431f-8ede-dd952adacfc9"),
    UserName = "elena_petrova",
    NormalizedUserName = "ELENA_PETROVA",
    Email = "elenap@gmail.com",
    NormalizedEmail = "ELENAP@GMAIL.COM",
    FirstName = "Elena",
    LastName = "Petrova",
    PasswordHash = "elena!",
    SecurityStamp = "e2f3a4b5-c6d7-8e9f-0a1b-2c3d4e5f6a7b",
    ConcurrencyStamp = "c9d0e1f2-a3b4-5c6d-7e8f-9a0b1c2d3e4f"
},
new User
{
    Id = new Guid("f6662c6a-414b-4b5c-ae1b-7b31103dd464"),
    UserName = "aleksandra_georgieva",
    NormalizedUserName = "ALEKSANDRA_GEORGIEVA",
    Email = "aleksandrag@gmail.com",
    NormalizedEmail = "ALEKSANDRAG@GMAIL.COM",
    FirstName = "Aleksandra",
    LastName = "Georgieva",
    PasswordHash = "aleksandra!",
    SecurityStamp = "f3a4b5c6-d7e8-9f0a-1b2c-3d4e5f6a7b8c",
    ConcurrencyStamp = "d0e1f2a3-b4c5-6d7e-8f9a-0b1c2d3e4f5a"
},
new User
{
    Id = new Guid("354fa92a-6b54-4d12-b90c-9926dc906462"),
    UserName = "maria_ivanova",
    NormalizedUserName = "MARIA_IVANOVA",
    Email = "mariai@gmail.com",
    NormalizedEmail = "MARIAI@GMAIL.COM",
    FirstName = "Maria",
    LastName = "Ivanova",
    PasswordHash = "maria!",
    SecurityStamp = "a4b5c6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    ConcurrencyStamp = "e1f2a3b4-c5d6-7e8f-9a0b-1c2d3e4f5a6b"
},
new User
{
    Id = new Guid("02e72b22-0abd-4ce4-80d1-30b8c13f952b"),
    UserName = "desislava_dimitrova",
    NormalizedUserName = "DESISLAVA_DIMITROVA",
    Email = "desid@gmail.com",
    NormalizedEmail = "DESID@GMAIL.COM",
    FirstName = "Desislava",
    LastName = "Dimitrova",
    PasswordHash = "desislava!",
    SecurityStamp = "b5c6d7e8-f9a0-1b2c-3d4e-5f6a7b8c9d0e",
    ConcurrencyStamp = "f2a3b4c5-d6e7-8f9a-0b1c-2d3e4f5a6b7c"
},
new User
{
    Id = new Guid("741d970d-f405-4bd1-94b2-eec2c3fb33e2"),
    UserName = "gergana_vasileva",
    NormalizedUserName = "GERGANA_VASILEVA",
    Email = "gerganav@gmail.com",
    NormalizedEmail = "GERGANAV@GMAIL.COM",
    FirstName = "Gergana",
    LastName = "Vasileva",
    PasswordHash = "gergana!",
    SecurityStamp = "c6d7e8f9-a0b1-2c3d-4e5f-6a7b8c9d0e1f",
    ConcurrencyStamp = "0d1e2f3a-4b5c-6d7e-8f9a-0b1c2d3e4f5a"
},
new User
{
    Id = new Guid("355ad73e-6b7d-4ade-846d-7cab0da06629"),
    UserName = "ralitsa_kostova",
    NormalizedUserName = "RALITSA_KOSTOVA",
    Email = "ralitsak@gmail.com",
    NormalizedEmail = "RALITSAK@GMAIL.COM",
    FirstName = "Ralitsa",
    LastName = "Kostova",
    PasswordHash = "ralitsa!",
    SecurityStamp = "d7e8f9a0-b1c2-3d4e-5f6a-7b8c9d0e1f2a",
    ConcurrencyStamp = "1e2f3a4b-5c6d-7e8f-9a0b-1c2d3e4f5a6b"
},
new User
{
    Id = new Guid("a7e0d718-a822-48db-b8ff-82cff6dbd5c7"),
    UserName = "petar_georgiev",
    NormalizedUserName = "PETAR_GEORGIEV",
    Email = "petarg@gmail.com",
    NormalizedEmail = "PETARG@GMAIL.COM",
    FirstName = "Petar",
    LastName = "Georgiev",
    PasswordHash = "petar!",
    SecurityStamp = "e8f9a0b1-c2d3-4e5f-6a7b-8c9d0e1f2a3b",
    ConcurrencyStamp = "2f3a4b5c-6d7e-8f9a-0b1c-2d3e4f5a6b7c"
},
new User
{
    Id = new Guid("04347895-6b6e-4608-be4c-5f428b759669"),
    UserName = "stefan_kolev",
    NormalizedUserName = "STEFAN_KOLEV",
    Email = "stefank@gmail.com",
    NormalizedEmail = "STEFANK@GMAIL.COM",
    FirstName = "Stefan",
    LastName = "Kolev",
    PasswordHash = "stefan!",
    SecurityStamp = "f9a0b1c2-d3e4-5f6a-7b8c-9d0e1f2a3b4c",
    ConcurrencyStamp = "3a4b5c6d-7e8f-9a0b-1c2d-3e4f5a6b7c8d"
},
new User
{
    Id = new Guid("96747275-9c90-449e-a91c-eb6863183a27"),
    UserName = "borislav_todorov",
    NormalizedUserName = "BORISLAV_TODOROV",
    Email = "borislavt@gmail.com",
    NormalizedEmail = "BORISLAVT@GMAIL.COM",
    FirstName = "Borislav",
    LastName = "Todorov",
    PasswordHash = "borislav!",
    SecurityStamp = "a0b1c2d3-e4f5-6a7b-8c9d-0e1f2a3b4c5d",
    ConcurrencyStamp = "4b5c6d7e-8f9a-0b1c-2d3e-4f5a6b7c8d9e"
},
new User
{
    Id = new Guid("865c2545-7806-4857-a621-f035e520a596"),
    UserName = "hristo_vasilev",
    NormalizedUserName = "HRISTO_VASILEV",
    Email = "hristov@gmail.com",
    NormalizedEmail = "HRISTOV@GMAIL.COM",
    FirstName = "Hristo",
    LastName = "Vasilev",
    PasswordHash = "hristo!",
    SecurityStamp = "b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e",
    ConcurrencyStamp = "5c6d7e8f-9a0b-1c2d-3e4f-5a6b7c8d9e0f"
},
new User
{
    Id = new Guid("c5982307-ef67-4b65-b438-8f9e1e3a240b"),
    UserName = "martin_iliev",
    NormalizedUserName = "MARTIN_ILIEV",
    Email = "martini@gmail.com",
    NormalizedEmail = "MARTINI@GMAIL.COM",
    FirstName = "Martin",
    LastName = "Iliev",
    PasswordHash = "martin!",
    SecurityStamp = "c2d3e4f5-a6b7-8c9d-0e1f-2a3b4c5d6e7f",
    ConcurrencyStamp = "6d7e8f9a-0b1c-2d3e-4f5a-6b7c8d9e0f1a"
});
        }
    }
}