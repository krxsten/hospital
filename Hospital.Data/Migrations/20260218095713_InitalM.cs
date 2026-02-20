using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitalM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfAssignedPatients",
                table: "DoctorsAndNurses");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Specializations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Diagnoses");

            migrationBuilder.AddColumn<int>(
                name: "CountOfAssignedPatients",
                table: "DoctorsAndNurses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
