using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Diagnoses");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Specializations",
                newName: "PublicID");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Nurses",
                newName: "PublicID");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Doctors",
                newName: "ImageURL");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Specializations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryID",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublicID",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("08ccdf4b-02ad-464f-9ef2-fb73ceee1826"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("0f6d5fde-bc75-4df5-8886-090806665b82"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("186296e2-7114-4291-aa3b-897b96c75c21"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("26189d95-7ca7-40f7-9384-8454cfb99247"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("2a4e1d97-8411-4cf8-9da2-af9452f16eca"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("6d3dacc1-3b7a-4e43-8caa-5b82a6f4a21f"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("8e296807-75cf-45dd-bdfc-179495465c09"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("e1ceefa2-e56b-4395-9049-c689bea9417f"),
                columns: new[] { "CloudinaryID", "ImageURL" },
                values: new object[] { "logo_ro7b24", "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png" });

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "ID",
                keyValue: new Guid("698d0579-913c-42af-8a45-924cd9f740bb"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "ID",
                keyValue: new Guid("9fb6048e-03ae-407f-a83e-51b6c5399b41"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "ID",
                keyValue: new Guid("e5f97752-f18b-4b36-8c47-4d238cb0e01f"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "ID",
                keyValue: new Guid("e6a3850b-7a1e-465c-83fd-57b1134c68d2"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("1a298771-7773-4c3b-828c-fff8dcedf0e9"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("4ee4ce69-da26-4f66-85cf-30623200cbf4"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("7a67c94b-50fb-4043-83f1-afdade20b451"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("c484edec-d525-438a-92c4-ad80a9a41878"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("e43cf086-24ad-4a75-b47e-549b8d8e467c"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("e82cc806-165f-4554-91f8-c9d9ae4909e5"),
                columns: new[] { "ImageURL", "PublicID" },
                values: new object[] { "https://res.cloudinary.com/dyoxqki3d/image/upload/v1774430017/logo_ro7b24.png", "logo_ro7b24" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "CloudinaryID",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "PublicID",
                table: "Diagnoses");

            migrationBuilder.RenameColumn(
                name: "PublicID",
                table: "Specializations",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "PublicID",
                table: "Nurses",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Doctors",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"),
                column: "Image",
                value: "fracture.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0"),
                column: "Image",
                value: "migraine.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("2dfdf306-41d8-4aca-abd6-91ed7d4adc8a"),
                column: "Image",
                value: "bronchitis.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"),
                column: "Image",
                value: "pneumonia.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c"),
                column: "Image",
                value: "diabetes.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("49a578fc-5f30-40b6-810f-3ca54b0e2a02"),
                column: "Image",
                value: "hashimoto.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"),
                column: "Image",
                value: "hypertension.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("885aea72-26c5-48b5-88cc-7128b7e81499"),
                column: "Image",
                value: "asthma.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("91b25ace-9e01-4c25-b0ea-3c8bad060315"),
                column: "Image",
                value: "flu.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("c97e4a52-4926-4268-8261-82739340e77b"),
                column: "Image",
                value: "raynauld.jpg");

            migrationBuilder.UpdateData(
                table: "Diagnoses",
                keyColumn: "ID",
                keyValue: new Guid("d793f73f-51a0-4ff0-b6fa-5ffd4d47cd15"),
                column: "Image",
                value: "asthma.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("08ccdf4b-02ad-464f-9ef2-fb73ceee1826"),
                column: "Image",
                value: "doctor1.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("0f6d5fde-bc75-4df5-8886-090806665b82"),
                column: "Image",
                value: "doctor9.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("186296e2-7114-4291-aa3b-897b96c75c21"),
                column: "Image",
                value: "doctor3.jpeg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("26189d95-7ca7-40f7-9384-8454cfb99247"),
                column: "Image",
                value: "doctor7.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("2a4e1d97-8411-4cf8-9da2-af9452f16eca"),
                column: "Image",
                value: "doctor4.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"),
                column: "Image",
                value: "doctor5.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("6d3dacc1-3b7a-4e43-8caa-5b82a6f4a21f"),
                column: "Image",
                value: "doctor8.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("8e296807-75cf-45dd-bdfc-179495465c09"),
                column: "Image",
                value: "doctor6.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"),
                column: "Image",
                value: "doctor10.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: new Guid("e1ceefa2-e56b-4395-9049-c689bea9417f"),
                column: "Image",
                value: "doctor2.jpeg");

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "ID",
                keyValue: new Guid("698d0579-913c-42af-8a45-924cd9f740bb"),
                column: "Image",
                value: "nurse1.png");

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "ID",
                keyValue: new Guid("9fb6048e-03ae-407f-a83e-51b6c5399b41"),
                column: "Image",
                value: "nurse4.png");

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "ID",
                keyValue: new Guid("e5f97752-f18b-4b36-8c47-4d238cb0e01f"),
                column: "Image",
                value: "nurse2.png");

            migrationBuilder.UpdateData(
                table: "Nurses",
                keyColumn: "ID",
                keyValue: new Guid("e6a3850b-7a1e-465c-83fd-57b1134c68d2"),
                column: "Image",
                value: "nurse3.png");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("1a298771-7773-4c3b-828c-fff8dcedf0e9"),
                column: "Image",
                value: "endocrinology.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"),
                column: "Image",
                value: "neurology.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("4ee4ce69-da26-4f66-85cf-30623200cbf4"),
                column: "Image",
                value: "orthopedics.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("7a67c94b-50fb-4043-83f1-afdade20b451"),
                column: "Image",
                value: "cardiology.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"),
                column: "Image",
                value: "infections.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"),
                column: "Image",
                value: "pulmonology.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"),
                column: "Image",
                value: "rheumatology.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("c484edec-d525-438a-92c4-ad80a9a41878"),
                column: "Image",
                value: "endocrinology.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"),
                column: "Image",
                value: "respiratory.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("e43cf086-24ad-4a75-b47e-549b8d8e467c"),
                column: "Image",
                value: "Traumatology.jpg");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "ID",
                keyValue: new Guid("e82cc806-165f-4554-91f8-c9d9ae4909e5"),
                column: "Image",
                value: "allergy.jpg");
        }
    }
}
