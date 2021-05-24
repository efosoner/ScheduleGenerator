using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleApp.Migrations
{
    public partial class StudentDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecId",
                table: "Students",
                newName: "Specialization");

            migrationBuilder.AddColumn<string>(
                name: "Index",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Students",
                newName: "SpecId");
        }
    }
}
