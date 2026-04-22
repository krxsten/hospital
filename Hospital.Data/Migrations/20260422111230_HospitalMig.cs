using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class HospitalMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    IsTaken = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnoseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SideEffects = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Medications_Diagnoses_DiagnoseID",
                        column: x => x.DiagnoseID,
                        principalTable: "Diagnoses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloudinaryID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nurses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Nurses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nurses_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nurses_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HospitalizationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HospitalizationTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    DischargeDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DischargeTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UCN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ID);
                    table.CheckConstraint("CK_DateOfBirth_Valid", "[DateOfBirth] >= '1920-01-01' AND [DateOfBirth] <= CAST(GETDATE() AS date)");
                    table.CheckConstraint("CK_DischargeDate_MinYear", "[DischargeDate] >= '1985-01-01'");
                    table.CheckConstraint("CK_HospitalizationDate_MinYear", "[HospitalizationDate] >= '1985-01-01'");
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorsAndNurses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NurseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsAndNurses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DoctorsAndNurses_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorsAndNurses_Nurses_NurseID",
                        column: x => x.NurseID,
                        principalTable: "Nurses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checkups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Checkups_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checkups_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientsAndDiagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnoseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsAndDiagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientsAndDiagnoses_Diagnoses_DiagnoseId",
                        column: x => x.DiagnoseId,
                        principalTable: "Diagnoses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientsAndDiagnoses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("02e72b22-0abd-4ce4-80d1-30b8c13f952b"), 0, "f2a3b4c5-d6e7-8f9a-0b1c-2d3e4f5a6b7c", "desid@gmail.com", false, "Desislava", "Dimitrova", false, null, "DESID@GMAIL.COM", "DESISLAVA_DIMITROVA", "AQAAAAIAAYagAAAAECSdvNgtOV0slc+hV12z39oXe+1UzBBjdznOrKd7WPfNv48fV8L5fgZapvhLNTnu1Q==", null, false, "b5c6d7e8-f9a0-1b2c-3d4e-5f6a7b8c9d0e", false, "desislava_dimitrova" },
                    { new Guid("04347895-6b6e-4608-be4c-5f428b759669"), 0, "3a4b5c6d-7e8f-9a0b-1c2d-3e4f5a6b7c8d", "stefank@gmail.com", false, "Stefan", "Kolev", false, null, "STEFANK@GMAIL.COM", "STEFAN_KOLEV", "AQAAAAIAAYagAAAAEHTk3IXZ/nf5WqgFo6Hs7c03Rs75Kr8PZp2v1EgZXuWmpqoSBv8SdZn58/+HlV1PYg==", null, false, "f9a0b1c2-d3e4-5f6a-7b8c-9d0e1f2a3b4c", false, "stefan_kolev" },
                    { new Guid("072eae42-46ab-4919-aae5-073aef56c00d"), 0, "a1b2c3d4-e5f6-4a5b-6c7d-8e9f0a1b2c3d", "ivanp@gmail.com", false, "Ivan", "Petrov", false, null, "IVANP@GMAIL.COM", "IVAN_PETROV", "AQAAAAIAAYagAAAAEOe15+vbwqzI9kXv2DSZyj7tPwBcFeL8u9UsFQ81/zIACj4iohmmXhiJj+Hf6vZLQQ==", null, false, "c4d1f2a3-6b7c-4d8e-9f0a-1b2c3d4e5f6g", false, "ivan_petrov" },
                    { new Guid("23b350b4-0dd6-43fc-b5dc-818faf2b74e6"), 0, "e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b", "aleksandurn@gmail.com", false, "Aleksandur", "Nikolov", false, null, "ALEKSANDURN@GMAIL.COM", "ALEKSANDUR_NIKOLOV", "AQAAAAIAAYagAAAAEHCEaAd2flMmmw4b8CPAXYN2tvxXUvgc9w+0rMG4yD7TArnLwSUWnzbmFXb4jo8U9w==", null, false, "a8b9c0d1-e2f3-4a5b-6c7d-8e9f0a1b2c3d", false, "aleksandur_nikolov" },
                    { new Guid("30f2b4ed-e0e3-4443-8595-4dc6e26b3338"), 0, "a7b8c9d0-e1f2-3a4b-5c6d-7e8f9a0b1c2d", "viktoriyan@gmail.com", false, "Viktoriya", "Nikolova", false, null, "VIKTORIYAN@GMAIL.COM", "VIKTORIYA_NIKOLOVA", "AQAAAAIAAYagAAAAEEjXNMDPDveSwsRH1wtMKl2p1WzpD3oTfIOksTKhTtF4Kf9OQV8rXl3M6kSiBUMLlQ==", null, false, "c0d1e2f3-a4b5-6c7d-8e9f-0a1b2c3d4e5f", false, "viktoriya_nikolova" },
                    { new Guid("354fa92a-6b54-4d12-b90c-9926dc906462"), 0, "e1f2a3b4-c5d6-7e8f-9a0b-1c2d3e4f5a6b", "mariai@gmail.com", false, "Maria", "Ivanova", false, null, "MARIAI@GMAIL.COM", "MARIA_IVANOVA", "AQAAAAIAAYagAAAAED2wagX3CcLcjKtHLjoSTueisYaCUcIfVfFsaXJwYqrgZ8lf65o2adbci2fzz5Uq2Q==", null, false, "a4b5c6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d", false, "maria_ivanova" },
                    { new Guid("355ad73e-6b7d-4ade-846d-7cab0da06629"), 0, "1e2f3a4b-5c6d-7e8f-9a0b-1c2d3e4f5a6b", "ralitsak@gmail.com", false, "Ralitsa", "Kostova", false, null, "RALITSAK@GMAIL.COM", "RALITSA_KOSTOVA", "AQAAAAIAAYagAAAAECJk5phRsgtUyOW0ARp2VrZf4fnzHM2/QYaph2bUUit0DkcMrOYk3+4ecfPxrcCeWw==", null, false, "d7e8f9a0-b1c2-3d4e-5f6a-7b8c9d0e1f2a", false, "ralitsa_kostova" },
                    { new Guid("3d86822f-0eba-44ce-8484-27addbfe7357"), 0, "c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f", "nikolaii@gmail.com", false, "Nikolai", "Ivanov", false, null, "NIKOLAII@GMAIL.COM", "NIKOLAI_IVANOV", "AQAAAAIAAYagAAAAEKPADKX2luBNBnB98li6DaOBSE0K9Fh90fCrNlOL3gJO/XVjiGExEHinp21lG683LQ==", null, false, "e6f7a8b9-c0d1-2e3f-4a5b-6c7d8e9f0a1b", false, "nikolai_ivanov" },
                    { new Guid("51daaed0-67e7-4c4a-b254-2745af5365df"), 0, "b8c9d0e1-f2a3-4b5c-6d7e-8f9a0b1c2d3e", "annaa@gmail.com", false, "Anna", "Aleksandrova", false, null, "ANNAA@GMAIL.COM", "ANNA_ALEKSANDROVA", "AQAAAAIAAYagAAAAEBHF7jaBBZTdVG7Q4wgsdPDzgAFQ1rJO+7kgPROeEoOyk56ThzeRdJm8c+pgla2rDw==", null, false, "d1e2f3a4-b5c6-7d8e-9f0a-1b2c3d4e5f6a", false, "anna_aleksandrova" },
                    { new Guid("741d970d-f405-4bd1-94b2-eec2c3fb33e2"), 0, "0d1e2f3a-4b5c-6d7e-8f9a-0b1c2d3e4f5a", "gerganav@gmail.com", false, "Gergana", "Vasileva", false, null, "GERGANAV@GMAIL.COM", "GERGANA_VASILEVA", "AQAAAAIAAYagAAAAEIzsXh/80UtuMaSxHNharQMLKn30OUedgSTke5LziVtO9vblbO/O4lBeXq4KacpIIw==", null, false, "c6d7e8f9-a0b1-2c3d-4e5f-6a7b8c9d0e1f", false, "gergana_vasileva" },
                    { new Guid("7c425879-d37a-48a6-91d9-2345120a3f6a"), 0, "b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e", "gergid@gmail.com", false, "Georgi", "Dimitrov", false, null, "GEORGID@GMAIL.COM", "GEORGI_DIMITROV", "AQAAAAIAAYagAAAAECSb7JJ2guKRHcLe0xe4Nh5xIr2WFPvj7NNdELQ2SWX1HujqP9NQOF9fUUrWzR7Ong==", null, false, "d5e6f7a8-b9c0-4d1e-2f3a-4b5c6d7e8f9a", false, "georgi_dimitrov" },
                    { new Guid("7dca2bf8-df73-4dbf-a602-52e147eafe1e"), 0, "d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a", "dimiturs@gmail.com", false, "Dimitur", "Stoyanov", false, null, "DIMITURS@GMAIL.COM", "DIMITUR_STOYANOV", "AQAAAAIAAYagAAAAECd4f8k8p4cb74V6hbxvJI1B+WUEoxQI7lGcwQJJ/AcsrpauKMmfBZtI/NVsTrDU3w==", null, false, "f7a8b9c0-d1e2-3f4a-5b6c-7d8e9f0a1b2c", false, "dimitur_stoyanov" },
                    { new Guid("865c2545-7806-4857-a621-f035e520a596"), 0, "5c6d7e8f-9a0b-1c2d-3e4f-5a6b7c8d9e0f", "hristov@gmail.com", false, "Hristo", "Vasilev", false, null, "HRISTOV@GMAIL.COM", "HRISTO_VASILEV", "AQAAAAIAAYagAAAAEGqrFqOdym91U7At5ImEDltYXplJtCUKeYmlzjo+7OnjisC3wf91hHDiOD/j4GKznA==", null, false, "b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e", false, "hristo_vasilev" },
                    { new Guid("96747275-9c90-449e-a91c-eb6863183a27"), 0, "4b5c6d7e-8f9a-0b1c-2d3e-4f5a6b7c8d9e", "borislavt@gmail.com", false, "Borislav", "Todorov", false, null, "BORISLAVT@GMAIL.COM", "BORISLAV_TODOROV", "AQAAAAIAAYagAAAAENOcc51UzWPwNcvZqjVlXh6yhh//kQIEe8/rfL4shjequoVaty93k7JAkjTnnCtwTQ==", null, false, "a0b1c2d3-e4f5-6a7b-8c9d-0e1f2a3b4c5d", false, "borislav_todorov" },
                    { new Guid("a7e0d718-a822-48db-b8ff-82cff6dbd5c7"), 0, "2f3a4b5c-6d7e-8f9a-0b1c-2d3e4f5a6b7c", "petarg@gmail.com", false, "Petar", "Georgiev", false, null, "PETARG@GMAIL.COM", "PETAR_GEORGIEV", "AQAAAAIAAYagAAAAEI/dbXnPYA2e5CNCQJV+EwbLEizigvDBR2dXi4wsoPOmZ/DN874lr+wV66STbVdX0w==", null, false, "e8f9a0b1-c2d3-4e5f-6a7b-8c9d0e1f2a3b", false, "petar_georgiev" },
                    { new Guid("c5982307-ef67-4b65-b438-8f9e1e3a240b"), 0, "6d7e8f9a-0b1c-2d3e-4f5a-6b7c8d9e0f1a", "martini@gmail.com", false, "Martin", "Iliev", false, null, "MARTINI@GMAIL.COM", "MARTIN_ILIEV", "AQAAAAIAAYagAAAAEMdWOGLMJx1D+sbEJnha1kgPgo57KaugKeRcT7YH7gv4xZQMD2Rb9JRqQFCVKu6UwQ==", null, false, "c2d3e4f5-a6b7-8c9d-0e1f-2a3b4c5d6e7f", false, "martin_iliev" },
                    { new Guid("cbdfa704-0f6d-431f-8ede-dd952adacfc9"), 0, "c9d0e1f2-a3b4-5c6d-7e8f-9a0b1c2d3e4f", "elenap@gmail.com", false, "Elena", "Petrova", false, null, "ELENAP@GMAIL.COM", "ELENA_PETROVA", "AQAAAAIAAYagAAAAEGDDfA6AFHdThKJaFRL/HbhN32jjIlHMEmytm/ClIlp0Pir0MUhBTmu8FuL/t6INzg==", null, false, "e2f3a4b5-c6d7-8e9f-0a1b-2c3d4e5f6a7b", false, "elena_petrova" },
                    { new Guid("d9ccb374-6b17-4e66-9c11-79412a9e1e93"), 0, "f6a7b8c9-d0e1-2f3a-4b5c-6d7e8f9a0b1c", "yoanai@gmail.com", false, "Yoana", "Ilieva", false, null, "YOANAI@GMAIL.COM", "YOANA_ILIEVA", "AQAAAAIAAYagAAAAECOfEAoHCcRMH5Umjxyh1Sugso3lXvP21peuJQqSJoCXOgxiD27Hh7JFeYtO3AMvIw==", null, false, "b9c0d1e2-f3a4-5b6c-7d8e-9f0a1b2c3d4e", false, "yoana_ilieva" },
                    { new Guid("e7d8baba-f7b1-4ed0-9bbb-139dc13e878e"), 0, "f259740e-7d8a-4c91-9556-54776103b41e", "kristeng@gmail.com", false, "Kristen", "Gencheva", false, null, "KRISTENG@GMAIL.COM", "KRISTEN_GENCHEVA", "AQAAAAIAAYagAAAAENmHmh7V0mmVkkR+i9QTgjnpMZje77l76Bbywho4v3x9Q5dyMKF3mVAfiQ5nEIYhXA==", null, false, "9699665e-7685-48b4-9f44-846c4f923e3e", false, "kristen_gencheva" },
                    { new Guid("f6662c6a-414b-4b5c-ae1b-7b31103dd464"), 0, "d0e1f2a3-b4c5-6d7e-8f9a-0b1c2d3e4f5a", "aleksandrag@gmail.com", false, "Aleksandra", "Georgieva", false, null, "ALEKSANDRAG@GMAIL.COM", "ALEKSANDRA_GEORGIEVA", "AQAAAAIAAYagAAAAEJgZUUwvdQ7+45txtOIK5Lk5u3JlP3vKE/PqILhHamhuR7SjU5SKtC1hLqSxtMEtsg==", null, false, "f3a4b5c6-d7e8-9f0a-1b2c-3d4e5f6a7b8c", false, "aleksandra_georgieva" }
                });

            migrationBuilder.InsertData(
                table: "Diagnoses",
                columns: new[] { "ID", "ImageURL", "Name", "PublicID" },
                values: new object[,]
                {
                    { new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764961/a72a5e9e-61ae-442b-8853-5bc76a336170.png", "Fracture", "a72a5e9e-61ae-442b-8853-5bc76a336170" },
                    { new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764975/328d764d-3a97-4933-8f28-a89c2f8805b7.png", "Migraine", "328d764d-3a97-4933-8f28-a89c2f8805b7" },
                    { new Guid("2dfdf306-41d8-4aca-abd6-91ed7d4adc8a"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764674/82339fab-d0e6-48a4-ba49-5ce412cac822.png", "Bronchitis", "82339fab-d0e6-48a4-ba49-5ce412cac822z" },
                    { new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764945/0f4c597f-8d2c-4a88-b923-fe49d824b5e6.png", "Pneumonia", "0f4c597f-8d2c-4a88-b923-fe49d824b5e6" },
                    { new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764641/48ae0b79-490c-4fb9-84cd-9c688ac54aea.png", "Diabetes", "48ae0b79-490c-4fb9-84cd-9c688ac54aea" },
                    { new Guid("49a578fc-5f30-40b6-810f-3ca54b0e2a02"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764602/71e1a51c-2d54-4ea6-8cc1-34b6716d2816.png", "Hashimoto's disease", "71e1a51c-2d54-4ea6-8cc1-34b6716d2816" },
                    { new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764927/db29fdde-76a6-4e2d-974c-5bfd4d9bbd71.png", "Hypertension", "v1775764927/db29fdde-76a6-4e2d-974c-5bfd4d9bbd71" },
                    { new Guid("885aea72-26c5-48b5-88cc-7128b7e81499"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764880/1272e03f-5e20-4989-b638-ebe83f57bf86.png", "Osteoporosis", "1272e03f-5e20-4989-b638-ebe83f57bf86" },
                    { new Guid("91b25ace-9e01-4c25-b0ea-3c8bad060315"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764569/b992faff-00ff-4c16-9a01-e8aa73b286cc.png", "Influenza", "b992faff-00ff-4c16-9a01-e8aa73b286cc" },
                    { new Guid("c97e4a52-4926-4268-8261-82739340e77b"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764908/9dfa36a2-4cc8-4319-acf1-1382c3766327.png", "Raynauld's syndrome", "9dfa36a2-4cc8-4319-acf1-1382c3766327" },
                    { new Guid("d793f73f-51a0-4ff0-b6fa-5ffd4d47cd15"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775764810/51b2718c-af7a-41f3-96fb-aba23237bb9a.png", "Asthma", "51b2718c-af7a-41f3-96fb-aba23237bb9a" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "ID", "IsTaken", "RoomNumber" },
                values: new object[,]
                {
                    { new Guid("097fe7eb-86e3-43ea-a6da-c8a90373f27a"), false, 109 },
                    { new Guid("0de4b3b8-318e-42aa-b896-04cd14749d17"), false, 106 },
                    { new Guid("1ba934d0-494e-431a-a7d6-2fff62af342a"), false, 103 },
                    { new Guid("3970dc21-5df6-42ac-95db-261e18812d4c"), true, 105 },
                    { new Guid("44a7a821-54c8-4d2c-acf3-efbfbd75c18e"), true, 208 },
                    { new Guid("53546e3d-53ee-4e2c-861a-3cf5a3584893"), true, 301 },
                    { new Guid("5b8e402c-ec9e-4e74-9cf2-ed1c05948e2a"), false, 101 },
                    { new Guid("85242158-d3c2-4542-bc27-88caf4a131c6"), false, 207 },
                    { new Guid("88b657b6-c9c3-41eb-a26f-5d637c704533"), false, 209 },
                    { new Guid("92aa881b-d346-49d4-8b3d-a76e13469e99"), true, 107 },
                    { new Guid("9e9a26f4-3d2e-4f9a-bcf1-9e63e057a7c6"), true, 201 },
                    { new Guid("b5825cde-c8df-4d53-8f69-6d4e6d2683cb"), false, 203 },
                    { new Guid("ba0b5ec2-b3c0-4901-8128-a3703d72ffd3"), true, 205 },
                    { new Guid("bb9bb784-dd42-4541-9ed2-01e7929a61ab"), true, 102 },
                    { new Guid("bee36b54-ff99-474a-b9de-82010ff3607c"), true, 204 },
                    { new Guid("c219c792-8a69-4850-ac40-a36f5f752786"), true, 202 },
                    { new Guid("c4ad2bf7-9eea-4555-b637-00bc1d11cba3"), true, 108 },
                    { new Guid("e748053a-f20a-4f80-a314-fbd9c2ac4e8a"), true, 104 },
                    { new Guid("f2a57cc6-8969-4f63-9691-2c449409ff4d"), true, 206 }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "ID", "EndTime", "StartTime", "Type" },
                values: new object[,]
                {
                    { new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), new TimeOnly(14, 0, 0), new TimeOnly(6, 0, 0), "Morning" },
                    { new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), new TimeOnly(22, 0, 0), new TimeOnly(14, 0, 0), "Afternoon" },
                    { new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), new TimeOnly(8, 0, 0), new TimeOnly(0, 0, 0), "Emergency" },
                    { new Guid("aaaaaaf9-5059-478c-8df4-db3fd4342b14"), new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0), "Weekend" },
                    { new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new TimeOnly(6, 0, 0), new TimeOnly(22, 0, 0), "Night" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "ID", "ImageURL", "PublicID", "SpecializationName" },
                values: new object[,]
                {
                    { new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111521_asrj53.png", "Screenshot_2026-04-02_111521_asrj53", "Neurology" },
                    { new Guid("4ee4ce69-da26-4f66-85cf-30623200cbf4"), "https://res.cloudinary.com/dyoxqki3d/image/upload/v1776837054/Screenshot_2026-04-02_111739_kgdwdt.png", "Screenshot_2026-04-02_111739_kgdwdt", "Orthopedics" },
                    { new Guid("7a67c94b-50fb-4043-83f1-afdade20b451"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111310_zgxd8c.png", "Screenshot_2026-04-02_111310_zgxd8c", "Cardiology" },
                    { new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111137_nbbhc6.png", "Screenshot_2026-04-02_111137_nbbhc6", "Infectious Disease" },
                    { new Guid("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111428_ukzxth.png", "Screenshot_2026-04-02_111428_ukzxth", "Pulmonology" },
                    { new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), "https://res.cloudinary.com/dyoxqki3d/image/upload/v1776836982/Screenshot_2026-04-02_111632_yhnkf5.png", "Screenshot_2026-04-02_111632_yhnkf5", "Rheumatology" },
                    { new Guid("c484edec-d525-438a-92c4-ad80a9a41878"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111403_uhhj71.png", "Screenshot_2026-04-02_111403_uhhj71", "Endocrinology" },
                    { new Guid("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117704/Screenshot_2026-04-02_111758_t9nbmc.png", "Screenshot_2026-04-02_111758_t9nbmc", "Respiratory" },
                    { new Guid("e43cf086-24ad-4a75-b47e-549b8d8e467c"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111550_rletvt.png", "Screenshot_2026-04-02_111550_rletvt", "Traumatology" },
                    { new Guid("e82cc806-165f-4554-91f8-c9d9ae4909e5"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775117702/Screenshot_2026-04-02_111610_awmauh.png", "Screenshot_2026-04-02_111610_awmauh", "Allergy & Immunology" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "ID", "CloudinaryID", "ImageURL", "IsAccepted", "ShiftId", "SpecializationId", "UserId" },
                values: new object[,]
                {
                    { new Guid("08ccdf4b-02ad-464f-9ef2-fb73ceee1826"), "doctor6_tvjvud", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045919/doctor6_tvjvud.jpg", true, new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), new Guid("072eae42-46ab-4919-aae5-073aef56c00d") },
                    { new Guid("0f6d5fde-bc75-4df5-8886-090806665b82"), "doctor3_djf78l", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045922/doctor3_djf78l.jpg", true, new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new Guid("4ee4ce69-da26-4f66-85cf-30623200cbf4"), new Guid("cbdfa704-0f6d-431f-8ede-dd952adacfc9") },
                    { new Guid("186296e2-7114-4291-aa3b-897b96c75c21"), "doctor4_cnmqpg", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045919/doctor4_cnmqpg.jpg", true, new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new Guid("c484edec-d525-438a-92c4-ad80a9a41878"), new Guid("3d86822f-0eba-44ce-8484-27addbfe7357") },
                    { new Guid("26189d95-7ca7-40f7-9384-8454cfb99247"), "doctor9_ksvypt", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045917/doctor9_ksvypt.jpg", true, new Guid("aaaaaaf9-5059-478c-8df4-db3fd4342b14"), new Guid("e82cc806-165f-4554-91f8-c9d9ae4909e5"), new Guid("30f2b4ed-e0e3-4443-8595-4dc6e26b3338") },
                    { new Guid("2a4e1d97-8411-4cf8-9da2-af9452f16eca"), "doctor8_daewlm", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045915/doctor8_daewlm.jpg", true, new Guid("aaaaaaf9-5059-478c-8df4-db3fd4342b14"), new Guid("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"), new Guid("7dca2bf8-df73-4dbf-a602-52e147eafe1e") },
                    { new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"), "f4c9ef33d04a22050038e9e53eeb7d85_w2l72d", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775044425/f4c9ef33d04a22050038e9e53eeb7d85_w2l72d.jpg", true, new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), new Guid("23b350b4-0dd6-43fc-b5dc-818faf2b74e6") },
                    { new Guid("6d3dacc1-3b7a-4e43-8caa-5b82a6f4a21f"), "doctor2_x95tr9", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045917/doctor2_x95tr9.jpg", true, new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), new Guid("c484edec-d525-438a-92c4-ad80a9a41878"), new Guid("51daaed0-67e7-4c4a-b254-2745af5365df") },
                    { new Guid("8e296807-75cf-45dd-bdfc-179495465c09"), "doctor1_jiqipy", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045917/doctor1_jiqipy.jpg", true, new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new Guid("e43cf086-24ad-4a75-b47e-549b8d8e467c"), new Guid("d9ccb374-6b17-4e66-9c11-79412a9e1e93") },
                    { new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"), "doctor5_vwg329", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045919/doctor5_vwg329.jpg", true, new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), new Guid("f6662c6a-414b-4b5c-ae1b-7b31103dd464") },
                    { new Guid("e1ceefa2-e56b-4395-9049-c689bea9417f"), "doctor7_ylf8tt", "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045915/doctor7_ylf8tt.jpg", true, new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), new Guid("7a67c94b-50fb-4043-83f1-afdade20b451"), new Guid("7c425879-d37a-48a6-91d9-2345120a3f6a") }
                });

            migrationBuilder.InsertData(
                table: "Medications",
                columns: new[] { "ID", "Description", "DiagnoseID", "Name", "SideEffects" },
                values: new object[,]
                {
                    { new Guid("04e1b6ac-4511-4cb2-91ad-ff8d8bf095c5"), "Diabetes medication", new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c"), "Metformin", "Fatigue" },
                    { new Guid("3857c780-691f-4ef1-ae47-853911e00a97"), "Blood thinner", new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"), "Aspirin", "Bleeding" },
                    { new Guid("754771d0-8581-4a0b-aa8e-fe312d90e5a5"), "Anti inflammatory", new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"), "Ibuprofen", "Stomach pain" },
                    { new Guid("7cf144e7-7d29-4991-948d-ffb592c85488"), "Antibiotic", new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"), "Amoxicillin", "Allergy" },
                    { new Guid("95344cea-8f8e-413e-8f2f-769749bee51b"), "Pain relief", new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0"), "Paracetamol", "Nausea" },
                    { new Guid("a73e5de4-cbe9-4e87-87a7-39230af38a1e"), "Blood pressure", new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"), "Lisinopril", "Cough" },
                    { new Guid("ea31422a-7b5b-42f1-a476-b9aaab041732"), "Acid reflux", new Guid("c97e4a52-4926-4268-8261-82739340e77b"), "Omeprazole", "Headache" }
                });

            migrationBuilder.InsertData(
                table: "Nurses",
                columns: new[] { "ID", "ImageURL", "IsAccepted", "PublicID", "ShiftId", "SpecializationId", "UserId" },
                values: new object[,]
                {
                    { new Guid("698d0579-913c-42af-8a45-924cd9f740bb"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775044959/a9da1dea6368ebb099100f489cc37cfe_zfh28g.jpg", true, "a9da1dea6368ebb099100f489cc37cfe_zfh28g", new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), new Guid("354fa92a-6b54-4d12-b90c-9926dc906462") },
                    { new Guid("9fb6048e-03ae-407f-a83e-51b6c5399b41"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045919/doctor5_vwg329.jpg", true, "doctor5_vwg329", new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), new Guid("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"), new Guid("355ad73e-6b7d-4ade-846d-7cab0da06629") },
                    { new Guid("e5f97752-f18b-4b36-8c47-4d238cb0e01f"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045913/doctor10_ewtass.jpg", true, "doctor10_ewtass", new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), new Guid("02e72b22-0abd-4ce4-80d1-30b8c13f952b") },
                    { new Guid("e6a3850b-7a1e-465c-83fd-57b1134c68d2"), "https://res.cloudinary.com/dyoxqki3d/image/upload/q_auto/f_auto/v1775045922/doctor3_djf78l.jpg", true, "doctor3_djf78l", new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), new Guid("741d970d-f405-4bd1-94b2-eec2c3fb33e2") }
                });

            migrationBuilder.InsertData(
                table: "DoctorsAndNurses",
                columns: new[] { "ID", "DoctorID", "NurseID" },
                values: new object[,]
                {
                    { new Guid("038d24e4-d697-4ab6-a04a-70989e133c70"), new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"), new Guid("e5f97752-f18b-4b36-8c47-4d238cb0e01f") },
                    { new Guid("5a6d7c84-48ec-4b3c-a2d6-a468f92f12dc"), new Guid("8e296807-75cf-45dd-bdfc-179495465c09"), new Guid("e6a3850b-7a1e-465c-83fd-57b1134c68d2") },
                    { new Guid("c5c2c0dd-c326-4f37-804c-e53354c825ed"), new Guid("e1ceefa2-e56b-4395-9049-c689bea9417f"), new Guid("698d0579-913c-42af-8a45-924cd9f740bb") }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "ID", "BirthCity", "DateOfBirth", "DischargeDate", "DischargeTime", "DoctorId", "HospitalizationDate", "HospitalizationTime", "PhoneNumber", "RoomId", "UCN", "UserId" },
                values: new object[,]
                {
                    { new Guid("06999216-e4e9-4455-856d-5246259b2684"), "Plovdiv", new DateOnly(1994, 3, 15), new DateOnly(2026, 7, 20), new TimeOnly(11, 30, 0), new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"), new DateOnly(2026, 7, 5), new TimeOnly(9, 0, 0), "0877123456", new Guid("53546e3d-53ee-4e2c-861a-3cf5a3584893"), "9413153443", new Guid("96747275-9c90-449e-a91c-eb6863183a27") },
                    { new Guid("718919c3-760f-4a6a-8abf-b1cd1b459d11"), "Yambol", new DateOnly(1995, 8, 12), new DateOnly(2026, 4, 24), new TimeOnly(15, 30, 0), new Guid("2a4e1d97-8411-4cf8-9da2-af9452f16eca"), new DateOnly(2026, 4, 18), new TimeOnly(11, 0, 0), "0891122334", new Guid("85242158-d3c2-4542-bc27-88caf4a131c6"), "9548127883", new Guid("865c2545-7806-4857-a621-f035e520a596") },
                    { new Guid("9783d8b3-014f-477a-b951-6ff87057b44f"), "Kazanlak", new DateOnly(2007, 10, 16), new DateOnly(2026, 6, 17), new TimeOnly(12, 30, 0), new Guid("6d3dacc1-3b7a-4e43-8caa-5b82a6f4a21f"), new DateOnly(2026, 5, 12), new TimeOnly(10, 30, 0), "0878104032", new Guid("e748053a-f20a-4f80-a314-fbd9c2ac4e8a"), "0750167656", new Guid("a7e0d718-a822-48db-b8ff-82cff6dbd5c7") },
                    { new Guid("ba24d819-24f4-4e94-bb71-77765076d46c"), "Sofia", new DateOnly(2002, 9, 15), new DateOnly(2026, 6, 15), new TimeOnly(12, 30, 0), new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"), new DateOnly(2026, 6, 1), new TimeOnly(9, 45, 0), "0881239876", new Guid("c219c792-8a69-4850-ac40-a36f5f752786"), "0249152349", new Guid("c5982307-ef67-4b65-b438-8f9e1e3a240b") },
                    { new Guid("eac603b7-4022-4fb1-8c04-28db2f0f2162"), "Stara Zagora", new DateOnly(1992, 11, 23), new DateOnly(2026, 3, 15), new TimeOnly(11, 0, 0), new Guid("0f6d5fde-bc75-4df5-8886-090806665b82"), new DateOnly(2026, 3, 1), new TimeOnly(14, 45, 0), "0882123456", new Guid("0de4b3b8-318e-42aa-b896-04cd14749d17"), "9251237890", new Guid("04347895-6b6e-4608-be4c-5f428b759669") }
                });

            migrationBuilder.InsertData(
                table: "Checkups",
                columns: new[] { "ID", "Date", "DoctorID", "PatientID", "Time" },
                values: new object[,]
                {
                    { new Guid("5dad99cf-b93a-4376-b772-86fd44246d7e"), new DateOnly(2026, 7, 19), new Guid("0f6d5fde-bc75-4df5-8886-090806665b82"), new Guid("eac603b7-4022-4fb1-8c04-28db2f0f2162"), new TimeOnly(8, 30, 0) },
                    { new Guid("7f55e59e-5ab2-4e36-92bd-d66f3c453ab9"), new DateOnly(2026, 6, 1), new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"), new Guid("06999216-e4e9-4455-856d-5246259b2684"), new TimeOnly(16, 30, 0) },
                    { new Guid("a828ee04-c7dc-417c-9d8b-85114a92ce47"), new DateOnly(2026, 12, 6), new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"), new Guid("ba24d819-24f4-4e94-bb71-77765076d46c"), new TimeOnly(10, 0, 0) },
                    { new Guid("aff1363e-af23-4c5b-a88f-ba81e536333b"), new DateOnly(2026, 5, 23), new Guid("2a4e1d97-8411-4cf8-9da2-af9452f16eca"), new Guid("718919c3-760f-4a6a-8abf-b1cd1b459d11"), new TimeOnly(15, 30, 0) },
                    { new Guid("d44665d6-6b30-4542-b932-cbb7ee71efbe"), new DateOnly(2026, 4, 12), new Guid("6d3dacc1-3b7a-4e43-8caa-5b82a6f4a21f"), new Guid("9783d8b3-014f-477a-b951-6ff87057b44f"), new TimeOnly(16, 30, 0) }
                });

            migrationBuilder.InsertData(
                table: "PatientsAndDiagnoses",
                columns: new[] { "Id", "DiagnoseId", "PatientId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"), new Guid("9783d8b3-014f-477a-b951-6ff87057b44f") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"), new Guid("06999216-e4e9-4455-856d-5246259b2684") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"), new Guid("eac603b7-4022-4fb1-8c04-28db2f0f2162") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Checkups_DoctorID",
                table: "Checkups",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Checkups_PatientID",
                table: "Checkups",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ShiftId",
                table: "Doctors",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsAndNurses_DoctorID_NurseID",
                table: "DoctorsAndNurses",
                columns: new[] { "DoctorID", "NurseID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsAndNurses_NurseID",
                table: "DoctorsAndNurses",
                column: "NurseID");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DiagnoseID",
                table: "Medications",
                column: "DiagnoseID");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_ShiftId",
                table: "Nurses",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_SpecializationId",
                table: "Nurses",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_UserId",
                table: "Nurses",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PhoneNumber",
                table: "Patients",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_RoomId",
                table: "Patients",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UCN",
                table: "Patients",
                column: "UCN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientsAndDiagnoses_DiagnoseId",
                table: "PatientsAndDiagnoses",
                column: "DiagnoseId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsAndDiagnoses_PatientId_DiagnoseId",
                table: "PatientsAndDiagnoses",
                columns: new[] { "PatientId", "DiagnoseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Checkups");

            migrationBuilder.DropTable(
                name: "DoctorsAndNurses");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "PatientsAndDiagnoses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Nurses");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
