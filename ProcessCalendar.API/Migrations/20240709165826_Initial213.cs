using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessCalendar.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial213 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OptionDate",
                table: "UserAppointInfors",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SelectedDate",
                table: "UserAppointInfors",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionDate",
                table: "UserAppointInfors");

            migrationBuilder.DropColumn(
                name: "SelectedDate",
                table: "UserAppointInfors");
        }
    }
}
