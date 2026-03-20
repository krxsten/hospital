using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class KristenMigration : Migration
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
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
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
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HospitalizationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HospitalizationTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    DischargeDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DischargeTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UCN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ID);
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
                    { new Guid("02e72b22-0abd-4ce4-80d1-30b8c13f952b"), 0, "C13", "n2@h.com", false, "Nurse", "Two", false, null, "N2@H.COM", "NURSE2", "HASH", null, false, "S13", false, "nurse2" },
                    { new Guid("04347895-6b6e-4608-be4c-5f428b759669"), 0, "C17", "p2@h.com", false, "Patient", "Two", false, null, "P2@H.COM", "PATIENT2", "HASH", null, false, "S17", false, "patient2" },
                    { new Guid("072eae42-46ab-4919-aae5-073aef56c00d"), 0, "C2", "doc1@h.com", false, "John", "Doe", false, null, "DOC1@H.COM", "DOC1", "HASH", null, false, "S2", false, "doc1" },
                    { new Guid("23b350b4-0dd6-43fc-b5dc-818faf2b74e6"), 0, "C6", "doc5@h.com", false, "Charlie", "Green", false, null, "DOC5@H.COM", "DOC5", "HASH", null, false, "S6", false, "doc5" },
                    { new Guid("30f2b4ed-e0e3-4443-8595-4dc6e26b3338"), 0, "C8", "doc7@h.com", false, "Eve", "Grey", false, null, "DOC7@H.COM", "DOC7", "HASH", null, false, "S8", false, "doc7" },
                    { new Guid("354fa92a-6b54-4d12-b90c-9926dc906462"), 0, "C12", "n1@h.com", false, "Nurse", "One", false, null, "N1@H.COM", "NURSE1", "HASH", null, false, "S12", false, "nurse1" },
                    { new Guid("355ad73e-6b7d-4ade-846d-7cab0da06629"), 0, "C15", "n4@h.com", false, "Nurse", "Four", false, null, "N4@H.COM", "NURSE4", "HASH", null, false, "S15", false, "nurse4" },
                    { new Guid("3d86822f-0eba-44ce-8484-27addbfe7357"), 0, "C4", "doc3@h.com", false, "Bob", "Brown", false, null, "DOC3@H.COM", "DOC3", "HASH", null, false, "S4", false, "doc3" },
                    { new Guid("51daaed0-67e7-4c4a-b254-2745af5365df"), 0, "C9", "doc8@h.com", false, "Frank", "Blue", false, null, "DOC8@H.COM", "DOC8", "HASH", null, false, "S9", false, "doc8" },
                    { new Guid("741d970d-f405-4bd1-94b2-eec2c3fb33e2"), 0, "C14", "n3@h.com", false, "Nurse", "Three", false, null, "N3@H.COM", "NURSE3", "HASH", null, false, "S14", false, "nurse3" },
                    { new Guid("7c425879-d37a-48a6-91d9-2345120a3f6a"), 0, "C3", "doc2@h.com", false, "Jane", "Smith", false, null, "DOC2@H.COM", "DOC2", "HASH", null, false, "S3", false, "doc2" },
                    { new Guid("7dca2bf8-df73-4dbf-a602-52e147eafe1e"), 0, "C5", "doc4@h.com", false, "Alice", "White", false, null, "DOC4@H.COM", "DOC4", "HASH", null, false, "S5", false, "doc4" },
                    { new Guid("865c2545-7806-4857-a621-f035e520a596"), 0, "C19", "p4@h.com", false, "Patient", "Four", false, null, "P4@H.COM", "PATIENT4", "HASH", null, false, "S19", false, "patient4" },
                    { new Guid("96747275-9c90-449e-a91c-eb6863183a27"), 0, "C18", "p3@h.com", false, "Patient", "Three", false, null, "P3@H.COM", "PATIENT3", "HASH", null, false, "S18", false, "patient3" },
                    { new Guid("a7e0d718-a822-48db-b8ff-82cff6dbd5c7"), 0, "C16", "p1@h.com", false, "Patient", "One", false, null, "P1@H.COM", "PATIENT1", "HASH", null, false, "S16", false, "patient1" },
                    { new Guid("c5982307-ef67-4b65-b438-8f9e1e3a240b"), 0, "C20", "p5@h.com", false, "Patient", "Five", false, null, "P5@H.COM", "PATIENT5", "HASH", null, false, "S20", false, "patient5" },
                    { new Guid("cbdfa704-0f6d-431f-8ede-dd952adacfc9"), 0, "C10", "doc9@h.com", false, "Grace", "Red", false, null, "DOC9@H.COM", "DOC9", "HASH", null, false, "S10", false, "doc9" },
                    { new Guid("d9ccb374-6b17-4e66-9c11-79412a9e1e93"), 0, "C7", "doc6@h.com", false, "Dave", "Black", false, null, "DOC6@H.COM", "DOC6", "HASH", null, false, "S7", false, "doc6" },
                    { new Guid("e7d8baba-f7b1-4ed0-9bbb-139dc13e878e"), 0, "C1", "gencheva@gmail.com", false, "Admin", "User", false, null, "GENCHEVA@GMAIL.COM", "GENCHEVA", "HASH", null, false, "S1", false, "gencheva" },
                    { new Guid("f6662c6a-414b-4b5c-ae1b-7b31103dd464"), 0, "C11", "doc10@h.com", false, "Hank", "Yellow", false, null, "DOC10@H.COM", "DOC10", "HASH", null, false, "S11", false, "doc10" }
                });

            migrationBuilder.InsertData(
                table: "Diagnoses",
                columns: new[] { "ID", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("02ce1c83-0198-4d90-9dc1-d697c61f936e"), "fracture.jpg", "Fracture" },
                    { new Guid("0b0a943c-4d25-4b22-b21f-ee4f80f8e6b0"), "migraine.jpg", "Migraine" },
                    { new Guid("2dfdf306-41d8-4aca-abd6-91ed7d4adc8a"), "bronchitis.jpg", "Bronchitis" },
                    { new Guid("2edd634a-5c31-4f68-b9b5-58c2f5b80216"), "pneumonia.jpg", "Pneumonia" },
                    { new Guid("46a961d1-e24f-4029-9c13-4ee9a345610c"), "diabetes.jpg", "Diabetes" },
                    { new Guid("49a578fc-5f30-40b6-810f-3ca54b0e2a02"), "hashimoto.jpg", "Hashimoto's disease" },
                    { new Guid("732a09fb-ad41-4059-829b-8f32cbf0ce2f"), "hypertension.jpg", "Hypertension" },
                    { new Guid("885aea72-26c5-48b5-88cc-7128b7e81499"), "asthma.jpg", "Osteoporosis" },
                    { new Guid("91b25ace-9e01-4c25-b0ea-3c8bad060315"), "flu.jpg", "Influenza" },
                    { new Guid("c97e4a52-4926-4268-8261-82739340e77b"), "raynauld.jpg", "Raynauld's syndrome" },
                    { new Guid("d793f73f-51a0-4ff0-b6fa-5ffd4d47cd15"), "asthma.jpg", "Asthma" }
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
                    { new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), new TimeSpan(0, 14, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0), "Morning" },
                    { new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0), "Afternoon" },
                    { new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), "Emergency" },
                    { new Guid("aaaaaaf9-5059-478c-8df4-db3fd4342b14"), new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), "Weekend" },
                    { new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new TimeSpan(0, 6, 0, 0, 0), new TimeSpan(0, 22, 0, 0, 0), "Night" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "ID", "Image", "SpecializationName" },
                values: new object[,]
                {
                    { new Guid("1a298771-7773-4c3b-828c-fff8dcedf0e9"), "endocrinology.jpg", "Endocrinology" },
                    { new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), "neurology.jpg", "Neurology" },
                    { new Guid("4ee4ce69-da26-4f66-85cf-30623200cbf4"), "orthopedics.jpg", "Orthopedics" },
                    { new Guid("7a67c94b-50fb-4043-83f1-afdade20b451"), "cardiology.jpg", "Cardiology" },
                    { new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), "infections.jpg", "Infectious Disease" },
                    { new Guid("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"), "pulmonology.jpg", "Pulmonology" },
                    { new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), "rheumatology.jpg", "Rheumatology" },
                    { new Guid("c484edec-d525-438a-92c4-ad80a9a41878"), "endocrinology.jpg", "Endocrinology" },
                    { new Guid("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"), "respiratory.jpg", "Respiratory" },
                    { new Guid("e43cf086-24ad-4a75-b47e-549b8d8e467c"), "Traumatology.jpg", "Traumatology" },
                    { new Guid("e82cc806-165f-4554-91f8-c9d9ae4909e5"), "allergy.jpg", "Allergy & Immunology" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "ID", "Image", "IsAccepted", "ShiftId", "SpecializationId", "UserId" },
                values: new object[,]
                {
                    { new Guid("08ccdf4b-02ad-464f-9ef2-fb73ceee1826"), "doctor1.jpg", true, new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), new Guid("072eae42-46ab-4919-aae5-073aef56c00d") },
                    { new Guid("0f6d5fde-bc75-4df5-8886-090806665b82"), "doctor9.jpg", true, new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new Guid("4ee4ce69-da26-4f66-85cf-30623200cbf4"), new Guid("cbdfa704-0f6d-431f-8ede-dd952adacfc9") },
                    { new Guid("186296e2-7114-4291-aa3b-897b96c75c21"), "doctor3.jpeg", true, new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new Guid("c484edec-d525-438a-92c4-ad80a9a41878"), new Guid("3d86822f-0eba-44ce-8484-27addbfe7357") },
                    { new Guid("26189d95-7ca7-40f7-9384-8454cfb99247"), "doctor7.jpg", true, new Guid("aaaaaaf9-5059-478c-8df4-db3fd4342b14"), new Guid("e82cc806-165f-4554-91f8-c9d9ae4909e5"), new Guid("30f2b4ed-e0e3-4443-8595-4dc6e26b3338") },
                    { new Guid("2a4e1d97-8411-4cf8-9da2-af9452f16eca"), "doctor4.jpg", true, new Guid("aaaaaaf9-5059-478c-8df4-db3fd4342b14"), new Guid("9cfb1a19-193f-4bf7-bc4b-c744a89f59eb"), new Guid("7dca2bf8-df73-4dbf-a602-52e147eafe1e") },
                    { new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"), "doctor5.jpg", true, new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), new Guid("23b350b4-0dd6-43fc-b5dc-818faf2b74e6") },
                    { new Guid("6d3dacc1-3b7a-4e43-8caa-5b82a6f4a21f"), "doctor8.jpg", true, new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), new Guid("1a298771-7773-4c3b-828c-fff8dcedf0e9"), new Guid("51daaed0-67e7-4c4a-b254-2745af5365df") },
                    { new Guid("8e296807-75cf-45dd-bdfc-179495465c09"), "doctor6.jpg", true, new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new Guid("e43cf086-24ad-4a75-b47e-549b8d8e467c"), new Guid("d9ccb374-6b17-4e66-9c11-79412a9e1e93") },
                    { new Guid("dcd275c5-67c4-423b-a7b2-78ab917a2d5d"), "doctor10.jpg", true, new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), new Guid("f6662c6a-414b-4b5c-ae1b-7b31103dd464") },
                    { new Guid("e1ceefa2-e56b-4395-9049-c689bea9417f"), "doctor2.jpeg", true, new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), new Guid("7a67c94b-50fb-4043-83f1-afdade20b451"), new Guid("7c425879-d37a-48a6-91d9-2345120a3f6a") }
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
                columns: new[] { "ID", "Image", "IsAccepted", "ShiftId", "SpecializationId", "UserId" },
                values: new object[,]
                {
                    { new Guid("698d0579-913c-42af-8a45-924cd9f740bb"), "nurse1.png", true, new Guid("0288c0db-23d0-4cef-b74c-ef997285b18c"), new Guid("8a42cdba-ff58-4129-aea8-ae4c3b32f353"), new Guid("354fa92a-6b54-4d12-b90c-9926dc906462") },
                    { new Guid("9fb6048e-03ae-407f-a83e-51b6c5399b41"), "nurse4.png", true, new Guid("2cd29802-44c5-4559-8cc3-225984ae748f"), new Guid("d9daaa5d-2c41-4fa4-b709-709fcfcd5cc0"), new Guid("355ad73e-6b7d-4ade-846d-7cab0da06629") },
                    { new Guid("e5f97752-f18b-4b36-8c47-4d238cb0e01f"), "nurse2.png", true, new Guid("3ba89da5-3a0d-44ff-97f9-f049bc9bdbe9"), new Guid("a5519f22-cefb-4771-a5e9-de7b40817df8"), new Guid("02e72b22-0abd-4ce4-80d1-30b8c13f952b") },
                    { new Guid("e6a3850b-7a1e-465c-83fd-57b1134c68d2"), "nurse3.png", true, new Guid("baaafe92-5c0f-420b-87f0-fb1da4868b41"), new Guid("1d3e1ea9-5265-4ff3-9875-d7ac37a2d8b2"), new Guid("741d970d-f405-4bd1-94b2-eec2c3fb33e2") }
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
                    { new Guid("5dad99cf-b93a-4376-b772-86fd44246d7e"), new DateOnly(2026, 7, 19), new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"), new Guid("eac603b7-4022-4fb1-8c04-28db2f0f2162"), new TimeOnly(8, 30, 0) },
                    { new Guid("7f55e59e-5ab2-4e36-92bd-d66f3c453ab9"), new DateOnly(2026, 6, 1), new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"), new Guid("06999216-e4e9-4455-856d-5246259b2684"), new TimeOnly(16, 30, 0) },
                    { new Guid("a828ee04-c7dc-417c-9d8b-85114a92ce47"), new DateOnly(2026, 12, 6), new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"), new Guid("ba24d819-24f4-4e94-bb71-77765076d46c"), new TimeOnly(10, 0, 0) },
                    { new Guid("aff1363e-af23-4c5b-a88f-ba81e536333b"), new DateOnly(2026, 5, 23), new Guid("3480fb00-bfdc-4139-91a3-a975153ab6b3"), new Guid("718919c3-760f-4a6a-8abf-b1cd1b459d11"), new TimeOnly(15, 30, 0) },
                    { new Guid("d44665d6-6b30-4542-b932-cbb7ee71efbe"), new DateOnly(2026, 4, 12), new Guid("08ccdf4b-02ad-464f-9ef2-fb73ceee1826"), new Guid("9783d8b3-014f-477a-b951-6ff87057b44f"), new TimeOnly(16, 30, 0) }
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
                name: "IX_Patients_RoomId",
                table: "Patients",
                column: "RoomId");

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
