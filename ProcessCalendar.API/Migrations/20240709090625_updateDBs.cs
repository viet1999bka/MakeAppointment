using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessCalendar.API.Migrations
{
    /// <inheritdoc />
    public partial class updateDBs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "UserAppointInfors",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "UserAppointInfors");
        }
    }
}
