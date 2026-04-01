using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class HomeMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("02e72b22-0abd-4ce4-80d1-30b8c13f952b"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "desid@gmail.com", "Desislava", "Dimitrova", "DESID@GMAIL.COM", "DESISLAVA_DIMITROVA", "desislava_dimitrova" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("04347895-6b6e-4608-be4c-5f428b759669"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "stefank@gmail.com", "Stefan", "Kolev", "STEFANK@GMAIL.COM", "STEFAN_KOLEV", "stefan_kolev" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("072eae42-46ab-4919-aae5-073aef56c00d"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "ivanp@gmail.com", "Ivan", "Petrov", "IVANP@GMAIL.COM", "IVAN_PETROV", "ivan_petrov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("23b350b4-0dd6-43fc-b5dc-818faf2b74e6"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "aleksandurn@gmail.com", "Aleksandur", "Nikolov", "ALEKSANDURN@GMAIL.COM", "ALEKSANDUR_NIKOLOV", "aleksandur_nikolov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("30f2b4ed-e0e3-4443-8595-4dc6e26b3338"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "viktoriyan@gmail.com", "Viktoriya", "Nikolova", "VIKTORIYAN@GMAIL.COM", "VIKTORIYA_NIKOLOVA", "viktoriya_nikolova" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("354fa92a-6b54-4d12-b90c-9926dc906462"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "mariai@gmail.com", "Maria", "Ivanova", "MARIAI@GMAIL.COM", "MARIA_IVANOVA", "maria_ivanova" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("355ad73e-6b7d-4ade-846d-7cab0da06629"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "ralitsak@gmail.com", "Ralitsa", "Kostova", "RALITSAK@GMAIL.COM", "RALITSA_KOSTOVA", "ralitsa_kostova" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3d86822f-0eba-44ce-8484-27addbfe7357"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "nikolaii@gmail.com", "Nikolai", "Ivanov", "NIKOLAII@GMAIL.COM", "NIKOLAI_IVANOV", "nikolai_ivanov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("51daaed0-67e7-4c4a-b254-2745af5365df"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "annaa@gmail.com", "Anna", "Aleksandrova", "ANNAA@GMAIL.COM", "ANNA_ALEKSANDROVA", "anna_aleksandrova" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("741d970d-f405-4bd1-94b2-eec2c3fb33e2"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "gerganav@gmail.com", "Gergana", "Vasileva", "GERGANAV@GMAIL.COM", "GERGANA_VASILEVA", "gergana_vasileva" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7c425879-d37a-48a6-91d9-2345120a3f6a"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "gergid@gmail.com", "Georgi", "Dimitrov", "GEORGID@GMAIL.COM", "GEORGI_DIMITROV", "georgi_dimitrov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7dca2bf8-df73-4dbf-a602-52e147eafe1e"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "dimiturs@gmail.com", "Dimitur", "Stoyanov", "DIMITURS@GMAIL.COM", "DIMITUR_STOYANOV", "dimitur_stoyanov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("865c2545-7806-4857-a621-f035e520a596"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "hristov@gmail.com", "Hristo", "Vasilev", "HRISTOV@GMAIL.COM", "HRISTO_VASILEV", "hristo_vasilev" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("96747275-9c90-449e-a91c-eb6863183a27"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "borislavt@gmail.com", "Borislav", "Todorov", "BORISLAVT@GMAIL.COM", "BORISLAV_TODOROV", "borislav_todorov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a7e0d718-a822-48db-b8ff-82cff6dbd5c7"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "petarg@gmail.com", "Petar", "Georgiev", "PETARG@GMAIL.COM", "PETAR_GEORGIEV", "petar_georgiev" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5982307-ef67-4b65-b438-8f9e1e3a240b"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "martini@gmail.com", "Martin", "Iliev", "MARTINI@GMAIL.COM", "MARTIN_ILIEV", "martin_iliev" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cbdfa704-0f6d-431f-8ede-dd952adacfc9"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "elenap@gmail.com", "Elena", "Petrova", "ELENAP@GMAIL.COM", "ELENA_PETROVA", "elena_petrova" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d9ccb374-6b17-4e66-9c11-79412a9e1e93"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "yoanai@gmail.com", "Yoana", "Ilieva", "YOANAI@GMAIL.COM", "YOANA_ILIEVA", "yoana_ilieva" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7d8baba-f7b1-4ed0-9bbb-139dc13e878e"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "kristeng@gmail.com", "KRISTEN", "GENCHEVA", "KRISTENG@GMAIL.COM", "KRISTEN_GENCHEVA", "kristen_gencheva" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f6662c6a-414b-4b5c-ae1b-7b31103dd464"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "aleksandrag@gmail.com", "Aleksandra", "Georgieva", "ALEKSANDRAG@GMAIL.COM", "ALEKSANDRA_GEORGIEVA", "aleksandra_georgieva" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523396/de9b8762-40d7-459f-9f11-aeb90f41f4a8.png", "de9b8762-40d7-459f-9f11-aeb90f41f4a8" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523421/31435cc9-7c7f-47ba-94c6-8e14a8790876.png", "31435cc9-7c7f-47ba-94c6-8e14a8790876" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("2dfdf306-41d8-4aca-abd6-91ed7d4adc8a"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523335/c4510bbe-5309-4952-9ccd-326308a2c64a.png", "c4510bbe-5309-4952-9ccd-326308a2c64a" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774522988/12f38fbe-0962-464f-89fc-26cf3598a478.png", "12f38fbe-0962-464f-89fc-26cf3598a478" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774522900/79aadf40-1666-44d1-8a39-b72d08f433ac.png", "79aadf40-1666-44d1-8a39-b72d08f433ac" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("49a578fc-5f30-40b6-810f-3ca54b0e2a02"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774522872/f83a45f2-58db-4045-99a9-8b5829b13f05.png", "f83a45f2-58db-4045-99a9-8b5829b13f05" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523282/5ab0f5a2-2ef5-4769-b2a7-9e485afd8a21.png", "5ab0f5a2-2ef5-4769-b2a7-9e485afd8a21" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("885aea72-26c5-48b5-88cc-7128b7e81499"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523117/88005d89-b665-4fac-b2a8-1325f75a2809.png", "88005d89-b665-4fac-b2a8-1325f75a2809" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("91b25ace-9e01-4c25-b0ea-3c8bad060315"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774522942/716959ed-ecf1-4c3e-a6ad-dfb7caca7863.png", "716959ed-ecf1-4c3e-a6ad-dfb7caca7863" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("c97e4a52-4926-4268-8261-82739340e77b"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523162/1c6fae53-754a-48e0-92ba-309ceaed5972.png", "1c6fae53-754a-48e0-92ba-309ceaed5972" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("d793f73f-51a0-4ff0-b6fa-5ffd4d47cd15"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774523070/38a63bdf-b3a2-4719-91d1-e874170d0387.png", "38a63bdf-b3a2-4719-91d1-e874170d0387" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("02e72b22-0abd-4ce4-80d1-30b8c13f952b"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "n2@h.com", "Nurse", "Two", "N2@H.COM", "NURSE2", "nurse2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("04347895-6b6e-4608-be4c-5f428b759669"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "p2@h.com", "Patient", "Two", "P2@H.COM", "PATIENT2", "patient2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("072eae42-46ab-4919-aae5-073aef56c00d"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc1@h.com", "John", "Doe", "DOC1@H.COM", "DOC1", "doc1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("23b350b4-0dd6-43fc-b5dc-818faf2b74e6"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc5@h.com", "Charlie", "Green", "DOC5@H.COM", "DOC5", "doc5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("30f2b4ed-e0e3-4443-8595-4dc6e26b3338"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc7@h.com", "Eve", "Grey", "DOC7@H.COM", "DOC7", "doc7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("354fa92a-6b54-4d12-b90c-9926dc906462"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "n1@h.com", "Nurse", "One", "N1@H.COM", "NURSE1", "nurse1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("355ad73e-6b7d-4ade-846d-7cab0da06629"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "n4@h.com", "Nurse", "Four", "N4@H.COM", "NURSE4", "nurse4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3d86822f-0eba-44ce-8484-27addbfe7357"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc3@h.com", "Bob", "Brown", "DOC3@H.COM", "DOC3", "doc3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("51daaed0-67e7-4c4a-b254-2745af5365df"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc8@h.com", "Frank", "Blue", "DOC8@H.COM", "DOC8", "doc8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("741d970d-f405-4bd1-94b2-eec2c3fb33e2"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "n3@h.com", "Nurse", "Three", "N3@H.COM", "NURSE3", "nurse3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7c425879-d37a-48a6-91d9-2345120a3f6a"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc2@h.com", "Jane", "Smith", "DOC2@H.COM", "DOC2", "doc2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7dca2bf8-df73-4dbf-a602-52e147eafe1e"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc4@h.com", "Alice", "White", "DOC4@H.COM", "DOC4", "doc4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("865c2545-7806-4857-a621-f035e520a596"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "p4@h.com", "Patient", "Four", "P4@H.COM", "PATIENT4", "patient4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("96747275-9c90-449e-a91c-eb6863183a27"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "p3@h.com", "Patient", "Three", "P3@H.COM", "PATIENT3", "patient3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a7e0d718-a822-48db-b8ff-82cff6dbd5c7"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "p1@h.com", "Patient", "One", "P1@H.COM", "PATIENT1", "patient1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5982307-ef67-4b65-b438-8f9e1e3a240b"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "p5@h.com", "Patient", "Five", "P5@H.COM", "PATIENT5", "patient5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cbdfa704-0f6d-431f-8ede-dd952adacfc9"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc9@h.com", "Grace", "Red", "DOC9@H.COM", "DOC9", "doc9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d9ccb374-6b17-4e66-9c11-79412a9e1e93"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc6@h.com", "Dave", "Black", "DOC6@H.COM", "DOC6", "doc6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7d8baba-f7b1-4ed0-9bbb-139dc13e878e"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "gencheva@gmail.com", "Admin", "User", "GENCHEVA@GMAIL.COM", "GENCHEVA", "gencheva" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f6662c6a-414b-4b5c-ae1b-7b31103dd464"),
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "doc10@h.com", "Hank", "Yellow", "DOC10@H.COM", "DOC10", "doc10" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("2dfdf306-41d8-4aca-abd6-91ed7d4adc8a"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("49a578fc-5f30-40b6-810f-3ca54b0e2a02"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("885aea72-26c5-48b5-88cc-7128b7e81499"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("91b25ace-9e01-4c25-b0ea-3c8bad060315"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("c97e4a52-4926-4268-8261-82739340e77b"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("d793f73f-51a0-4ff0-b6fa-5ffd4d47cd15"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });
        }
    }
}
